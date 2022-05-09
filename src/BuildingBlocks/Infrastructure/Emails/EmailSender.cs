using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.BuildingBlocks.Application.Emails;
using Serilog;
using System;

namespace SatisfactoryPlanner.BuildingBlocks.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        private readonly EmailsConfiguration _configuration;

        private readonly IDbConnectionFactory _dbConnectionFactory;

        public EmailSender(
            ILogger logger,
            EmailsConfiguration configuration,
            IDbConnectionFactory dbConnectionFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _dbConnectionFactory = dbConnectionFactory;
        }

        public void SendEmail(EmailMessage message)
        {
            var sqlConnection = _dbConnectionFactory.GetOpenConnection();

            // TODO this won't work in my db
            sqlConnection.ExecuteScalar(
                "INSERT INTO [app].[Emails] ([Id], [From], [To], [Subject], [Content], [Date]) " +
                "VALUES (@Id, @From, @To, @Subject, @Content, @Date) ",
                new
                {
                    Id = Guid.NewGuid(),
                    From = _configuration.FromEmail,
                    message.To,
                    message.Subject,
                    message.Content,
                    Date = DateTime.UtcNow
                });

            _logger.Information(
                "Email sent. From: {From}, To: {To}, Subject: {Subject}, Content: {Content}.",
                _configuration.FromEmail,
                message.To,
                message.Subject,
                message.Content);
        }
    }
}