using System;
using AP.ChevronCoop.Commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace ChevronCoop.API.Config
{
    public class ValidationFilterAttribute : IActionFilter
    {
        private readonly CoreAppSettings _options;
        public readonly record struct APIResponse(bool Error, string Message);
        public ValidationFilterAttribute(IOptions<CoreAppSettings> options)
        {
            _options = options.Value;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string apikey = context.HttpContext.Request.Headers["ApiKey"];

            if (string.IsNullOrEmpty(apikey))
            {
                var apiResponse = new NetPayAPIResponse()
                {
                    Error = true,
                    Message = "API Key cannot be empty in the header"
                };

                context.Result = new BadRequestObjectResult(apiResponse);
            }

            if (apikey != _options.NetPayUATAPIKey)
            {
                var apiResponse = new NetPayAPIResponse()
                {
                    Error = true,
                    Message = "Invalid API Key in the header"
                };
                context.Result = new BadRequestObjectResult(apiResponse);
            }


        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }

    public class NetPayAPIResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}

