using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Entities;


namespace AP.ChevronCoop.AppDomain.Security.Approvals
{
    public partial class CreateApprovalCommand : IRequest<CommandResult<ApprovalViewModel>>
    {
        public string Module { get; set; }
        public string ApprovalWorkflowId { get; set; }
        public ApprovalType Type { get; set; }
        public string Payload { get; set; }
        public string RequestedByUserId { get; set; }
        public string? Comment { get; set; }
        public int CurrentApprovalState { get; set; }
        
        
        
        // public string EntityPageUrl { get; set; }
        // public string EntityType { get; set; }
        // public string EntityId { get; set; }
        // public string RequestEntityId { get; set; }
        // public DateTimeOffset? RequestDate { get; set; }
        // public string? ProcessedByUserId { get; set; }
        // public DateTimeOffset? ProcessedDate { get; set; }
        // public bool IsApprovalCompleted { get; set; }
        // public string EmailTitle { get; set; }
	}

}
