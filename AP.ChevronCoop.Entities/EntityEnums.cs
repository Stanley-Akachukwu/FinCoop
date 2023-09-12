using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.Entities;

public enum ApprovalType
{
    LOAN_PRODUCT = 1,
    LOAN_PRODUCT_APPLICATION = 2,
    DEPOSIT_PRODUCT = 3,
    RETIREE_SWITCH = 4,
    FINANCIAL_TRANSACTION = 5,
    MEMBER_BULK_UPLOAD = 6,
    MEMBER_ENROLLMENT = 7,
    KYC_COMPLETION = 8,
    ADMIN_MEMBER = 9,
    WORKFLOW_SETUP = 10,
    FIXED_DEPOSIT_APPLICATION = 11,
    SPECIAL_DEPOSIT_APPLICATION = 12,
    SPECIAL_DEPOSIT_CASH_ADDITION = 13,
    SPECIAL_DEPOSIT_FUND_TRANSFER = 14,
    SPECIAL_DEPOSIT_WITHDRAWAL = 15,
    LOAN_TOPUP_APPLICATION = 17,
    LOAN_OFFSET_APPLICATION = 18,
    SAVING_DEPOSIT_APPLICATION = 19,
    SAVINGS_INCREASE_DECREASE = 21,
    SAVINGS_CASH_ADDITION,
    FIXED_DEPOSIT_CHANGE_IN_MATURITY,
    FIXED_DEPOSIT_LIQUIDATION,
    LOAN_DISBURSEMENT,
    LOAN_DISBURSEMENT_TOPUP,
    SPECIAL_DEPOSIT_INCREASE_DECREASE,

}

public enum TransactionType
{
    GENERAL_TRANSACTION,
    OPENING_BALANCE,
    JOURNAL_CORRECTION,
    JOURNAL_REVERSAL,
    LOAN_TOPUP,
    LOAN_OFFSET_TRANSFER,
    LOAN_OFFSET_SAVINGS,
    LOAN_OFFSET_SPECIAL_DEPOSIT,
    LOAN_DISBURSEMENT_FULL,
    LOAN_DISBURSEMENT_TOPUP,
    LOAN_INITIALIZE_OFFSET,
    LOAN_DISBURSEMENT_PARTIAL,
    LOAN_CASH_REPAYMENT,
    LOAN_PAYROLL_REPAYMENT,
    LOAN_TRANSACTION,
    FIXED_DEPOSIT_APPLICATION_FUNDING,
    FIXED_DEPOSIT_TRANSACTION,
    SPECIAL_DEPOSIT_APPLICATION_FUNDING,
    SAVING_DEPOSIT_APPLICATION_FUNDING,
    SAVINGS_TRANSACTION,
    FIXED_DEPOSIT_PAYROLL_FUNDING,
    SPECIAL_DEPOSIT_PAYROLL_FUNDING,
    SPECIAL_DEPOSIT_TRANSACTION,
    SAVINGS_DEPOSIT_PAYROLL_FUNDING,
    SPECIAL_DEPOSIT_CASH_ADDITION,
    SPECIAL_DEPOSIT_FUND_TRANSFER,
    SPECIAL_DEPOSIT_WITHDRAWAL,
    SAVINGS_WITHDRAWAL,
    SAVINGS_TRANSFER,
    SAVINGS_CASH_ADDITION,
    FIXED_DEPOSIT_LIQUIDATION,
    SPECIAL_DEPOSIT_INTEREST_ADDITION,
    FIXED_DEPOSIT_INTEREST_ADDITION,
    LOAN_PAYROLL_REPAYMENT_PARTIAL,



    PAYROLL_TRANSACTION,
    EXPENSE_TRANSACTION,
    PURCHASE_TRANSACTION,
    SALES_TRANSACTION,
    ASSET_TRANSACTION,
    EQUITY_TRANSACTION,
    INVENTTORY_TRANSACTION,
    COGS_TRANSACTION,
    BANK_TRANSACTION,
    YEAR_CLOSING_TRANSACTION,
}

public enum TransactionAction
{
    POST = 1,
    REVERSAL = 2,
    UPDATE = 3
}






/*
 * 
 * 



 */


public enum ControlAccounts
{

    // DR OPENING BALANCE CTRL ACC 1billion
    //CR UBA BANK ACCT 1billion

    //where LedgerAccount.Code==ControlAccounts.SD_PAYROLL_CONTROL

