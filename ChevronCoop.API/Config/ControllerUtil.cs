using AP.ChevronCoop.Commons;
using Microsoft.AspNetCore.Mvc;

namespace ChevronCoop.API.Config
{
    public class ControllerUtil
    {


        public static async Task<IActionResult> MapResponseByStatusCode<T>(T response, int? statusCode)
        {
            var result = new ObjectResult(new CommandResult<T>())
            {
                StatusCode = StatusCodes.Status200OK
            };

            switch (statusCode)
            {
                case StatusCodes.Status200OK:
                    {
                        result = new ObjectResult(response)
                        {
                            StatusCode = StatusCodes.Status200OK
                        };
                        break;
                    }
                case StatusCodes.Status201Created:
                    {
                        result = new ObjectResult(response)
                        {
                            StatusCode = StatusCodes.Status201Created
                        };
                        break;
                    }
                case StatusCodes.Status400BadRequest:
                    {
                        result = new ObjectResult(response)
                        {
                            StatusCode = StatusCodes.Status400BadRequest
                        };
                        break;
                    }
                case StatusCodes.Status500InternalServerError:
                    {
                        result = new ObjectResult(response)
                        {
                            StatusCode = StatusCodes.Status500InternalServerError
                        };
                        break;
                    }
                default: break;
            }

            return await Task.FromResult(result);

        }
    }

}
