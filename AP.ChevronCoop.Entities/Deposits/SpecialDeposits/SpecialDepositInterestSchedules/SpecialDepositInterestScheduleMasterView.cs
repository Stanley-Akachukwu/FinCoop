using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestSchedules
{

    [Table(nameof(SpecialDepositInterestScheduleMasterView), Schema = "Deposits")]
    public partial class SpecialDepositInterestScheduleMasterView
    {

        public long? RowNumber { get; set; } 
        public string Id { get; set; } 
        public string? CronJobConfigId { get; set; } 
        public string ScheduleName { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 
        public bool IsProcessed { get; set; } 
        public DateTime? ProcessedDate { get; set; } 
        public string? Description { get; set; } 
        public bool IsActive { get; set; } 
        public string? CreatedByUserId { get; set; } 
        public DateTimeOffset? DateCreated { get; set; } 
        public string? UpdatedByUserId { get; set; } 
        public DateTimeOffset? DateUpdated { get; set; } 
        public string? DeletedByUserId { get; set; } 
        public bool IsDeleted { get; set; } 
        public DateTimeOffset? DateDeleted { get; set; } 
        public Guid RowVersion { get; set; } 
        public string? FullText { get; set; } 
        public string? Tags { get; set; } 
        public string? Caption { get; set; } 
        public string? CronJobConfigId_DeductionScheduleId { get; set; } 
        public string? CronJobConfigId_CronJobType { get; set; } 
        public string? CronJobConfigId_JobName { get; set; } 
        public DateTime? CronJobConfigId_JobDate { get; set; } 
        public string? CronJobConfigId_JobStatus { get; set; } 
        public DateTime? CronJobConfigId_ComputationStartDate { get; set; } 
        public DateTime? CronJobConfigId_ComputationEndDate { get; set; } 
        public int? CronJobConfigId_RecordsProcessed { get; set; } 
        public bool? CronJobConfigId_IsActive { get; set; } 
        public string? CronJobConfigId_CreatedByUserId { get; set; } 
        public string? CronJobConfigId_UpdatedByUserId { get; set; } 
        public string? CronJobConfigId_DeletedByUserId { get; set; } 
        public bool? CronJobConfigId_IsDeleted { get; set; } 
        public string? CronJobConfigId_Tags { get; set; } 
        public string? CronJobConfigId_Caption { get; set; } 
        public decimal? CronJobConfigId_TotalAmount { get; set; } 
        public long? CronJobConfigId_TotalCount { get; set; } 

    }

}
