using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Tailwindcss;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ChevronCoop.Web.AppUI.BlazorServer.Shared
{
	public partial class AdminSidebar
	{
		private bool canEditProfile;

		private string userRole { get; set; } = "None";
		private bool disableMenu { get; set; } = false;
		private bool isCustomer { get; set; } = false;
		private bool isAdmin { get; set; } = false;
		[Inject]
		AuthenticationStateProvider AuthenticationStateProvider { get; set; }
		[Inject]

		NavigationManager NavigationManager { get; set; }
		[Inject]
		IEntityDataService DataService { get; set; }

		List<SidemenuItem> SidebarMenu { get; set; } = new List<SidemenuItem>
		{
		  new SidemenuItem { Id = "dashboard", CssClass = @Tailwind.SideMenuDashboardActiveLink, DefaultCssClass = @Tailwind.SideMenuDashboardActiveLink, ActiveCssClass = @Tailwind.ActiveSidebarMenu },
		new SidemenuItem { Id = "globalcodes", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink, ActiveCssClass = @Tailwind.ActiveSideBarSubmenu },
		new SidemenuItem { Id = "departments", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "currencies", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "banks", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "companybank", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "approvalssetup", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "chartofaccounts", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "transactionsjournal", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink, ActiveCssClass = @Tailwind.ActiveSideBarSubmenu },
		new SidemenuItem { Id = "productssetup", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "globalconfiguration", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "chargessetup", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "loanaccounts", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "loanapplications", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "depositaccounts", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "depositapplications", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink, ActiveCssClass = @Tailwind.ActiveSideBarSubmenu },
		new SidemenuItem { Id = "payroll", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "approvals", CssClass = Tailwind.SideMenuDashboardActiveLink, DefaultCssClass = Tailwind.SideMenuDashboardActiveLink , ActiveCssClass = @Tailwind.ActiveSidebarMenu},
		new SidemenuItem { Id = "managestaff", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "manageroles", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "managemembers", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "datamigration", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "Accounts", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink, ActiveCssClass = @Tailwind.ActiveSideBarSubmenu },
		new SidemenuItem { Id = "requestshistory", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "newapplication", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "report", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "depositaccounts", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "depositrequesthistory", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},
		new SidemenuItem { Id = "depositnewapplication", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink , ActiveCssClass = @Tailwind.ActiveSideBarSubmenu},

		new SidemenuItem { Id = "guarantorrequests", CssClass = Tailwind.SideMenuDashboardActiveLink, DefaultCssClass = Tailwind.SideMenuDashboardActiveLink , ActiveCssClass = @Tailwind.ActiveSidebarMenu},
		new SidemenuItem { Id = "audittrails", CssClass = Tailwind.SideMenuDashboardActiveLink, DefaultCssClass = Tailwind.SideMenuDashboardActiveLink , ActiveCssClass = @Tailwind.ActiveSidebarMenu},
		new SidemenuItem { Id = "myprofile", CssClass = Tailwind.SideMenuDashboardActiveLink, DefaultCssClass = Tailwind.SideMenuDashboardActiveLink, ActiveCssClass = @Tailwind.ActiveSidebarMenu},
		new SidemenuItem { Id = "mybankaccounts", CssClass = Tailwind.SideMenuLink, DefaultCssClass = Tailwind.SideMenuLink, ActiveCssClass = @Tailwind.ActiveSideBarSubmenu },
		
		};

		protected override async void OnInitialized()
		{
			var options = microsoftIdentityOptions.CurrentValue;
			canEditProfile = !string.IsNullOrEmpty(options.EditProfilePolicyId);
			DisableMenu();
			HideMenu();

		}



		private async Task GoToDashBoard()
		{
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			var user = authState.User;

			if (user.Identity.IsAuthenticated)
			{
				userRole = user.FindFirstValue(ClaimTypes.Role);
				var uriValue = user.FindFirstValue(ClaimTypes.Uri);
				if (!string.IsNullOrEmpty(uriValue) && uriValue == "/admin/dashboard")
					NavigationManager.NavigateTo("/Dashboard", false);
				else
					NavigationManager.NavigateTo("/admin/dashboard", false);

			}
			SetActiveMenuItem("Dashboard");

		}
		private async Task DisableMenu()
		{

			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			var user = authState.User;

			if (user.Identity.IsAuthenticated)
			{
				var hasCompletedKyc = Boolean.Parse(user.FindFirstValue(ClaimTypes.StateOrProvince));


				if (!hasCompletedKyc)
				{
					var uriValue = user.FindFirstValue(ClaimTypes.Uri);
					if (!string.IsNullOrEmpty(uriValue) && uriValue == "/admin/dashboard")
						disableMenu = true;
					else
						disableMenu = false;
				}

			}
			else
			{
				NavigationManager.NavigateTo("/identity/account/login");
			}
		}
		private void GoToProfile()
		{
			SetActiveMenuItem("myprofile");
			navigationManager.NavigateTo("/complete-kyc", false);
		}
		private void GoToApprovals()
		{
			SetActiveMenuItem("approvals");
			navigationManager.NavigateTo("/approvals/all", false);
		}
		private void GoToGuarantoryApproval()
		{
			SetActiveMenuItem("guarantorrequests");
			navigationManager.NavigateTo("/security/guarantorapproval", false);
		}
		private async Task GoToLoanPage()
		{
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			var user = authState.User;

			if (user.Identity.IsAuthenticated)
			{
				NavigationManager.NavigateTo("/account/loanproductsapplications", false);
			}

		}
		private void GoToMyBankAccounts()
		{
			SetActiveMenuItem("mybankaccounts");
			navigationManager.NavigateTo("/mybankaccounts", false);
		}
		private async Task HideMenu()
		{
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			var CurrentUser = authState.User;
			if (CurrentUser != null)
			{
				var admin = CurrentUser.Claims.Where(f => f.Type == "IsAdmin").FirstOrDefault();
				var customerId = CurrentUser.Claims.Where(f => f.Type == "CustomerId").FirstOrDefault();
				if (admin != null && admin.Value.ToLower() == "false" && customerId != null)
					isCustomer = true;
				if (admin != null && admin.Value.ToLower() == "true")
					isAdmin = true;
			}
		}


		private string activeMenuItem;

		private void SetActiveMenuItem(string menuId)
		{
			activeMenuItem = menuId.ToLower();
			SidebarMenu.ForEach(menu => menu.CssClass = menu.Id.ToLower() == activeMenuItem ? menu.ActiveCssClass : menu.DefaultCssClass);
		}



	}
}
