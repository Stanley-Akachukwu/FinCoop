namespace AP.ChevronCoop.Entities.Documents.PayrollDeductionDocuments;

public class PayrollDeductionDocumentMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string? PayrollDeductionScheduleId { get; set; }
    public int ProcessSequence { get; set; }
    public bool IsProcessed { get; set; }
    public DateTime ProcessedDate { get; set; }
    public string? DocumentData { get; set; }
    public string? MimeType { get; set; }
    public string FileName { get; set; }
    public int FileSize { get; set; }
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
    public string? PayrollDeductionScheduleId_ScheduleName { get; set; }
    public string? PayrollDeductionScheduleId_ScheduleType { get; set; }
    public string? PayrollDeductionScheduleId_BankAccountId { get; set; }
    public string? PayrollDeductionScheduleId_SpecialDepositBankAccountId { get; set; }
    public string? PayrollDeductionScheduleId_FixedDepositBankAccountId { get; set; }
    public int? PayrollDeductionScheduleId_DeductionsCount { get; set; }
    public decimal? PayrollDeductionScheduleId_TotalDeductions { get; set; }
    public int? PayrollDeductionScheduleId_MinDecimalPlace { get; set; }
    public int? PayrollDeductionScheduleId_MaxDecimalPlace { get; set; }
    public DateTime? PayrollDeductionScheduleId_AdviseDate { get; set; }
    public DateTime? PayrollDeductionScheduleId_ExpectedDate { get; set; }
    public bool? PayrollDeductionScheduleId_IsPosted { get; set; }
    public DateTime? PayrollDeductionScheduleId_PayrollDate { get; set; }
    public bool? PayrollDeductionScheduleId_IsUploaded { get; set; }
    public DateTime? PayrollDeductionScheduleId_LastUploadedDate { get; set; }
    public bool? PayrollDeductionScheduleId_IsProcessed { get; set; }
    public DateTime? PayrollDeductionScheduleId_ProcessedDate { get; set; }
    public int? PayrollDeductionScheduleId_GenerateDeductionCronJobStatus { get; set; }
    public DateTime? PayrollDeductionScheduleId_GenerateDeductionCronJobStartedDate { get; set; }
    public DateTime? PayrollDeductionScheduleId_GenerateDeductionCronJobCompletedDate { get; set; }
    public int? PayrollDeductionScheduleId_ProcessDeductionCronJobStatus { get; set; }
    public DateTime? PayrollDeductionScheduleId_ProcessDeductionCronJobStartedDate { get; set; }
    public DateTime? PayrollDeductionScheduleId_ProcessDeductionCronJobCompletedDate { get; set; }
    public bool? PayrollDeductionScheduleId_IsActive { get; set; }
    public string? PayrollDeductionScheduleId_CreatedByUserId { get; set; }
    public string? PayrollDeductionScheduleId_UpdatedByUserId { get; set; }
    public string? PayrollDeductionScheduleId_DeletedByUserId { get; set; }
    public bool? PayrollDeductionScheduleId_IsDeleted { get; set; }
    public string? PayrollDeductionScheduleId_Tags { get; set; }
    public string? PayrollDeductionScheduleId_Caption { get; set; }
}