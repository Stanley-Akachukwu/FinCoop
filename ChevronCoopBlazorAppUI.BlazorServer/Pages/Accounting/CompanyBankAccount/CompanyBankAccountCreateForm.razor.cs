using AntDesign;
using AP.ChevronCoop.AppDomain.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Commons;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;
using System.Text.Json;


namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounting.CompanyBankAccount
{
    public partial class CompanyBankAccountCreateForm
    {
        public CompanyBankAccountCreateForm()
        {
        }


        private Query Query_Combo; // = new Query();

        string notificationText;
        bool showPopup = false;


        [Parameter]
        public CreateCompanyBankAccountCommand Model { get; set; }


        [Parameter]
        public EventCallback<CreateCompanyBankAccountCommand> ModelChanged { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Inject]
        ILogger<CompanyBankAccountCreateForm> Logger { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }


        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
            }
        }


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Query_Combo = new Query();
        }


        public async Task OnSaveClose()
        {
            var rsp = await DataService
                .Create<CreateCompanyBankAccountCommand, CommandResult<CompanyBankAccountViewModel>>(
                    nameof(CompanyBankAccount), Model);


            if (!rsp.IsSuccessStatusCode)
            {
                var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                var msg = rspContent?.Response;
                if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                {
                    msg = rspContent.ValidationErrors[0].Error;
                }


                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = msg,
                    NotificationType = NotificationType.Error
                });
            }
            else
            {
                notificationText = $"Record successfully created";
                showPopup = true;

                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Success",
                    Description = notificationText,
                    NotificationType = NotificationType.Success
                });

                OnCancel();
            }
        }

        public async Task OnCancel()
        {
            ShowModal = false;
            await ShowModalChanged.InvokeAsync(ShowModal);

            Model = new CreateCompanyBankAccountCommand();
            await ModelChanged.InvokeAsync(Model);

            showPopup = false;
            _navigationManager.NavigateTo("/financials/company/bank", forceLoad: true);
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

        public async Task OnInput(Microsoft.AspNetCore.Components.ChangeEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        public async Task OnChange(Microsoft.AspNetCore.Components.ChangeEventArgs args)
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