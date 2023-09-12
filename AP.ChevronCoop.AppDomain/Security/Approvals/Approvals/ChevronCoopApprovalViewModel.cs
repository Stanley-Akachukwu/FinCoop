
using AP.ChevronCoop.Entities;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.Approvals
{
    public class ChevronCoopApprovalViewModel
    {
        public string? ApprovalId { get; set; }
        public ApprovalStatus Status { get; set; } = ApprovalStatus.INITIATED;
        public List<ApprovalPartialView> ApprovalPartialViews { get; set; }
    }
    public class ChildView
    {
        public string? FieldLabel { get; set; }
        public string? FieldValue { get; set; }
        public bool? IsFileDownload { get; set; } = false;
    }

    public class TabularView
    {
        public int FieldSN { get; set; }
        public string? FieldLabel { get; set; }
        public string? FieldValue { get; set; }
        public string? FieldValue2 { get; set; }
        public string? FieldValue3 { get; set; }
        public string? FieldValue4 { get; set; }
        public string? FieldValue5 { get; set; }
        public string? FieldValue6 { get; set; }
        public string? FieldValue7 { get; set; }
        public string? FieldValue8 { get; set; }
        public string? FieldValue9 { get; set; }
        public string? FieldValue10 { get; set; }
    }
     
    public class ApprovalPartialView
    {
        public int ViewId { get; set; }
        public string? Title { get; set; }
        public List<ChildView> Children { get; set; } = new List<ChildView>();
        public bool IsTabularView { get; set; }
        public bool IsMultipleFields { get; set; }
        public List<TabularView> ChildRows { get; set; } = new List<TabularView>();
        public List<string> FieldHeaders { get; set; } = new List<string>();

    }

}
