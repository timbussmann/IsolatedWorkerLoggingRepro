namespace IsolatedWorkerLoggingRepro
{
    using System.Net;
    using Microsoft.Azure.Functions.Worker;
    using Microsoft.Azure.Functions.Worker.Http;
    using Microsoft.Extensions.Logging;

    public static class Function1
    {
        [Function("HttpSender")]
        public static HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("DemoLogger"); // only logs when configuring the default logger, configuring default to "None" and "DemoLogger" to "Debug" will result in no log output
            logger.LogDebug($"Loglevel {nameof(LogLevel.Debug)}"); // never logged, even when default logger set to Debug level
            logger.LogInformation($"Loglevel {nameof(LogLevel.Information)}");
            logger.LogError($"Loglevel {nameof(LogLevel.Error)}");

            var r = req.CreateResponse(HttpStatusCode.OK);
            return r;
        }
    }
}
