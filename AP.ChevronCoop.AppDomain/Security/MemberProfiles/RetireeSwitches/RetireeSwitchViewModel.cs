using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.RetireeSwitches
{
    public partial class RetireeSwitchViewModel : BaseViewModel
    {
        public string EventGlobalCodeId { get; set; }
        public string MemberProfileId { get; set; }
        public string Status { get; set; }
        public string ApprovalStateNote { get; set; }
        public string InitiatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovalId { get; set; }
        public string EntityId { get; set; }
        public string ApprovalWorkflowId { get; set; }
    }
}
