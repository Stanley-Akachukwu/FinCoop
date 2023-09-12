
using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals;

namespace ChevronCoop.Web.AppUI.BlazorServer.Services
{
    

    public class ApprovalStateContainerService
    {
        public ApprovalMasterView SelectedApprovalMasterView { get; set; }
        public event Action OnStateChange;
        public void SetValue(ApprovalMasterView value)
        {
            this.SelectedApprovalMasterView = value;
            NotifyStateChanged();
        }
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }

    //public class ApprovalListStateContainerService
    //{
    //    public ApprovalMasterView SelectedApprovalMasterView { get; set; }
    //    public event Action OnStateChange;
    //    public void SetValue(ApprovalMasterView value)
    //    {
    //        this.SelectedApprovalMasterView = value;
    //        NotifyStateChanged();
    //    }
    //    //public List<ApprovalMasterView> CurrentUserApprovalList { get; set; }
    //    //public void SetValue(List<ApprovalMasterView> value)
    //    //{
    //    //    this.CurrentUserApprovalList = value;
    //    //    NotifyStateChanged();
    //    //}
    //    private void NotifyStateChanged() => OnStateChange?.Invoke();
    //}


}


