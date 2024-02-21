using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.BuildingBlocks.Application;

namespace SatisfactoryPlanner.API.Configuration.Validation
{
    public class InvalidCommandProblemDetails : ProblemDetails
    {
        public List<string> Errors { get; }

        public InvalidCommandProblemDetails(InvalidCommandException exception)
        {
            Title = "Command validation error";
            Status = StatusCodes.Status400BadRequest;
            Type = "https://somedomain/validation-error";
            Errors = exception.Errors;
        }
    }
}