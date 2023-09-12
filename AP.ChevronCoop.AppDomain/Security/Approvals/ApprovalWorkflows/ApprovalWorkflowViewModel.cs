using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows
{
    public class ApprovalWorkflowViewModel 
    {
		public string? Id { get; set; }
		public string? WorkflowName { get; set; }
		public List<ApprovalGroupViewModel> ApprovalGroups { get; set; }
		
    }
    
    
    public class GetApprovalWorkflowViewModel 
    {
	    public string? Id { get; set; }
	    public string? WorkflowName { get; set; }
	    public List<WorkflowApprovalGroupModel> ApprovalGroups { get; set; }
		
    }
}
 


     