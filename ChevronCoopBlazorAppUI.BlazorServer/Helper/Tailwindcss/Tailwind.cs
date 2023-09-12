using Microsoft.Identity.Client;

namespace ChevronCoop.Web.AppUI.BlazorServer.Helper.Tailwindcss
{
    public static class Tailwind
    {
        #region TextBox

        public static string TextBoxWithShadow { get; set; } = "shadow-sm bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-500 focus:border-primary-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500";

        public static string TextBox_General { get; set; } = "shadow-sm bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-500 focus:border-primary-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500";

        public static string Past_to_Compare { get; set; } = "bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-500 focus:border-primary-500 block w-full pl-10 p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-gray-200 dark:focus:ring-primary-500 dark:focus:border-primary-500";

        

        public static string TextBoxWithShadow300With { get; set; } = "shadow-sm bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-500 focus:border-primary-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500";

        public static string Textbox_With_smallText { get; set; } = "bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-500 focus:border-primary-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500";

        public static string Textbox_Biodata { get; set; } = "shadow-sm bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-500 focus:border-primary-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500";

        public static string TextBoxPrimaryEmail { get; set; } = "shadow-sm bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-500 focus:border-primary-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500";

        public static string SearchText = "bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-500 focus:border-primary-500 block w-full pl-10 p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-gray-200 dark:focus:ring-primary-500 dark:focus:border-primary-500";
        public static string SearchInput = "block w-full p-2 pl-10 text-sm text-gray-900 border border-gray-300 rounded-lg bg-gray-50 focus:ring-primary-500 focus:border-primary-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500";

        #endregion


        #region Buttons

        public static string Button_Blue { get; set; } = "text-white bg-CEMCS-Blue-100 hover:bg-CEMCS-Blue-100 focus:ring-4 focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800";

        public static string Button_Cancel { get; set; } = "text-CEMCS-Blue-100 border border-gray-300 focus:outline-none hover:bg-gray-100 focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800";

        public static string Button_Green { get; set; } = "text-white bg-green-500 hover:bg-green-600 focus:ring-4 focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800";
        public static string Compare_Button { get; set; } = "text-white bg-CEMCS-Blue-100 hover:bg-CEMCS-Blue-100 focus:ring-4 focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800";

        public static string SideMenuButton = "flex items-center p-2 w-full text-base font-normal text-gray-900 rounded-lg transition duration-75 group hover:bg-gray-100 dark:text-gray-200 dark:hover:bg-gray-700";

		public static string ActiveSideBarSubmenu = "flex items-center p-2 pl-11 text-base font-normal text-gray-900 rounded-lg transition duration-75 group hover:bg-gray-100 dark:text-gray-200 dark:hover:bg-gray-700 bg-sidebar-active";

		public static string ActiveSidebarMenu = "flex items-center p-2 w-full text-base font-normal text-gray-900 rounded-lg transition duration-75 group hover:bg-gray-100 dark:text-gray-200 dark:hover:bg-gray-700 bg-sidebar-active";

        public static string GreenButton_Inline = "inline-flex items-center py-2 px-3 text-sm font-medium text-center text-white rounded-lg bg-green-500 hover:bg-green-500 focus:ring-4 focus:ring-primary-300 sm:ml-auto dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800";

        public static string Search_Button_Blue = "text-white absolute right-0 bottom-0 top-0 bg-primary-700 hover:bg-primary-800 focus:ring-4 focus:outline-none focus:ring-primary-300 font-medium rounded-r-lg text-sm px-4 py-2 dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800";

        public static string Small_Blue_Button = "mt-4 py-2 px-8 w-full text-xs font-medium text-center text-white rounded-lg bg-CEMCS-Blue-100 hover:bg-primary-800 focus:ring-4 focus:ring-primary-300 sm:w-auto dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800";

        public static string Close_Button_For_X = "text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 absolute top-2.5 right-2.5 inline-flex items-center dark:hover:bg-gray-600 dark:hover:text-white";