    GENERAL_BANK_CONTROL,
    //GENERAL_BANK_SUSPENSE,
    GENERAL_CHARGE_CONTROL,
    //GENERAL_CHARGE_SUSPENSE,
    GENERAL_BAL_CONTROL,
    //GENERAL_BAL_SUSPENSE,
    GENERAL_EXPENSE_CONTROL,
    GENERAL_PAYABLE_CONTROL,
    GENERAL_RECEIVABLE_CONTROL,
    GENERAL_INCOME_CONTROL,
    GENERAL_SUSPENSE,

    COMPANY_CASH_CONTROL,
    COMPANY_BANK_CONTROL,


    CUSTOMER_CASH_CONTROL,
    CUSTOMER_BANK_CONTROL,

    PAYROLL_CONTROL,
    PAYROLL_SUSPENSE,
    PAYROLL_DISBURSEMENT_CONTROL,
    PAYROLL_DEPOSIT_CONTROL,

    LOAN_PROD_CONTROL,
    LOAN_CHARGE_CONTROL,
    LOAN_COY_BANK_CONTROL,
    LOAN_CUST_BANK_CONTROL,

    LOAN_PAYROLL_CONTROL,
    LOAN_PROD_PRINCIPAL_CONTROL,
    LOAN_ACC_PRINCIPAL_CONTROL,
    LOAN_PROD_UNEARNED_INTEREST_CONTROL,
    LOAN_ACC_UNEARNED_INTEREST_CONTROL,
    LOAN_PROD_EARNED_INTEREST_CONTROL,
    LOAN_ACC_EARNED_INTEREST_CONTROL,
    LOAN_PROD_CHARGE_ACCRUAL_CONTROL,
    LOAN_ACC_CHARGE_ACCRUAL_CONTROL,
    LOAN_PROD_CHARGE_COLLECT_CONTROL,
    LOAN_ACC_CHARGE_COLLECT_CONTROL,

    LOAN_BAL_CONTROL,
    LOAN_PRINCIPAL_BAL_CONTROL,
    LOAN_INTEREST_BAL_CONTROL,
    LOAN_EXPENSE_CONTROL,
    LOAN_PAYABLE_CONTROL,
    LOAN_RECEIVABLE_CONTROL,
    LOAN_INCOME_CONTROL,
    LOAN_SUSPENSE,



    SAVINGS_PROD_CONTROL, ///GL.Code="SAVINGS_PROD_CONTROL"
    SAVINGS_CHARGE_CONTROL,
    SAVINGS_COY_BANK_CONTROL,
    SAVINGS_CUST_BANK_CONTROL,
    SAVINGS_PAYROLL_CONTROL,
    SAVINGS_INTEREST_ACCRUAL_CONTROL,
    SAVINGS_INTEREST_PAYOUT_CONTROL,
    SAVINGS_BAL_CONTROL,
    SAVINGS_EXPENSE_CONTROL,
    SAVINGS_PAYABLE_CONTROL,
    SAVINGS_RECEIVABLE_CONTROL,
    SAVINGS_INCOME_CONTROL,
    SAVINGS_SUSPENSE,


    SD_PROD_CONTROL,
    SD_CHARGE_CONTROL,
    SD_COY_BANK_CONTROL,
    SD_CUST_BANK_CONTROL,
    SD_PAYROLL_CONTROL,
    SD_BAL_CONTROL,
    SD_PROD_INTEREST_ACCRUAL_CONTROL,
    SD_ACC_INTEREST_ACCRUAL_CONTROL,
    SD_PROD_INTEREST_ADD_CONTROL,
    SD_ACC_INTEREST_ADD_CONTROL,
    SD_PROD_INTEREST_PAYOUT_CONTROL,
    SD_ACC_INTEREST_PAYOUT_CONTROL,
    SD_PROD_WTD_CONTROL,
    SD_ACC_WTD_CONTROL,
    SD_INTEREST_BAL_CONTROL,
    SD_EXPENSE_CONTROL,
    SD_PAYABLE_CONTROL,
    SD_RECEIVABLE_CONTROL,
    SD_INCOME_CONTROL,
    SD_SUSPENSE,



