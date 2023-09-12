using AP.ChevronCoop.Entities.Accounting.AccountingPeriods;
using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Entities.Accounting.FinancialCalendars;
using AP.ChevronCoop.Entities.Accounting.JournalEntries;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.Accounting.LienTypes;
using AP.ChevronCoop.Entities.Accounting.PaymentModes;
using AP.ChevronCoop.Entities.Accounting.TransactionDocuments;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.CommonViews;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositApplications;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositChangeInMaturities;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositImmediateLiquidations;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestAdditions;
using AP.ChevronCoop.Entities.Deposits.Products.CustomerDepositProductPublications;
using AP.ChevronCoop.Entities.Deposits.Products.DepartmentDepositProductPublications;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProductCharges;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProductInterestRanges;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountDeductionSchedules;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsCashAdditions;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountDeductionSchedules;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositCashAdditions;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositFundTransfer;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestAdditions;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestSchedules;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositWithdrawals;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using AP.ChevronCoop.Entities.Documents.DocumentTypes;
using AP.ChevronCoop.Entities.Documents.OfficeDocuments;
using AP.ChevronCoop.Entities.Documents.OfficePhotos;
using AP.ChevronCoop.Entities.Documents.OfficeSheets;
using AP.ChevronCoop.Entities.Employees;
using AP.ChevronCoop.Entities.LoanOffsetTransactions;
using AP.ChevronCoop.Entities.Loans.CustomerLoanProductPublications;
using AP.ChevronCoop.Entities.Loans.DepartmentLoanProductPublications;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanApplicationApprovals;
using AP.ChevronCoop.Entities.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.Entities.Loans.LoanApplicationItems;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanApplicationSchedules;
using AP.ChevronCoop.Entities.Loans.LoanDisbursementCharges;
using AP.ChevronCoop.Entities.Loans.LoanDisbursements;
using AP.ChevronCoop.Entities.Loans.LoanOffSetCharges;
using AP.ChevronCoop.Entities.Loans.LoanOffSetTransactions;
using AP.ChevronCoop.Entities.Loans.LoanProductCharges;
using AP.ChevronCoop.Entities.Loans.LoanProductPublications.DepartmentLoanProductPublications;
using AP.ChevronCoop.Entities.Loans.LoanProductPublications.MemberLoanProductPublications;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using AP.ChevronCoop.Entities.Loans.LoanRepayment;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentCharges;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentSchedules;
using AP.ChevronCoop.Entities.Loans.LoanTopupCharges;
using AP.ChevronCoop.Entities.Loans.LoanTopupTransactions;
using AP.ChevronCoop.Entities.LoanTopupTransactions;
using AP.ChevronCoop.Entities.MasterData.Banks;
using AP.ChevronCoop.Entities.MasterData.Charges;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using AP.ChevronCoop.Entities.MasterData.Departments;
using AP.ChevronCoop.Entities.MasterData.GlobalCodes;
using AP.ChevronCoop.Entities.MasterData.Locations;
using AP.ChevronCoop.Entities.Payroll;
using AP.ChevronCoop.Entities.Payroll.PayrollCronJobConfigurations;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionScheduleItems;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionSchedules;
using AP.ChevronCoop.Entities.Security.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalDocuments;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupMembers;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;
using AP.ChevronCoop.Entities.Security.AuditTrails;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoleClaims;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserClaims;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserTokens;
using AP.ChevronCoop.Entities.Security.Auth.Permissions;
using AP.ChevronCoop.Entities.Security.MemberProfiles.EnrollmentPayments;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBankAccounts;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBeneficiaries;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberNextOfKins;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace ChevronCoop.API.Config;

