using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BookStore.Core.Service.Contract;
using System.Text;

namespace BookStore.Api.Helper
{
    public class CashedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;

        public CashedAttribute(int timeToLiveSeconds)
        {
            _timeToLiveSeconds = timeToLiveSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cachingService = context.HttpContext.RequestServices.GetRequiredService<ICashingService>();

            var cachKey = GenerateCashKeyFromRequest(context.HttpContext.Request);

            var getResponseCaching = await cachingService.GetCashResponseAsync(cachKey);
            if (!string.IsNullOrEmpty(getResponseCaching))
            {
                var result = new ContentResult()
                {
                    Content = getResponseCaching,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = result;
                return;
            }

            var nextExecution = await next.Invoke();
            if (nextExecution.Result is OkObjectResult okObjectResult && okObjectResult.Value != null)
            {
                await cachingService.CashResponseAsync(cachKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
            }
        }
        private string GenerateCashKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append(request.Path);

            foreach (var (key, value) in request.Query.OrderBy(key => key.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}
