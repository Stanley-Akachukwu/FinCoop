using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.MasterData.GlobalCodes;
using MediatR;
using System.ComponentModel.DataAnnotations;


namespace AP.ChevronCoop.AppDomain.Security.Approvals
{
    public partial class UpdateApprovalCommand : UpdateCommand, IRequest<CommandResult<ApprovalViewModel>>
    {


       

        [MaxLength(512)]

        public string? Module { get; set; }

        [MaxLength(2048)]

        public string? Payload { get; set; }

        [MaxLength(512)]

        public string? EntityPageUrl { get; set; }

        [MaxLength(512)]

        public string? EntityType { get; set; }

        [MaxLength(80)]

        public string? EntityId { get; set; }
        [MaxLength(80)]

        public string? RequestEntityId { get; set; }

        [MaxLength(256)]

        public string? TableName { get; set; }

        [MaxLength(256)]

        public string? RequestedByUserId { get; set; }


        public DateTimeOffset? RequestDate { get; set; }

        [MaxLength(256)]

        public string? ProcessedByUserId { get; set; }


        public DateTimeOffset? ProcessedDate { get; set; }

        [MaxLength(256)]
        public string? ApprovalWorkflowId { get; set; }
        public int CurrentApprovalState { get; set; }
        [MaxLength(512)]
        public string? Comment { get; set; }
        public bool IsApprovalCompleted { get; set; }
        public string EmailTitle { get; set; }
        [Required]
        public string? AppliedStatus { get; set; }

    }
}
