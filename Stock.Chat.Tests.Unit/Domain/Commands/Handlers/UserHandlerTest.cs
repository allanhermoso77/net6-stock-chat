using AutoMapper;
using Stock.Chat.Application.AutoMapper;
using Stock.Chat.Domain.CommandHandlers;
using Stock.Chat.Domain.Commands;
using Stock.Chat.Domain.Commands.Message;
using Stock.Chat.Domain.Entities;
using Stock.Chat.Domain.Interfaces;
using Stock.Chat.CrossCutting.Handler;
using Stock.Chat.CrossCutting.Notifications;
using Stock.Chat.Infrastructure.Data;
using Stock.Chat.Infrastructure.Data.Repositories;
using Moq;
using Xunit;
using FluentAssertions;
using Stock.Chat.Tests.Fixture;

namespace Stock.Chat.Tests.Domain.Command.Handlers
{
    public class UserHandlerTest : StockChatDbContextFixure
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mock<IMediatorHandler> _mockMediator;
        private readonly IUserRepository _userRepository;
        private readonly DomainNotificationHandler _domainNotificationHandler;
        private readonly IMapper _mapper;
        private readonly UserHandler handler;

        public UserHandlerTest()
        {
            db = GetDbInstance();
            _unitOfWork = new UnitOfWork(db);
            _userRepository = new UserRepository(db);
            _mockMediator = new Mock<IMediatorHandler>();
            _domainNotificationHandler = new DomainNotificationHandler();
            _mockMediator.Setup(x => x.RaiseEvent(It.IsAny<DomainNotification>())).Callback<DomainNotification>((x) =>
            {
                _domainNotificationHandler.Handle(x, CancellationToken.None);
            });
            _mapper = AutoMapperConfig.RegisterMappings().CreateMapper();
            handler = new UserHandler(_unitOfWork, _userRepository, _mockMediator.Object, _mapper);
        }

        [Fact]
        public async Task Should_not_register_user_name_is_required()
        {
            //Arrange
            var user = new UserAddCommand
            {
                UserName = "guest",
                Password = "123456",
                SecondPassword = "123456"
            };

            //Act
            var result = await handler.Handle(user, CancellationToken.None);

            //Arrange
            result.Should().BeFalse();
            _domainNotificationHandler.HasNotifications().Should().BeTrue();
        }

        [Fact]
        public async Task Should_not_register_username_is_invalid()
        {
            //Arrange
            var user = new UserAddCommand
            {
                UserName = "",
                Password = "123456",
                SecondPassword = "123456",
                Name =  "Test Name"
            };

            //Act
            var result = await handler.Handle(user, CancellationToken.None);

            //Arrange
            result.Should().BeFalse();
            _domainNotificationHandler.GetNotifications().Any().Should().BeTrue();
        }

        [Fact]
        public async Task Should_not_register_password_are_not_equal()
        {
            //Arrange
            string expectedMessageError = "The passwords are not equal";
            var user = new UserAddCommand
            {
                UserName = "guest",
                Password = "123456",
                SecondPassword = "123465",
                Name = "UserTest"
            };

            //Act
            var result = await handler.Handle(user, CancellationToken.None);

            //Arrange
            result.Should().BeFalse();
            _domainNotificationHandler.GetNotifications().Any(x => x.Value == expectedMessageError).Should().BeTrue();
        }

        [Fact]
        public async Task Should_not_register_password_have_less_than_six()
        {
            //Arrange
            var user = new UserAddCommand
            {
                UserName = "guest",
                Password = "12345",
                SecondPassword = "12345",
                Name = "UserTest"
            };

            //Act
            var result = await handler.Handle(user, CancellationToken.None);

            //Arrange
            result.Should().BeFalse();
            _domainNotificationHandler.GetNotifications().Any().Should().BeTrue();
        }

        [Fact]
        public async Task Should_register_user_valid()
        {
            //Arrange
            var user = new UserAddCommand
            {
                UserName = "guest",
                Password = "123456",
                SecondPassword = "123456",
                Name = "UserTest"
            };

            //Act
            var result = await handler.Handle(user, CancellationToken.None);

            //Arrange
            result.Should().BeTrue();
            _domainNotificationHandler.HasNotifications().Should().BeFalse();
        }

        [Fact]
        public async Task Should_not_register_message_field_message_is_required()
        {
            //Arrange
            var message = new MessageAddCommand()
            {
                Sender = "usertest@hotmail.com"
            };

            //Act
            var result = await handler.Handle(message, CancellationToken.None);

            //Arrange
            result.Should().BeFalse();
            _domainNotificationHandler.GetNotifications().Any().Should().BeTrue();
        }

        [Fact]
        public async Task Should_not_register_message_field_sender_is_required()
        {
            //Arrange
            var message = new MessageAddCommand()
            {
                Message = "Hello world"
            };

            //Act
            var result = await handler.Handle(message, CancellationToken.None);

            //Arrange
            result.Should().BeFalse();
            _domainNotificationHandler.GetNotifications().Any().Should().BeTrue();
        }

        [Fact]
        public async Task Should_register_messsage()
        {
            //Arrange
            var message = new MessageAddCommand()
            {
                Message = "Hello world",
                Sender = "usertest@hotmail.com"
            };

            //Act
            var result = await handler.Handle(message, CancellationToken.None);

            //Arrange
            result.Should().BeTrue();
            _domainNotificationHandler.HasNotifications().Should().BeFalse();
        }
    }
}
