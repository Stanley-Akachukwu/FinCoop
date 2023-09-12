﻿using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User
{
    public partial class UserCreateForm
    {
        public UserCreateForm()
        {
        }

        private Query Query_Combo; // = new Query();
        string notificationText;
        bool showPopup = false;
        bool reloadGrid = false;

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public CreateApplicationUserCommand Model { get; set; }


        [Parameter]
        public EventCallback<CreateApplicationUserCommand> ModelChanged { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Inject]
        ILogger<UserCreateForm> Logger { get; set; }

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


        string DROPDOWN_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(ApplicationRoleMasterView)}";


        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        private string bearToken { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetCurrentUser();
            await base.OnInitializedAsync();
            Model = new CreateApplicationUserCommand
            {
                Gender = Gender.UNKNOWN.ToString(),
                Status = MemberProfileStatus.PENDING_APPROVAL.ToString(),
            };
            Query_Combo = new Query();
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                bearToken = CurrentUser.FindFirstValue(ClaimTypes.Hash);
                if (string.IsNullOrEmpty(bearToken))
                {
                    _navigationManager.NavigateTo("/Identity/Account/Logout", forceLoad: true);
                }

                HeaderData.Add("Bearer", bearToken);
            }
        }

        private void ValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedValue = args.Value;
            if (selectedValue.Count() > 0)
            {
                Model.RoleIds = new List<string>(selectedValue);
            }
        }

        public async Task OnSaveClose()
        {
            try
            {
                Model.Status = MemberProfileStatus.PENDING_APPROVAL.ToString();
                var checkIsSuccessfull = await ValidateEntry();
                if (checkIsSuccessfull)
                {
                    var rsp = await DataService
                        .Create<CreateApplicationUserCommand, CommandResult<ApplicationUserViewModel>>(
                            nameof(ApplicationUser), Model);

                    Logger.LogInformation($"error->{JsonSerializer.Serialize(rsp?.Error?.Content)}");

                    if (!rsp.IsSuccessStatusCode)
                    {
                        var serverErrorMessages = "Server Error.";
                        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);
                        var msg = rsp.ReasonPhrase;

                        if (rspContent != null && rspContent.ValidationErrors != null &&
                            rspContent.ValidationErrors.Any())
                        {
                            serverErrorMessages += " " + rspContent.ValidationErrors[0].Error;
                        }

                        if (!string.IsNullOrEmpty(rspContent.Message))
                        {
                            serverErrorMessages += " " + rspContent.Message;
                        }

                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = msg,
                            NotificationType = NotificationType.Error,
                            Description = rspContent.Message,
                        });
                    }
                    else
                    {
                        var payload = JsonSerializer.Serialize(Model);
                        await _auditLogService.LogAudit("Admin User Creation.",
                            $"Created admin user with ID- {rsp.Content.Response.Id}.", "Security", payload,
                            CurrentUser);

                        notificationText = $"Login credentials has been sent to the staff email address";
                        showPopup = true;
                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = "Success",
                            Description = notificationText,
                            NotificationType = NotificationType.Success
                        });
                        reloadGrid = true;
                        OnCancel();
                    }
                }
            }
            catch (Exception ex)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = ex.Message,
                    Description = "Request could not be processed.",
                    NotificationType = NotificationType.Error
                });
            }
        }

        public async Task OnCancel()
        {
            if (reloadGrid)
            {
                NavigationManager.NavigateTo("/security/users", true);
                reloadGrid = false;
            }

            ShowModal = false;
            await ShowModalChanged.InvokeAsync(ShowModal);

            Model = new CreateApplicationUserCommand();
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

        public async Task<bool> ValidateEntry()
        {
            if (string.IsNullOrEmpty(Model.Status))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Staff status is required",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            //else if (string.IsNullOrEmpty(Model.MiddleName))
            //{
            //    await notificationService.Open(new NotificationConfig()
            //    {
            //        Message = "Error",
            //        Description = "Please, provide staff middle name",
            //        NotificationType = NotificationType.Error
            //    });
            //    return false;
            //}
            else if (string.IsNullOrEmpty(Model.LastName))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, provide staff last name",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(Model.FirstName))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, provide staff first name",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (Model.RoleIds == null || Model.RoleIds.Count == 0)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, Provide staff Role",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(Model.PhoneNumber))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, Provide staff Phone No",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(Model.MembershipId))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, Provide staff Membership No.",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(Model.Email))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, Provide staff Email Address",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(Model.Gender))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, Provide staff Gender",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(Model.DepartmentId))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, Provide staff Department",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(Model.Address))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, Provide staff Address",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (!string.IsNullOrEmpty(Model.MembershipId) && Model.MembershipId.Length > 10)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Invalid Membership No.",
                    Description = " Membership No. must not be  more than 10 characters",
                    NotificationType = NotificationType.Error
                });
                return false;
            }

            return true;
        }
    }
}