    FD_PROD_CONTROL,
    FD_CHARGE_CONTROL,
    FD_COY_BANK_CONTROL,
    FD_CUST_BANK_CONTROL,
    FD_PAYROLL_CONTROL,
    FD_BAL_CONTROL,
    FD_PROD_INTEREST_ACCRUAL_CONTROL,
    FD_ACC_INTEREST_ACCRUAL_CONTROL,
    FD_PROD_INTEREST_ADD_CONTROL,
    FD_ACC_INTEREST_ADD_CONTROL,
    FD_PROD_INTEREST_PAYOUT_CONTROL,
    FD_ACC_INTEREST_PAYOUT_CONTROL,
    FD_INTREST_BAL_CONTROL,
    FD_EXPENSE_CONTROL,
    FD_PAYABLE_CONTROL,
    FD_RECEIVABLE_CONTROL,
    FD_INCOME_CONTROL,
    FD_SUSPENSE,
    FD_PROD_CHARGE_ACCRUAL_CONTROL,
    FD_ACC_CHARGE_ACCRUAL_CONTROL,
    FD_PROD_CHARGE_COLLECT_CONTROL,
    FD_ACC_CHARGE_COLLECT_CONTROL,


}




public enum LoanRepaymentMode
{
    SPECIAL_DEPOSIT = 1,
    SAVINGS = 2,
    BANK_TRANSFER = 3,
    PAYROLL = 4,
    //OFFSET = 5
}

public enum LoanDisbursementMode
{
    SPECIAL_DEPOSIT = 1,
    BANK_TRANSFER = 2,
    SAVINGS = 3,
    CHEQUE = 4,
    CASH = 5,
    INITIALIZED = 6
}

public enum TransactionStatus
{
    PENDING = 1,
    AWAITING_RECONCILIATION = 2,
    AWAITING_CLEARING = 3,
    AWAITING_SETTLEMENT = 4,
    FAILED = 5,
    REVERSED = 6,
    SUCCESS = 7,
    REJECTED = 8
}

public enum AllowedOffsetType
{
    NONE = 1,
    FULL = 2,
    PARTIAL = 3,
    IN_LIEU_OF_PAYROLL = 4
}

public enum LoanProductType
{
    EXECUTIVE_LOAN = 1,
    TARGET_LOAN = 2,
    LONG_TERM_LOAN = 3,
    SHORT_TERM_LOAN = 4,
    CAR_LOAN = 5,
    HOUSE_APPLIANCE_LOAN = 6
}

public enum PublicationType
{
    ALL,
    CUSTOMER,
    DEPARTMENT
}

public enum Gender
{
    MALE = 1,
    FEMALE = 2,
    UNKNOWN = 3
}

public enum LoanApplicationItemType
{
    CAR = 1,
    HOME_APPLIANCE = 2,
    OTHERS = 3
}

public enum GuarantorType
{
    REGULAR = 1,
    RETIREE = 2
}

public enum ApplicationType
{
    APPLICATION = 1,
    TOPUP = 2
}

public enum PaymentChannel
{
    CASH = 1,
    TELLER = 2,
    MOBILE = 3,
    WEB = 4,
    POS = 5,
    ATM = 6,
    UNKNOWN = 7
}

public enum COAHeaderType
{
    Group = 1,
    Reporting = 2
}

public enum COAType
{
    ASSET = 1,
    LIABILITY = 2,
    EQUITY = 3,
    INCOME = 4,
    EXPENSE = 5,
    COGS = 6,
    //SECURITY = 7,
    CONTROL = 7,
    MEMO = 8,
    SUSPENSE = 9,
}

public enum SequenceNames
{
    Asset = 1,
    Liability = 2,
    Equity = 3,
    Income = 4,
    Expense = 5,
    COGS = 6,
    Security = 7,
    Deposits = 8,
    Loans = 9,
    Repayments = 10,
    Withdrawals = 11,
    General = 12,
    Others = 13
}

public enum BalanceFlag
{
    DEBIT = 1,
    CREDIT = 2
}

//public enum LedgerBalanceType
//{
//    Debit = 1, Credit = 2
//}

public enum LedgerBalanceUOM
{
    CURRENCY = 1,
    UNIT = 2,
    QUANTITY = 3,
    VOLUME = 4
}

public enum TransactionEntryType
{
    DEBIT = 1,
    CREDIT = 2
}

public enum KYCFieldType
{
    String = 1,
    Number = 2,
    Date = 3,
    DateRange = 4,
    Time = 5,
    Photo = 6,
    Document = 7
}

