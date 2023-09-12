using AntDesign;
using AP.ChevronCoop.AppDomain.MasterData.Country;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.EnrollmentPayments;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.MasterData.Locations;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Org.BouncyCastle.Asn1.Ocsp;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Auth
{
    public partial class MemberCreateForm
    {
        public MemberCreateForm()
        {
        }

		[Inject]
		IMasterViews _MasterViews { get; set; }
		[Parameter]
        public string MemberType { get; set; }

        public bool IsRetireeMemberType { get; set; } = false;
        private Query Query_Combo;
        public List<Country> Countries { get; set; }
        string notificationText;
        string focusedNotificationText;
        bool showPopup = false;

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Parameter]
        public RegisterMemberCommand Model { get; set; }

        [Parameter]
        public EventCallback<RegisterMemberCommand> ModelChanged { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Inject]
        ILogger<MemberCreateForm> Logger { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        ModalService modalService { get; set; }

        [Inject]
        TempObjectService tempService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }


        [Inject]
        IEntityDataService DataService { get; set; }

        private EditContext? editContext;

        [Inject] IConfiguration configuration { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }
        string bearToken { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }

        public void ChangeLocation(ChangeEventArgs<string, LocationMasterView> args)
        {
            var selected = args.ItemData;
        }
  

        public List<LocationMasterView> locations = new List<LocationMasterView>();
    protected override async Task OnInitializedAsync()
        {

            Model = new RegisterMemberCommand() { };

            editContext = new EditContext(Model);
            if (string.Compare("regular", MemberType) == 0)
            {
                IsRetireeMemberType = false;
                Model.Role = AP.ChevronCoop.Entities.Security.MemberType.REGULAR;
            }

           else if (string.Compare("retiree", MemberType) == 0)
            {
                IsRetireeMemberType = true;
                Model.Role = AP.ChevronCoop.Entities.Security.MemberType.RETIREE;
            }

          else if (string.Compare("expatriate", MemberType) == 0)
            {
                IsRetireeMemberType = false;
                Model.Role = AP.ChevronCoop.Entities.Security.MemberType.EXPATRIATE;
            }

            await base.OnInitializedAsync();

            Query_Combo = new Query();
            await GetLocationsAsync();

        }

        public async Task GetLocationsAsync()
        {
           
			 var rsplocation = await _MasterViews.GetCustomMasterView<LocationMasterView>(nameof(LocationMasterView), "locationType", "COUNTRY", "Id, Name, Code");
            locations = rsplocation.OrderBy(c => c.Name).ToList();
			 
		}

      
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
            }
        }

        protected async Task HandleNextClick()
        {
            try
            {
                #region      Validations
                if (string.IsNullOrEmpty(Model.Password))
                {
                    focusedNotificationText = "Please enter a valid password. Password must contain a letter.";
                    ShowFormValid(false);
                    await InvokeAsync(StateHasChanged);
                    return;
                }

                if (!IsContainedLetter(Model.Password))
                {
                    focusedNotificationText = "Please enter a valid password. Password must contain a letter.";
                    ShowFormValid(false);
                    await InvokeAsync(StateHasChanged);
                    return;
                }

                if (!IsContainedDigit(Model.Password))
                {
                    focusedNotificationText = "Please enter a valid password. Password must contain a digit.";
                    ShowFormValid(false);
                    await InvokeAsync(StateHasChanged);
                    return;
                }

                if (!IsContainedSymbol(Model.Password))
                {
                    focusedNotificationText = "Please enter a valid password. Password must contain a symbol.";
                    ShowFormValid(false);
                    await InvokeAsync(StateHasChanged);
                    return;
                }

                if (!HasUpperCase(Model.Password))
                {
                    focusedNotificationText = "Password must contain an upper case letter.";
                    ShowFormValid(false);
                    await InvokeAsync(StateHasChanged);
                    return;
                }

                if (!IsValidEmail(Model.Email))
                {
                    focusedNotificationText = "Password enter a valid email address.";
                    ShowFormValid(false);
                    await InvokeAsync(StateHasChanged);
                    return;
                }

                if (String.Compare(Model.Password, Model.ConfirmPassword) != 0)
                {
                    focusedNotificationText = "Password did not match.";
                    ShowFormValid(false);
                    await InvokeAsync(StateHasChanged);
                    return;
                }

                if (Model.MembershipId.Length > 10)
                {
                    focusedNotificationText = "Please enter a valid Membership No.";
                    ShowFormValid(false);
                    await InvokeAsync(StateHasChanged);
                    return;
                }

                if (!@Model.TermsAndCondition)
                {
                    focusedNotificationText = "Please accept terms and conditions";
                    ShowFormValid(false);
                    await InvokeAsync(StateHasChanged);
                    return;
                }
                #endregion

                Logger.LogInformation($"request content->{JsonSerializer.Serialize(Model)}");


                var rsp = await DataService
                    .CreateEnrolment<RegisterMemberCommand, CommandResult<RegisterMemberViewModel>>(Model);

                Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(rsp?.Content)}");



                if (!rsp.IsSuccessStatusCode)
                {
                    var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                    if (IsIncompleteEnrollment(rspContent.ValidationErrors))
                    {
                        var hasPaid = await HasMadeEnrollmentPayment(Model);
                        if (!hasPaid)
                        {
                            var update = new UpdateMemberRegistrationCommand 
                            { 
                                TermsAndCondition=Model.TermsAndCondition,
                                Email=Model.Email,
                                FirstName=Model.FirstName,
                                LastName=Model.LastName,
                                IsKycStarted=Model.IsKycStarted,
                                Location=Model.Location,
                                MembershipId=Model.MembershipId,
                                Password=Model.Password,
                                ConfirmPassword = Model.ConfirmPassword,
                                Role = Model.Role,
                            };
                            var payload = JsonSerializer.Serialize(Model);
                            Logger.LogInformation(payload);
                            var updateRsp = await DataService.UpdateEnrolment<UpdateMemberRegistrationCommand, CommandResult<RegisterMemberViewModel>>(update);
                            if (updateRsp.IsSuccessStatusCode)
                            {
                                tempService.SetTempObject(updateRsp.Content.Response);
                            }
                            await _auditLogService.LogAuditByEmail("Self Enrollment Update.", $"{Model.Email} Updated enrollment.",
                                "Security", payload, Model.Email);
                            UriHelper.NavigateTo("/verify-email", forceLoad: true);
                        }
                    }
                    var msg = rspContent?.Message;

                    if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                    {
                        msg = rspContent.ValidationErrors[0].Error;
                    }
                     focusedNotificationText = msg;
                    ShowFormValid(false);
                    await InvokeAsync(StateHasChanged);
                }
                else
                {
                    //get response object
                    var rspContent =
                        JsonSerializer.Deserialize<CommandResult<RegisterMemberViewModel>>(rsp.Content.ToJson());
                    var responseData = rspContent?.Response;

                    tempService.SetTempObject(responseData);

                    var payload = JsonSerializer.Serialize(Model);
                    await _auditLogService.LogAuditByEmail("Self Enrollment.", $"{Model.Email} initiated enrollment.",
                        "Security", payload, Model.Email);
                    UriHelper.NavigateTo("/verify-email", forceLoad: true);
                }
            }
            catch (Exception exp)
            {
                focusedNotificationText = "Error occurred while processing your request. Contact admin."+ exp?.Message + " - "+ exp.InnerException?.Message;
                ShowFormValid(false);
                await InvokeAsync(StateHasChanged);
            }
        }

        private bool IsIncompleteEnrollment(List<ModelValidationError> validationErrors)
        {
            foreach (var v in validationErrors)
            {
                if (String.Compare(v.FieldName.ToLower(), "Email".ToLower()) == 0 && v.Error.Contains("Member with the provided email exists.") || String.Compare(v.FieldName.ToLower(), "MembershipId".ToLower()) == 0 && v.Error.Contains("Member with the provided membership ID exists.")) return true;
            }
                return false;
        }
        public async Task<bool> HasMadeEnrollmentPayment(RegisterMemberCommand model)
        {
            var checkPaymentInfoModel = new CheckEnrollmentPaymentInfoCommand { Email = model.Email, MembershipId = model.MembershipId };
            var rsp = await DataService.CheckEnrolmentPaymentInfo<CheckEnrollmentPaymentInfoCommand, CommandResult<bool>>(checkPaymentInfoModel);
            var rspContent = JsonSerializer.Deserialize<CommandResult<bool>>(rsp.Content.ToJson());
            if (rspContent == null)
                return false;

            return rspContent.Response;
        }
        static bool IsLetter(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        static bool IsSymbol(char c)
        {
            return c > 32 && c < 127 && !IsDigit(c) && !IsLetter(c);
        }

        static bool HasUpperCase(string password)
        {
            var hasUpperCase = password.Any(char.IsUpper);
            return hasUpperCase;
        }

        static bool IsContainedLetter(string password)
        {
            return
                password.Any(l => IsLetter(l));
        }

        static bool IsContainedDigit(string password)
        {
            return
                password.Any(d => IsDigit(d));
        }

        static bool IsContainedSymbol(string password)
        {
            return
                password.Any(s => IsSymbol(s));
        }

        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private bool isFormValid { get; set; } = true;

        private void ShowFormValid(bool valid)
        {
            isFormValid = valid;
        }

        public List<string> Errors { get; set; }


        public async Task OnCancel()
        {
            ShowModal = false;
            await ShowModalChanged.InvokeAsync(ShowModal);

            Model = new RegisterMemberCommand();
            await ModelChanged.InvokeAsync(Model);

            showPopup = false;
        }


        private void OnFilterCombo(FilteringEventArgs args)
        {
            WhereFilter filter1 = new WhereFilter
            {
                Field = "Description",
                Operator = "contains",
                value = args.Text
            };

            WhereFilter filter2 = new WhereFilter
            {
                Field = "Name",
                Operator = "contains",
                value = args.Text
            };
        }


        private void OnFileUploaded(UploadChangeEventArgs args)
        {
        }

        public async Task OnInput(ChangeEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        public async Task OnChange(ChangeEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        private async Task onFocus(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }
    }
}