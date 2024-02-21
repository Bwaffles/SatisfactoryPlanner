namespace SatisfactoryPlanner.API.Configuration.ExecutionContext
{
    internal class CorrelationMiddleware(RequestDelegate next)
    {
        internal const string CorrelationHeaderKey = "CorrelationId";

        public async Task Invoke(HttpContext context)
        {
            var correlationId = Guid.NewGuid();

            context.Request?.Headers.Append(CorrelationHeaderKey, correlationId.ToString());

            await next.Invoke(context);
        }
    }
}