public enum PartnerBankAccountType
{
    CASH_ACCOUNT = 1,
    CHARGE_ACCOUNT = 2,
    BANK_ACCOUNT = 4,
    SAVINGS_ACCOUNT = 5,
    CURRENT_ACCOUNT = 6
}

public enum Tenure
{
    NONE = 0,

    [Display(Name = "Daily (360 days/pa)")]
    DAILY_360 = 360,

    [Display(Name = "Daily (365 days/pa)")]
    DAILY_365 = 365,

    [Display(Name = "Daily (366 days/pa)")]
    DAILY_366 = 366,

    [Display(Name = "Weekly (52 weeks/pa)")]
    WEEKLY = 52,

    [Display(Name = "Bi-weekly (26 fortnights/pa)")]
    BI_WEEKLY = 26,

    [Display(Name = "Monthly (12 months/pa)")]
    MONTHLY = 12,

    [Display(Name = "Quaterly (4 quarters/pa)")]
    QUARTERLY = 4,

    [Display(Name = "Bi-Annually (2 times/pa)")]
    SEMI_ANNUALLY = 2,

    [Display(Name = "Annually (Once/pa)")] ANNUALLY = 1
}


public enum DaysInYear
{
    DAYS_360 = 360,
    DAYS_365 = 365,
    DAYS_366 = 366
}


public enum ProductType
{
    LOAN = 1,
    DEPOSIT = 2
}

public enum ProductPublicationTypes
{
    ALL = 1,
    SPECIFIC_USER = 2,
    SPECIFIC_DEPARTMENT = 3
}

public enum ChargeMethod
{
    FLAT = 1,
    PERCENT = 2
}

public enum ChargeTarget
{
    NONE = 0,
    PRINCIPAL = 1,
    PRINCIPAL_BALANCE = 2,
    INTEREST = 3,
    INTEREST_BALANCE = 4,
    PRINCIPAL_PLUS_INTEREST = 5,
    PRINCIPAL_BAL_PLUS_INTEREST_BAL = 6,
    PERIOD_PRINCIPAL = 7,
    PERIOD_INTEREST = 8,
    PERIOD_PAYMENT = 9,
    OFFSET_AMOUNT = 10,
    TOP_UP_AMOUNT = 11,
    TOP_UP_INTEREST = 12,
    TOP_UP_PRINCIPAL = 13,

    SAVINGS_BALANCE = 14,
    SPECIAL_DEPOSIT_BALANCE = 15,
    FIXED_DEPOSIT_BALANCE = 16,

    VALUE = 17
}

public enum ChargeType
{
    LOAN_DISBURSEMENT = 1,
    LOAN_TOP_UP = 2,
    LOAN_EARLY_OFFSET_WAIVER = 3, //charge type for offsetting a loan before the configured offset period
    LOAN_ADMIN_OFFSET = 4, //charge type for offsetting a loan after the configured offset period
    LOAN_WAITING_PERIOD = 5, //charge for skipping the configured period to wait to apply for a new loan

    GENERAL_SERVICE = 6,
    SMS_SERVICE = 7,
    EMAIL_SERVICE = 8,
    GENRAL_ADMIN = 9,

    FIXED_DEPOSIT_LIQUIDATION_CHARGE = 10
}
public enum DepositChargeType
{
    NONE = 0,
    CASH_ADDITION = 1,
    WITHDRAWAL = 2,
    INTEREST_ADDITION = 3,
    TRANSFER_CHARGE = 4,
    GENERAL_SERVICE = 5,
    SMS_SERVICE = 6,
    EMAIL_SERVICE = 7,
    GENERAL_ADMIN = 8,
    WAIVER_CHARGE = 9,
    FIXED_DEPOSIT_LIQUIDATION_CHARGE = 10,
}

public enum ProductStatus
{
    ACTIVE = 1,
    PENDING = 2,
    DEACTIVATED = 3,
    DELETED = 4,
    PUBLISHED = 5,
    PENDING_APPROVAL = 6,
    APPROVED = 7,
    REJECTED = 8
}

public enum GuarantorApprovalType
{
    LOAN_APPLICATION = 1,
    LOAN_TOPUP = 2
}

public enum ApprovalStatus
{
    CREATED = 1,
    INITIATED = 2,
    PENDING_APPROVAL = 3,
    APPROVED = 4,
    REJECTED = 5,
    ON_GOING = 6
}