public class ODataEdmBuilder
{
    public static IEdmModel GetAppCoreEdmModel()
    {
        var odataBuilder = new ODataConventionModelBuilder();

        //odataBuilder.EntitySet<Currency>("Currency2").EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<FinancialCalendar>(nameof(FinancialCalendar)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LienType>(nameof(LienType)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<TransactionJournal>(nameof(TransactionJournal)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<PaymentMode>(nameof(PaymentMode)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<Charge>(nameof(Charge)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LedgerAccount>(nameof(LedgerAccount)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<AccountingPeriod>(nameof(AccountingPeriod)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<TransactionDocument>(nameof(TransactionDocument)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<CompanyBankAccount>(nameof(CompanyBankAccount)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<JournalEntry>(nameof(JournalEntry)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<FinancialCalendarMasterView>(nameof(FinancialCalendarMasterView)).EntityType
          .HasKey(e => e.Id);
        odataBuilder.EntitySet<JournalEntryMasterView>(nameof(JournalEntryMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LedgerAccountMasterView>(nameof(LedgerAccountMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LienTypeMasterView>(nameof(LienTypeMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<ChargeMasterView>(nameof(ChargeMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<CompanyBankAccountMasterView>(nameof(CompanyBankAccountMasterView)).EntityType
          .HasKey(e => e.Id);
        odataBuilder.EntitySet<AccountingPeriodMasterView>(nameof(AccountingPeriodMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<PaymentModeMasterView>(nameof(PaymentModeMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<TransactionDocumentMasterView>(nameof(TransactionDocumentMasterView)).EntityType
          .HasKey(e => e.Id);
        odataBuilder.EntitySet<TransactionJournalMasterView>(nameof(TransactionJournalMasterView)).EntityType
          .HasKey(e => e.Id);
        odataBuilder.EntitySet<Customer>(nameof(Customer)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<CustomerMasterView>(nameof(CustomerMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<DocumentTypeMasterView>(nameof(DocumentTypeMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<OfficeDocumentMasterView>(nameof(OfficeDocumentMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<OfficePhotoMasterView>(nameof(OfficePhotoMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<OfficeSheetMasterView>(nameof(OfficeSheetMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<DocumentType>(nameof(DocumentType)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<OfficeDocument>(nameof(OfficeDocument)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<OfficePhoto>(nameof(OfficePhoto)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<OfficeSheet>(nameof(OfficeSheet)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<EmployeeMasterView>(nameof(EmployeeMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<Employee>(nameof(Employee)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<LocationMasterView>(nameof(LocationMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<BankMasterView>(nameof(BankMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<DepartmentMasterView>(nameof(DepartmentMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<CurrencyMasterView>(nameof(CurrencyMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<GlobalCodeMasterView>(nameof(GlobalCodeMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<Location>(nameof(Location)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<GlobalCode>(nameof(GlobalCode)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<Bank>(nameof(Bank)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<Currency>(nameof(Currency)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<Department>(nameof(Department)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<ApplicationUser>(nameof(ApplicationUser)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<ApplicationRole>(nameof(ApplicationRole)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<CustomerBankAccount>(nameof(CustomerBankAccount)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<EnrollmentPaymentInfo>(nameof(EnrollmentPaymentInfo)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<ApprovalDocument>(nameof(ApprovalDocument)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<Approval>(nameof(Approval)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<ApprovalRole>(nameof(ApprovalRole)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<ApplicationRoleClaim>(nameof(ApplicationRoleClaim)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<ApplicationUserClaim>(nameof(ApplicationUserClaim)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<MemberProfile>(nameof(MemberProfile)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<Permission>(nameof(Permission)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<ApplicationUserLogin>(nameof(ApplicationUserLogin)).EntityType.HasKey(e => e.UserId);
        odataBuilder.EntitySet<ApplicationUserRole>(nameof(ApplicationUserRole)).EntityType
          .HasKey(e => new { e.UserId, e.RoleId });
        odataBuilder.EntitySet<ApplicationUserToken>(nameof(ApplicationUserToken)).EntityType.HasKey(e => e.UserId);
        odataBuilder.EntitySet<ApplicationRoleMasterView>(nameof(ApplicationRoleMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<ApplicationRoleClaimMasterView>(nameof(ApplicationRoleClaimMasterView)).EntityType
          .HasKey(e => e.Id);
        odataBuilder.EntitySet<ApplicationUserMasterView>(nameof(ApplicationUserMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<ApplicationUserClaimMasterView>(nameof(ApplicationUserClaimMasterView)).EntityType
          .HasKey(e => e.Id);
        odataBuilder.EntitySet<ApplicationUserLoginMasterView>(nameof(ApplicationUserLoginMasterView)).EntityType
          .HasKey(e => e.UserId);
        odataBuilder.EntitySet<ApplicationUserRoleMasterView>(nameof(ApplicationUserRoleMasterView)).EntityType
          .HasKey(e => new { e.UserId, e.RoleId });
        odataBuilder.EntitySet<ApplicationUserTokenMasterView>(nameof(ApplicationUserTokenMasterView)).EntityType
          .HasKey(e => e.UserId);
        odataBuilder.EntitySet<ApprovalMasterView>(nameof(ApprovalMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<ApprovalDocumentMasterView>(nameof(ApprovalDocumentMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<ApprovalRoleMasterView>(nameof(ApprovalRoleMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<CustomerBankAccountMasterView>(nameof(CustomerBankAccountMasterView)).EntityType
          .HasKey(e => e.Id);
        odataBuilder.EntitySet<EnrollmentPaymentInfoMasterView>(nameof(EnrollmentPaymentInfoMasterView)).EntityType
          .HasKey(e => e.Id);
        odataBuilder.EntitySet<MemberProfileMasterView>(nameof(MemberProfileMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<PermissionMasterView>(nameof(PermissionMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<MemberNextOfKin>(nameof(MemberNextOfKin)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<MemberNextOfKinMasterView>(nameof(MemberNextOfKinMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<MemberBeneficiary>(nameof(MemberBeneficiary)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<MemberBeneficiaryMasterView>(nameof(MemberBeneficiaryMasterView)).EntityType
          .HasKey(e => e.Id);

        // odataBuilder.EntitySet<RetireeSwitch>(nameof(RetireeSwitch)).EntityType.HasKey(e => e.Id);
        // odataBuilder.EntitySet<RetireeSwitchMasterView>(nameof(RetireeSwitchMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<AuditTrail>(nameof(AuditTrail)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<AuditTrailMasterView>(nameof(AuditTrailMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<MemberBulkUploadSession>(nameof(MemberBulkUploadSession)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<MemberBulkUploadSessionMasterView>(nameof(MemberBulkUploadSessionMasterView)).EntityType
          .HasKey(e => e.MemberBulkUploadSessionId);
        odataBuilder.EntitySet<MemberProfileViaUploadMasterView>(nameof(MemberProfileViaUploadMasterView)).EntityType
          .HasKey(e => e.Id);




        // Deposit Products 
        odataBuilder.EntitySet<SpecialDepositIncreaseDecrease>(nameof(SpecialDepositIncreaseDecrease)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<SpecialDepositIncreaseDecreaseMasterView>(nameof(SpecialDepositIncreaseDecreaseMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<SpecialDepositAccountApplication>(nameof(SpecialDepositAccountApplication)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<SpecialDepositAccountApplicationMasterView>(nameof(SpecialDepositAccountApplicationMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<SpecialDepositAccountDeductionSchedule>(nameof(SpecialDepositAccountDeductionSchedule)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<SpecialDepositAccountDeductionScheduleMasterView>(nameof(SpecialDepositAccountDeductionScheduleMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<SpecialDepositAccount>(nameof(SpecialDepositAccount)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<SpecialDepositAccountMasterView>(nameof(SpecialDepositAccountMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<SpecialDepositCashAddition>(nameof(SpecialDepositCashAddition)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<SpecialDepositCashAdditionMasterView>(nameof(SpecialDepositCashAdditionMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<SpecialDepositFundTransfer>(nameof(SpecialDepositFundTransfer)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<SpecialDepositFundTransferMasterView>(nameof(SpecialDepositFundTransferMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<SpecialDepositInterestAddition>(nameof(SpecialDepositInterestAddition)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<SpecialDepositInterestAdditionMasterView>(nameof(SpecialDepositInterestAdditionMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<SpecialDepositInterestScheduleItem>(nameof(SpecialDepositInterestScheduleItem)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<SpecialDepositInterestScheduleItemMasterView>(nameof(SpecialDepositInterestScheduleItemMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<SpecialDepositInterestSchedule>(nameof(SpecialDepositInterestSchedule)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<SpecialDepositInterestScheduleMasterView>(nameof(SpecialDepositInterestScheduleMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<SpecialDepositWithdrawal>(nameof(SpecialDepositWithdrawal)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<SpecialDepositWithdrawalMasterView>(nameof(SpecialDepositWithdrawalMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<DepartmentDepositProductPublication>(nameof(DepartmentDepositProductPublication)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<DepartmentDepositProductPublicationMasterView>(nameof(DepartmentDepositProductPublicationMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<CustomerDepositProductPublication>(nameof(CustomerDepositProductPublication)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<CustomerDepositProductPublicationMasterView>(nameof(CustomerDepositProductPublicationMasterView)).EntityType.HasKey(e => e.Id);


        odataBuilder.EntitySet<DepositApplicationsMasterView>(nameof(DepositApplicationsMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<DepositAccountsMasterView>(nameof(DepositAccountsMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<FixedDepositActionsMasterView>(nameof(FixedDepositActionsMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<PayrollDeductionScheduleMasterView>(nameof(PayrollDeductionScheduleMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<PayrollDeductionScheduleItemMasterView>(nameof(PayrollDeductionScheduleItemMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<PayrollCronJobConfigMasterView>(nameof(PayrollCronJobConfigMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<PayrollDeductionItemMasterView>(nameof(PayrollDeductionItemMasterView)).EntityType.HasKey(e => e.Id);


        odataBuilder.EntitySet<DepositProduct>(nameof(DepositProduct)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<DepositProductMasterView>(nameof(DepositProductMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<DepositProductInterestRange>(nameof(DepositProductInterestRange)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<DepositProductInterestRangeMasterView>(nameof(DepositProductInterestRangeMasterView)).EntityType.HasKey(e => e.Id);


        odataBuilder.EntitySet<DepositProductCharge>(nameof(DepositProductCharge)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<DepositProductChargeMasterView>(nameof(DepositProductChargeMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<ApprovalGroup>(nameof(ApprovalGroup)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<ApprovalGroupMasterView>(nameof(ApprovalGroupMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<ApprovalGroupMember>(nameof(ApprovalGroupMember)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<ApprovalGroupMemberMasterView>(nameof(ApprovalGroupMemberMasterView)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<ApprovalWorkflowMasterView>(nameof(ApprovalWorkflowMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<ApprovalWorkflow>(nameof(ApprovalWorkflow)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<ApprovalStatsMasterView>(nameof(ApprovalStatsMasterView)).EntityType.HasKey(e => e.RowNumber);

        odataBuilder.EntitySet<CustomerLoanProductPublication>(nameof(CustomerLoanProductPublication)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<CustomerLoanProductPublicationMasterView>(nameof(CustomerLoanProductPublicationMasterView))
            .EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<DepartmentLoanProductPublication>(nameof(DepartmentLoanProductPublication)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<DepartmentLoanProductPublicationMasterView>(nameof(DepartmentLoanProductPublicationMasterView))
            .EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<LoanAccount>(nameof(LoanAccount)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanAccountMasterView>(nameof(LoanAccountMasterView))
            .EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<LoanApplication>(nameof(LoanApplication)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanApplicationMasterView>(nameof(LoanApplicationMasterView))
            .EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<LoanApplicationApproval>(nameof(LoanApplicationApproval)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanApplicationApprovalMasterView>(nameof(LoanApplicationApprovalMasterView))
            .EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<LoanApplicationSchedule>(nameof(LoanApplicationSchedule)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanApplicationScheduleMasterView>(nameof(LoanApplicationScheduleMasterView))
            .EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<LoanDisbursementCharge>(nameof(LoanDisbursementCharge)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanDisbursementChargeMasterView>(nameof(LoanDisbursementChargeMasterView))
            .EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<LoanDisbursement>(nameof(LoanDisbursement)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanDisbursementMasterView>(nameof(LoanDisbursementMasterView))
            .EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<LoanOffSetCharge>(nameof(LoanOffSetCharge)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanOffSetChargeMasterView>(nameof(LoanOffSetChargeMasterView))
            .EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<LoanOffset>(nameof(LoanOffset)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanOffsetMasterView>(nameof(LoanOffsetMasterView))
            .EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<LoanRepayment>(nameof(LoanRepayment)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanRepaymentMasterView>(nameof(LoanRepaymentMasterView))
            .EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<LoanRepaymentCharge>(nameof(LoanRepaymentCharge)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanRepaymentChargeMasterView>(nameof(LoanRepaymentChargeMasterView))
            .EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<LoanProductMasterView>(nameof(LoanProductMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanProductChargeMasterView>(nameof(LoanProductChargeMasterView)).EntityType
          .HasKey(e => e.Id);

        odataBuilder.EntitySet<LoanProduct>(nameof(LoanProduct)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanProductCharge>(nameof(LoanProductCharge)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<LoanTopup>(nameof(LoanTopup)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanTopupMasterView>(nameof(LoanTopupMasterView)).EntityType
          .HasKey(e => e.Id);

        odataBuilder.EntitySet<LoanTopupCharge>(nameof(LoanTopupCharge)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanTopupChargeMasterView>(nameof(LoanTopupChargeMasterView)).EntityType
          .HasKey(e => e.Id);

        odataBuilder.EntitySet<LoanApplicationGuarantorMasterView>(nameof(LoanApplicationGuarantorMasterView)).EntityType
          .HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanApplicationGuarantorApprovalMasterView>(nameof(LoanApplicationGuarantorApprovalMasterView)).EntityType
          .HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanApplicationGuarantor>(nameof(LoanApplicationGuarantor)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<LoanApplicationItemMasterView>(nameof(LoanApplicationItemMasterView)).EntityType
          .HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanApplicationItem>(nameof(LoanApplicationItem)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<LoanApplicationMasterView>(nameof(LoanApplicationMasterView)).EntityType.HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanApplication>(nameof(LoanApplication)).EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<LoanRepaymentScheduleMasterView>(nameof(LoanRepaymentScheduleMasterView)).EntityType
          .HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanRepaymentScheduleMasterView>(nameof(LoanRepaymentScheduleMasterView)).EntityType
          .HasKey(e => e.Id);
        odataBuilder.EntitySet<LoanRepaymentSchedule>(nameof(LoanRepaymentSchedule)).EntityType.HasKey(e => e.Id);
        // odataBuilder
        //   .EntitySet<LoanApplicationGuarantorApprovalMasterView>(nameof(LoanApplicationGuarantorApprovalMasterView))
        //   .EntityType.HasKey(e => e.Id);

        odataBuilder.EntitySet<FixedDepositAccountApplicationMasterView>(nameof(FixedDepositAccountApplicationMasterView)).EntityType
            .HasKey(e => e.Id);

        odataBuilder.EntitySet<FixedDepositAccountMasterView>(nameof(FixedDepositAccountMasterView)).EntityType
            .HasKey(e => e.Id);

        odataBuilder.EntitySet<FixedDepositChangeInMaturityMasterView>(nameof(FixedDepositChangeInMaturityMasterView)).EntityType
             .HasKey(e => e.Id);


        odataBuilder.EntitySet<FixedDepositInterestAdditionMasterView>(nameof(FixedDepositInterestAdditionMasterView)).EntityType
                 .HasKey(e => e.Id);


        odataBuilder.EntitySet<FixedDepositInterestScheduleMasterView>(nameof(FixedDepositInterestScheduleMasterView)).EntityType
                    .HasKey(e => e.Id);


        odataBuilder.EntitySet<FixedDepositInterestScheduleItemMasterView>(nameof(FixedDepositInterestScheduleItemMasterView)).EntityType
                        .HasKey(e => e.Id);

        odataBuilder.EntitySet<FixedDepositLiquidationMasterView>(nameof(FixedDepositLiquidationMasterView)).EntityType
                        .HasKey(e => e.Id);


        odataBuilder.EntitySet<SavingsAccountMasterView>(nameof(SavingsAccountMasterView)).EntityType
                            .HasKey(e => e.Id);


        odataBuilder.EntitySet<SavingsAccountApplicationMasterView>(nameof(SavingsAccountApplicationMasterView)).EntityType
                                .HasKey(e => e.Id);

        odataBuilder.EntitySet<SavingsCashAdditionMasterView>(nameof(SavingsCashAdditionMasterView)).EntityType
                                .HasKey(e => e.Id);

        odataBuilder.EntitySet<SavingsActionsMasterView>(nameof(SavingsActionsMasterView)).EntityType
                                .HasKey(e => e.Id);

        odataBuilder.EntitySet<SpecialDepositActionsMasterView>(nameof(SpecialDepositActionsMasterView)).EntityType
                                .HasKey(e => e.Id);

        odataBuilder.EntitySet<SavingsIncreaseDecreaseMasterView>(nameof(SavingsIncreaseDecreaseMasterView)).EntityType
                                .HasKey(e => e.Id);

        odataBuilder.EntitySet<SavingsAccountDeductionScheduleMasterView>(nameof(SavingsAccountDeductionScheduleMasterView)).EntityType
                                   .HasKey(e => e.Id);


        odataBuilder.EntitySet<MemberBankAccount>(nameof(MemberBankAccount)).EntityType
                                .HasKey(e => e.Id);

        odataBuilder.EntitySet<MemberBankAccountMasterView>(nameof(MemberBankAccountMasterView)).EntityType
                                   .HasKey(e => e.Id);

        odataBuilder.EntitySet<CustomerPaymentDocumentMasterView>(nameof(CustomerPaymentDocumentMasterView)).EntityType
                           .HasKey(e => e.Id);

        

        return odataBuilder.GetEdmModel();
    }
}

