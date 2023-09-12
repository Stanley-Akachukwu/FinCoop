namespace AP.ChevronCoop.Entities.Loans.LoanAccounts;

public class LoanAccountMasterView
{
    public long? RowNumber { get; set; }
    public string Id { get; set; }
    public string AccountNo { get; set; }
    public string LoanApplicationId { get; set; }
    public string CustomerId { get; set; }
    public string PrincipalBalanceAccountId { get; set; }
    public string PrincipalLossAccountId { get; set; }
    public string EarnedInterestAccountId { get; set; }
    public string InterestBalanceAccountId { get; set; }
    public string UnearnedInterestAccountId { get; set; }
    public string InterestLossAccountId { get; set; }
    public string InterestWaivedAccountId { get; set; }
    public string ChargesAccruedAccountId { get; set; }
    public string ChargesIncomeAccountId { get; set; }
    public string ChargesWaivedAccountId { get; set; }
    public decimal Principal { get; set; }
    public string TenureUnit { get; set; }
    public decimal TenureValue { get; set; }
    public DateTimeOffset RepaymentCommencementDate { get; set; }
    public bool UseSpecialDeposit { get; set; }
    public string? SpecialDepositAccountId { get; set; }
    public string? DestinationAccountId { get; set; }
    public bool IsClosed { get; set; }
    public DateTime? DateClosed { get; set; }
    public string? ClosedByUserId { get; set; }
    public string? LoanTopupId { get; set; }
    public string InterestPayoutAccountId { get; set; }
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
    public string LoanCreationType { get; set; }
    public string? ParentAccountId { get; set; }
    public string? RootParentAccountId { get; set; }
    public string? LoanApplicationId_ApplicationNo { get; set; }
    public string? LoanApplicationId_AccountNo { get; set; }
    public string? LoanApplicationId_LoanProductId { get; set; }
    public string? LoanApplicationId_ApprovalId { get; set; }
    public string? LoanApplicationId_CustomerId { get; set; }
    public decimal? LoanApplicationId_Principal { get; set; }
    public string? LoanApplicationId_TenureUnit { get; set; }
    public decimal? LoanApplicationId_TenureValue { get; set; }
    public DateTimeOffset? LoanApplicationId_RepaymentCommencementDate { get; set; }
    public bool? LoanApplicationId_UseSpecialDeposit { get; set; }
    public string? LoanApplicationId_SpecialDepositId { get; set; }
    public string? LoanApplicationId_CustomerDisbursementAccountId { get; set; }
    public string? LoanApplicationId_QualificationTargetProductId { get; set; }
    public string? LoanApplicationId_Status { get; set; }
    public string? LoanApplicationId_QualificationTargetProductType { get; set; }
    public bool? LoanApplicationId_IsActive { get; set; }
    public string? LoanApplicationId_CreatedByUserId { get; set; }
    public string? LoanApplicationId_UpdatedByUserId { get; set; }
    public string? LoanApplicationId_DeletedByUserId { get; set; }
    public bool? LoanApplicationId_IsDeleted { get; set; }
    public string? LoanApplicationId_Tags { get; set; }
    public string? LoanApplicationId_Caption { get; set; }
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
    public string? PrincipalBalanceAccountId_AccountType { get; set; }
    public string? PrincipalBalanceAccountId_UOM { get; set; }
    public string? PrincipalBalanceAccountId_CurrencyId { get; set; }
    public string? PrincipalBalanceAccountId_Code { get; set; }
    public string? PrincipalBalanceAccountId_Name { get; set; }
    public string? PrincipalBalanceAccountId_ParentId { get; set; }
    public decimal? PrincipalBalanceAccountId_ClearedBalance { get; set; }
    public decimal? PrincipalBalanceAccountId_UnclearedBalance { get; set; }
    public decimal PrincipalBalanceAccountId_LedgerBalance { get; set; }
    public decimal PrincipalBalanceAccountId_AvailableBalance { get; set; }
    public bool? PrincipalBalanceAccountId_IsOfficeAccount { get; set; }
    public bool? PrincipalBalanceAccountId_AllowManualEntry { get; set; }
    public bool? PrincipalBalanceAccountId_IsClosed { get; set; }
    public DateTime? PrincipalBalanceAccountId_DateClosed { get; set; }
    public string? PrincipalBalanceAccountId_ClosedByUserName { get; set; }
    public bool? PrincipalBalanceAccountId_IsActive { get; set; }
    public string? PrincipalBalanceAccountId_CreatedByUserId { get; set; }
    public string? PrincipalBalanceAccountId_UpdatedByUserId { get; set; }
    public string? PrincipalBalanceAccountId_DeletedByUserId { get; set; }
    public bool? PrincipalBalanceAccountId_IsDeleted { get; set; }
    public string? PrincipalBalanceAccountId_Tags { get; set; }
    public string? PrincipalBalanceAccountId_Caption { get; set; }
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
    public string? EarnedInterestAccountId_AccountType { get; set; }
    public string? EarnedInterestAccountId_UOM { get; set; }
    public string? EarnedInterestAccountId_CurrencyId { get; set; }
    public string? EarnedInterestAccountId_Code { get; set; }
    public string? EarnedInterestAccountId_Name { get; set; }
    public string? EarnedInterestAccountId_ParentId { get; set; }
    public decimal? EarnedInterestAccountId_ClearedBalance { get; set; }
    public decimal? EarnedInterestAccountId_UnclearedBalance { get; set; }
    public decimal EarnedInterestAccountId_LedgerBalance { get; set; }
    public decimal? EarnedInterestAccountId_AvailableBalance { get; set; }
    public bool? EarnedInterestAccountId_IsOfficeAccount { get; set; }
    public bool? EarnedInterestAccountId_AllowManualEntry { get; set; }
    public bool? EarnedInterestAccountId_IsClosed { get; set; }
    public DateTime? EarnedInterestAccountId_DateClosed { get; set; }
    public string? EarnedInterestAccountId_ClosedByUserName { get; set; }
    public bool? EarnedInterestAccountId_IsActive { get; set; }
    public string? EarnedInterestAccountId_CreatedByUserId { get; set; }
    public string? EarnedInterestAccountId_UpdatedByUserId { get; set; }
    public string? EarnedInterestAccountId_DeletedByUserId { get; set; }
    public bool? EarnedInterestAccountId_IsDeleted { get; set; }
    public string? EarnedInterestAccountId_Tags { get; set; }
    public string? EarnedInterestAccountId_Caption { get; set; }
    public string? InterestBalanceAccountId_AccountType { get; set; }
    public string? InterestBalanceAccountId_UOM { get; set; }
    public string? InterestBalanceAccountId_CurrencyId { get; set; }
    public string? InterestBalanceAccountId_Code { get; set; }
    public string? InterestBalanceAccountId_Name { get; set; }
    public string? InterestBalanceAccountId_ParentId { get; set; }
    public decimal? InterestBalanceAccountId_ClearedBalance { get; set; }
    public decimal? InterestBalanceAccountId_UnclearedBalance { get; set; }
    public decimal? InterestBalanceAccountId_LedgerBalance { get; set; }
    public decimal? InterestBalanceAccountId_AvailableBalance { get; set; }
    public bool? InterestBalanceAccountId_IsOfficeAccount { get; set; }
    public bool? InterestBalanceAccountId_AllowManualEntry { get; set; }
    public bool? InterestBalanceAccountId_IsClosed { get; set; }
    public DateTime? InterestBalanceAccountId_DateClosed { get; set; }
    public string? InterestBalanceAccountId_ClosedByUserName { get; set; }
    public bool? InterestBalanceAccountId_IsActive { get; set; }
    public string? InterestBalanceAccountId_CreatedByUserId { get; set; }
    public string? InterestBalanceAccountId_UpdatedByUserId { get; set; }
    public string? InterestBalanceAccountId_DeletedByUserId { get; set; }
    public bool? InterestBalanceAccountId_IsDeleted { get; set; }
    public string? InterestBalanceAccountId_Tags { get; set; }
    public string? InterestBalanceAccountId_Caption { get; set; }
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
    public string? SpecialDepositAccountId_ApplicationId { get; set; }
    public string? SpecialDepositAccountId_AccountNo { get; set; }
    public string? SpecialDepositAccountId_CustomerId { get; set; }
    public string? SpecialDepositAccountId_DepositProductId { get; set; }
    public string? SpecialDepositAccountId_DepositAccountId { get; set; }
    public string? SpecialDepositAccountId_ChargesAccruedAccountId { get; set; }
    public string? SpecialDepositAccountId_ChargesIncomeAccountId { get; set; }
    public string? SpecialDepositAccountId_ChargesWaivedAccountId { get; set; }
    public string? SpecialDepositAccountId_InterestEarnedAccountId { get; set; }
    public string? SpecialDepositAccountId_InterestPayoutAccountId { get; set; }
    public decimal? SpecialDepositAccountId_FundingAmount { get; set; }
    public decimal? SpecialDepositAccountId_InterestRate { get; set; }
    public DateTime? SpecialDepositAccountId_LastInterestComputationDate { get; set; }
    public decimal? SpecialDepositAccountId_MaximumBalanceLimit { get; set; }
    public decimal? SpecialDepositAccountId_MinimumBalanceLimit { get; set; }
    public decimal? SpecialDepositAccountId_SingleWithdrawalLimit { get; set; }
    public decimal? SpecialDepositAccountId_DailyWithdrawalLimit { get; set; }
    public decimal? SpecialDepositAccountId_WeeklyWithdrawalLimit { get; set; }
    public decimal? SpecialDepositAccountId_MonthlyWithdrawalLimit { get; set; }
    public bool? SpecialDepositAccountId_IsClosed { get; set; }
    public DateTime? SpecialDepositAccountId_DateClosed { get; set; }
    public string? SpecialDepositAccountId_ClosedByUserId { get; set; }
    public bool? SpecialDepositAccountId_IsActive { get; set; }
    public string? SpecialDepositAccountId_CreatedByUserId { get; set; }
    public string? SpecialDepositAccountId_UpdatedByUserId { get; set; }
    public string? SpecialDepositAccountId_DeletedByUserId { get; set; }
    public bool? SpecialDepositAccountId_IsDeleted { get; set; }
    public string? SpecialDepositAccountId_Tags { get; set; }
    public string? SpecialDepositAccountId_Caption { get; set; }
    public string? DestinationAccountId_LedgerAccountId { get; set; }
    public string? DestinationAccountId_CustomerId { get; set; }
    public string? DestinationAccountId_BankId { get; set; }
    public string? DestinationAccountId_AccountName { get; set; }
    public string? DestinationAccountId_AccountNumber { get; set; }
    public string? DestinationAccountId_BVN { get; set; }
    public string? DestinationAccountId_Branch { get; set; }
    public bool? DestinationAccountId_IsActive { get; set; }
    public string? DestinationAccountId_CreatedByUserId { get; set; }
    public string? DestinationAccountId_UpdatedByUserId { get; set; }
    public string? DestinationAccountId_DeletedByUserId { get; set; }
    public bool? DestinationAccountId_IsDeleted { get; set; }
    public string? DestinationAccountId_Tags { get; set; }
    public string? DestinationAccountId_Caption { get; set; }
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
    public string? LoanTopupId_LoanAccountId { get; set; }
    public decimal? LoanTopupId_TopupAmount { get; set; }
    public string? LoanTopupId_DestinationType { get; set; }
    public string? LoanTopupId_SpecialDepositAccountId { get; set; }
    public string? LoanTopupId_CustomerBankAccountId { get; set; }
    public decimal? LoanTopupId_OldPrincipalBalance { get; set; }
    public decimal? LoanTopupId_NewPrincipalBalance { get; set; }
    public decimal? LoanTopupId_OldInterestBalance { get; set; }
    public decimal? LoanTopupId_NewInterestBalance { get; set; }
    public decimal? LoanTopupId_TotalTopupCharges { get; set; }
    public DateTimeOffset? LoanTopupId_TopupDate { get; set; }
    public DateTimeOffset? LoanTopupId_CommencementDate { get; set; }
    public string? LoanTopupId_TransactionJournalId { get; set; }
    public string? LoanTopupId_ApprovalId { get; set; }
    public bool? LoanTopupId_IsProcessed { get; set; }
    public DateTime? LoanTopupId_ProcessedDate { get; set; }
    public string? LoanTopupId_Status { get; set; }
    public bool? LoanTopupId_IsActive { get; set; }
    public string? LoanTopupId_CreatedByUserId { get; set; }
    public string? LoanTopupId_UpdatedByUserId { get; set; }
    public string? LoanTopupId_DeletedByUserId { get; set; }
    public bool? LoanTopupId_IsDeleted { get; set; }
    public string? LoanTopupId_Tags { get; set; }
    public string? LoanTopupId_Caption { get; set; }
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
    public string? ParentAccount_AccountNo { get; set; }
    public string? ParentAccount_LoanApplicationId { get; set; }
    public string? ParentAccount_CustomerId { get; set; }
    public string? ParentAccount_PrincipalBalanceAccountId { get; set; }
    public string? ParentAccount_PrincipalLossAccountId { get; set; }
    public string? ParentAccount_EarnedInterestAccountId { get; set; }
    public string? ParentAccount_InterestBalanceAccountId { get; set; }
    public string? ParentAccount_UnearnedInterestAccountId { get; set; }
    public string? ParentAccount_InterestLossAccountId { get; set; }
    public string? ParentAccount_InterestWaivedAccountId { get; set; }
    public string? ParentAccount_ChargesAccruedAccountId { get; set; }
    public string? ParentAccount_ChargesIncomeAccountId { get; set; }
    public string? ParentAccount_ChargesWaivedAccountId { get; set; }
    public decimal? ParentAccount_Principal { get; set; }
    public string? ParentAccount_TenureUnit { get; set; }
    public decimal? ParentAccount_TenureValue { get; set; }
    public DateTimeOffset? ParentAccount_RepaymentCommencementDate { get; set; }
    public bool? ParentAccount_UseSpecialDeposit { get; set; }
    public string? ParentAccount_SpecialDepositAccountId { get; set; }
    public string? ParentAccount_DestinationAccountId { get; set; }
    public bool? ParentAccount_IsClosed { get; set; }
    public DateTime? ParentAccount_DateClosed { get; set; }
    public string? ParentAccount_ClosedByUserId { get; set; }
    public string? ParentAccount_LoanTopupId { get; set; }
    public string? ParentAccount_InterestPayoutAccountId { get; set; }
    public bool? ParentAccount_IsActive { get; set; }
    public string? ParentAccount_CreatedByUserId { get; set; }
    public string? ParentAccount_UpdatedByUserId { get; set; }
    public string? ParentAccount_DeletedByUserId { get; set; }
    public bool? ParentAccount_IsDeleted { get; set; }
    public string? ParentAccount_Tags { get; set; }
    public string? ParentAccount_Caption { get; set; }
    public string? ParentAccount_LoanCreationType { get; set; }
    public string? ParentAccount_ParentAccountId { get; set; }
    public string? ParentAccount_RootParentAccountId { get; set; }
    public string? RootParentAccount_AccountNo { get; set; }
    public string? RootParentAccount_LoanApplicationId { get; set; }
    public string? RootParentAccount_CustomerId { get; set; }
    public string? RootParentAccount_PrincipalBalanceAccountId { get; set; }
    public string? RootParentAccount_PrincipalLossAccountId { get; set; }
    public string? RootParentAccount_EarnedInterestAccountId { get; set; }
    public string? RootParentAccount_InterestBalanceAccountId { get; set; }
    public string? RootParentAccount_UnearnedInterestAccountId { get; set; }
    public string? RootParentAccount_InterestLossAccountId { get; set; }
    public string? RootParentAccount_InterestWaivedAccountId { get; set; }
    public string? RootParentAccount_ChargesAccruedAccountId { get; set; }
    public string? RootParentAccount_ChargesIncomeAccountId { get; set; }
    public string? RootParentAccount_ChargesWaivedAccountId { get; set; }
    public decimal? RootParentAccount_Principal { get; set; }
    public string? RootParentAccount_TenureUnit { get; set; }
    public decimal? RootParentAccount_TenureValue { get; set; }
    public DateTimeOffset? RootParentAccount_RepaymentCommencementDate { get; set; }
    public bool? RootParentAccount_UseSpecialDeposit { get; set; }
    public string? RootParentAccount_SpecialDepositAccountId { get; set; }
    public string? RootParentAccount_DestinationAccountId { get; set; }
    public bool? RootParentAccount_IsClosed { get; set; }
    public DateTime? RootParentAccount_DateClosed { get; set; }
    public string? RootParentAccount_ClosedByUserId { get; set; }
    public string? RootParentAccount_LoanTopupId { get; set; }
    public string? RootParentAccount_InterestPayoutAccountId { get; set; }
    public bool? RootParentAccount_IsActive { get; set; }
    public string? RootParentAccount_CreatedByUserId { get; set; }
    public string? RootParentAccount_UpdatedByUserId { get; set; }
    public string? RootParentAccount_DeletedByUserId { get; set; }
    public bool? RootParentAccount_IsDeleted { get; set; }
    public string? RootParentAccount_Tags { get; set; }
    public string? RootParentAccount_Caption { get; set; }
    public string? RootParentAccount_LoanCreationType { get; set; }
    public string? RootParentAccount_ParentAccountId { get; set; }
    public string? RootParentAccount_RootParentAccountId { get; set; }
}