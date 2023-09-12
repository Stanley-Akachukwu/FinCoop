using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccountApplications;
using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositChangeInMaturities;
using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositLiquidations;
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsCashAdditions;
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositCashAdditions;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositFundTransfers;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositWithdrawals;
using AP.ChevronCoop.AppDomain.Loans.LoanOffsets;
using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.AppDomain.Loans.LoanTopups;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProductCharges;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.LoanOffsetTransactions;
using AP.ChevronCoop.Entities.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.Entities.Loans.LoanDisbursements;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberNextOfKins;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory
{
    public  class ApprovalDetailFactory : IApprovalDetailFactory
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly CoreAppSettings _options;
        private CancellationTokenSource cts = new CancellationTokenSource();

        public ApprovalDetailFactory(ChevronCoopDbContext dbContext, IOptions<CoreAppSettings> options)
        {
            _dbContext = dbContext;
            _options = options.Value;
        }
        public async Task ProcessProviderApprovalDetails(Approval approval)
        {
            ApprovalType type = approval.ApprovalType;

            if (type != ApprovalType.WORKFLOW_SETUP)
            {
                try
                {
                    if (string.IsNullOrEmpty(approval?.ApprovalViewModelPayload))
                        ProcessDetails(type, approval);
                }
                catch (Exception)
                {
                }
            }
        }
        private void ProcessDetails(ApprovalType type, Approval approval)
        {
            switch (type)
            {
                case ApprovalType.LOAN_PRODUCT:
                    GetLoanProductDetails(approval.Payload, approval.Id);
                    break;
                case ApprovalType.DEPOSIT_PRODUCT:
                    GetDepositProductDetails(approval.Payload, approval.Id);
                    break;

                case ApprovalType.RETIREE_SWITCH:
                    GetRetireeSwitchDetails(approval.Payload, approval.Id);
                    break;

                case ApprovalType.KYC_COMPLETION:
                    GetKYCDetails(approval.Payload, approval.Id);
                    break;

                case ApprovalType.MEMBER_BULK_UPLOAD:
                    GetBulkUploadDetails(approval.Payload, approval.Id);
                    break;

                case ApprovalType.SAVING_DEPOSIT_APPLICATION:
                    GetSavingsAccountApplicationDetails(approval.Payload, approval.Id);
                    break;

                case ApprovalType.FIXED_DEPOSIT_APPLICATION:
                    GetFDApplicationDetails(approval.Payload, approval.Id);
                    break;

                case ApprovalType.FIXED_DEPOSIT_CHANGE_IN_MATURITY:
                    GetFDChangeInMaturityDetails(approval.Payload, approval.Id);
                    break;

                case ApprovalType.FIXED_DEPOSIT_LIQUIDATION:
                    GetFDLiquidationDetails(approval.Payload, approval.Id);
                    break;

                case ApprovalType.LOAN_PRODUCT_APPLICATION:
                    GetLoanApplicationDetails(approval.Payload, approval.EntityId, approval.Id);
                    break;

                case ApprovalType.SPECIAL_DEPOSIT_APPLICATION:
                    GetSDApplicationDetails(approval.Payload, approval.Id);
                    break;

                case ApprovalType.SAVINGS_INCREASE_DECREASE:
                    GetSavingsIncreaseDecreaseDetails(approval.Payload, approval.Id);
                    break;

                case ApprovalType.SAVINGS_CASH_ADDITION:
                    GetSavingsCashAdditionDetails(approval.Payload, approval.Id);
                    break;

                case ApprovalType.SPECIAL_DEPOSIT_CASH_ADDITION:
                    GetSDCashAdditionDetails(approval.Payload, approval.Id);
                    break;

                case ApprovalType.SPECIAL_DEPOSIT_FUND_TRANSFER:
                    GetSDFundTransferDetails(approval.Payload, approval.Id);
                    break;

                case ApprovalType.SPECIAL_DEPOSIT_WITHDRAWAL:
                    GetSDWithdrawalDetails(approval.Payload, approval.Id);
                    break;

                case ApprovalType.LOAN_DISBURSEMENT:
                case ApprovalType.LOAN_DISBURSEMENT_TOPUP:
                    GetLoanDisbursementDetails(approval.Payload, approval.Id);
                    break;

                case ApprovalType.LOAN_TOPUP_APPLICATION:
                    GetLoanTopUpDetails(approval.Payload, approval.Id);
                    break;

                case ApprovalType.LOAN_OFFSET_APPLICATION:
                    GetLoanOffSetDetails(approval.Payload, approval.Id);
                    break;

                case ApprovalType.SPECIAL_DEPOSIT_INCREASE_DECREASE:
                    GetSDIncreaseDecreaseDetails(approval.Payload, approval.Id);
                    break;

                default:
                    break;

            }
        }
        private void UpdateApproval(ChevronCoopApprovalViewModel details)
        {
            var detailString = System.Text.Json.JsonSerializer.Serialize(details);
            var approval = _dbContext.Approvals.FirstOrDefault(x => x.Id == details.ApprovalId);
            approval.ApprovalViewModelPayload = detailString;
            _dbContext.Approvals.Update(approval);
            _dbContext.SaveChanges();
        }
        private void GetLoanProductDetails(string payload, string approvalId)
        {
            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;


            var entity = System.Text.Json.JsonSerializer.Deserialize<CreateLoanProductCommand>(payload);
            if (entity == null) return;

            var defaultCurrency = _dbContext.Currencies.Where(c => c.Id == entity.DefaultCurrencyId).FirstOrDefault();
            if (defaultCurrency == null) return;

            #region    All details
            var hasAdminCharges = entity.HasAdminCharges ? "YES" : "NO";
            var isTargetLoan = entity.IsTargetLoan ? "YES" : "NO";
            var enableSavingsOffset = entity.EnableSavingsOffset ? "YES" : "NO";
            var enableChargeWaiver = entity.EnableChargeWaiver ? "YES" : "NO";
            var enableTopUpCharges = entity.EnableTopUpCharges ? "YES" : "NO";
            var enableWaitingPeriodCharge = entity.EnableWaitingPeriodCharge  ? "YES" : "NO";
            var isGuarantorRequired = entity.IsGuarantorRequired ? "YES" : "NO";
            var enableAdminOffsetCharge = entity.EnableAdminOffsetCharge ? "YES" : "NO";



            var children = new List<ChildView>();
            children.Add(new ChildView { FieldLabel = "Product Code", FieldValue = entity?.Code });
            children.Add(new ChildView { FieldLabel = "Payroll Code", FieldValue = entity?.PayrollCode });
            children.Add(new ChildView { FieldLabel = "Name", FieldValue = entity?.Name });
            children.Add(new ChildView { FieldLabel = "ShortName", FieldValue = entity?.ShortName });
            children.Add(new ChildView { FieldLabel = "Loan Product Type", FieldValue = entity.LoanProductType.ToString() });
            // children.Add(new ChildView { FieldLabel = "ShortName", FieldValue = entity?.ShortName });
            children.Add(new ChildView { FieldLabel = $"Tenure({nameof(entity.TenureUnit)})", FieldValue = $"Min:{entity?.MinTenureValue:n} - Max:{entity?.MaxTenureValue:n}" });
            children.Add(new ChildView { FieldLabel = $"Repayment Period", FieldValue = $"{entity?.RepaymentPeriod?.ToString()}" });
            children.Add(new ChildView { FieldLabel = $"Interest Method", FieldValue = $"{entity?.InterestMethod}" });
            children.Add(new ChildView { FieldLabel = "Principal Limit Type", FieldValue = entity?.PrincipalLimitType });
            children.Add(new ChildView { FieldLabel = "Principal Multiple", FieldValue = $"{entity?.PrincipalMultiple:n}" });
            children.Add(new ChildView { FieldLabel = "Principal MinLimit", FieldValue = $"{entity?.PrincipalMinLimit:n}" });
            children.Add(new ChildView { FieldLabel = "Principal MaxLimit", FieldValue = $"{entity?.PrincipalMaxLimit:n}" });
            children.Add(new ChildView { FieldLabel = "TenureUnit", FieldValue = $"{entity?.TenureUnit}" });
            children.Add(new ChildView { FieldLabel = "MinTenureValue", FieldValue = $"{entity?.MinTenureValue:n}" });
            children.Add(new ChildView { FieldLabel = "MaxTenureValue", FieldValue = $"{entity?.MaxTenureValue:n}" });
            children.Add(new ChildView { FieldLabel = "RepaymentPeriod", FieldValue = $"{entity?.RepaymentPeriod}" });
            children.Add(new ChildView { FieldLabel = "InterestMethod", FieldValue = $"{entity?.InterestMethod}" });
            children.Add(new ChildView { FieldLabel = "InterestCalculationMethod", FieldValue = $"{entity?.InterestCalculationMethod}" });
            children.Add(new ChildView { FieldLabel = "DaysInYear", FieldValue = $"{entity?.DaysInYear}" });
            children.Add(new ChildView { FieldLabel = "InterestRate", FieldValue = $"{entity?.InterestRate:n}" });
            children.Add(new ChildView { FieldLabel = "HasAdminCharges", FieldValue = $"{hasAdminCharges}" });
            children.Add(new ChildView { FieldLabel = "IsTargetLoan", FieldValue = $"{isTargetLoan}" });
            children.Add(new ChildView { FieldLabel = "BenefitCode", FieldValue = $"{entity?.BenefitCode}" });
            children.Add(new ChildView { FieldLabel = "AllowedOffsetType", FieldValue = $"{entity?.AllowedOffsetType}" });
            children.Add(new ChildView { FieldLabel = "OffsetPeriodUnit", FieldValue = $"{entity?.OffsetPeriodUnit}" });
            children.Add(new ChildView { FieldLabel = "OffsetPeriodValue", FieldValue = $"{entity?.OffsetPeriodValue:n}" });
            children.Add(new ChildView { FieldLabel = "WaitingPeriodUnit", FieldValue = $"{entity.WaitingPeriodUnit}" });
            children.Add(new ChildView { FieldLabel = "WaitingPeriodValue", FieldValue = $"{entity?.WaitingPeriodValue:n}" });
            children.Add(new ChildView { FieldLabel = "GuarantorMinYear", FieldValue = $"{entity?.GuarantorMinYear:n}" });
            children.Add(new ChildView { FieldLabel = "GuarantorAmountLimit", FieldValue = $"{entity?.GuarantorAmountLimit:n}" });
            children.Add(new ChildView { FieldLabel = "EmployeeGuarantorCount", FieldValue = $"{entity?.EmployeeGuarantorCount:n}" });
            children.Add(new ChildView { FieldLabel = "EnableSavingsOffset", FieldValue = $"{enableSavingsOffset}" });
            children.Add(new ChildView { FieldLabel = "EnableChargeWaiver", FieldValue = $"{enableChargeWaiver}" });
            children.Add(new ChildView { FieldLabel = "EnableTopUpCharges", FieldValue = $"{enableTopUpCharges}" });
            children.Add(new ChildView { FieldLabel = "EnableWaitingPeriodCharge", FieldValue = $"{enableWaitingPeriodCharge}" });
            children.Add(new ChildView { FieldLabel = "IsGuarantorRequired", FieldValue = $"{isGuarantorRequired}" });
            children.Add(new ChildView { FieldLabel = "NonEmployeeGuarantorCount", FieldValue = $"{entity?.NonEmployeeGuarantorCount:n}" });
            children.Add(new ChildView { FieldLabel = "QualificationTargetProduct", FieldValue = $"{entity.QualificationTargetProduct}" });
            children.Add(new ChildView { FieldLabel = "QualificationMinBalancePercentage", FieldValue = $"{entity?.QualificationMinBalancePercentage:n}" });
            children.Add(new ChildView { FieldLabel = "EnableChargeWaiver", FieldValue = $"{enableChargeWaiver}" });
            children.Add(new ChildView { FieldLabel = "EnableTopUpCharges", FieldValue = $"{enableTopUpCharges}" });
            children.Add(new ChildView { FieldLabel = "EnableWaitingPeriodCharge", FieldValue = $"{enableWaitingPeriodCharge}" });
            children.Add(new ChildView { FieldLabel = "enableAdminOffsetCharge", FieldValue = $"{isGuarantorRequired}" });

            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = "Loan Product Details",
                Children = children

            });



            if (entity?.MemberTypes != null)
                if (entity.MemberTypes.Any())
                {
                    var memberTypesChildren = new List<ChildView>();
                    var sn = 0;
                    foreach (var b in entity.MemberTypes)
                    {
                        sn++;
                        memberTypesChildren.Add(new ChildView { FieldLabel = $"{sn}. {nameof(b)}", FieldValue = $"{b}" });
                    }
                    partialViews.Add(new ApprovalPartialView
                    {
                        ViewId = viewId++,
                        IsTabularView = false,
                        Title = "Member Types",
                        Children = memberTypesChildren
                    });
                }
            

            #endregion

            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            details.ApprovalId = approvalId;
            UpdateApproval(details);
        }
        
        private void GetDepositProductDetails(string payload, string approvalId)
        {
            var rsp = new CommandResult<string>();
            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;


            var entity = System.Text.Json.JsonSerializer.Deserialize<CreateDepositProductCommand>(payload);
            if (entity == null || string.IsNullOrEmpty(entity.CreatedByUserId)) return;

            var defaultCurrency = _dbContext.Currencies.Where(c => c.Id == entity.DefaultCurrencyId).FirstOrDefault();


            var productCharges = new List<string>();
            var charges = new List<DepositProductCharge>();

            if (entity.ProductCharges != null)
                    if (entity.ProductCharges.Any())
                          productCharges = entity.ProductCharges.Select(x => x.ChargeId).ToList();


            if (productCharges != null)
                if (productCharges.Any())
                    charges =  _dbContext.DepositProductCharges.Where(d => productCharges.Contains(d.Id)).ToList();



            #region    All details
            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = "Deposit Product Details",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Product Code",FieldValue=entity.Code},
                     new ChildView { FieldLabel ="Name",FieldValue=entity.Name},
                     new ChildView { FieldLabel ="ShortName",FieldValue=entity.ShortName},
                     new ChildView { FieldLabel =$"Tenure({nameof(entity.Tenure)})",FieldValue=$"{entity.TenureValue:n}"},
                     new ChildView { FieldLabel =$"Minimum Contribution Regular",FieldValue=$"{entity.MinimumContributionRegular:n}"},
                     new ChildView { FieldLabel =$"Minimum Contribution Retiree",FieldValue=$"{entity.MinimumContributionRetiree:n}"},
                       new ChildView { FieldLabel ="Default Currency",FieldValue=defaultCurrency?.Name},
                 }
            });

            var sn = 0;
            if (charges != null)
                if (charges.Any())
                {
                    var chargeChildren = new List<TabularView>();
                    foreach (var b in charges)
                    {
                        sn++;
                        chargeChildren.Add(new TabularView { FieldSN = sn, FieldLabel = b?.Charge?.Name, FieldValue = $"{b.Charge.ChargeValue}" });
                    }
                    partialViews.Add(new ApprovalPartialView
                    {
                        ViewId = viewId++,
                        IsTabularView = true,
                        Title = "Deposit Product Charges",
                        ChildRows = chargeChildren
                    });
                }



            sn = 0;
            if (entity?.InterestRanges != null)
                if (entity.InterestRanges.Any())
                {
                    var interestChildren = new List<TabularView>();
                    foreach (var b in entity.InterestRanges)
                    {
                        sn++;
                        interestChildren.Add(new TabularView { FieldSN = sn, FieldLabel = $"{b.InterestRate:n}", FieldValue = $"Max:{b.UpperLimit:n} Min:{b.LowerLimit:n}" });
                    }
                    partialViews.Add(new ApprovalPartialView
                    {
                        ViewId = viewId++,
                        IsTabularView = true,
                        Title = "Deposit Product Interest Ranges",
                        ChildRows = interestChildren
                    });
                }




            #endregion
            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            details.ApprovalId = approvalId;
            UpdateApproval(details);
        }
        private void GetKYCDetails(string payload, string approvalId)
        {
            var rsp = new CommandResult<string>();
            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;

            var request = System.Text.Json.JsonSerializer.Deserialize<UpdateMemberProfileCommand>(payload);
            if (request == null) return;


            var entity = _dbContext.MemberProfiles.Include(m => m.Department)
                .FirstOrDefault(m => m.ApplicationUserId == request.ApplicationUserId);
            var customer = _dbContext.Customers.Where(c=>c.ApplicationUserId==entity.ApplicationUserId).FirstOrDefault();

            var kins = new List<MemberNextOfKin>();
            if(entity!=null)
               kins = _dbContext.MemberNextOfKins.Where(m => m.ProfileId == entity.Id).ToList();

            var banks = new List<CustomerBankAccount>();

            if (customer != null)
                banks = _dbContext.CustomerBankAccounts.Where(m => m.CustomerId == customer.Id).Include(b => b.Bank).ToList();


            var paymentInfo = _dbContext.EnrollmentPaymentInfos.FirstOrDefault(m => m.ProfileId == entity.Id);

            var evidenceOfPayment = "";
            if (paymentInfo != null)
            {
                string evidenceBase64String = Convert.ToBase64String(paymentInfo?.Evidence);
                evidenceOfPayment = @"data:image/" + paymentInfo.MimeType + ";base64," + evidenceBase64String;
            }

          
            #region    All details

            var dob = entity.DOB;

            var kycInfos = new List<ChildView>();

            if (!string.IsNullOrEmpty(customer?.ProfileImageUrl))
            {
                kycInfos.Add(new ChildView { FieldLabel = "Profile Image ", FieldValue = $"{customer?.ProfileImageUrl}", IsFileDownload = true });
            }
            kycInfos.Add(new ChildView { FieldLabel = "FirstName", FieldValue = entity?.FirstName });
            kycInfos.Add(new ChildView { FieldLabel = "MiddleName", FieldValue = entity?.MiddleName });
            kycInfos.Add(new ChildView { FieldLabel = "LastName", FieldValue = entity?.LastName });
            kycInfos.Add(new ChildView { FieldLabel = "Gender", FieldValue = entity.Gender.ToString() });
            kycInfos.Add(new ChildView { FieldLabel = "Member Type", FieldValue = customer?.MemberType.ToString() });
            kycInfos.Add(new ChildView { FieldLabel = "Department", FieldValue = entity?.Department?.Name });
            if (dob != null)
            {
                kycInfos.Add(new ChildView { FieldLabel = "Date of Birth", FieldValue = $"{entity?.DOB.Value.Date.ToString("MM/dd/yyyy")}" });
            }
            else
            {
                kycInfos.Add(new ChildView { FieldLabel = "Date of Birth", FieldValue = $"N/A" });
            }
            kycInfos.Add(new ChildView { FieldLabel = "JobRole", FieldValue = entity?.JobRole });
            kycInfos.Add(new ChildView { FieldLabel = "Date of Employment", FieldValue = $"{entity?.DateOfEmployment.Value.Date.ToString("MM/dd/yyyy")}" });

            if (!string.IsNullOrEmpty(evidenceOfPayment))
            {
                kycInfos.Add(new ChildView { FieldLabel = "Evidence of Enrollment payment", FieldValue = $"{evidenceOfPayment}", IsFileDownload = true });
            }

            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = $"Member KYC  Request Details({entity.MembershipId})",
                Children = kycInfos
            });

            
            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = "Means of Identification",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Identification Type",FieldValue=entity?.IdentificationType},
                     new ChildView { FieldLabel ="IdentificationNumber",FieldValue=entity?.IdentificationNumber},
                     new ChildView { FieldLabel ="MembershipId",FieldValue=entity?.MembershipId},
                     new ChildView { FieldLabel ="CAI",FieldValue=entity?.CAI},
                     new ChildView { FieldLabel ="Rank",FieldValue=entity?.Rank},
                     new ChildView { FieldLabel ="RetireeNumber",FieldValue=entity?.RetireeNumber},
                     new ChildView { FieldLabel = "Identification Image ", FieldValue = $"{customer?.IdentificationUrl}", IsFileDownload = true }
                 }
            });
             
            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = "Contact Details",
                Children = new List<ChildView>
                 {
                    new ChildView { FieldLabel ="Primary Email",FieldValue=entity?.PrimaryEmail},
                     new ChildView { FieldLabel ="Secondary Email",FieldValue=entity?.SecondaryEmail},
                     new ChildView { FieldLabel ="Primary Phone",FieldValue=entity?.PrimaryPhone},
                     new ChildView { FieldLabel ="Secondary Phone",FieldValue=entity?.SecondaryPhone},
                 }
            });



            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = "Location",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Country",FieldValue=entity?.Country},
                     new ChildView { FieldLabel ="State of Origin",FieldValue=entity?.StateOfOrigin},
                     new ChildView { FieldLabel ="Residential Address",FieldValue=entity?.ResidentialAddress},
                       new ChildView { FieldLabel ="Office Address",FieldValue=entity?.OfficeAddress},
                       new ChildView { FieldLabel ="Address",FieldValue=entity?.Address},

                 }
            });




            DateTime kycStartDate = entity.KycStartDate ?? DateTime.Now;
            DateTime kycCompletedDate = entity.KycCompletedDate ?? DateTime.Now;
            DateTime kycSubmittedOn = entity.KycSubmittedOn ?? DateTime.Now;

            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = "KYC Dates",
                Children = new List<ChildView>
                 {
                    new ChildView { FieldLabel ="Kyc Start Date",FieldValue=$"{kycStartDate.ToString("dddd, dd MMMM yyyy")}"},
                    new ChildView { FieldLabel ="Kyc Completion Date",FieldValue=$"{kycCompletedDate.ToString("dddd, dd MMMM yyyy")}"},
                    new ChildView { FieldLabel ="Kyc Submitted On",FieldValue=$"{kycSubmittedOn.ToString("dddd, dd MMMM yyyy")}"},

                 }
            });


            int sn = 1;
            if (kins != null)
                if (kins.Any())
                {
                    var kinsChildren = new List<TabularView>();

                    foreach (var g in kins)
                    {
                        
                        kinsChildren.Add(new TabularView { FieldSN = sn, FieldValue = g.FirstName, FieldValue2 = g.LastName, FieldValue3 = g.Phone, FieldValue4 = g.Email, FieldValue5 = g.Address, FieldValue6 = g.Relationship });
                        sn++;
                    }
                   
                    partialViews.Add(new ApprovalPartialView()
                    {
                        ViewId = viewId++,
                        Title = "Next of Kin Details",
                        IsMultipleFields = true,
                        IsTabularView = true,
                        FieldHeaders = new List<string> { "  #", "First Name", "Last Name", "Contact No.", "Email", "Address", "Relationship" },
                        ChildRows = kinsChildren
                    });
                }

           


            sn = 0;
            if (banks != null)
                if (banks.Any())
                {
                    var bnkChildren = new List<ChildView>();

                    foreach (var b in banks)
                    {
                        sn++;
                        bnkChildren.Add(new ChildView { FieldLabel = b.Bank.Name, FieldValue = $"{b.AccountNumber}" });
                    }
                    partialViews.Add(new ApprovalPartialView
                    {
                        ViewId = viewId++,
                        Title = "Bank Details",
                        IsTabularView = false,
                        Children = bnkChildren
                    });
                }




            #endregion
            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            details.ApprovalId = approvalId;
            UpdateApproval(details);

        }
        private void GetRetireeSwitchDetails(string payload, string approvalId)
        {
            var rsp = new CommandResult<string>();
            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;

            var request = System.Text.Json.JsonSerializer.Deserialize<CreateRetireeSwitchCommand>(payload);
            if (request == null) return;

            var entity = _dbContext.MemberProfiles.Include(m => m.Department).FirstOrDefault(m => m.Id == request.MemberProfileId);


            #region    All details

            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = $"Member KYC  Request Details({entity.MembershipId})",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="FirstName",FieldValue=entity.FirstName},
                     new ChildView { FieldLabel ="MiddleName",FieldValue=entity.MiddleName},
                     new ChildView { FieldLabel ="LastName",FieldValue=entity.LastName},
                     new ChildView { FieldLabel ="Date of Birth",FieldValue=$"{entity.DOB.Value.Date.ToString("MM/dd/yyyy")}"},
                     new ChildView { FieldLabel ="Gender",FieldValue=entity.Gender.ToString()},
                     new ChildView { FieldLabel ="Member Type",FieldValue=entity.YearsOfService.ToString()},
                     new ChildView { FieldLabel ="Department",FieldValue=entity?.Department?.Name},
                     new ChildView { FieldLabel ="JobRole",FieldValue=entity.JobRole},
                 }
            });


            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = "Means of Identification",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Identification Type",FieldValue=entity.IdentificationType},
                     new ChildView { FieldLabel ="IdentificationNumber",FieldValue=entity.IdentificationNumber},
                     new ChildView { FieldLabel ="MembershipId",FieldValue=entity.MembershipId},
                     new ChildView { FieldLabel ="CAI",FieldValue=entity.CAI},
                     new ChildView { FieldLabel ="Rank",FieldValue=entity.Rank},
                     new ChildView { FieldLabel ="RetireeNumber",FieldValue=entity.RetireeNumber},
                 }
            });


            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = "Contact Details",
                Children = new List<ChildView>
                 {
                    new ChildView { FieldLabel ="Primary Email",FieldValue=entity?.PrimaryEmail},
                     new ChildView { FieldLabel ="Secondary Email",FieldValue=entity?.SecondaryEmail},
                     new ChildView { FieldLabel ="Primary Phone",FieldValue=entity?.PrimaryPhone},
                     new ChildView { FieldLabel ="Secondary Phone",FieldValue=entity?.SecondaryPhone},
                 }
            });


            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            #endregion

            details.ApprovalId = approvalId;
             UpdateApproval(details);
        }
        private void GetBulkUploadDetails(string payload, string approvalId)
        {
            var rsp = new CommandResult<string>();
            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;

            var entity = System.Text.Json.JsonSerializer.Deserialize<CreateMemberBulkUploadCommand>(payload);
            if (entity == null ) return;

            #region    All details

            var tempUploads = _dbContext.MemberBulkUploadTemp.Where(b => b.SessionId == entity.SessionId).ToList();
            if (!tempUploads.Any()) return;

            var date = tempUploads[0].DateCreated ?? tempUploads[0].DateCreated;

           
            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = $"Member Bulk Upload Registration({entity.SessionId})",
                Children = new List<ChildView>
                 {
                    new ChildView { FieldLabel ="Size",FieldValue=$"{tempUploads.Count.ToString()}"},
                    new ChildView { FieldLabel ="Session ID",FieldValue=$"{entity.SessionId}"},
                    new ChildView { FieldLabel ="Upload Date",FieldValue=$"{date.Value.ToString("dddd, dd MMMM yyyy")}"},
                 }
            });
            
            var childRows = new List<TabularView>();
            var sn = 1;
            foreach (var c in tempUploads)
            {
                childRows.Add(new TabularView { FieldSN = sn, FieldValue = c.FirstName, FieldValue2 = c.LastName, FieldValue3 = c.Gender, FieldValue4 = c.Email, FieldValue5 = c.PhoneNumber, FieldValue6 = c.MembershipNumber, FieldValue7 = c.MemberType.ToString(), FieldValue8 = c.Status });
                sn++;
            }

            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = $"Propective Members",
                IsMultipleFields = true,
                IsTabularView = true,
                FieldHeaders = new List<string> { "  #","First Name", "Last Name", "Gender", "Email", "Phone", "Member ID", "MemberType", "Status", },
                ChildRows= childRows
            });

            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            #endregion

            details.ApprovalId = approvalId;
             UpdateApproval(details);
        }
        private void GetSavingsAccountApplicationDetails(string payload, string approvalId)
        {
            var rsp = new CommandResult<string>();

            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;


            var entity = System.Text.Json.JsonSerializer.Deserialize<CreateSavingsAccountApplicationCommand>(payload);
            if (entity == null) return;

            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == entity.CustomerId);
            if (customer == null) return;


            DepositProduct product = _dbContext.DepositProducts.Where(d => d.Id == entity.DepositProductId)
                .Include(p => p.PublishedByUser)
                .Include(p => p.DefaultCurrency).FirstOrDefault();
            if (product == null) return;


            #region    All details

            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = $"Fixed Deposit Account Application Request({product.Name}-{product.Code})",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Amount",FieldValue=$"{entity.Amount:n}"},
                     //new ChildView { FieldLabel ="ApplicationNo",FieldValue=entity.ApplicationNo},
                      new ChildView { FieldLabel ="Application Date",FieldValue=$"{DateTime.Now.ToString("dddd, dd MMMM yyyy")}"},
                 }
            });

            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = "Deposit Product Details",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Product Code",FieldValue=product.Code},
                     new ChildView { FieldLabel ="Name",FieldValue=product.Name},
                     new ChildView { FieldLabel ="ShortName",FieldValue=product.ShortName},
                     new ChildView { FieldLabel ="MinimumAge",FieldValue=product.MinimumAge.ToString()},
                     new ChildView { FieldLabel ="MaximumAge",FieldValue=product.MaximumAge.ToString()},
                     new ChildView { FieldLabel =$"Tenure({nameof(product.Tenure)})",FieldValue=$"{product.TenureValue:n}"},
                      new ChildView { FieldLabel ="Publication Type",FieldValue=product.PublicationType.ToString()},
                       new ChildView { FieldLabel ="Published By",FieldValue=product.PublishedByUser.UserName},
                       new ChildView { FieldLabel ="Default Currency",FieldValue=product.DefaultCurrency.Name},
                 }
            });


            partialViews.Add(GetCustomerDetails(viewId, customer));

            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            #endregion

            details.ApprovalId = approvalId;
             UpdateApproval(details);
        }
        private void GetFDLiquidationDetails(string payload, string approvalId)
        {
            var rsp = new CommandResult<string>();

            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;
            string LiquidationAccountType = "";
            string LiquidationAccount = "";
            string LiquidationAccountNo = "";
            string downloadLink = "";


            var entity = System.Text.Json.JsonSerializer.Deserialize<CreateFixedDepositLiquidationCommand>(payload);
            if (entity == null || string.IsNullOrEmpty(entity.CreatedByUserId)) return;

            var fixedDepositAccount = _dbContext.FixedDepositAccounts.Where(d => d.Id == entity.FixedDepositAccountId).Include(s => s.Customer)
                .Include(c => c.DepositAccount).FirstOrDefault();
            if (fixedDepositAccount == null) return;

            var customer = fixedDepositAccount?.Customer;
            if (customer == null) return;

            var liquidationCharges = _dbContext.FixedDepositLiquidationCharges.Where(c => c.FixedDepositLiquidationId == entity.FixedDepositAccountId).ToList();


            var masterView = _dbContext.DepositAccountsMasterView.Where(m => m.Id == fixedDepositAccount.Id).FirstOrDefault();

            if (entity.LiquidationAccountType == WithdrawalAccountType.SAVINGS_ACCOUNT)
            {
                var savingsAccount = _dbContext.SavingsAccounts.Where(s => s.Id == entity.LiquidationAccountId).FirstOrDefault();
                LiquidationAccountType = entity.LiquidationAccountType.ToString();
                LiquidationAccount = $"Savings Account";
                LiquidationAccountNo = $"{savingsAccount.AccountNo}";
            }

            if (entity.LiquidationAccountType == WithdrawalAccountType.SPECIAL_DEPOSIT_ACCOUNT)
            {
                var specialDepositAccount = _dbContext.SpecialDepositAccounts.Where(s => s.Id == entity.LiquidationAccountId).FirstOrDefault();
                LiquidationAccountType = entity.LiquidationAccountType.ToString();
                LiquidationAccount = $"Special Deposit Account";
                LiquidationAccountNo = $"{specialDepositAccount.AccountNo}";
            }


            if (entity.LiquidationAccountType == WithdrawalAccountType.EXISTING_BANK_ACCOUNT)
            {
                var customerBankAccount = _dbContext.CustomerBankAccounts.Where(s => s.Id == entity.LiquidationAccountId).FirstOrDefault();
                LiquidationAccountType = entity.LiquidationAccountType.ToString();
                LiquidationAccount = $"customer Bank Account";
                LiquidationAccountNo = $"{customerBankAccount.AccountNumber}";
            }

            #region    All details
            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = $"Fixed Deposit Account Change In Maturity Request({fixedDepositAccount.AccountNo})",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Liquidation Account",FieldValue=LiquidationAccount},
                      new ChildView { FieldLabel ="Liquidation Account Number",FieldValue=LiquidationAccountNo},
                      new ChildView { FieldLabel ="Maturity Instruction Type",FieldValue=entity.LiquidationAccountType.ToString()},
                      new ChildView { FieldLabel ="Application Date",FieldValue=$"{DateTime.Now.ToString("dddd, dd MMMM yyyy")}"},
                 }
            });

            if (liquidationCharges != null)
                if (liquidationCharges.Any())
                {
                    var childViews = new List<TabularView>();
                    int sn = 0;
                    foreach (var k in liquidationCharges)
                    {
                        sn++;
                        childViews.Add(new TabularView { FieldSN = sn, FieldLabel = nameof(k.ChargeType), FieldValue = $"{k.LiquidationCharge:n}" });
                    }
                    partialViews.Add(new ApprovalPartialView
                    {
                        ViewId = viewId++,
                        Title = "Liquidation Charges",
                        IsTabularView = true,
                        ChildRows = childViews
                    });
                }

            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = "Fixed Deposit Account Details",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Fixed Deposit AccountNo",FieldValue=fixedDepositAccount.AccountNo},
                     new ChildView { FieldLabel ="Interest Rate",FieldValue=$"{fixedDepositAccount.InterestRate:n}"},
                     new ChildView { FieldLabel ="Ledger Balance",FieldValue=$"{masterView.LedgerBalance:n}"},
                      new ChildView { FieldLabel ="Interest Balance",FieldValue=$"{masterView.InterestEarnedAccountId_LedgerBalance:n}"},
                 }
            });

            partialViews.Add(GetCustomerDetails(viewId, customer));

            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            #endregion
            details.ApprovalId = approvalId;
            UpdateApproval(details);
        }
        private void GetFDChangeInMaturityDetails(string payload, string approvalId)
        {
            var rsp = new CommandResult<string>();

            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;
            string LiquidationAccountType = "";
            string LiquidationAccount = "";
            string LiquidationAccountNo = "";
            string downloadLink = "";


            var entity = System.Text.Json.JsonSerializer.Deserialize<CreateFixedDepositChangeInMaturityCommand>(payload);
            if (entity == null || string.IsNullOrEmpty(entity.CreatedByUserId)) return;

            var fixedDepositAccount = _dbContext.FixedDepositAccounts.Where(s => s.Id == entity.FixedDepositAccountId)
          .Include(s => s.Customer).Include(c => c.DepositAccount).FirstOrDefault();
            if (fixedDepositAccount == null) return;

            var customer = fixedDepositAccount?.Customer;
            if (customer == null) return;

            var masterView = _dbContext.DepositAccountsMasterView.Where(m => m.Id == fixedDepositAccount.Id).FirstOrDefault();

            if (entity.LiquidationAccountType == WithdrawalAccountType.SAVINGS_ACCOUNT)
            {
                var savingsAccount = _dbContext.SavingsAccounts.Where(s => s.Id == entity.LiquidationAccountId).FirstOrDefault();
                LiquidationAccountType = entity.LiquidationAccountType.ToString(); 
                LiquidationAccount = $"Savings Account";
                LiquidationAccountNo = $"{savingsAccount.AccountNo}";
            }

            if (entity.LiquidationAccountType == WithdrawalAccountType.SPECIAL_DEPOSIT_ACCOUNT)
            {
                var specialDepositAccount = _dbContext.SpecialDepositAccounts.Where(s => s.Id == entity.LiquidationAccountId).FirstOrDefault();
                LiquidationAccountType = entity.LiquidationAccountType.ToString();
                LiquidationAccount = $"Special Deposit Account";
                LiquidationAccountNo = $"{specialDepositAccount.AccountNo}";
            }


            if (entity.LiquidationAccountType == WithdrawalAccountType.EXISTING_BANK_ACCOUNT)
            {
                var customerBankAccount = _dbContext.CustomerBankAccounts.Where(s => s.Id == entity.LiquidationAccountId).FirstOrDefault();
                LiquidationAccountType = entity.LiquidationAccountType.ToString();
                LiquidationAccount = $"customer Bank Account";
                LiquidationAccountNo = $"{customerBankAccount.AccountNumber}";
            }


            #region    All details
            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = $"Fixed Deposit Account Change In Maturity Request({fixedDepositAccount.AccountNo})",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Liquidation Account",FieldValue=LiquidationAccount},
                      new ChildView { FieldLabel ="Liquidation Account Number",FieldValue=LiquidationAccountNo},
                      new ChildView { FieldLabel ="Current Maturity Instruction Type",FieldValue=entity.MaturityInstructionType.ToString()},
                       new ChildView { FieldLabel ="Old Maturity Instruction Type",FieldValue=fixedDepositAccount.MaturityInstructionType.ToString()},
                       new ChildView { FieldLabel ="Liquidation Account Type",FieldValue=entity.LiquidationAccountType.ToString()},
                      new ChildView { FieldLabel ="Application Date",FieldValue=$"{DateTime.Now.ToString("dddd, dd MMMM yyyy")}"},
                 }
            });


            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = "Fixed Deposit Account Details",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Fixed Deposit AccountNo",FieldValue=fixedDepositAccount.AccountNo},
                     new ChildView { FieldLabel ="Interest Rate",FieldValue=$"{fixedDepositAccount.InterestRate:n}"},
                     new ChildView { FieldLabel ="Available Balance",FieldValue=$"{masterView?.LedgerBalance:n}"},
                 }
            });
            partialViews.Add(GetCustomerDetails(viewId, customer));

            #endregion
            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            details.ApprovalId = approvalId;
             UpdateApproval(details);
        }
        private void GetFDApplicationDetails(string payload, string approvalId)
        {
            var rsp = new CommandResult<string>();

            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;
            string LiquidationAccountType = "";
            string LiquidationAccount = "";
            string LiquidationAccountNo = "";
            string downloadLink = "";


            var entity = System.Text.Json.JsonSerializer.Deserialize<CreateFixedDepositAccountApplicationCommand>(payload);
            if (entity == null) return;

            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == entity.CustomerId);
            if (customer == null) return;


            DepositProduct product = _dbContext.DepositProducts.Where(d => d.Id == entity.DepositProductId)
                .Include(p => p.PublishedByUser)
                .Include(p => p.DefaultCurrency).FirstOrDefault();
            if (product == null) return;


            if (entity.LiquidationAccountType == WithdrawalAccountType.SAVINGS_ACCOUNT)
            {
                var savingsAccount = _dbContext.SavingsAccounts.Where(s => s.Id == entity.LiquidationAccountId).FirstOrDefault();
                LiquidationAccountType = entity.LiquidationAccountType.ToString();
                LiquidationAccount = $"Savings Account";
                LiquidationAccountNo = $"{savingsAccount.AccountNo}";
            }

            if (entity.LiquidationAccountType == WithdrawalAccountType.SPECIAL_DEPOSIT_ACCOUNT)
            {
                var specialDepositAccount = _dbContext.SpecialDepositAccounts.Where(s => s.Id == entity.LiquidationAccountId).FirstOrDefault();
                LiquidationAccountType = entity.LiquidationAccountType.ToString();
                LiquidationAccount = $"Special Deposit Account";
                LiquidationAccountNo = $"{specialDepositAccount.AccountNo}";
            }

            if (entity.LiquidationAccountType == WithdrawalAccountType.EXISTING_BANK_ACCOUNT)
            {
                var customerBankAccount = _dbContext.CustomerBankAccounts.Where(s => s.Id == entity.LiquidationAccountId).FirstOrDefault();
                LiquidationAccountType = entity.LiquidationAccountType.ToString();
                LiquidationAccount = $"customer Bank Account";
                LiquidationAccountNo = $"{customerBankAccount.AccountNumber}";

            }



            #region    All details
            var childViews = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Amount",FieldValue=$"{entity.Amount:n}"},
                     new ChildView { FieldLabel ="InterestRate",FieldValue=$"{entity.InterestRate:n}"},
                     new ChildView { FieldLabel ="Liquidation Account",FieldValue=LiquidationAccount},
                      new ChildView { FieldLabel ="Liquidation Account Number",FieldValue=LiquidationAccountNo},
                      new ChildView { FieldLabel ="Deposit Funding Source Type",FieldValue=entity.ModeOfPayment.ToString()},
                      new ChildView { FieldLabel ="Maturity Instruction Type",FieldValue=entity.MaturityInstructionType.ToString()},
                      new ChildView { FieldLabel ="Maturity Instruction Type",FieldValue=entity.LiquidationAccountType.ToString()},
                      new ChildView { FieldLabel ="Application Date",FieldValue=$"{DateTime.Now.ToString("dddd, dd MMMM yyyy")}"},
                 };

            if (entity.LiquidationAccountType == WithdrawalAccountType.EXISTING_BANK_ACCOUNT)
            {
                childViews.Add(new ChildView { FieldLabel = "Reciept", FieldValue = $"{entity.Document}", IsFileDownload = true });
            }
            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = $"Fixed Deposit Account Application Request({product.Name}-{product.Code})",
                Children = childViews
            });

            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = "Deposit Product Details",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Product Code",FieldValue=product.Code},
                     new ChildView { FieldLabel ="Name",FieldValue=product.Name},
                     new ChildView { FieldLabel ="ShortName",FieldValue=product.ShortName},
                     new ChildView { FieldLabel ="MinimumAge",FieldValue=product.MinimumAge.ToString()},
                     new ChildView { FieldLabel ="MaximumAge",FieldValue=product.MaximumAge.ToString()},
                     new ChildView { FieldLabel =$"Tenure({nameof(product.Tenure)})",FieldValue=$"{product.TenureValue:n}"},
                      new ChildView { FieldLabel ="Publication Type",FieldValue=product.PublicationType.ToString()},
                       new ChildView { FieldLabel ="Published By",FieldValue=product.PublishedByUser.UserName},
                       new ChildView { FieldLabel ="Default Currency",FieldValue=product.DefaultCurrency.Name},
                 }
            });



            partialViews.Add(GetCustomerDetails(viewId, customer));


            #endregion
            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            details.ApprovalId = approvalId;
            UpdateApproval(details);
        }
        private void GetLoanApplicationDetails(string payload, string entityId, string approvalId)
        {

            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;

            var entity = _dbContext.LoanApplications
                .Include(l=>l.Customer)
                .FirstOrDefault(l=>l.Id == entityId);
            
            if (entity == null) return;

            var customer = entity.Customer;  
            if (customer == null) return;

            var applicationItems = _dbContext.LoanApplicationItems.Where(l=>l.LoanApplicationId==entity.Id).ToList();


            LoanProduct product = _dbContext.LoanProducts
                .Include(p => p.PublishedByUser)
                .Include(p => p.DefaultCurrency)
                .FirstOrDefault(d => d.Id == entity.LoanProductId);
            if (product == null) return;

            var sdP = new SpecialDepositAccount();


            #region    All details

            var UsesSD = entity.UseSpecialDeposit ? "YES" : "NO";


            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = $"Loan Product Application Request({product.Name}-{product.Code})",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Principal Amount",FieldValue=$"{entity.Principal:n}"},
                     new ChildView { FieldLabel =$"Tenure({nameof(entity.TenureUnit)})",FieldValue=$"{entity.TenureValue:n}"},
                      new ChildView { FieldLabel ="Repayment Start Date",FieldValue=$"{entity.RepaymentCommencementDate.ToString("dddd, dd MMMM yyyy")}"},
                      new ChildView { FieldLabel ="Application Date",FieldValue=$"{DateTime.Now.ToString("dddd, dd MMMM yyyy")}"},
                      new ChildView { FieldLabel ="UseSpecialDeposit",FieldValue=$"{UsesSD}"},
                 }
            });



            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = "Loan Product Details",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Product Code",FieldValue=product.Code},
                     new ChildView { FieldLabel ="Name",FieldValue=product.Name},
                     new ChildView { FieldLabel ="ShortName",FieldValue=product.ShortName},
                     new ChildView { FieldLabel ="MinimumAge",FieldValue=product.MinimumAge.ToString()},
                     new ChildView { FieldLabel ="MaximumAge",FieldValue=product.MaximumAge.ToString()},
                      new ChildView { FieldLabel ="Publication Type",FieldValue=product.PublicationType.ToString()},
                       new ChildView { FieldLabel ="Published By",FieldValue=product.PublishedByUser.UserName},
                       new ChildView { FieldLabel ="Default Currency",FieldValue=product.DefaultCurrency.Name},
                 }
            });

            var loanDestAccounts = new List<TabularView>();
            if (!entity.UseSpecialDeposit)
            {
                var destAccount = _dbContext.CustomerBankAccounts.FirstOrDefault(c => c.Id == entity.CustomerDisbursementAccountId);
                if (destAccount != null)
                    loanDestAccounts.Add(new TabularView { FieldSN = 1, FieldLabel = "Customer Disbursement Account", FieldValue = $"Customer Disbursement Account({destAccount.AccountNumber})" });

                if (loanDestAccounts.Any())
                {
                    partialViews.Add(new ApprovalPartialView
                    {
                        ViewId = viewId++,
                        Title = " Disbursement Accounts",
                        IsTabularView = true,
                        ChildRows = loanDestAccounts
                    });
                }
               
            }

            if (entity.UseSpecialDeposit)
            {
                sdP = _dbContext.SpecialDepositAccounts.FirstOrDefault(c => c.CustomerId == entity.CustomerId && c.DepositProductId == entity.LoanProductId);
                if (sdP != null)
                    loanDestAccounts.Add(new TabularView { FieldSN = 1, FieldLabel = "Special Deposit Account", FieldValue = $"Special Deposit Account  ({sdP.AccountNo})" });


                if (loanDestAccounts.Any())
                {
                    partialViews.Add(new ApprovalPartialView
                    {
                        ViewId = viewId++,
                        Title = " Disbursement Accounts",
                        IsTabularView = true,
                        ChildRows = loanDestAccounts
                    });
                }
                  
            }

            



            var appliances = new List<TabularView>();

            if (applicationItems != null)
                if (applicationItems.Any())
                {
                    var sn = 1;
                    foreach (var item in applicationItems)
                    {
                        appliances.Add(new TabularView { FieldSN = sn, FieldValue = item.Name, FieldValue2 = item.BrandName, FieldValue3 = item.Model, FieldValue4 = item.Color, FieldValue5 = $"{item.Amount:n}"  });
                        sn++;
                    }

                    partialViews.Add(new ApprovalPartialView
                    {
                        ViewId = viewId++,
                        Title = $"Appliance Details",
                        IsMultipleFields = true,
                        IsTabularView = true,
                        FieldHeaders = new List<string> { "    #", "Name", "Brand Name", "Model", "Color","Amount"  },
                        ChildRows = appliances
                    });
                }


            


            partialViews.Add(GetLoanGuarrantorDetails(viewId, entity.Id));
            partialViews.Add(GetCustomerDetails(viewId++, customer));

            #endregion
            
            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            details.ApprovalId = approvalId;
             UpdateApproval(details);
        }
        private void GetSDApplicationDetails(string payload, string approvalId)
        {

            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;


            var entity = System.Text.Json.JsonSerializer.Deserialize<CreateSpecialDepositAccountApplicationCommand>(payload);
            if (entity == null) return;

            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == entity.CustomerId);
            if (customer == null) return;


            DepositProduct product = _dbContext.DepositProducts.Where(d => d.Id == entity.DepositProductId)
                .Include(p => p.PublishedByUser)
                .Include(p => p.DefaultCurrency).FirstOrDefault();
            if (product == null) return;


            #region    All details

            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = $"Special Deposit Account Application Request({product.Name}-{product.Code})",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Amount",FieldValue=$"{entity.Amount:n}"},
                     new ChildView { FieldLabel ="InterestRate",FieldValue=$"{entity.InterestRate:n}"},
                     new ChildView { FieldLabel ="PaymentBankName",FieldValue=entity.PaymentBankName},
                     new ChildView { FieldLabel ="PaymentAccountNumber",FieldValue=entity.PaymentAccountNumber},
                      new ChildView { FieldLabel ="Deposit Funding Source Type",FieldValue=entity.ModeOfPayment.ToString()},
                      new ChildView { FieldLabel ="Application Date",FieldValue=$"{DateTime.Now.ToString("dddd, dd MMMM yyyy")}"},
                 }
            });


            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = "Deposit Product Details",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Product Code",FieldValue=product.Code},
                     new ChildView { FieldLabel ="Name",FieldValue=product.Name},
                     new ChildView { FieldLabel ="ShortName",FieldValue=product.ShortName},
                     new ChildView { FieldLabel ="MinimumAge",FieldValue=product.MinimumAge.ToString()},
                     new ChildView { FieldLabel ="MaximumAge",FieldValue=product.MaximumAge.ToString()},
                     new ChildView { FieldLabel =$"Tenure({nameof(product.Tenure)})",FieldValue=$"{product.TenureValue:n}"},
                      new ChildView { FieldLabel ="Publication Type",FieldValue=product.PublicationType.ToString()},
                       new ChildView { FieldLabel ="Published By",FieldValue=product.PublishedByUser.UserName},
                       new ChildView { FieldLabel ="Default Currency",FieldValue=product.DefaultCurrency.Name},
                 }
            });



            partialViews.Add(GetCustomerDetails(viewId, customer));


            #endregion
            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            details.ApprovalId = approvalId;
             UpdateApproval(details);
        }
        private void GetSavingsIncreaseDecreaseDetails(string payload, string approvalId)
        {
            var rsp = new CommandResult<string>();
            var transferToSavingsAccount = new SavingsAccount();
            var transferToFixedDepositAccount = new FixedDepositAccount();
            var savingsAccount = new SavingsAccount();

            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;


            var entity = System.Text.Json.JsonSerializer.Deserialize<CreateSavingsIncreaseDecreaseCommand>(payload);
            if (entity == null) return;


            savingsAccount = _dbContext.SavingsAccounts.Where(s => s.Id == entity.SavingsAccountId)
              .Include(s => s.Customer).FirstOrDefault();
            if (savingsAccount == null) return;

            var customer = savingsAccount?.Customer;
            if (customer == null) return;


            var increaseDecrease = _dbContext.SavingsIncreaseDecreases.Where(c => c.SavingsAccountId == savingsAccount.Id).FirstOrDefault();
            if (savingsAccount == null) return;


            #region    All details

            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = $"Savings Account {nameof(entity.ContributionChangeRequest)} Request({savingsAccount.AccountNo})",
                Children = new List<ChildView>
                 {
                      new ChildView { FieldLabel ="Transaction",FieldValue=entity.ContributionChangeRequest.ToString()},
                     new ChildView { FieldLabel ="New Amount",FieldValue=$"{entity.Amount:n}"},
                     new ChildView { FieldLabel ="Old Amount",FieldValue=$"{savingsAccount.PayrollAmount:n}"},
                      new ChildView { FieldLabel ="Application Date",FieldValue=$"{DateTime.Now.ToString("dddd, dd MMMM yyyy")}"},
                      new ChildView { FieldLabel ="Deposit Funding Source Type",FieldValue=entity.ContributionChangeRequest.ToString()},
                 }
            });



            partialViews.Add(GetCustomerDetails(viewId, customer));

            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            #endregion

            details.ApprovalId = approvalId;
             UpdateApproval(details);
        }
        private void GetSavingsCashAdditionDetails(string payload, string approvalId)
        {
            var rsp = new CommandResult<string>();
            var transferToSavingsAccount = new SavingsAccount();
            var transferToFixedDepositAccount = new FixedDepositAccount();
            var savingsAccount = new SavingsAccount();

            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;


            var entity = System.Text.Json.JsonSerializer.Deserialize<CreateSavingsCashAdditionCommand>(payload);
            if (entity == null || string.IsNullOrEmpty(entity.CreatedByUserId)) return;

            savingsAccount = _dbContext.SavingsAccounts.Where(s => s.Id == entity.SavingsAccountId)
             .Include(s => s.Customer).FirstOrDefault();
            if (entity == null) return;

            var customer = savingsAccount?.Customer;
            if (customer == null) return;


            var cashAddition = _dbContext.SavingsCashAdditions.Where(c => c.SavingsAccountId == savingsAccount.Id).Include(c => c.CustomerPaymentDocument).FirstOrDefault();
            if (cashAddition == null) return;


            #region    All details

            var cashAdditionPartial = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Transaction",FieldValue="Cash Addition"},
                     new ChildView { FieldLabel ="Amount",FieldValue=$"{entity.Amount:n}"},
                      new ChildView { FieldLabel ="Application Date",FieldValue=$"{DateTime.Now.ToString("dddd, dd MMMM yyyy")}"},
                      new ChildView { FieldLabel ="Deposit Funding Source Type",FieldValue=entity.ModeOfPayment.ToString()},


                 };

            if (cashAddition.ModeOfPayment == DepositFundingSourceType.BANK_TRANSFER)
            {
                cashAdditionPartial.Add(new ChildView { FieldLabel = "Reciept", FieldValue = $"{entity.Document}", IsFileDownload = true });
            }
            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = $"Savings Cash Addition Request({savingsAccount.AccountNo})",
                Children = cashAdditionPartial
            });



            partialViews.Add(GetCustomerDetails(viewId, customer));

            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            #endregion

            details.ApprovalId = approvalId;
             UpdateApproval(details);
        }
        private void GetSDCashAdditionDetails(string payload, string approvalId)
        {
            var specialDepositAccount = new SpecialDepositAccount();

            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;


            var entity = System.Text.Json.JsonSerializer.Deserialize<CreateSpecialDepositCashAdditionCommand>(payload);
            if (entity == null) return;

            specialDepositAccount = _dbContext.SpecialDepositAccounts.Where(c => c.Id == entity.SpecialDepositAccountId)
                .Include(s=>s.Customer).Include(s => s.DepositAccount).FirstOrDefault();
            if (specialDepositAccount == null) return;

            var customer = specialDepositAccount?.Customer;
            if (customer == null) return;



            var cashAddition = _dbContext.SpecialDepositCashAdditions.Where(c => c.SpecialDepositAccountId == specialDepositAccount.Id).Include(c => c.CustomerPaymentDocument).FirstOrDefault();
            if (cashAddition == null) return;


            #region    All details
            var cashAdditionDetails = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Transaction",FieldValue="Cash Addition"},
                     new ChildView { FieldLabel ="Cash Addition",FieldValue=$"{entity.Amount:n}"},
                      new ChildView { FieldLabel ="Old Balance",FieldValue=$"{specialDepositAccount.DepositAccount.LedgerBalance:n}"},
                      new ChildView { FieldLabel ="Application Date",FieldValue=$"{DateTime.Now.ToString("dddd, dd MMMM yyyy")}"},
                      new ChildView { FieldLabel ="Deposit Funding Source Type",FieldValue=entity.ModeOfPayment.ToString()},

                 };

            if (cashAddition.ModeOfPayment == DepositFundingSourceType.BANK_TRANSFER)
            {
                cashAdditionDetails.Add(new ChildView { FieldLabel = "Reciept", FieldValue = $"{entity.Document}", IsFileDownload = true });
            }
            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = $"Special Deposit Cash Addition Request({specialDepositAccount?.AccountNo})",
                Children = cashAdditionDetails
            });



            partialViews.Add(GetCustomerDetails(viewId, customer));
            #endregion
            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            details.ApprovalId = approvalId;
             UpdateApproval(details);
        }
        private void GetSDWithdrawalDetails(string payload, string approvalId)
        {
            var withdrawToBank = new CustomerBankAccount();
            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;


              var request = System.Text.Json.JsonSerializer.Deserialize<CreateSpecialDepositWithdrawalCommand>(payload);
             var  entity= _dbContext.SpecialDepositAccounts.Where(s => s.Id == request.SpecialDepositSourceAccountId)
                .Include(s => s.Customer).FirstOrDefault();
            if (entity == null) return;

            var customer = entity.Customer;
            if (customer == null) return;


            withdrawToBank = _dbContext.CustomerBankAccounts.Where(c => c.Id == request.CustomerDestinationBankAccountId).Include(c => c.Bank).FirstOrDefault();
            if (withdrawToBank == null) return;

            #region    All details
            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = "Special Deposit Withdrawal Request",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Transaction",FieldValue="WithDrawal"},
                     new ChildView { FieldLabel ="Amount",FieldValue=$"{request.Amount:n}"},
                     new ChildView { FieldLabel ="Application Date",FieldValue=$"{DateTime.Now.ToString("dddd, dd MMMM yyyy")}"},
                 }
            });


            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = " Withdrawal Destination",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Bank",FieldValue=withdrawToBank.Bank.Name},
                     new ChildView { FieldLabel ="Account Name",FieldValue=withdrawToBank.AccountName},
                     new ChildView { FieldLabel ="Account Number",FieldValue=withdrawToBank.AccountNumber},
                 }
            });

            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = "Special Deposit Account",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Account No.",FieldValue=entity.AccountNo},
                 }
            });

            partialViews.Add(GetCustomerDetails(viewId, customer));


            #endregion
            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            details.ApprovalId = approvalId;
             UpdateApproval(details);
        }
        private void GetSDFundTransferDetails(string payload, string approvalId)
        {
            var rsp = new CommandResult<string>();
            var transferToSavingsAccount = new SavingsAccount();
            var transferToFixedDepositAccount = new FixedDepositAccount();
            var specialDepositAccount = new SpecialDepositAccount();

            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;
            string destinationType = "";
            string destinationAccountNo = "";


            var entity = System.Text.Json.JsonSerializer.Deserialize<CreateSpecialDepositFundTransferCommand>(payload);
            if (entity == null) return;


            specialDepositAccount = _dbContext.SpecialDepositAccounts.Where(s => s.Id == entity.SpecialDepositAccountId)
              .Include(s => s.Customer).FirstOrDefault();
            if (specialDepositAccount == null) return;

            var customer = specialDepositAccount?.Customer;
            if (customer == null) return;

            if (!string.IsNullOrEmpty(entity.SavingAccountDestinationId))
                transferToSavingsAccount = _dbContext.SavingsAccounts.FirstOrDefault(c => c.Id == entity.SavingAccountDestinationId);

            if (!string.IsNullOrEmpty(entity.FixedDepositDestinationAccountId))
                transferToFixedDepositAccount = _dbContext.FixedDepositAccounts.FirstOrDefault(c => c.Id == entity.FixedDepositDestinationAccountId);


            #region    All details
            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = $"Special Deposit Fund Transfer Request({specialDepositAccount.AccountNo})",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Transaction",FieldValue="Fund Transfer"},
                     new ChildView { FieldLabel ="Amount",FieldValue=$"{entity.Amount:n}"},
                      new ChildView { FieldLabel ="Application Date",FieldValue=$"{DateTime.Now.ToString("dddd, dd MMMM yyyy")}"},
                 }
            });


            if (entity.DestinationAccountType == DestinationAccountType.FIXED_DEPOSIT_ACCOUNT)
            {
                destinationType = "Fixed Deposit Account";
                destinationAccountNo = transferToFixedDepositAccount.AccountNo;
            }
            else
            {
                destinationType = "Savings Account";
                destinationAccountNo = transferToSavingsAccount.AccountNo;
            }

            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = " Fund Transfer Destination",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Deposit Account Type",FieldValue=destinationType},
                     new ChildView { FieldLabel ="Account Number",FieldValue=destinationAccountNo},
                 }
            });



            partialViews.Add(GetCustomerDetails(viewId, customer));


            #endregion
            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            details.ApprovalId = approvalId;
             UpdateApproval(details);
        }
        private void GetLoanOffSetDetails(string payload, string approvalId)
        {
            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;


            var entity = System.Text.Json.JsonSerializer.Deserialize<CreateLoanOffsetCommand>(payload);
            var loanAccount = _dbContext.LoanAccounts.Where(l => l.Id == entity.LoanAccountId).Include(l=>l.Customer).FirstOrDefault();

            if (entity == null) return;

            var customer = loanAccount.Customer; 
            if (customer == null) return;


            var specialDepositAccount = _dbContext.SpecialDepositAccounts.FirstOrDefault(c => c.Id == entity.SpecialDepositAccountId);
            var savingsAccount = _dbContext.SavingsAccounts.FirstOrDefault(c => c.Id == entity.SavingsAccountId);
            List<LoanOffSetCharge> LoanOffSetCharges = _dbContext.LoanOffSetCharges.Where(l => l.LoanOffsetId == entity.LoanAccountId).Include(c => c.OffsetCharge).ToList();

            #region    All details

            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = $"Loan Offset Request({loanAccount.AccountNo})",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Offset Amount",FieldValue=$"{entity.OffsetAmount:n}"},
                     new ChildView { FieldLabel ="Principal Balance",FieldValue=$"{entity.PrincipalBalance:n}"},
                     new ChildView { FieldLabel ="Interest Balance",FieldValue=$"{entity.InterestBalance:n}"},
                     new ChildView { FieldLabel ="Offset Amount",FieldValue=$"{entity.OffsetAmount:n}"},
                     new ChildView { FieldLabel ="Allowed Offset Type",FieldValue=$"{entity.AllowedOffsetType }" },
                     new ChildView { FieldLabel ="Loan Repayment Mode",FieldValue=$"{entity.LoanRepaymentMode }" },
                     new ChildView { FieldLabel ="Mode Of Payment",FieldValue=$"{entity.AllowedOffsetType.ToString() }" },
                     new ChildView { FieldLabel ="OffSet Repayment Date",FieldValue=$"{entity.OffSetRepaymentDate.ToString("dddd, dd MMMM yyyy")}"},
                     new ChildView { FieldLabel ="OffsetToBeCalculatedAfter",FieldValue=$"{entity.OffsetToBeCalculatedAfter.ToString("dddd, dd MMMM yyyy")}"},
                     new ChildView { FieldLabel ="DeductionStartAfter",FieldValue=$"{entity.DeductionStartAfter.ToString("dddd, dd MMMM yyyy")}"},

                 }
            });


            if (entity.LoanRepaymentMode == LoanRepaymentMode.BANK_TRANSFER)
            {
                var paymentInfo = _dbContext.CustomerPaymentDocuments.FirstOrDefault(m => m.Id == entity.CustomerPaymentDocumentId);

                var downloadAble = "";
                if (paymentInfo != null)
                {
                    downloadAble = @"data:image/" + paymentInfo.MimeType + ";base64," + paymentInfo?.Document;
                    partialViews.Add(new ApprovalPartialView
                    {
                        ViewId = viewId++,
                        Title = $"Evidence of Payment",
                        Children = new List<ChildView>
                         {
                              new ChildView { FieldLabel = "", FieldValue = $"{downloadAble}" , IsFileDownload = true  },
                         }
                    });
                }
            }

            partialViews.Add(GetCustomerDetails(viewId, customer));

            #endregion

            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            details.ApprovalId = approvalId;
             UpdateApproval(details);
        }
        private void GetLoanTopUpDetails(string payload, string approvalId)
        {

            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;


            var entity = System.Text.Json.JsonSerializer.Deserialize<CreateLoanTopupCommand>(payload);
            var loanAccount = _dbContext.LoanAccounts.Where(l => l.Id == entity.LoanAccountId)
                .Include(s => s.Customer).FirstOrDefault();



            if (entity == null) return;

            var customer = loanAccount.Customer;
            if (customer == null) return;

            
            var specialDepositAccount = _dbContext.SpecialDepositAccounts.FirstOrDefault(c => c.Id == entity.SpecialDepositAccountId);
            var customerBankAccount = _dbContext.CustomerBankAccounts.FirstOrDefault(c => c.Id == entity.CustomerBankAccountId);

            var accountDetails = "";
            if (entity.DestinationType == TopupFundingSourceType.EXISTING_BANK_ACCOUNT)
            {
                accountDetails = customerBankAccount?.AccountNumber;
            }
            
            if (entity.DestinationType == TopupFundingSourceType.SPECIAL_DEPOSIT_ACCOUNT)
            {
                accountDetails = specialDepositAccount?.AccountNo;
            }
            #region    All details
            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = $"Loan Topup Request({loanAccount?.AccountNo})",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Topup Amount",FieldValue=$"{entity.TopupAmount:n}"},
                     new ChildView { FieldLabel ="Principal Balance",FieldValue=$"{entity.PrincipalBalance:n}"},
                     new ChildView { FieldLabel ="Interest Balance",FieldValue=$"{entity.InterestBalance:n}"},
                     new ChildView { FieldLabel ="Destination Type",FieldValue=$"{entity.DestinationType }" },
                     new ChildView { FieldLabel ="Destination Account",FieldValue=$"{ accountDetails}"},
                         new ChildView { FieldLabel ="Commencement Date",FieldValue=$"{entity.CommencementDate.ToString("dddd, dd MMMM yyyy")}"},
                         new ChildView { FieldLabel ="Topup Date",FieldValue=$"{entity.TopupDate.ToString("dddd, dd MMMM yyyy")}"},

                 }
            });

            

            partialViews.Add(GetLoanGuarrantorDetails(viewId, loanAccount.LoanApplicationId));

            partialViews.Add(GetCustomerDetails(viewId++, customer));


            #endregion

            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            details.ApprovalId = approvalId;
            UpdateApproval(details);
        }
        private void  GetLoanDisbursementDetails(string payload, string approvalId)
        {
            var rsp = new CommandResult<string>();

            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;
            var title = "";


            var approval = _dbContext.Approvals.Where(p=>p.Id == approvalId).FirstOrDefault();


            var entity = _dbContext.LoanDisbursements.Where(x => x.Id == approval.EntityId).Include(l => l.LoanAccount).ThenInclude(l => l.Customer).FirstOrDefault();

            if (entity == null) return;

            if (approval?.ApprovalType == ApprovalType.LOAN_DISBURSEMENT)
                title = $"Loan Disbursement Request({entity?.LoanAccount?.AccountNo})";
            else
                title = title = $"Loan Topup Disbursement Request({entity?.LoanAccount?.AccountNo})";

           


            var customer = entity?.LoanAccount?.Customer; 
            if (customer == null) return;


            entity.SpecialDepositAccount = _dbContext.SpecialDepositAccounts.FirstOrDefault(c => c.Id == entity.SpecialDepositAccountId);
            entity.CustomerBankAccount = _dbContext.CustomerBankAccounts.FirstOrDefault(c => c.Id == entity.CustomerBankAccountId);
            entity.DisbursementAccount = _dbContext.CompanyBankAccounts.FirstOrDefault(c => c.Id == entity.DisbursementAccountId);
            List<LoanDisbursementCharge> charges = _dbContext.LoanDisbursementCharges.Where(l => l.LoanDisbursementId == entity.Id).Include(c => c.DisbursementCharge).ToList();

            


            var accountDetails = "";
            if (entity.DisbursementMode == LoanDisbursementMode.SPECIAL_DEPOSIT)
            {
                accountDetails = entity.SpecialDepositAccount.AccountNo;
            }
            
            if (entity.DisbursementMode == LoanDisbursementMode.BANK_TRANSFER)
            {
                accountDetails = entity.CustomerBankAccount.AccountNumber;
            }

            
            #region    All details
            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = title,
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Disbursement Amount",FieldValue=$"{entity.Amount:n}"},
                     new ChildView { FieldLabel ="Disbursement Mode",FieldValue=$"{entity.DisbursementMode }" },
                      new ChildView { FieldLabel ="Disbursement Date",FieldValue=$"{entity.DisbursementDate.Value.ToString("dddd, dd MMMM yyyy")}"},

                 }
            });
            var accountList = new List<ChildView>();
            
            
            if (entity.DisbursementMode == LoanDisbursementMode.BANK_TRANSFER)
            {
                accountList.Add(new ChildView { FieldLabel = "Disbursement Account", FieldValue = $"Disbursement Account({entity?.DisbursementAccount?.AccountNumber})" });

                accountList.Add(new ChildView { FieldLabel = "Customer Bank Account", FieldValue = $"Customer Bank Account({entity?.CustomerBankAccount?.AccountNumber})" });
                partialViews.Add(new ApprovalPartialView
                {
                    ViewId = viewId++,
                    Title = "Customer Loan Operational Accounts",
                    Children = accountList
                });
            }

            if (entity.DisbursementMode == LoanDisbursementMode.SPECIAL_DEPOSIT)
            {
                accountList.Add(new ChildView { FieldLabel = "Disbursement Account", FieldValue = $"Disbursement Account({entity?.DisbursementAccount?.AccountNumber})" });

                accountList.Add(new ChildView { FieldLabel = "Special Deposit Account", FieldValue = $"Special Deposit Account({entity?.SpecialDepositAccount?.AccountNo})" });
                partialViews.Add(new ApprovalPartialView
                {
                    ViewId = viewId++,
                    Title = "Customer Loan Operational Accounts",
                    Children = accountList
                });
            }

           

             

            if (charges!=null && charges.Any())
            {
                var chargesChildren = new List<TabularView>();
                int sn = 0;
                foreach (var k in charges)
                {
                    var chargeType = k.ChargeType.ToString()==null ? "LOAN_DISBURSEMENT" : $"{k.ChargeType.ToString()}";
                    sn++;
                    chargesChildren.Add( new TabularView { FieldSN = sn, FieldLabel = chargeType, FieldValue = $"{chargeType}({k.DisbursementCharge.ChargeValue:n})" });
                }
                partialViews.Add(new ApprovalPartialView
                {
                    ViewId = viewId++,
                    IsTabularView = true,
                    Title = "Loan Disbursement Charges",
                    ChildRows = chargesChildren
                });
            }


            partialViews.Add(GetLoanGuarrantorDetails(viewId, entity.LoanAccount.LoanApplicationId));
            partialViews.Add(GetCustomerDetails(viewId++, customer));
            #endregion
            
            
            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            
            details.ApprovalId = approvalId;
            
            UpdateApproval(details);
        }
        private void GetSDIncreaseDecreaseDetails(string payload, string approvalId)
        {
            var specialDepositAccount = new SpecialDepositAccount();

            var details = new ChevronCoopApprovalViewModel();
            var partialViews = new List<ApprovalPartialView>();
            int viewId = 1;


            var entity = _dbContext.SpecialDepositIncreaseDecreases.Where(s => s.ApprovalId == approvalId).FirstOrDefault();
            if (entity == null) return;

            specialDepositAccount = _dbContext.SpecialDepositAccounts.Where(c => c.Id == entity.SpecialDepositAccountId)
                .Include(s=>s.Customer)
                .Include(s=>s.DepositProduct)
                .FirstOrDefault();
            if (specialDepositAccount == null) return;


            var customer = specialDepositAccount.Customer;
            if (customer == null) return;


            var increaseDecrease = _dbContext.SpecialDepositIncreaseDecreases.Where(c => c.SpecialDepositAccountId == specialDepositAccount.Id).FirstOrDefault();


            #region    All details

            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = $"SpecialDeposit Account {nameof(entity.ContributionChangeRequest)} Request({specialDepositAccount.AccountNo})",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Transaction",FieldValue=entity.ContributionChangeRequest.ToString()},
                     new ChildView { FieldLabel ="Amount",FieldValue=$"{entity.Amount:n}"},
                      new ChildView { FieldLabel ="Application Date",FieldValue=$"{increaseDecrease.DateCreated.Value.ToString("dddd, dd MMMM yyyy")}"},
                      new ChildView { FieldLabel ="Deposit Funding Source Type",FieldValue=entity.ContributionChangeRequest.ToString()},
                 }
            });

            partialViews.Add(new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = $"SpecialDeposit Account",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Account No",FieldValue=specialDepositAccount.AccountNo},
                     new ChildView { FieldLabel ="Initial Funding Amount",FieldValue=$"{increaseDecrease.Amount:n}"},
                      new ChildView { FieldLabel ="Product Name",FieldValue=specialDepositAccount.DepositProduct.Name},
                      new ChildView { FieldLabel ="Deposit Funding Source Type",FieldValue=entity.ContributionChangeRequest.ToString()},
                      new ChildView { FieldLabel ="Application Date",FieldValue=$"{specialDepositAccount.DateCreated.Value.ToString("dddd, dd MMMM yyyy")}"},
                 }
            });



            partialViews.Add(GetCustomerDetails(viewId, customer));

            details = new ChevronCoopApprovalViewModel
            {
                ApprovalPartialViews = partialViews
            };
            #endregion

            details.ApprovalId = approvalId;
            UpdateApproval(details);
        }
        private ApprovalPartialView GetCustomerDetails(int viewId, Customer customer)
        {
            return new ApprovalPartialView
            {
                ViewId = viewId++,
                Title = "Customer Details",
                Children = new List<ChildView>
                 {
                     new ChildView { FieldLabel ="Name",FieldValue=$"{customer.FirstName} {customer.MiddleName} {customer.LastName}"},
                     new ChildView { FieldLabel ="Contact Number",FieldValue=$"{customer.PrimaryPhone}"},
                     new ChildView { FieldLabel ="Email",FieldValue=$"{customer.PrimaryEmail}"},
                      new ChildView { FieldLabel ="Member Type",FieldValue=$"{customer.MemberType}"},
                       new ChildView { FieldLabel ="Member ID",FieldValue=$"{customer.MemberId}"},
                 }
            };
        }
        private ApprovalPartialView GetLoanGuarrantorDetails(int viewId,string loanApplicationId)
        {
            var partialView = new ApprovalPartialView();
            viewId++;

            var loanGuarantors = _dbContext.LoanApplicationGuarantors
                .Include(g => g.Guarantor)
                .ThenInclude(d => d.Department)
                .Where(g => g.LoanApplicationId == loanApplicationId).ToList();
            if (loanGuarantors != null)
                if (loanGuarantors.Any())
                {
                    var guarantors = new List<TabularView>();
                    var sn = 1;
                    foreach (var g in loanGuarantors)
                    {
                        guarantors.Add(new TabularView { FieldSN = sn, FieldValue = g.Guarantor.FirstName, FieldValue2 = g.Guarantor.LastName, FieldValue3 = g.Guarantor.MemberId, FieldValue4 = g.GuarantorType.ToString(), FieldValue5 = g.Guarantor?.Department?.Name });
                        sn++;
                    }

                    partialView = new ApprovalPartialView()
                    {
                        ViewId = viewId,
                        Title = $"Guarantors",
                        IsMultipleFields = true,
                        IsTabularView = true,
                        FieldHeaders = new List<string> { "  #", "First Name", "Last Name", "Member ID", "GuarantorType", "Department" },
                        ChildRows = guarantors
                    };

                }

            return partialView;
        }
        public async Task<string> ProcessDetail(ApprovalType type,string approvalId)
        {
            //List<Approval> approvals = await _dbContext.Approvals.Where(x => x.ApprovalType == ApprovalType.KYC_COMPLETION).OrderByDescending(x => x.DateCreated).Take(20).ToListAsync(cts.Token);

            //if (approvals.Any())
            //    foreach (var approval in approvals)
            //    {
            //        if (approval == null) return "Approval not found";

            //        //if (string.IsNullOrEmpty(approval?.ApprovalViewModelPayload))
            //            ProcessDetails(approval.ApprovalType, approval);
            //    }
            //return $"{approvals.Count} was checked.";
            //--------------------------------------------------------------------------------
            var approval = await _dbContext.Approvals.Where(x => x.Id == approvalId).FirstOrDefaultAsync();
            if (approval == null) return "Approval not found";

            if (string.IsNullOrEmpty(approval?.ApprovalViewModelPayload))
                ProcessDetails(type, approval);

            approval = await _dbContext.Approvals.Where(x => x.Id == approvalId).FirstOrDefaultAsync();
            return approval?.ApprovalViewModelPayload;
        }
    }

    //public class LoanDisbursementApprovalDetails
    //{
    //    [DisplayName("Disbursement Amount")]
    //    public decimal DisbursementAmount { get; set; }
    //    [DisplayName("Mode of Disbursement")]
    //    public string? ModeOfDisbursement { get; set; }
    //    [DisplayName("Disbursement Date")]
    //    public DateTimeOffset DisbursementDate { get; set; }
    //    public List<AccountApprovalDetail> AccountDetails { get; set; } = new List<AccountApprovalDetail>();
    //    public List<DisbursementChargeApprovalDetail> Charges { get; set; } = new List<DisbursementChargeApprovalDetail>();
    //    public List<LoanApplicationGuarantor> LoanApplicationGuarantors { get; set; } = new List<LoanApplicationGuarantor>();
    //    public CustomerApprovalDetail Customer { get; set; } = new CustomerApprovalDetail();
    //}
    //public class AccountApprovalDetail
    //{
    //    public string? Name { get; set; }
    //    public string? AccountNo { get; set; }
    //    public string? Description { get; set; }
    //}
    //public class DisbursementChargeApprovalDetail
    //{
    //    public string? ChargeType { get; set; }
    //    public string? Description { get; set; }
    //    public decimal Amount { get; set; }
    //}

    //public class CustomerApprovalDetail
    //{
    //    public string? FullName { get; set; }
    //    public string? ContactNumber { get; set; }
    //    public string? MemberType { get; set; }
    //    public string? MemberId { get; set; }
    //}

    //private void GetLoanDisbursementDetails(string payload, string approvalId)
    //{
    //    var rsp = new CommandResult<string>();

    //    var details = new ChevronCoopApprovalViewModel();
    //    var partialViews = new List<ApprovalPartialView>();
    //    int viewId = 1;
    //    var title = "";


    //    //var request = System.Text.Json.JsonSerializer.Deserialize<LoanDisbursement>(payload);
    //    var approval = _dbContext.Approvals.Where(p => p.Id == approvalId).FirstOrDefault();



    //    var entity = _dbContext.LoanDisbursements.Where(x => x.Id == approval.EntityId).Include(l => l.LoanAccount).ThenInclude(l => l.Customer).FirstOrDefault();
    //    //  entity.LoanAccount = _dbContext.LoanAccounts.Where(l => l.Id == entity.LoanAccountId).Include(l => l.Customer).FirstOrDefault();

    //    if (entity == null) return;

    //    if (approval?.ApprovalType == ApprovalType.LOAN_DISBURSEMENT)
    //        title = $"Loan Disbursement Request({entity?.LoanAccount?.AccountNo})";
    //    else
    //        title = title = $"Loan Topup Disbursement Request({entity?.LoanAccount?.AccountNo})";



    //    var customer = entity?.LoanAccount?.Customer;
    //    if (customer == null) return;


    //    var loanDisburmentApprovalDetails = new LoanDisbursementApprovalDetails
    //    {
    //        Customer = new CustomerApprovalDetail
    //        {
    //            FullName = $"{customer.FirstName} {customer.MiddleName} {customer.LastName}",
    //            ContactNumber = $"{customer.PrimaryPhone}",
    //            MemberId = $"{customer.MemberId}",
    //            MemberType = $"{customer.MemberType}"
    //        },
    //    };


    //    entity.SpecialDepositAccount = _dbContext.SpecialDepositAccounts.FirstOrDefault(c => c.Id == entity.SpecialDepositAccountId);
    //    entity.CustomerBankAccount = _dbContext.CustomerBankAccounts.FirstOrDefault(c => c.Id == entity.CustomerBankAccountId);
    //    entity.DisbursementAccount = _dbContext.CompanyBankAccounts.FirstOrDefault(c => c.Id == entity.DisbursementAccountId);
    //    List<LoanDisbursementCharge> charges = _dbContext.LoanDisbursementCharges.Where(l => l.LoanDisbursementId == entity.Id).Include(c => c.DisbursementCharge).ToList();




    //    var accountDetails = "";
    //    if (entity.DisbursementMode == LoanDisbursementMode.SPECIAL_DEPOSIT)
    //    {
    //        accountDetails = entity.SpecialDepositAccount.AccountNo;
    //        loanDisburmentApprovalDetails.ModeOfDisbursement = nameof(LoanDisbursementMode.SPECIAL_DEPOSIT);
    //    }

    //    if (entity.DisbursementMode == LoanDisbursementMode.BANK_TRANSFER)
    //    {
    //        accountDetails = entity.CustomerBankAccount.AccountNumber;
    //        loanDisburmentApprovalDetails.ModeOfDisbursement = nameof(LoanDisbursementMode.BANK_TRANSFER);

    //    }


    //    #region    All details
    //    partialViews.Add(new ApprovalPartialView
    //    {
    //        ViewId = viewId++,
    //        Title = title,
    //        Children = new List<ChildView>
    //             {
    //                 new ChildView { FieldLabel ="Disbursement Amount",FieldValue=$"{entity.Amount:n}"},
    //                 new ChildView { FieldLabel ="Disbursement Mode",FieldValue=$"{entity.DisbursementMode }" },
    //                  new ChildView { FieldLabel ="Disbursement Date",FieldValue=$"{entity.DisbursementDate.Value.ToString("dddd, dd MMMM yyyy")}"},

    //             }
    //    });
    //    var accountList = new List<ChildView>();


    //    if (entity.DisbursementMode == LoanDisbursementMode.BANK_TRANSFER)
    //    {
    //        accountList.Add(new ChildView { FieldLabel = "Disbursement Account", FieldValue = $"Disbursement Account({entity?.DisbursementAccount?.AccountNumber})" });

    //        accountList.Add(new ChildView { FieldLabel = "Customer Bank Account", FieldValue = $"Customer Bank Account({entity?.CustomerBankAccount?.AccountNumber})" });
    //        partialViews.Add(new ApprovalPartialView
    //        {
    //            ViewId = viewId++,
    //            Title = "Customer Loan Operational Accounts",
    //            Children = accountList
    //        });

    //        loanDisburmentApprovalDetails.AccountDetails.Add(new AccountApprovalDetail
    //        {
    //            AccountNo = entity?.DisbursementAccount?.AccountNumber,
    //            Description = "Disbursement Account",
    //            Name = "Disbursement Account",
    //        });

    //        loanDisburmentApprovalDetails.AccountDetails.Add(new AccountApprovalDetail
    //        {
    //            AccountNo = entity.CustomerBankAccount.AccountNumber,
    //            Description = $"Customer Bank{entity.CustomerBankAccount.Bank.Name} Account",
    //            Name = "Disbursement Account",
    //        });
    //    }

    //    if (entity.DisbursementMode == LoanDisbursementMode.SPECIAL_DEPOSIT)
    //    {
    //        accountList.Add(new ChildView { FieldLabel = "Disbursement Account", FieldValue = $"Disbursement Account({entity?.DisbursementAccount?.AccountNumber})" });

    //        accountList.Add(new ChildView { FieldLabel = "Special Deposit Account", FieldValue = $"Special Deposit Account({entity?.SpecialDepositAccount?.AccountNo})" });
    //        partialViews.Add(new ApprovalPartialView
    //        {
    //            ViewId = viewId++,
    //            Title = "Customer Loan Operational Accounts",
    //            Children = accountList
    //        });


    //        loanDisburmentApprovalDetails.AccountDetails.Add(new AccountApprovalDetail
    //        {
    //            AccountNo = entity?.DisbursementAccount?.AccountNumber,
    //            Description = "Disbursement Account",
    //            Name = "Disbursement Account",
    //        });
    //        loanDisburmentApprovalDetails.AccountDetails.Add(new AccountApprovalDetail
    //        {
    //            AccountNo = entity.SpecialDepositAccount.AccountNo,
    //            Description = "Special Deposit Account",
    //            Name = "Special Deposit Account",
    //        });
    //    }




    //    if (charges != null && charges.Any())
    //    {
    //        var chargesChildren = new List<TabularView>();
    //        var DisbursementCharges = new List<DisbursementChargeApprovalDetail>();

    //        int sn = 0;
    //        foreach (var k in charges)
    //        {
    //            var chargeType = k.ChargeType.ToString() == null ? "LOAN_DISBURSEMENT" : $"{k.ChargeType.ToString()}";
    //            sn++;
    //            chargesChildren.Add(new TabularView { FieldSN = sn, FieldLabel = chargeType, FieldValue = $"{chargeType}({k.DisbursementCharge.ChargeValue:n})" });

    //            DisbursementCharges.Add(new DisbursementChargeApprovalDetail { Amount = k.DisbursementCharge.ChargeValue, ChargeType = chargeType, Description = "LOAN_DISBURSEMENT" });
    //        }
    //        partialViews.Add(new ApprovalPartialView
    //        {
    //            ViewId = viewId++,
    //            IsTabularView = true,
    //            Title = "Loan Disbursement Charges",
    //            ChildRows = chargesChildren
    //        });
    //        loanDisburmentApprovalDetails.Charges = DisbursementCharges;
    //    }


    //    partialViews.Add(GetLoanGuarrantorDetails(viewId, entity?.LoanAccount?.LoanApplicationId));
    //    partialViews.Add(GetCustomerDetails(viewId++, customer));

    //    var loanGuarantors = _dbContext.LoanApplicationGuarantors
    //       .Include(g => g.Guarantor)
    //    .ThenInclude(d => d.Department)
    //       .Where(g => g.LoanApplicationId == entity.LoanAccount.LoanApplicationId).ToList();
    //    loanDisburmentApprovalDetails.LoanApplicationGuarantors = loanGuarantors;
    //    loanDisburmentApprovalDetails.DisbursementDate = entity.DisbursementDate.Value;
    //    loanDisburmentApprovalDetails.DisbursementAmount = entity.Amount;
    //    #endregion


    //    //details = new ChevronCoopApprovalViewModel
    //    //{
    //    //    ApprovalPartialViews = partialViews
    //    //};

    //    //details.ApprovalId = approvalId;

    //    //UpdateApproval(details);

    //    //--------------------------------------------------

    //    var details2 = System.Text.Json.JsonSerializer.Serialize(loanDisburmentApprovalDetails);
    //    var details3 = Newtonsoft.Json.JsonConvert.SerializeObject(loanDisburmentApprovalDetails);

    //    approval.ApprovalViewModelPayload = details2;
    //    _dbContext.Approvals.Update(approval);
    //    _dbContext.SaveChanges();
    //}
}