//public enum BaseApprovalStatus
//{
//    PENDING,
//    APPROVED,
//    REJECTED
//}
public enum ChargeCalculationMethod
{
    ADD = 1,
    SUBTRACT = 2
}

public enum InterestMethod
{
    SIMPLE = 1,

    COMPOUND = 2
}


public enum InterestCalculationMethod
{
    FLAT_RATE = 1,
    DECLINING_BALANCE = 2,
    //DECLINING_BALANCE_EQUAL = 3
}


public enum AccountStatus
{
    Active = 1,
    InActive = 2,
    Closed = 3
}

public enum LocationType
{
    COUNTRY = 1,
    STATE = 2,
    LGA = 3,
    ZONE = 4,
    AREA = 5,
    REGION = 6,
    DISTRICT = 7,
    DIVISION = 8,
    STREET = 9,
    ADDRESS = 10,
    CITY = 11,
    TOWN = 12,
    VILLAGE = 13,
    RESIDENCE = 14,
    BUILDING = 15,
    OTHER = 16
}

public enum GlobalCodeTypeKeys
{
    GENERIC = 1,
    ID = 2,
    SALUTATION = 3,
    GENDER = 4,
    MARITAL_STATUS = 5,
    RELATIONSHIP = 6,
    CONTACT = 7,
    PERSONAL_CONTACT = 8,
    REFERENCE = 9,
    DOCUMENT = 10,
    PHOTO = 11,
    VIDEO = 12,
    MEDIA = 13,
    LOCATION = 14,
    ADDRESS = 15,
    CURRENCY = 16,
    BANK = 17,
    BANK_BRANCH = 18,
    BANK_ACCOUNT = 19,
    BLOOD_GROUP = 20,
    GENOTYPE = 21,
    ALLERGY = 22,

    EDUCATION_SCHOOL = 23,
    ORG_UNIT = 24,
    PARTNER_STATUS = 25,

    CONTRACT = 26,
    CONTRACT_BASIS = 27,
    WORKSHIFT = 28,
    EMPLOYEE = 29,
    EMPLOYEE_STATUS = 30,
    EMPLOYEE_STRUCTURE = 31,
    DEPARTMENT = 32,
    COMPANY_STRUCTURE = 33,
    TIME_PERIOD = 34,
    DISCIPLINARY_ACTION = 35,
    GRIEVANCE_ACTION = 36,
    GRIEVANCE = 37,
    JOB_TITLE = 38,
    PAY = 39,
    PAY_CHANNEL = 40,
    DEDUCTION = 41,
    PENSION_PROVIDER = 42,
    LEAVE = 43,
    AGENT = 44,
    SERVICE = 45,
    SEVERITY = 46,
    CLIENT = 47,
    TICKET = 48,
    TICKET_STATUS = 49,
    COA = 50,
    PAYMENT_TERMS = 51,

    UOM = 52,

    CUSTOMER = 53,
    CUSTOMER_STATUS = 54,
    VENDOR = 55,
    VENDOR_STATUS = 56,
    APPROVAL_EVENT = 57,
    AUDIT_EVENT = 58,
    COUNTRY = 59,

    DEPOSIT_PRODUCT_TYPE = 60,
    LOAN_PRODUCT_TYPE = 61,
    MEMBER_TYPE = 62
}

public enum ApprovalNotificationTypes
{
    None = 0,
    INITIAL = 1,
    REMINDER = 2,
    ESCALATION = 3
}

public enum DepositProductType
{
    SAVINGS = 1,

    FIXED_DEPOSIT = 2,

    SPECIAL_DEPOSIT = 3
}

public enum LoanApplicationStatus
{
    PENDING = 1,
    APPROVED = 2,
    REJECTED = 3,
    AWAITING_GUARANTOR_APPROVAL = 4,
}

public enum WithdrawalAccountType
{
    SPECIAL_DEPOSIT_ACCOUNT = 1,

    SAVINGS_ACCOUNT = 2,

    EXISTING_BANK_ACCOUNT = 3
}

public enum DestinationAccountType
{
    SAVINGS_ACCOUNT = 1,
    EXISTING_BANK_ACCOUNT = 2,
    FIXED_DEPOSIT_ACCOUNT = 3
}

public enum TopupFundingSourceType
{
    EXISTING_BANK_ACCOUNT = 1,
    SPECIAL_DEPOSIT_ACCOUNT = 2
}

