﻿using Stock.Chat.Application.Services;
using Stock.Chat.Domain.CommandHandlers;
using Stock.Chat.Domain.Entities;
using Stock.Chat.Domain.Interfaces;
using Stock.Chat.Domain.Interfaces.Services;
using Stock.Chat.CrossCutting.Handler;
using Stock.Chat.CrossCutting;
using Stock.Chat.CrossCutting.Notifications;
using Stock.Chat.Infrastructure.Data;
using Stock.Chat.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using FluentAssertions;
using Stock.Chat.Tests.Fixture;

namespace Stock.Chat.Tests.Domain.Commands.Handlers
{
    public class IdentityHandlerTest : StockChatDbContextFixure
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mock<IMediatorHandler> _mockMediator;
        private readonly IUserRepository _userRepository;
        private readonly IIdentityService _identityService;
        private readonly DomainNotificationHandler _domainNotificationHandler;
        private readonly IdentityHandler handler;

        public IdentityHandlerTest()
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

            _userRepository.Add(new User
            {
                UserName = "test",
                Name = "Test",
                Password = Cryptography.PasswordEncrypt("123456")
            });
            _unitOfWork.Commit();

            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x[It.Is<string>(s => s.Equals("Jwt:Issuer"))]).Returns("Test");
            mockConfig.Setup(x => x[It.Is<string>(s => s.Equals("Jwt:Duration"))]).Returns("120");
            mockConfig.Setup(x => x[It.Is<string>(s => s.Equals("Jwt:Key"))]).Returns("IZpipYfLNJro403pIZpipYfLNJro403p");

            _identityService = new IdentityService(_userRepository, mockConfig.Object);
            handler = new IdentityHandler(_unitOfWork, _mockMediator.Object, _identityService);
        }

        [Fact]
        public async Task Should_not_get_authenticated_invalid_username()
        {
            //Arrange
            var userAuth = new AuthenticateUserCommand { UserName = string.Empty, Password = "123356" };

            //Act
            var result = await handler.Handle(userAuth, CancellationToken.None);

            //Assert
            result.Token.Should().BeNullOrEmpty();
            _domainNotificationHandler.HasNotifications().Should().BeTrue();
        }

        [Fact]
        public async Task Should_not_get_authenticated_invalid_password()
        {
            //Arrange
            var userAuth = new AuthenticateUserCommand { UserName = "test", Password = string.Empty };

            //Act
            var result = await handler.Handle(userAuth, CancellationToken.None);

            //Assert
            result.Token.Should().BeNullOrEmpty();
            _domainNotificationHandler.HasNotifications().Should().BeTrue();
        }

        [Fact]
        public async Task Should_get_authenticated()
        {
            //Arrange
            var userAuth = new AuthenticateUserCommand { UserName = "test", Password = "123456" };

            //Act
            var result = await handler.Handle(userAuth, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Token.Should().NotBeNullOrEmpty();
            _domainNotificationHandler.HasNotifications().Should().BeFalse();
        }
    }
}
