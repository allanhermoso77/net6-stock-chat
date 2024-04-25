
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Stock.Chat.Application.SignalR;
using Stock.Chat.Domain.CommandHandlers;
using Stock.Chat.CrossCutting.Handler;
using Stock.Chat.CrossCutting.Notifications;
using Stock.Chat.CrossCutting.Models;

namespace Stock.Chat.Api.Controllers
{
    [ApiController]
    [Route("api/identity")]
    [AllowAnonymous]
    public class IdentityController : BaseController
    {
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(
            INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator,
            ILogger<IdentityController> logger) 
            : base(notifications, mediator) 
        {
            _logger = logger;
        }

        /// <summary>
        /// Identity control for login
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiOkReturn))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(AuthenticateUserCommand command)
        {
            var token = await _mediator.SendCommandResult(command);

            if (token != null)
            {
                _logger.LogInformation($"{command.UserName} logged in");
                return Response(token);
            }

            return Unauthorized();
        }
    }
}