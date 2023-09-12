//using AP.ChevronCoop.Commons;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ChevronCoop.Web.AppUI.BlazorServer.Config
//{

//    public class ModelValidationResult : IActionResult
//    {


//        public Task ExecuteResultAsync(ActionContext context)
//        {


//            var modelStateEntries = context.ModelState.Where(e => e.Value.Errors.Count > 0); //.ToArray();
//            var errors = new List<ModelValidationError>();

//            if (modelStateEntries.Any())
//            {

//                errors.AddRange(context.ModelState.AllModelErrors());

//            }

//            var errormsg = "Request model failed validation";



//            var problemDetails = new CommandResult<string>
//            {
//                ErrorFlag = true,
//                Response = errormsg,
//                Title = errormsg,
//                Detail = errormsg,
//                Status = StatusCodes.Status400BadRequest,
//                ValidationErrors = errors,
//            };

//            context.HttpContext.Response.ContentType = "application/json";
//            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
//            context.HttpContext.Response.WriteAsync(problemDetails.ToJson());

//            return Task.CompletedTask;
//        }

//    }
//}