public enum TransactionChannel

{
    BANK_TRANSFER = 1,

    PAYROLL = 2,

    DEPOSIT = 3,

    SAVINGS = 4
}

public enum DepositFundingSourceType
{
    NONE = 0,
    SPECIAL_DEPOSIT = 1,
    PAYROLL = 2,
    BANK_TRANSFER = 3,
    SAVINGS = 4,
    ROLLOVER = 5,
}

public enum DepositTransactionType
{
    FUND_TRANSFER = 1,
    CASH_ADDITION = 2,
    PAYROLL_FUNDING = 3
}

public enum MaturityInstructionType
{
    [Display(Name = "Rollover Principal and Interest")]
    ROLLOVER_PRINCIPAL_AND_INTEREST = 1,

    [Display(Name = "Rollover Principal and Liquidate Interest")]
    ROLLOVER_PRINCIPAL_AND_LIQUIDATE_INTEREST = 2,

    [Display(Name = "Liquidate Principal and Interest")]
    LIQUIDATE_PRINCIPAL_AND_INTEREST = 3
}

public enum MemberPaymentUploadType
{
    FIXED_DEPOSIT_PAYMENT = 1,
    SPECIAL_DEPOSIT_PAYMENT = 2,
    SAVINGS_DEPOSIT_PAYMENT = 3,
    LOAN_OFFSET = 4
}

public enum ContributionChangeRequest
{
    [Display(Name = "Increase monthly contribution amount")]
    INCREASE = 1,

    [Display(Name = "Decrease monthly contribution amount")]
    DECREASE = 2
}

public enum DepositAccountActionType
{
    NONE = 0,
    CASH_ADDITION = 1,
    WITHDRAWAL = 2,
    FUND_TRANSFER = 3,
    LOAN_OFFSET = 4
}
//public enum TransactionStatus
//{
//    SUCCESSFULL, DISPUTE, FAILED, PENDING
//}

public enum DisbursementStatusType
{
    APPROVED = 1,
    REJECTED = 2,
    DISBURSED = 3,
    PENDING = 4
}

public enum PayrollDeductionType
{
    LOAN = 1,
    SAVINGS = 2,
    FIXED_DEPOSIT = 3,
    SPECIAL_DEPOSIT = 4,
    OTHER = 5
}

public enum PayrollErrorType
{
    OVER_PAYMENT = 1,
    UNDER_PAYMENT = 2,
    INVALID_ACCOUNT_NO = 3,
    INVALID_EMPLOYEE_NO = 4,
    INVALID_BATCH_NO = 5,
    INVALID_PAYROLL_DATE = 6,
    INVALID_PAYROLL_CODE = 7,
    PENDING = 8,
    COMPLETE = 9,
    MATCHED = 10,
    GENRIC_ERROR = 11
}


public enum PayrollScheduleType
{
    PAYROLL_DEDUCTION = 1,
    INTEREST_COMPUTATION = 2
}

public enum CronJobType
{
    LOAN_REPAYMENT_DEDUCTION = 1, //compute repayment deductions from payroll for loan accounts->loan repayment schedule
    SPECIAL_DEPOSIT_DEDUCTION = 2, //compute funding deductions from payroll for sd accounts

    SAVINGS_ACCOUNT_DEDUCTION = 3, //compute funding from payroll for savings accounts

    //after you upload payroll deductions CSV
    LOAN_REPAYMENT_PAYROLL = 4, //update loan account repayments from payroll deductions
    SAVINGS_ACCOUNT_PAYROLL = 5, //update savings account funding from payroll deductions
    SPECIAL_DEPOSIT_PAYROLL = 6, //update sd account funding from payroll deductions
                                 //FIXED_DEPOSIT_PAYROLL = 3,

    INTEREST_COMPUTATION_FIXED_DEPOSIT = 8,
    MONTHLY_INTEREST_COMPUTATION_SPECIAL_DEPOSIT = 9,
    DAILY_INTEREST_COMPUTATION_SPECIAL_DEPOSIT = 10
}

public enum CronJobStatus
{
    PENDING = 1,
    STARTED = 2,
    COMPLETED = 3,
    FAILED = 4
}

public enum LoanCreationType
{
    APPLICATION = 1,
    OFFSET = 2,
    TOPUP = 3
}

public enum NotificationMessageType
{
    SAVINGS,
    SPECIAL_DEPOSIT,
    LOAN
}