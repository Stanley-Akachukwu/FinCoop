using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Services.LoanProducts.Interface;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Services.LoanProducts
{
    public class LoanHelperService : ILoanHelperService
    {

        private readonly IEntityDataService DataService;
        public LoanHelperService(IEntityDataService entityDataService)
        {
            DataService = entityDataService;
        }
        public async Task<CommonResponseDTO> CheckCustomerLoanEligibility(string ProductId, string CustomerId)
        {
            CommonResponseDTO commonResponseDTO = new CommonResponseDTO();
            if (!string.IsNullOrEmpty(CustomerId) && !string.IsNullOrEmpty(ProductId))
            {
                CustomerLoanProductEligibilityCommand command = new CustomerLoanProductEligibilityCommand()
                {
                    CustomerId = CustomerId,
                    LoanProductId = ProductId,
                };
                var rsp =
                    await DataService.PostCommand<CustomerLoanProductEligibilityCommand, CommandResult<bool>>(nameof(LoanProduct), "customerLoanProductEligibility", command);


                if (!rsp.IsSuccessStatusCode)
                {
                    if (!string.IsNullOrEmpty(rsp.Error.Content))
                    {
                        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                        var msg = rspContent?.Response;
                        if (rspContent != null && rspContent.ValidationErrors != null &&
                            rspContent.ValidationErrors.Any())
                        {
                            msg = rspContent.ValidationErrors[0].Error;
                        }

                        if (msg == null && rspContent?.Message != null)
                            msg = rspContent.Message;
                        commonResponseDTO.IsEligible = false;
                        commonResponseDTO.IsError = true;
                        commonResponseDTO.Message = msg;
                        return commonResponseDTO;

                    }
                    else
                    {
                        commonResponseDTO.IsEligible = false;
                        commonResponseDTO.IsError = true;
                        commonResponseDTO.Message = "Oops! Something Went Wrong. Please try again later. Thanks";
                        return commonResponseDTO;

                    }
                }
                else
                {

                    var rspContent = JsonSerializer.Deserialize<CommandResult<bool>>(rsp.Content.ToJson());
                    if (rspContent != null)
                    {
                        commonResponseDTO.IsEligible = rspContent.Response;
                        commonResponseDTO.Message = rspContent.Message;
                        return commonResponseDTO;
                    }
                }

            }
            return commonResponseDTO;
        }


    }
}
