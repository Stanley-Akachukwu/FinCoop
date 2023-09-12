using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Deposits.Products.DepartmentDepositProductPublications;

[Table(nameof(DepartmentDepositProductPublicationMasterView), Schema = "Deposits")]
public class DepartmentDepositProductPublicationMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string PublicationType { get; set; }
    public string ProductId { get; set; }
    public string DepartmentId { get; set; }
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
    public string? ProductId_Code { get; set; }
    public string? ProductId_Name { get; set; }
    public string? ProductId_ShortName { get; set; }
    public string? ProductId_ApprovalWorkflowId { get; set; }
    public string? ProductId_ApprovalId { get; set; }
    public int? ProductId_MinimumAge { get; set; }
    public int? ProductId_MaximumAge { get; set; }
    public string? ProductId_Tenure { get; set; }
    public decimal? ProductId_TenureValue { get; set; }
    public string? ProductId_Status { get; set; }
    public string? ProductId_PublicationType { get; set; }
    public string? ProductId_PublishedByUserId { get; set; }
    public string? ProductId_DefaultCurrencyId { get; set; }
    public string? ProductId_BankDepositAccountId { get; set; }
    public string? ProductId_ProductDepositAccountId { get; set; }
    public string? ProductId_ChargesIncomeAccountId { get; set; }
    public string? ProductId_ChargesAccrualAccountId { get; set; }
    public string? ProductId_ChargesWaivedAccountId { get; set; }
    public string? ProductId_InterestPayableAccountId { get; set; }
    public string? ProductId_InterestPayoutAccountId { get; set; }
    public bool? ProductId_IsInterestEnabled { get; set; }
    public decimal? ProductId_MinimumContributionRegular { get; set; }
    public decimal? ProductId_MinimumContributionRetiree { get; set; }
    public string? ProductId_ProductType { get; set; }
    public bool? ProductId_IsActive { get; set; }
    public string? ProductId_CreatedByUserId { get; set; }
    public string? ProductId_UpdatedByUserId { get; set; }
    public string? ProductId_DeletedByUserId { get; set; }
    public bool? ProductId_IsDeleted { get; set; }
    public string? ProductId_Tags { get; set; }
    public string? ProductId_Caption { get; set; }
    public bool? ProductId_IsDefaultProduct { get; set; }
    public string? DepartmentId_Name { get; set; }
    public bool? DepartmentId_IsActive { get; set; }
    public string? DepartmentId_CreatedByUserId { get; set; }
    public string? DepartmentId_UpdatedByUserId { get; set; }
    public string? DepartmentId_DeletedByUserId { get; set; }
    public bool? DepartmentId_IsDeleted { get; set; }
    public string? DepartmentId_Tags { get; set; }
    public string? DepartmentId_Caption { get; set; }
}