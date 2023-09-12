﻿namespace AP.ChevronCoop.Entities.Loans.LoanProducts;

public class LoanProductMasterView
{
  public long? RowNumber { get; set; }
  public string Id { get; set; }
  public string Code { get; set; }
  public string? PayrollCode { get; set; }
  public string Name { get; set; }
  public string ShortName { get; set; }
  public string ApprovalWorkflowId { get; set; }
  public string? ApprovalId { get; set; }
  public decimal PrincipalMultiple { get; set; }
  public decimal PrincipalMinLimit { get; set; }
  public decimal PrincipalMaxLimit { get; set; }
  public string TenureUnit { get; set; }
  public decimal MinTenureValue { get; set; }
  public decimal MaxTenureValue { get; set; }
  public string RepaymentPeriod { get; set; }
  public string InterestMethod { get; set; }
  public string InterestCalculationMethod { get; set; }
  public decimal DaysInYear { get; set; }
  public decimal InterestRate { get; set; }
  public bool HasAdminCharges { get; set; }
  public bool IsTargetLoan { get; set; }
  public string? BenefitCode { get; set; }
  public string AllowedOffsetType { get; set; }
  public string OffsetPeriodUnit { get; set; }
  public decimal OffsetPeriodValue { get; set; }
  public bool EnableSavingsOffset { get; set; }
  public bool EnableChargeWaiver { get; set; }
  public bool EnableTopUp { get; set; }
  public bool EnableTopUpCharges { get; set; }
  public bool EnableAdminOffsetCharge { get; set; }
  public bool EnableWaitingPeriod { get; set; }
  public string WaitingPeriodUnit { get; set; }
  public decimal WaitingPeriodValue { get; set; }
  public bool EnableWaitingPeriodCharge { get; set; }
  public bool IsGuarantorRequired { get; set; }
  public int GuarantorMinYear { get; set; }
  public decimal GuarantorAmountLimit { get; set; }
  public int EmployeeGuarantorCount { get; set; }
  public int NonEmployeeGuarantorCount { get; set; }
  public string QualificationTargetProduct { get; set; }
  public decimal QualificationMinBalancePercentage { get; set; }
  public string? SavingsOffSetProductIdList { get; set; }
  public string? MemberTypeIdList { get; set; }
  public int? MinimumAge { get; set; }
  public int? MaximumAge { get; set; }
  public string? DefaultCurrencyId { get; set; }
  public string LoanProductType { get; set; }
  public string? BankDepositAccountId { get; set; }
  public string? DisbursementAccountId { get; set; }
  public string PrincipalAccountId { get; set; }
  public string PrincipalLossAccountId { get; set; }
  public string InterestIncomeAccountId { get; set; }
  public string UnearnedInterestAccountId { get; set; }
  public string InterestLossAccountId { get; set; }
  public string PenalInterestReceivableAccountId { get; set; }
  public string InterestWaivedAccountId { get; set; }
  public string ChargesIncomeAccountId { get; set; }
  public string ChargesAccrualAccountId { get; set; }
  public string ChargesWaivedAccountId { get; set; }
  public string Status { get; set; }
  public int PublicationType { get; set; }
  public string? PublishedByUserId { get; set; }
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
  public string? ApprovalWorkflowId_WorkflowName { get; set; }
  public bool? ApprovalWorkflowId_IsDefaultApprovalWorkflow { get; set; }
  public int? ApprovalWorkflowId_RequiredApprovers { get; set; }
  public int? ApprovalWorkflowId_RequiredGroups { get; set; }
  public bool? ApprovalWorkflowId_IsActive { get; set; }
  public string? ApprovalWorkflowId_CreatedByUserId { get; set; }
  public string? ApprovalWorkflowId_UpdatedByUserId { get; set; }
  public string? ApprovalWorkflowId_DeletedByUserId { get; set; }
  public bool? ApprovalWorkflowId_IsDeleted { get; set; }
  public string? ApprovalWorkflowId_Tags { get; set; }
  public string? ApprovalWorkflowId_Caption { get; set; }
  public string? ApprovalId_Module { get; set; }
  public string? ApprovalId_ApprovalType { get; set; }
  public string? ApprovalId_Status { get; set; }
  public int? ApprovalId_CurrentSequence { get; set; }
  public string? ApprovalId_ApprovalWorkflowId { get; set; }
  public string? ApprovalId_Payload { get; set; }
  public bool? ApprovalId_IsApprovalCompleted { get; set; }
  public string? ApprovalId_Comment { get; set; }
  public string? ApprovalId_EntityId { get; set; }
  public int? ApprovalId_TriedCount { get; set; }
  public bool? ApprovalId_IsActive { get; set; }
  public string? ApprovalId_CreatedByUserId { get; set; }
  public string? ApprovalId_UpdatedByUserId { get; set; }
  public string? ApprovalId_DeletedByUserId { get; set; }
  public bool? ApprovalId_IsDeleted { get; set; }
  public string? ApprovalId_Tags { get; set; }
  public string? ApprovalId_Caption { get; set; }
  public string? ApprovalId_ApprovalViewModelPayload { get; set; }
  public string? DefaultCurrencyId_Code { get; set; }
  public string? DefaultCurrencyId_Name { get; set; }
  public string? DefaultCurrencyId_Symbol { get; set; }
  public string? DefaultCurrencyId_IsoSymbol { get; set; }
  public int? DefaultCurrencyId_DecimalPlaces { get; set; }
  public string? DefaultCurrencyId_Format { get; set; }
  public bool? DefaultCurrencyId_IsActive { get; set; }
  public string? DefaultCurrencyId_CreatedByUserId { get; set; }
  public string? DefaultCurrencyId_UpdatedByUserId { get; set; }
  public string? DefaultCurrencyId_DeletedByUserId { get; set; }
  public bool? DefaultCurrencyId_IsDeleted { get; set; }
  public string? DefaultCurrencyId_Tags { get; set; }
  public string? DefaultCurrencyId_Caption { get; set; }
  public string? BankDepositAccountId_LedgerAccountId { get; set; }
  public string? BankDepositAccountId_BankId { get; set; }
  public string? BankDepositAccountId_BranchName { get; set; }
  public string? BankDepositAccountId_BranchAddress { get; set; }
  public string? BankDepositAccountId_CurrencyId { get; set; }
  public string? BankDepositAccountId_AccountName { get; set; }
  public string? BankDepositAccountId_AccountNumber { get; set; }
  public string? BankDepositAccountId_BVN { get; set; }
  public bool? BankDepositAccountId_IsActive { get; set; }
  public string? BankDepositAccountId_CreatedByUserId { get; set; }
  public string? BankDepositAccountId_UpdatedByUserId { get; set; }
  public string? BankDepositAccountId_DeletedByUserId { get; set; }
  public bool? BankDepositAccountId_IsDeleted { get; set; }
  public string? BankDepositAccountId_Tags { get; set; }
  public string? BankDepositAccountId_Caption { get; set; }
  public string? DisbursementAccountId_LedgerAccountId { get; set; }
  public string? DisbursementAccountId_BankId { get; set; }
  public string? DisbursementAccountId_BranchName { get; set; }
  public string? DisbursementAccountId_BranchAddress { get; set; }
  public string? DisbursementAccountId_CurrencyId { get; set; }
  public string? DisbursementAccountId_AccountName { get; set; }
  public string? DisbursementAccountId_AccountNumber { get; set; }
  public string? DisbursementAccountId_BVN { get; set; }
  public bool? DisbursementAccountId_IsActive { get; set; }
  public string? DisbursementAccountId_CreatedByUserId { get; set; }
  public string? DisbursementAccountId_UpdatedByUserId { get; set; }
  public string? DisbursementAccountId_DeletedByUserId { get; set; }
  public bool? DisbursementAccountId_IsDeleted { get; set; }
  public string? DisbursementAccountId_Tags { get; set; }
  public string? DisbursementAccountId_Caption { get; set; }
  public string? PrincipalAccountId_AccountType { get; set; }
  public string? PrincipalAccountId_UOM { get; set; }
  public string? PrincipalAccountId_CurrencyId { get; set; }
  public string? PrincipalAccountId_Code { get; set; }
  public string? PrincipalAccountId_Name { get; set; }
  public string? PrincipalAccountId_ParentId { get; set; }
  public decimal? PrincipalAccountId_ClearedBalance { get; set; }
  public decimal? PrincipalAccountId_UnclearedBalance { get; set; }
  public decimal? PrincipalAccountId_LedgerBalance { get; set; }
  public decimal? PrincipalAccountId_AvailableBalance { get; set; }
  public bool? PrincipalAccountId_IsOfficeAccount { get; set; }
  public bool? PrincipalAccountId_AllowManualEntry { get; set; }
  public bool? PrincipalAccountId_IsClosed { get; set; }
  public DateTime? PrincipalAccountId_DateClosed { get; set; }
  public string? PrincipalAccountId_ClosedByUserName { get; set; }
  public bool? PrincipalAccountId_IsActive { get; set; }
  public string? PrincipalAccountId_CreatedByUserId { get; set; }
  public string? PrincipalAccountId_UpdatedByUserId { get; set; }
  public string? PrincipalAccountId_DeletedByUserId { get; set; }
  public bool? PrincipalAccountId_IsDeleted { get; set; }
  public string? PrincipalAccountId_Tags { get; set; }
  public string? PrincipalAccountId_Caption { get; set; }
  public string? PrincipalLossAccountId_AccountType { get; set; }
  public string? PrincipalLossAccountId_UOM { get; set; }
  public string? PrincipalLossAccountId_CurrencyId { get; set; }
  public string? PrincipalLossAccountId_Code { get; set; }
  public string? PrincipalLossAccountId_Name { get; set; }
  public string? PrincipalLossAccountId_ParentId { get; set; }
  public decimal? PrincipalLossAccountId_ClearedBalance { get; set; }
  public decimal? PrincipalLossAccountId_UnclearedBalance { get; set; }
  public decimal? PrincipalLossAccountId_LedgerBalance { get; set; }
  public decimal? PrincipalLossAccountId_AvailableBalance { get; set; }
  public bool? PrincipalLossAccountId_IsOfficeAccount { get; set; }
  public bool? PrincipalLossAccountId_AllowManualEntry { get; set; }
  public bool? PrincipalLossAccountId_IsClosed { get; set; }
  public DateTime? PrincipalLossAccountId_DateClosed { get; set; }
  public string? PrincipalLossAccountId_ClosedByUserName { get; set; }
  public bool? PrincipalLossAccountId_IsActive { get; set; }
  public string? PrincipalLossAccountId_CreatedByUserId { get; set; }
  public string? PrincipalLossAccountId_UpdatedByUserId { get; set; }
  public string? PrincipalLossAccountId_DeletedByUserId { get; set; }
  public bool? PrincipalLossAccountId_IsDeleted { get; set; }
  public string? PrincipalLossAccountId_Tags { get; set; }
  public string? PrincipalLossAccountId_Caption { get; set; }
  public string? InterestIncomeAccountId_AccountType { get; set; }
  public string? InterestIncomeAccountId_UOM { get; set; }
  public string? InterestIncomeAccountId_CurrencyId { get; set; }
  public string? InterestIncomeAccountId_Code { get; set; }
  public string? InterestIncomeAccountId_Name { get; set; }
  public string? InterestIncomeAccountId_ParentId { get; set; }
  public decimal? InterestIncomeAccountId_ClearedBalance { get; set; }
  public decimal? InterestIncomeAccountId_UnclearedBalance { get; set; }
  public decimal? InterestIncomeAccountId_LedgerBalance { get; set; }
  public decimal? InterestIncomeAccountId_AvailableBalance { get; set; }
  public bool? InterestIncomeAccountId_IsOfficeAccount { get; set; }
  public bool? InterestIncomeAccountId_AllowManualEntry { get; set; }
  public bool? InterestIncomeAccountId_IsClosed { get; set; }
  public DateTime? InterestIncomeAccountId_DateClosed { get; set; }
  public string? InterestIncomeAccountId_ClosedByUserName { get; set; }
  public bool? InterestIncomeAccountId_IsActive { get; set; }
  public string? InterestIncomeAccountId_CreatedByUserId { get; set; }
  public string? InterestIncomeAccountId_UpdatedByUserId { get; set; }
  public string? InterestIncomeAccountId_DeletedByUserId { get; set; }
  public bool? InterestIncomeAccountId_IsDeleted { get; set; }
  public string? InterestIncomeAccountId_Tags { get; set; }
  public string? InterestIncomeAccountId_Caption { get; set; }
  public string? UnearnedInterestAccountId_AccountType { get; set; }
  public string? UnearnedInterestAccountId_UOM { get; set; }
  public string? UnearnedInterestAccountId_CurrencyId { get; set; }
  public string? UnearnedInterestAccountId_Code { get; set; }
  public string? UnearnedInterestAccountId_Name { get; set; }
  public string? UnearnedInterestAccountId_ParentId { get; set; }
  public decimal? UnearnedInterestAccountId_ClearedBalance { get; set; }
  public decimal? UnearnedInterestAccountId_UnclearedBalance { get; set; }
  public decimal? UnearnedInterestAccountId_LedgerBalance { get; set; }
  public decimal? UnearnedInterestAccountId_AvailableBalance { get; set; }
  public bool? UnearnedInterestAccountId_IsOfficeAccount { get; set; }
  public bool? UnearnedInterestAccountId_AllowManualEntry { get; set; }
  public bool? UnearnedInterestAccountId_IsClosed { get; set; }
  public DateTime? UnearnedInterestAccountId_DateClosed { get; set; }
  public string? UnearnedInterestAccountId_ClosedByUserName { get; set; }
  public bool? UnearnedInterestAccountId_IsActive { get; set; }
  public string? UnearnedInterestAccountId_CreatedByUserId { get; set; }
  public string? UnearnedInterestAccountId_UpdatedByUserId { get; set; }
  public string? UnearnedInterestAccountId_DeletedByUserId { get; set; }
  public bool? UnearnedInterestAccountId_IsDeleted { get; set; }
  public string? UnearnedInterestAccountId_Tags { get; set; }
  public string? UnearnedInterestAccountId_Caption { get; set; }
  public string? InterestLossAccountId_AccountType { get; set; }
  public string? InterestLossAccountId_UOM { get; set; }
  public string? InterestLossAccountId_CurrencyId { get; set; }
  public string? InterestLossAccountId_Code { get; set; }
  public string? InterestLossAccountId_Name { get; set; }
  public string? InterestLossAccountId_ParentId { get; set; }
  public decimal? InterestLossAccountId_ClearedBalance { get; set; }
  public decimal? InterestLossAccountId_UnclearedBalance { get; set; }
  public decimal? InterestLossAccountId_LedgerBalance { get; set; }
  public decimal? InterestLossAccountId_AvailableBalance { get; set; }
  public bool? InterestLossAccountId_IsOfficeAccount { get; set; }
  public bool? InterestLossAccountId_AllowManualEntry { get; set; }
  public bool? InterestLossAccountId_IsClosed { get; set; }
  public DateTime? InterestLossAccountId_DateClosed { get; set; }
  public string? InterestLossAccountId_ClosedByUserName { get; set; }
  public bool? InterestLossAccountId_IsActive { get; set; }
  public string? InterestLossAccountId_CreatedByUserId { get; set; }
  public string? InterestLossAccountId_UpdatedByUserId { get; set; }
  public string? InterestLossAccountId_DeletedByUserId { get; set; }
  public bool? InterestLossAccountId_IsDeleted { get; set; }
  public string? InterestLossAccountId_Tags { get; set; }
  public string? InterestLossAccountId_Caption { get; set; }
  public string? PenalInterestReceivableAccountId_AccountType { get; set; }
  public string? PenalInterestReceivableAccountId_UOM { get; set; }
  public string? PenalInterestReceivableAccountId_CurrencyId { get; set; }
  public string? PenalInterestReceivableAccountId_Code { get; set; }
  public string? PenalInterestReceivableAccountId_Name { get; set; }
  public string? PenalInterestReceivableAccountId_ParentId { get; set; }
  public decimal? PenalInterestReceivableAccountId_ClearedBalance { get; set; }
  public decimal? PenalInterestReceivableAccountId_UnclearedBalance { get; set; }
  public decimal? PenalInterestReceivableAccountId_LedgerBalance { get; set; }
  public decimal? PenalInterestReceivableAccountId_AvailableBalance { get; set; }
  public bool? PenalInterestReceivableAccountId_IsOfficeAccount { get; set; }
  public bool? PenalInterestReceivableAccountId_AllowManualEntry { get; set; }
  public bool? PenalInterestReceivableAccountId_IsClosed { get; set; }
  public DateTime? PenalInterestReceivableAccountId_DateClosed { get; set; }
  public string? PenalInterestReceivableAccountId_ClosedByUserName { get; set; }
  public bool? PenalInterestReceivableAccountId_IsActive { get; set; }
  public string? PenalInterestReceivableAccountId_CreatedByUserId { get; set; }
  public string? PenalInterestReceivableAccountId_UpdatedByUserId { get; set; }
  public string? PenalInterestReceivableAccountId_DeletedByUserId { get; set; }
  public bool? PenalInterestReceivableAccountId_IsDeleted { get; set; }
  public string? PenalInterestReceivableAccountId_Tags { get; set; }
  public string? PenalInterestReceivableAccountId_Caption { get; set; }
  public string? InterestWaivedAccountId_AccountType { get; set; }
  public string? InterestWaivedAccountId_UOM { get; set; }
  public string? InterestWaivedAccountId_CurrencyId { get; set; }
  public string? InterestWaivedAccountId_Code { get; set; }
  public string? InterestWaivedAccountId_Name { get; set; }
  public string? InterestWaivedAccountId_ParentId { get; set; }
  public decimal? InterestWaivedAccountId_ClearedBalance { get; set; }
  public decimal? InterestWaivedAccountId_UnclearedBalance { get; set; }
  public decimal? InterestWaivedAccountId_LedgerBalance { get; set; }
  public decimal? InterestWaivedAccountId_AvailableBalance { get; set; }
  public bool? InterestWaivedAccountId_IsOfficeAccount { get; set; }
  public bool? InterestWaivedAccountId_AllowManualEntry { get; set; }
  public bool? InterestWaivedAccountId_IsClosed { get; set; }
  public DateTime? InterestWaivedAccountId_DateClosed { get; set; }
  public string? InterestWaivedAccountId_ClosedByUserName { get; set; }
  public bool? InterestWaivedAccountId_IsActive { get; set; }
  public string? InterestWaivedAccountId_CreatedByUserId { get; set; }
  public string? InterestWaivedAccountId_UpdatedByUserId { get; set; }
  public string? InterestWaivedAccountId_DeletedByUserId { get; set; }
  public bool? InterestWaivedAccountId_IsDeleted { get; set; }
  public string? InterestWaivedAccountId_Tags { get; set; }
  public string? InterestWaivedAccountId_Caption { get; set; }
  public string? ChargesIncomeAccountId_AccountType { get; set; }
  public string? ChargesIncomeAccountId_UOM { get; set; }
  public string? ChargesIncomeAccountId_CurrencyId { get; set; }
  public string? ChargesIncomeAccountId_Code { get; set; }
  public string? ChargesIncomeAccountId_Name { get; set; }
  public string? ChargesIncomeAccountId_ParentId { get; set; }
  public decimal? ChargesIncomeAccountId_ClearedBalance { get; set; }
  public decimal? ChargesIncomeAccountId_UnclearedBalance { get; set; }
  public decimal? ChargesIncomeAccountId_LedgerBalance { get; set; }
  public decimal? ChargesIncomeAccountId_AvailableBalance { get; set; }
  public bool? ChargesIncomeAccountId_IsOfficeAccount { get; set; }
  public bool? ChargesIncomeAccountId_AllowManualEntry { get; set; }
  public bool? ChargesIncomeAccountId_IsClosed { get; set; }
  public DateTime? ChargesIncomeAccountId_DateClosed { get; set; }
  public string? ChargesIncomeAccountId_ClosedByUserName { get; set; }
  public bool? ChargesIncomeAccountId_IsActive { get; set; }
  public string? ChargesIncomeAccountId_CreatedByUserId { get; set; }
  public string? ChargesIncomeAccountId_UpdatedByUserId { get; set; }
  public string? ChargesIncomeAccountId_DeletedByUserId { get; set; }
  public bool? ChargesIncomeAccountId_IsDeleted { get; set; }
  public string? ChargesIncomeAccountId_Tags { get; set; }
  public string? ChargesIncomeAccountId_Caption { get; set; }
  public string? ChargesAccrualAccountId_AccountType { get; set; }
  public string? ChargesAccrualAccountId_UOM { get; set; }
  public string? ChargesAccrualAccountId_CurrencyId { get; set; }
  public string? ChargesAccrualAccountId_Code { get; set; }
  public string? ChargesAccrualAccountId_Name { get; set; }
  public string? ChargesAccrualAccountId_ParentId { get; set; }
  public decimal? ChargesAccrualAccountId_ClearedBalance { get; set; }
  public decimal? ChargesAccrualAccountId_UnclearedBalance { get; set; }
  public decimal? ChargesAccrualAccountId_LedgerBalance { get; set; }
  public decimal? ChargesAccrualAccountId_AvailableBalance { get; set; }
  public bool? ChargesAccrualAccountId_IsOfficeAccount { get; set; }
  public bool? ChargesAccrualAccountId_AllowManualEntry { get; set; }
  public bool? ChargesAccrualAccountId_IsClosed { get; set; }
  public DateTime? ChargesAccrualAccountId_DateClosed { get; set; }
  public string? ChargesAccrualAccountId_ClosedByUserName { get; set; }
  public bool? ChargesAccrualAccountId_IsActive { get; set; }
  public string? ChargesAccrualAccountId_CreatedByUserId { get; set; }
  public string? ChargesAccrualAccountId_UpdatedByUserId { get; set; }
  public string? ChargesAccrualAccountId_DeletedByUserId { get; set; }
  public bool? ChargesAccrualAccountId_IsDeleted { get; set; }
  public string? ChargesAccrualAccountId_Tags { get; set; }
  public string? ChargesAccrualAccountId_Caption { get; set; }
  public string? ChargesWaivedAccountId_AccountType { get; set; }
  public string? ChargesWaivedAccountId_UOM { get; set; }
  public string? ChargesWaivedAccountId_CurrencyId { get; set; }
  public string? ChargesWaivedAccountId_Code { get; set; }
  public string? ChargesWaivedAccountId_Name { get; set; }
  public string? ChargesWaivedAccountId_ParentId { get; set; }
  public decimal? ChargesWaivedAccountId_ClearedBalance { get; set; }
  public decimal? ChargesWaivedAccountId_UnclearedBalance { get; set; }
  public decimal? ChargesWaivedAccountId_LedgerBalance { get; set; }
  public decimal? ChargesWaivedAccountId_AvailableBalance { get; set; }
  public bool? ChargesWaivedAccountId_IsOfficeAccount { get; set; }
  public bool? ChargesWaivedAccountId_AllowManualEntry { get; set; }
  public bool? ChargesWaivedAccountId_IsClosed { get; set; }
  public DateTime? ChargesWaivedAccountId_DateClosed { get; set; }
  public string? ChargesWaivedAccountId_ClosedByUserName { get; set; }
  public bool? ChargesWaivedAccountId_IsActive { get; set; }
  public string? ChargesWaivedAccountId_CreatedByUserId { get; set; }
  public string? ChargesWaivedAccountId_UpdatedByUserId { get; set; }
  public string? ChargesWaivedAccountId_DeletedByUserId { get; set; }
  public bool? ChargesWaivedAccountId_IsDeleted { get; set; }
  public string? ChargesWaivedAccountId_Tags { get; set; }
  public string? ChargesWaivedAccountId_Caption { get; set; }
  public string? PublishedByUserId_AdObjectId { get; set; }
  public bool? PublishedByUserId_IsAdmin { get; set; }
  public string? PublishedByUserId_SecondaryPhone { get; set; }
  public bool? PublishedByUserId_SecondaryPhoneConfirmed { get; set; }
  public string? PublishedByUserId_UserName { get; set; }
  public string? PublishedByUserId_NormalizedUserName { get; set; }
  public string? PublishedByUserId_Email { get; set; }
  public string? PublishedByUserId_NormalizedEmail { get; set; }
  public bool? PublishedByUserId_EmailConfirmed { get; set; }
  public string? PublishedByUserId_PasswordHash { get; set; }
  public string? PublishedByUserId_SecurityStamp { get; set; }
  public string? PublishedByUserId_ConcurrencyStamp { get; set; }
  public string? PublishedByUserId_PhoneNumber { get; set; }
  public bool? PublishedByUserId_PhoneNumberConfirmed { get; set; }
  public bool? PublishedByUserId_TwoFactorEnabled { get; set; }
  public DateTimeOffset? PublishedByUserId_LockoutEnd { get; set; }
  public bool? PublishedByUserId_LockoutEnabled { get; set; }
  public int? PublishedByUserId_AccessFailedCount { get; set; }
}