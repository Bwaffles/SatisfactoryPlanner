using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application.Emails;
using SatisfactoryPlanner.Modules.UserAccess.Application.Configuration.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.UserRegistrations.SendUserRegistrationConfirmationEmail
{
    internal class
        SendUserRegistrationConfirmationEmailCommandHandler : ICommandHandler<SendUserRegistrationConfirmationEmailCommand>
    {
        private readonly IEmailSender _emailSender;

        public SendUserRegistrationConfirmationEmailCommandHandler(
            IEmailSender emailSender)
            => _emailSender = emailSender;

        public Task<Unit> Handle(SendUserRegistrationConfirmationEmailCommand command,
            CancellationToken cancellationToken)
        {
            var link = $"<a href=\"{command.ConfirmLink}{command.UserRegistrationId.Value}\">link</a>";

            var content = $"Welcome to Satisfactory Planner! Please confirm your registration using this {link}.";

            var emailMessage = new EmailMessage(
                command.Email,
                "Satisfactory Planner - Please confirm your registration",
                content);

            _emailSender.SendEmail(emailMessage);

            return Unit.Task;
        }
    }
}