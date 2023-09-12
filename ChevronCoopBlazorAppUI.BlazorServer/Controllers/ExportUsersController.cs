using AP.ChevronCoop.Entities.Security.MemberProfiles;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using Microsoft.AspNetCore.Mvc;
using Refit;
using Microsoft.Extensions.Configuration;

namespace ChevronCoop.Web.AppUI.BlazorServer.Controllers
{
	public class ExportUsersController : ExportController
	{
		private readonly IEntityDataService DataService;
	
		private readonly IConfiguration _configuration;
		
		

		public ExportUsersController(IConfiguration configuration)
		{
			
			_configuration = configuration;
			var apiAddress=_configuration[ConfigKeys.API_HOST];
			DataService = RestService.For<IEntityDataService>(apiAddress);
		}

		[HttpGet("/export/users/csv")]
		public async Task<FileStreamResult> ExportUsersToCSV()
		{
			var fileName = "Users";

			var rsp = await DataService.GetMemberProfileViewResult<IEnumerable<MemberProfileViewModelResult>>();
			if (rsp.IsSuccessStatusCode)
			{
				IEnumerable<MemberProfileViewModelResult> returnedMembers = rsp.Content;

				if (returnedMembers.Any())
				{
					IQueryable<MemberProfileViewModelResult> usersList = returnedMembers.AsQueryable();
					return ToCSV(ApplyQuery(usersList, Request.Query), fileName);
				}
				else
				{
					var newList = new List<MemberProfileViewModelResult>();
					var usersList1 = newList.AsQueryable();
					return ToCSV(ApplyQuery(usersList1, Request.Query), fileName);
				}
				
			}
			else
			{
				var newList=new List<MemberProfileViewModelResult>();	
				var usersList=newList.AsQueryable();
				return ToCSV(ApplyQuery(usersList, Request.Query), fileName);
			}
				
		}


        [HttpGet("/export/users/csv(status='{status}')")]
        public async Task<FileStreamResult> ExportUsersWithStatusToCSV(string status)
        {
			var fileName = "";

			if(status=="ACTIVE")
			{
				fileName = "ActiveEnrollments";
                var rsp = await DataService.GetMemberProfileViewResult<IEnumerable<MemberProfileViewModelResult>>();
                if (rsp.IsSuccessStatusCode)
                {
                    IEnumerable<MemberProfileViewModelResult> returnedMembers = rsp.Content;

                    if (returnedMembers.Any())
                    {
                        IQueryable<MemberProfileViewModelResult> usersList = returnedMembers.AsQueryable();
                        usersList = usersList.Where(p => p.status == status);
                        return ToCSV(ApplyQuery(usersList, Request.Query), fileName);
                    }
                    else
                    {
                        var newList = new List<MemberProfileViewModelResult>();
                        var usersList1 = newList.AsQueryable();
                        return ToCSV(ApplyQuery(usersList1, Request.Query), fileName);
                    }

                }
                else
                {
                    var newList = new List<MemberProfileViewModelResult>();
                    var usersList = newList.AsQueryable();
                    return ToCSV(ApplyQuery(usersList, Request.Query), fileName);
                }
            }
			else
			{
				fileName = "InactiveEnrollments";
                var rsp = await DataService.GetMemberProfileViewResult<IEnumerable<MemberProfileViewModelResult>>();
                if (rsp.IsSuccessStatusCode)
                {
                    IEnumerable<MemberProfileViewModelResult> returnedMembers = rsp.Content;

                    if (returnedMembers.Any())
                    {
                        IQueryable<MemberProfileViewModelResult> usersList = returnedMembers.AsQueryable();
                        usersList = usersList.Where(p => p.status != "ACTIVE");
                        return ToCSV(ApplyQuery(usersList, Request.Query), fileName);
                    }
                    else
                    {
                        var newList = new List<MemberProfileViewModelResult>();
                        var usersList1 = newList.AsQueryable();
                        return ToCSV(ApplyQuery(usersList1, Request.Query), fileName);
                    }

                }
                else
                {
                    var newList = new List<MemberProfileViewModelResult>();
                    var usersList = newList.AsQueryable();
                    return ToCSV(ApplyQuery(usersList, Request.Query), fileName);
                }
            }




           

        }


        [HttpGet("/export/users/notcompletedkyc/csv")]
        public async Task<FileStreamResult> ExportNotCompletedKYCToCSV()
        {
            var fileName = "Users";
            var rsp = await DataService.GetMemberProfileViewResult<IEnumerable<MemberProfileViewModelResult>>();
            if (rsp.IsSuccessStatusCode)
            {
                IEnumerable<MemberProfileViewModelResult> returnedMembers = rsp.Content;

                if (returnedMembers.Any())
                {
                    IQueryable<MemberProfileViewModelResult> usersList = returnedMembers.Where(p=>p.isKycCompleted==false).AsQueryable();
                    return ToCSV(ApplyQuery(usersList, Request.Query), fileName);
                }
                else
                {
                    var newList = new List<MemberProfileViewModelResult>();
                    var usersList1 = newList.AsQueryable();
                    return ToCSV(ApplyQuery(usersList1, Request.Query), fileName);
                }

            }
            else
            {
                var newList = new List<MemberProfileViewModelResult>();
                var usersList = newList.AsQueryable();
                return ToCSV(ApplyQuery(usersList, Request.Query), fileName);
            }

        }
    }
}
