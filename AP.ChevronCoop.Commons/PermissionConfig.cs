namespace AP.ChevronCoop.Commons;

public static class PermissionConfig
{
  // Format: {Module}.{Group}.{Action}
  
  public const string SecurityPermissionView = "security.permission.view";
  
  
  public const string SecurityStaffDisable = "security.staff.disable";
  public const string SecurityStaffAddnew = "security.staff.addnew";
  public const string SecurityStaffUpdate = "security.staff.update";
  public const string SecurityStaffRole = "security.staff.role";
  public const string SecurityStaffEnable = "security.staff.enable";

  public const string SecurityRoleDisable = "security.roles.disable";
  public const string SecurityRoleEnable = "security.roles.enable";
  public const string SecurityRoleAddnew = "security.roles.addnew";
  public const string SecurityRoleUpdate = "security.roles.update";

  public const string SecurityMemberDisable = "security.member.disable";
  public const string SecurityMemberEnable = "security.member.enable";
  public const string SecurityMemberAddnew = "security.member.addnew";
  public const string SecurityMemberUpdate = "security.member.update";
  public const string SecurityMemberView = "security.member.view";

  public const string SecurityVendorDisable = "security.vendor.disable";
  public const string SecurityVendorEnable = "security.vendor.enable";
  public const string SecurityVendorAddnew = "security.vendor.addnew";
  public const string SecurityVendorUpdate = "security.vendor.update";

  public const string SecurityCustomerDisable = "security.customer.disable";
  public const string SecurityCustomerEnable = "security.customer.enable";
  public const string SecurityCustomerAddnew = "security.customer.addnew";
  public const string SecurityCustomerUpdate = "security.customer.update";

  public const string SecurityViewAudit = "security.view.audit";
  public const string SecurityPendingApproval = "security.pending.approval";
  public const string SecurityDataMigration = "security.data.migration";
  public const string SecurityMigrationApproval = "security.migration.approval";

  public const string DepositsChargeCreate = "deposits.charge.create";
  public const string DepositsProductCreate = "deposits.product.create";
  public const string DepositsProductApprove = "deposits.product.approval";
  public const string SecurityApprovalsView = "security.approvals.view";

  public const string MasterDataGlobalCodeCreate = "masterdata.globalcode.create";
  public const string MasterDataGlobalCodeView = "masterdata.globalcode.view";
  public const string MasterDataGlobalCodeUpdate = "masterdata.globalcode.update";

  public const string MasterDataCurrencyView = "masterdata.currency.view";
  public const string MasterDataCurrencyUpdate = "masterdata.currency.update";
  public const string MasterDataCurrencyCreate = "masterdata.currency.create";

  public const string MasterDataBankView = "masterdata.bank.view";
  public const string MasterDataBankUpdate = "masterdata.bank.update";
  public const string MasterDataBankCreate = "masterdata.bank.create";
  public const string SetupConfigurationView = "setup.configuration.view";

  public const string AccountLoanApplication = "account.loan.application";
  public const string AccountLoanView = "account.loan.view";
  public const string AccountDepositApplication = "account.deposit.application";
  public const string AccountDepositView = "account.deposit.view";
  public const string AccoutLoanReport = "account.loan.report";
  public const string AccountDePositReport = "account.deposit.report";
}