using Stock.Chat.Domain.Commands.User;
using Stock.Chat.CrossCutting.Models;

namespace Stock.Chat.Domain.CommandHandlers
{
    public class AuthenticateUserCommand : LoginCommand<TokenJWT>
    {
        public override bool IsValid()
        {
            ValidationResult = new AuthenticateUserValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        internal class AuthenticateUserValidator : LoginValidator<AuthenticateUserCommand>
        {
            protected override void StartRules() => base.StartRules();
        }
    }
}
