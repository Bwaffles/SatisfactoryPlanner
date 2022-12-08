using Microsoft.AspNetCore.Http;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using SatisfactoryPlanner.Modules.UserAccess.Application.Users.GetUsers;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;

namespace SatisfactoryPlanner.API.Configuration.ExecutionContext
{
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserAccessModule _userAccessModule;

        private HttpContext? HttpContext => _httpContextAccessor.HttpContext;

        public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor, IUserAccessModule userAccessModule)
        {
            _httpContextAccessor = httpContextAccessor;
            _userAccessModule = userAccessModule;
        }

        public Guid UserId
        {
            get
            {
                if (!IsAvailable)
                    throw new ApplicationException("Http context is not available.");

                // Get the Auth0 User Id from the Access Token of the request.
                var auth0UserId = HttpContext
                    .User
                    .Claims
                    .SingleOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?
                    .Value;

                if (auth0UserId == null)
                    throw new ApplicationException("Can't retrieve auth0UserId from access token.");

                // Get the User Id of the user in our system from the Auth0 User Id. 
                // Don't want the entire application to be dependent on the 3rd party authentication platform I'm using.
                var getCurrentUserTask = _userAccessModule.ExecuteQueryAsync(new GetUsersQuery(auth0UserId));
                var currentUser = getCurrentUserTask.Result.SingleOrDefault();
                if (currentUser == null) // This should only happen when user first signs up 
                    throw new ApplicationException($"No user exists for auth0UserId {auth0UserId}.");

                return currentUser.Id;
            }
        }

        public Guid CorrelationId
        {
            get
            {
                if (!IsAvailable)
                    throw new ApplicationException("Http context is not available.");

                if (!HttpContext.Request.Headers.ContainsKey(CorrelationMiddleware.CorrelationHeaderKey))
                    throw new ApplicationException("Correlation id is not available.");

                var correlationId = HttpContext.Request.Headers[CorrelationMiddleware.CorrelationHeaderKey].ToString();
                return Guid.Parse(correlationId);
            }
        }

        [MemberNotNullWhen(true, nameof(HttpContext))]
        public bool IsAvailable => HttpContext != null;
    }
}