        public static string Done_Button_SuccessMessage = "my-2 py-3 px-5 w-full text-base font-medium text-center text-white rounded-lg bg-CEMCS-Blue-100 hover:bg-CEMCS-Blue-100 focus:ring-4 focus:ring-primary-300 dark:bg-CEMCS-Blue-100 dark:hover:bg-CEMCS-Blue-100 dark:focus:ring-primary-800";

        public static string Back_Button = "text-CEMCS-Blue-100 border border-CEMCS-Blue-100 bg-white hover:bg-white focus:ring-4 focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800";

        public static string ThreeDotBtn = "inline-flex items-center p-0.5 text-sm font-medium text-center text-gray-500 hover:text-gray-800 rounded-lg focus:outline-none dark:text-gray-400 dark:hover:text-gray-100";

        public static string LoginButton = "py-3 px-5 w-full text-base font-medium text-center text-white rounded-lg bg-CEMCS-Blue-100 hover:bg-CEMCS-Blue-100 focus:ring-4 focus:ring-primary-300  dark:bg-CEMCS-Blue-100 dark:hover:bg-CEMCS-Blue-100 dark:focus:ring-primary-800";

		#endregion

		#region Images

		public static string ShadowImages { get; set; } = "h-auto max-w-sm rounded-lg shadow-none transition-shadow duration-300 ease-in-out hover:shadow-lg hover:shadow-black/30";

        #endregion

        #region DropdownList

        public static string DropdownList { get; set; } = "bg-gray-50 border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-500 focus:border-primary-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500";

        public static string DropDownList_blue { get; set; } = "bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500";

        public static string DropDownList_primary { get; set; } = "bg-gray-50 border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-500 focus:border-primary-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500";

        public static string Compare_dropDown { get; set; } = "bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500";

        public static string ComboBox_General { get; set; } = "shadow-sm bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-500 focus:border-primary-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500";

        public static string ToggleButton { get; set; } = "w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-blue-300 dark:peer-focus:ring-blue-800 rounded-full peer dark:bg-gray-700";

        #endregion



        #region DateTimePicker

        public static string DateTimePicker_1 { get; set; } = "shadow-sm bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-500 focus:border-primary-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500";

        #endregion


        #region Validation Message

        public static string Validation_Red { get; set; } = "mt-2 text-sm text-red-600 dark:text-red-500";


        #endregion

        #region Navigation Link

        public static string SideMenuLink = "flex items-center p-2 pl-11 text-base font-normal text-gray-900 rounded-lg transition duration-75 group hover:bg-gray-100 dark:text-gray-200 dark:hover:bg-gray-700";

        public static string SideMenuDashboardActiveLink = "flex items-center p-2 text-base font-normal text-gray-900 rounded-lg hover:bg-sidebar-active  dark:text-gray-200 dark:hover:bg-gray-700";

        public static string PlainLink = "p-2 text-sm font-medium rounded-lg text-primary-700 hover:bg-gray-100 dark:text-primary-500 dark:hover:bg-gray-700";

        public static string DefaultLink = "inline-flex items-center p-2 text-xs font-medium uppercase rounded-lg text-primary-700 sm:text-sm hover:bg-gray-100 dark:text-primary-500 dark:hover:bg-gray-700";

        public static string BlockLink = "block py-2 px-4 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white";

        public static string FilterLink = "block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white";

        #endregion


        #region Radio Button

        public static string RadioButton = "block ml-2 text-sm font-medium text-gray-900 dark:text-gray-300";

        #endregion

        #region Other Tailwind Links


        public static string SpanCSS = "ml-1 inline-flex items-center justify-center w-6 h-6 text-xs font-bold text-red-500 bg-red-100 border-2 border-white rounded-full -top-2 -right-2 dark:border-gray-900";

        #endregion

        #region Labels

        public static string PageHeaders = "text-3xl";

        #endregion


    }
}
