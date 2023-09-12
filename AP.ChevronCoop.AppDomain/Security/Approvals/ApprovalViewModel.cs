using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupMembers;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;

namespace AP.ChevronCoop.AppDomain.Security.Approvals
{
    public partial class ApprovalViewModel : BaseViewModel
    {
        public ApprovalViewModel()
        {
            EntityDbLocations = GetEntityDbTableLocations();
        }

        private List<EntityDbTableLocation> GetEntityDbTableLocations()
        {
            return new List<EntityDbTableLocation>() 
            { 
                new EntityDbTableLocation(){ Id=1,Name ="RetireeSwitch"},
                new EntityDbTableLocation(){ Id=2,Name ="MemberBulkUploadSession"},
                new EntityDbTableLocation(){ Id=3,Name ="LoanProduct"},
                new EntityDbTableLocation(){ Id=4,Name ="DepositProduct"},
                new EntityDbTableLocation(){ Id=5,Name ="MemberProfile"},
            };   
        }

        public string Id { get; set; }
        public string? Module { get; set; }
       // public string Payload { get; set; }
        public string? EntityPageUrl { get; set; }
        public string? EntityType { get; set; }
        public string? EntityId { get; set; }
        public string? TableName { get; set; }
        public string? ApprovalWorkflowId { get; set; }
        public string? Comment { get; set; }
        public bool IsApprovalCompleted { get; set; }
        public string EmailTitle { get; set; }
        public string RequestAttributesDetails { get; set; }
        public string ApprovalWorkflowType { get; set; }
        public List<ApprovalGroupMember> ApprovalGroupMembers { get; set; } = new List<ApprovalGroupMember>();
        public virtual List<EntityDbTableLocation> EntityDbLocations { get; set; } = new List<EntityDbTableLocation>();

    }
    
}

 




