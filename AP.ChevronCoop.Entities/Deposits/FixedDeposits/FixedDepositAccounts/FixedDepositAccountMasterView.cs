namespace AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;

public class FixedDepositAccountMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string? ApplicationId { get; set; }
    public string AccountNo { get; set; }
    public string CustomerId { get; set; }
    public string DepositProductId { get; set; }
    public string DepositAccountId { get; set; }
    public string ChargesAccruedAccountId { get; set; }
    public string ChargesIncomeAccountId { get; set; }
    public string InterestEarnedAccountId { get; set; }
    public string InterestPayoutAccountId { get; set; }
    public string ChargesWaivedAccountId { get; set; }
    public decimal Amount { get; set; }
    public string TenureUnit { get; set; }
    public decimal TenureValue { get; set; }
    public decimal InterestRate { get; set; }
    public string MaturityInstructionType { get; set; }
    public string LiquidationAccountType { get; set; }
    public string? SavingsLiquidationAccountId { get; set; }
    public string? SpecialDepositLiquidationAccountId { get; set; }
    public string? CustomerBankLiquidationAccountId { get; set; }
    public DateTime? LastInterestComputationDate { get; set; }
    public bool HasMature { get; set; }
    public bool IsClosed { get; set; }
    public DateTime? DateClosed { get; set; }
    public string? ClosedByUserId { get; set; }
    public decimal MaximumBalanceLimit { get; set; }
    public decimal MinimumBalanceLimit { get; set; }
    public decimal SingleWithdrawalLimit { get; set; }
    public decimal DailyWithdrawalLimit { get; set; }
    public decimal WeeklyWithdrawalLimit { get; set; }
    public decimal MonthlyWithdrawalLimit { get; set; }
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
    public string Caption => $"{AccountNo} ({CustomerId_LastName}, {CustomerId_FirstName})";
    public string? ParentAccountId { get; set; }
    public string? RootParentAccountId { get; set; }
    public string? ApplicationId_ApplicationNo { get; set; }
    public string? ApplicationId_CustomerId { get; set; }
    public string? ApplicationId_DepositProductId { get; set; }
    public decimal? ApplicationId_Amount { get; set; }
    public string? ApplicationId_TenureUnit { get; set; }
    public decimal? ApplicationId_TenureValue { get; set; }
    public decimal? ApplicationId_InterestRate { get; set; }
    public string? ApplicationId_MaturityInstructionType { get; set; }
    public string? ApplicationId_LiquidationAccountType { get; set; }
    public string? ApplicationId_SavingsLiquidationAccountId { get; set; }
    public string? ApplicationId_SpecialDepositLiquidationAccountId { get; set; }
    public string? ApplicationId_CustomerBankLiquidationAccountId { get; set; }
    public string? ApplicationId_ModeOfPayment { get; set; }
    public string? ApplicationId_SpecialDepositFundingSourceAccountId { get; set; }
    public string? ApplicationId_CustomerBankFundingSourceAccountId { get; set; }
    public string? ApplicationId_PaymentDocumentId { get; set; }
    public string? ApplicationId_ApprovalId { get; set; }
    public bool? ApplicationId_IsActive { get; set; }
    public string? ApplicationId_CreatedByUserId { get; set; }
    public string? ApplicationId_UpdatedByUserId { get; set; }
    public string? ApplicationId_DeletedByUserId { get; set; }
    public bool? ApplicationId_IsDeleted { get; set; }
    public string? ApplicationId_Tags { get; set; }
    public string? ApplicationId_Caption { get; set; }
    public string? CustomerId_CustomerNo { get; set; }
    public string? CustomerId_ApplicationUserId { get; set; }
    public string? CustomerId_CashAccountId { get; set; }
    public int? CustomerId_YearsOfService { get; set; }
    public bool? CustomerId_IsKycStarted { get; set; }
    public bool? CustomerId_IsKycCompleted { get; set; }
    public DateTime? CustomerId_KycStartDate { get; set; }
    public DateTime? CustomerId_KycCompletedDate { get; set; }
    public string? CustomerId_Status { get; set; }
    public string? CustomerId_MemberType { get; set; }
    public string? CustomerId_Gender { get; set; }
    public string? CustomerId_ProfileImageUrl { get; set; }
    public string? CustomerId_PassportUrl { get; set; }
    public string? CustomerId_IdentificationType { get; set; }
    public string? CustomerId_IdentificationNumber { get; set; }
    public string? CustomerId_IdentificationUrl { get; set; }
    public bool? CustomerId_KycSubmitted { get; set; }
    public DateTime? CustomerId_KycSubmittedOn { get; set; }
    public bool? CustomerId_KycApproved { get; set; }
    public DateTime? CustomerId_KycApprovedOn { get; set; }
    public bool? CustomerId_KycApprovedBy { get; set; }
    public string? CustomerId_FirstName { get; set; }
    public string? CustomerId_LastName { get; set; }
    public string? CustomerId_MiddleName { get; set; }
    public string? CustomerId_DepartmentId { get; set; }
    public string? CustomerId_MemberId { get; set; }
    public string? CustomerId_CAI { get; set; }
    public string? CustomerId_RetireeNumber { get; set; }
    public string? CustomerId_StateOfOrigin { get; set; }
    public string? CustomerId_PrimaryEmail { get; set; }
    public string? CustomerId_SecondaryEmail { get; set; }
    public string? CustomerId_PrimaryPhone { get; set; }
    public string? CustomerId_SecondaryPhone { get; set; }
    public string? CustomerId_ResidentialAddress { get; set; }
    public string? CustomerId_OfficeAddress { get; set; }
    public string? CustomerId_Rank { get; set; }
    public string? CustomerId_JobRole { get; set; }
    public DateTimeOffset? CustomerId_DOB { get; set; }
    public string? CustomerId_Address { get; set; }
    public string? CustomerId_Country { get; set; }
    public string? CustomerId_State { get; set; }
    public bool? CustomerId_IsActive { get; set; }
    public string? CustomerId_CreatedByUserId { get; set; }
    public string? CustomerId_UpdatedByUserId { get; set; }
    public string? CustomerId_DeletedByUserId { get; set; }
    public bool? CustomerId_IsDeleted { get; set; }
    public string? CustomerId_Tags { get; set; }
    public string? CustomerId_Caption { get; set; }
    public DateTime? CustomerId_DateOfEmployment { get; set; }
    public string? DepositProductId_Code { get; set; }
    public string? DepositProductId_Name { get; set; }
    public string? DepositProductId_ShortName { get; set; }
    public string? DepositProductId_ApprovalWorkflowId { get; set; }
    public string? DepositProductId_ApprovalId { get; set; }
    public int? DepositProductId_MinimumAge { get; set; }
    public int? DepositProductId_MaximumAge { get; set; }
    public string? DepositProductId_Tenure { get; set; }
    public decimal? DepositProductId_TenureValue { get; set; }
    public string? DepositProductId_Status { get; set; }
    public string? DepositProductId_PublicationType { get; set; }
    public string? DepositProductId_PublishedByUserId { get; set; }
    public string? DepositProductId_DefaultCurrencyId { get; set; }
    public string? DepositProductId_BankDepositAccountId { get; set; }
    public string? DepositProductId_ProductDepositAccountId { get; set; }
    public string? DepositProductId_ChargesIncomeAccountId { get; set; }
    public string? DepositProductId_ChargesAccrualAccountId { get; set; }
    public string? DepositProductId_ChargesWaivedAccountId { get; set; }
    public string? DepositProductId_InterestPayableAccountId { get; set; }
    public string? DepositProductId_InterestPayoutAccountId { get; set; }
    public bool? DepositProductId_IsInterestEnabled { get; set; }
    public decimal? DepositProductId_MinimumContributionRegular { get; set; }
    public decimal? DepositProductId_MinimumContributionRetiree { get; set; }
    public string? DepositProductId_ProductType { get; set; }
    public bool? DepositProductId_IsActive { get; set; }
    public string? DepositProductId_CreatedByUserId { get; set; }
    public string? DepositProductId_UpdatedByUserId { get; set; }
    public string? DepositProductId_DeletedByUserId { get; set; }
    public bool? DepositProductId_IsDeleted { get; set; }
    public string? DepositProductId_Tags { get; set; }
    public string? DepositProductId_Caption { get; set; }
    public bool? DepositProductId_IsDefaultProduct { get; set; }
    public string? DepositAccountId_AccountType { get; set; }
    public string? DepositAccountId_UOM { get; set; }
    public string? DepositAccountId_CurrencyId { get; set; }
    public string? DepositAccountId_Code { get; set; }
    public string? DepositAccountId_Name { get; set; }
    public string? DepositAccountId_ParentId { get; set; }
    public decimal? DepositAccountId_ClearedBalance { get; set; }
    public decimal? DepositAccountId_UnclearedBalance { get; set; }
    public decimal? DepositAccountId_LedgerBalance { get; set; }
    public decimal? DepositAccountId_AvailableBalance { get; set; }
    public bool? DepositAccountId_IsOfficeAccount { get; set; }
    public bool? DepositAccountId_AllowManualEntry { get; set; }
    public bool? DepositAccountId_IsClosed { get; set; }
    public DateTime? DepositAccountId_DateClosed { get; set; }
    public string? DepositAccountId_ClosedByUserName { get; set; }
    public bool? DepositAccountId_IsActive { get; set; }
    public string? DepositAccountId_CreatedByUserId { get; set; }
    public string? DepositAccountId_UpdatedByUserId { get; set; }
    public string? DepositAccountId_DeletedByUserId { get; set; }
    public bool? DepositAccountId_IsDeleted { get; set; }
    public string? DepositAccountId_Tags { get; set; }
    public string? DepositAccountId_Caption { get; set; }
    public string? ChargesAccruedAccountId_AccountType { get; set; }
    public string? ChargesAccruedAccountId_UOM { get; set; }
    public string? ChargesAccruedAccountId_CurrencyId { get; set; }
    public string? ChargesAccruedAccountId_Code { get; set; }
    public string? ChargesAccruedAccountId_Name { get; set; }
    public string? ChargesAccruedAccountId_ParentId { get; set; }
    public decimal? ChargesAccruedAccountId_ClearedBalance { get; set; }
    public decimal? ChargesAccruedAccountId_UnclearedBalance { get; set; }
    public decimal? ChargesAccruedAccountId_LedgerBalance { get; set; }
    public decimal? ChargesAccruedAccountId_AvailableBalance { get; set; }
    public bool? ChargesAccruedAccountId_IsOfficeAccount { get; set; }
    public bool? ChargesAccruedAccountId_AllowManualEntry { get; set; }
    public bool? ChargesAccruedAccountId_IsClosed { get; set; }
    public DateTime? ChargesAccruedAccountId_DateClosed { get; set; }
    public string? ChargesAccruedAccountId_ClosedByUserName { get; set; }
    public bool? ChargesAccruedAccountId_IsActive { get; set; }
    public string? ChargesAccruedAccountId_CreatedByUserId { get; set; }
    public string? ChargesAccruedAccountId_UpdatedByUserId { get; set; }
    public string? ChargesAccruedAccountId_DeletedByUserId { get; set; }
    public bool? ChargesAccruedAccountId_IsDeleted { get; set; }
    public string? ChargesAccruedAccountId_Tags { get; set; }
    public string? ChargesAccruedAccountId_Caption { get; set; }
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
    public string? InterestEarnedAccountId_AccountType { get; set; }
    public string? InterestEarnedAccountId_UOM { get; set; }
    public string? InterestEarnedAccountId_CurrencyId { get; set; }
    public string? InterestEarnedAccountId_Code { get; set; }
    public string? InterestEarnedAccountId_Name { get; set; }
    public string? InterestEarnedAccountId_ParentId { get; set; }
    public decimal? InterestEarnedAccountId_ClearedBalance { get; set; }
    public decimal? InterestEarnedAccountId_UnclearedBalance { get; set; }
    public decimal? InterestEarnedAccountId_LedgerBalance { get; set; }
    public decimal? InterestEarnedAccountId_AvailableBalance { get; set; }
    public bool? InterestEarnedAccountId_IsOfficeAccount { get; set; }
    public bool? InterestEarnedAccountId_AllowManualEntry { get; set; }
    public bool? InterestEarnedAccountId_IsClosed { get; set; }
    public DateTime? InterestEarnedAccountId_DateClosed { get; set; }
    public string? InterestEarnedAccountId_ClosedByUserName { get; set; }
    public bool? InterestEarnedAccountId_IsActive { get; set; }
    public string? InterestEarnedAccountId_CreatedByUserId { get; set; }
    public string? InterestEarnedAccountId_UpdatedByUserId { get; set; }
    public string? InterestEarnedAccountId_DeletedByUserId { get; set; }
    public bool? InterestEarnedAccountId_IsDeleted { get; set; }
    public string? InterestEarnedAccountId_Tags { get; set; }
    public string? InterestEarnedAccountId_Caption { get; set; }
    public string? InterestPayoutAccountId_AccountType { get; set; }
    public string? InterestPayoutAccountId_UOM { get; set; }
    public string? InterestPayoutAccountId_CurrencyId { get; set; }
    public string? InterestPayoutAccountId_Code { get; set; }
    public string? InterestPayoutAccountId_Name { get; set; }
    public string? InterestPayoutAccountId_ParentId { get; set; }
    public decimal? InterestPayoutAccountId_ClearedBalance { get; set; }
    public decimal? InterestPayoutAccountId_UnclearedBalance { get; set; }
    public decimal? InterestPayoutAccountId_LedgerBalance { get; set; }
    public decimal? InterestPayoutAccountId_AvailableBalance { get; set; }
    public bool? InterestPayoutAccountId_IsOfficeAccount { get; set; }
    public bool? InterestPayoutAccountId_AllowManualEntry { get; set; }
    public bool? InterestPayoutAccountId_IsClosed { get; set; }
    public DateTime? InterestPayoutAccountId_DateClosed { get; set; }
    public string? InterestPayoutAccountId_ClosedByUserName { get; set; }
    public bool? InterestPayoutAccountId_IsActive { get; set; }
    public string? InterestPayoutAccountId_CreatedByUserId { get; set; }
    public string? InterestPayoutAccountId_UpdatedByUserId { get; set; }
    public string? InterestPayoutAccountId_DeletedByUserId { get; set; }
    public bool? InterestPayoutAccountId_IsDeleted { get; set; }
    public string? InterestPayoutAccountId_Tags { get; set; }
    public string? InterestPayoutAccountId_Caption { get; set; }
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
    public string? SavingsLiquidationAccountId_ApplicationId { get; set; }
    public string? SavingsLiquidationAccountId_AccountNo { get; set; }
    public string? SavingsLiquidationAccountId_CustomerId { get; set; }
    public string? SavingsLiquidationAccountId_DepositProductId { get; set; }
    public string? SavingsLiquidationAccountId_LedgerDepositAccountId { get; set; }
    public string? SavingsLiquidationAccountId_ChargesPayableAccountId { get; set; }
    public string? SavingsLiquidationAccountId_ChargesAccruedAccountId { get; set; }
    public string? SavingsLiquidationAccountId_ChargesWaivedAccountId { get; set; }
    public string? SavingsLiquidationAccountId_ChargesIncomeAccountId { get; set; }
    public decimal? SavingsLiquidationAccountId_PayrollAmount { get; set; }
    public bool? SavingsLiquidationAccountId_IsClosed { get; set; }
    public DateTime? SavingsLiquidationAccountId_DateClosed { get; set; }
    public string? SavingsLiquidationAccountId_ClosedByUserId { get; set; }
    public decimal? SavingsLiquidationAccountId_MaximumBalanceLimit { get; set; }
    public decimal? SavingsLiquidationAccountId_MinimumBalanceLimit { get; set; }
    public decimal? SavingsLiquidationAccountId_SingleWithdrawalLimit { get; set; }
    public decimal? SavingsLiquidationAccountId_DailyWithdrawalLimit { get; set; }
    public decimal? SavingsLiquidationAccountId_WeeklyWithdrawalLimit { get; set; }
    public decimal? SavingsLiquidationAccountId_MonthlyWithdrawalLimit { get; set; }
    public bool? SavingsLiquidationAccountId_IsActive { get; set; }
    public string? SavingsLiquidationAccountId_CreatedByUserId { get; set; }
    public string? SavingsLiquidationAccountId_UpdatedByUserId { get; set; }
    public string? SavingsLiquidationAccountId_DeletedByUserId { get; set; }
    public bool? SavingsLiquidationAccountId_IsDeleted { get; set; }
    public string? SavingsLiquidationAccountId_Tags { get; set; }
    public string? SavingsLiquidationAccountId_Caption { get; set; }
    public string? SpecialDepositLiquidationAccountId_ApplicationId { get; set; }
    public string? SpecialDepositLiquidationAccountId_AccountNo { get; set; }
    public string? SpecialDepositLiquidationAccountId_CustomerId { get; set; }
    public string? SpecialDepositLiquidationAccountId_DepositProductId { get; set; }
    public string? SpecialDepositLiquidationAccountId_DepositAccountId { get; set; }
    public string? SpecialDepositLiquidationAccountId_ChargesAccruedAccountId { get; set; }
    public string? SpecialDepositLiquidationAccountId_ChargesIncomeAccountId { get; set; }
    public string? SpecialDepositLiquidationAccountId_ChargesWaivedAccountId { get; set; }
    public string? SpecialDepositLiquidationAccountId_InterestEarnedAccountId { get; set; }
    public string? SpecialDepositLiquidationAccountId_InterestPayoutAccountId { get; set; }
    public decimal? SpecialDepositLiquidationAccountId_FundingAmount { get; set; }
    public decimal? SpecialDepositLiquidationAccountId_InterestRate { get; set; }
    public DateTime? SpecialDepositLiquidationAccountId_LastInterestComputationDate { get; set; }
    public decimal? SpecialDepositLiquidationAccountId_MaximumBalanceLimit { get; set; }
    public decimal? SpecialDepositLiquidationAccountId_MinimumBalanceLimit { get; set; }
    public decimal? SpecialDepositLiquidationAccountId_SingleWithdrawalLimit { get; set; }
    public decimal? SpecialDepositLiquidationAccountId_DailyWithdrawalLimit { get; set; }
    public decimal? SpecialDepositLiquidationAccountId_WeeklyWithdrawalLimit { get; set; }
    public decimal? SpecialDepositLiquidationAccountId_MonthlyWithdrawalLimit { get; set; }
    public bool? SpecialDepositLiquidationAccountId_IsClosed { get; set; }
    public DateTime? SpecialDepositLiquidationAccountId_DateClosed { get; set; }
    public string? SpecialDepositLiquidationAccountId_ClosedByUserId { get; set; }
    public bool? SpecialDepositLiquidationAccountId_IsActive { get; set; }
    public string? SpecialDepositLiquidationAccountId_CreatedByUserId { get; set; }
    public string? SpecialDepositLiquidationAccountId_UpdatedByUserId { get; set; }
    public string? SpecialDepositLiquidationAccountId_DeletedByUserId { get; set; }
    public bool? SpecialDepositLiquidationAccountId_IsDeleted { get; set; }
    public string? SpecialDepositLiquidationAccountId_Tags { get; set; }
    public string? SpecialDepositLiquidationAccountId_Caption { get; set; }
    public string? CustomerBankLiquidationAccountId_LedgerAccountId { get; set; }
    public string? CustomerBankLiquidationAccountId_CustomerId { get; set; }
    public string? CustomerBankLiquidationAccountId_BankId { get; set; }
    public string? CustomerBankLiquidationAccountId_AccountName { get; set; }
    public string? CustomerBankLiquidationAccountId_AccountNumber { get; set; }
    public string? CustomerBankLiquidationAccountId_BVN { get; set; }
    public string? CustomerBankLiquidationAccountId_Branch { get; set; }
    public bool? CustomerBankLiquidationAccountId_IsActive { get; set; }
    public string? CustomerBankLiquidationAccountId_CreatedByUserId { get; set; }
    public string? CustomerBankLiquidationAccountId_UpdatedByUserId { get; set; }
    public string? CustomerBankLiquidationAccountId_DeletedByUserId { get; set; }
    public bool? CustomerBankLiquidationAccountId_IsDeleted { get; set; }
    public string? CustomerBankLiquidationAccountId_Tags { get; set; }
    public string? CustomerBankLiquidationAccountId_Caption { get; set; }
    public string? ClosedByUserId_AdObjectId { get; set; }
    public bool? ClosedByUserId_IsAdmin { get; set; }
    public string? ClosedByUserId_SecondaryPhone { get; set; }
    public bool? ClosedByUserId_SecondaryPhoneConfirmed { get; set; }
    public string? ClosedByUserId_UserName { get; set; }
    public string? ClosedByUserId_NormalizedUserName { get; set; }
    public string? ClosedByUserId_Email { get; set; }
    public string? ClosedByUserId_NormalizedEmail { get; set; }
    public bool? ClosedByUserId_EmailConfirmed { get; set; }
    public string? ClosedByUserId_PasswordHash { get; set; }
    public string? ClosedByUserId_SecurityStamp { get; set; }
    public string? ClosedByUserId_ConcurrencyStamp { get; set; }
    public string? ClosedByUserId_PhoneNumber { get; set; }
    public bool? ClosedByUserId_PhoneNumberConfirmed { get; set; }
    public bool? ClosedByUserId_TwoFactorEnabled { get; set; }
    public DateTimeOffset? ClosedByUserId_LockoutEnd { get; set; }
    public bool? ClosedByUserId_LockoutEnabled { get; set; }
    public int? ClosedByUserId_AccessFailedCount { get; set; }
    public string? ParentAccount_ApplicationId { get; set; }
    public string? ParentAccount_AccountNo { get; set; }
    public string? ParentAccount_CustomerId { get; set; }
    public string? ParentAccount_DepositProductId { get; set; }
    public string? ParentAccount_DepositAccountId { get; set; }
    public string? ParentAccount_ChargesAccruedAccountId { get; set; }
    public string? ParentAccount_ChargesIncomeAccountId { get; set; }
    public string? ParentAccount_InterestEarnedAccountId { get; set; }
    public string? ParentAccount_InterestPayoutAccountId { get; set; }
    public string? ParentAccount_ChargesWaivedAccountId { get; set; }
    public decimal? ParentAccount_Amount { get; set; }
    public string? ParentAccount_TenureUnit { get; set; }
    public decimal? ParentAccount_TenureValue { get; set; }
    public decimal? ParentAccount_InterestRate { get; set; }
    public string? ParentAccount_MaturityInstructionType { get; set; }
    public string? ParentAccount_LiquidationAccountType { get; set; }
    public string? ParentAccount_SavingsLiquidationAccountId { get; set; }
    public string? ParentAccount_SpecialDepositLiquidationAccountId { get; set; }
    public string? ParentAccount_CustomerBankLiquidationAccountId { get; set; }
    public DateTime? ParentAccount_LastInterestComputationDate { get; set; }
    public bool? ParentAccount_HasMature { get; set; }
    public bool? ParentAccount_IsClosed { get; set; }
    public DateTime? ParentAccount_DateClosed { get; set; }
    public string? ParentAccount_ClosedByUserId { get; set; }
    public decimal? ParentAccount_MaximumBalanceLimit { get; set; }
    public decimal? ParentAccount_MinimumBalanceLimit { get; set; }
    public decimal? ParentAccount_SingleWithdrawalLimit { get; set; }
    public decimal? ParentAccount_DailyWithdrawalLimit { get; set; }
    public decimal? ParentAccount_WeeklyWithdrawalLimit { get; set; }
    public decimal? ParentAccount_MonthlyWithdrawalLimit { get; set; }
    public bool? ParentAccount_IsActive { get; set; }
    public string? ParentAccount_CreatedByUserId { get; set; }
    public string? ParentAccount_UpdatedByUserId { get; set; }
    public string? ParentAccount_DeletedByUserId { get; set; }
    public bool? ParentAccount_IsDeleted { get; set; }
    public string? ParentAccount_Tags { get; set; }
    public string? ParentAccount_Caption { get; set; }
    public string? ParentAccount_ParentAccountId { get; set; }
    public string? ParentAccount_RootParentAccountId { get; set; }
    public string? RootParentAccount_ApplicationId { get; set; }
    public string? RootParentAccount_AccountNo { get; set; }
    public string? RootParentAccount_CustomerId { get; set; }
    public string? RootParentAccount_DepositProductId { get; set; }
    public string? RootParentAccount_DepositAccountId { get; set; }
    public string? RootParentAccount_ChargesAccruedAccountId { get; set; }
    public string? RootParentAccount_ChargesIncomeAccountId { get; set; }
    public string? RootParentAccount_InterestEarnedAccountId { get; set; }
    public string? RootParentAccount_InterestPayoutAccountId { get; set; }
    public string? RootParentAccount_ChargesWaivedAccountId { get; set; }
    public decimal? RootParentAccount_Amount { get; set; }
    public string? RootParentAccount_TenureUnit { get; set; }
    public decimal? RootParentAccount_TenureValue { get; set; }
    public decimal? RootParentAccount_InterestRate { get; set; }
    public string? RootParentAccount_MaturityInstructionType { get; set; }
    public string? RootParentAccount_LiquidationAccountType { get; set; }
    public string? RootParentAccount_SavingsLiquidationAccountId { get; set; }
    public string? RootParentAccount_SpecialDepositLiquidationAccountId { get; set; }
    public string? RootParentAccount_CustomerBankLiquidationAccountId { get; set; }
    public DateTime? RootParentAccount_LastInterestComputationDate { get; set; }
    public bool? RootParentAccount_HasMature { get; set; }
    public bool? RootParentAccount_IsClosed { get; set; }
    public DateTime? RootParentAccount_DateClosed { get; set; }
    public string? RootParentAccount_ClosedByUserId { get; set; }
    public decimal? RootParentAccount_MaximumBalanceLimit { get; set; }
    public decimal? RootParentAccount_MinimumBalanceLimit { get; set; }
    public decimal? RootParentAccount_SingleWithdrawalLimit { get; set; }
    public decimal? RootParentAccount_DailyWithdrawalLimit { get; set; }
    public decimal? RootParentAccount_WeeklyWithdrawalLimit { get; set; }
    public decimal? RootParentAccount_MonthlyWithdrawalLimit { get; set; }
    public bool? RootParentAccount_IsActive { get; set; }
    public string? RootParentAccount_CreatedByUserId { get; set; }
    public string? RootParentAccount_UpdatedByUserId { get; set; }
    public string? RootParentAccount_DeletedByUserId { get; set; }
    public bool? RootParentAccount_IsDeleted { get; set; }
    public string? RootParentAccount_Tags { get; set; }
    public string? RootParentAccount_Caption { get; set; }
    public string? RootParentAccount_ParentAccountId { get; set; }
    public string? RootParentAccount_RootParentAccountId { get; set; }
    public string Details => $"{AccountNo} ({CustomerId_LastName}, {CustomerId_FirstName})";
}