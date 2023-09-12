using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.AppDomain.Deposits.DepositTransactions;
using AP.ChevronCoop.AppDomain.Loans.LoanTransactions;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.DepositCronJobConfigurations;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountDeductionSchedules;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountDeductionSchedules;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentSchedules;
using AP.ChevronCoop.Entities.Payroll;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionSchedules;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupMembers;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AutoMapper;
using AutoMapper.Execution;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AP.ChevronCoop.AppCore.Services.BackgroundServices
{
    public class PayrollScheduleBackgroundService : IPayrollScheduleBackgroundService

    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly CancellationTokenSource cts = new();
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;
        private readonly string complianceDeptName = "Default";
        private readonly CoreAppSettings _options;
        // private readonly string COMPLETE_STATUS = "COMPLETE";
        // private readonly string ERROR_AMOUNT_STATUS = "AMOUNT MISMATCHED";

        /// <summary>
        /// Create Schedule Job
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="mediator"></param>
        /// <param name="emailService"></param>
        public PayrollScheduleBackgroundService(ChevronCoopDbContext dbContext, IMapper mapper,
            ILoggerService logger, IMediator mediator, IEmailService emailService, IOptions<CoreAppSettings> options)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
            _emailService = emailService;
            _options = options.Value;
        }

        /// <summary>
        /// Create schedule jobs 
        /// </summary>
        /// <param name="payrollDeductionSchedule"></param>
        /// <returns></returns>
        public async Task<bool> CreateScheduledJobs(PayrollDeductionSchedule payrollDeductionSchedule)
        {
            if (payrollDeductionSchedule.ScheduleType == PayrollScheduleType.PAYROLL_DEDUCTION)
            {
                // create 3 jobs
                PayrollCronJobConfig savingsCronJobConfig = new()
                {
                    CronJobType = CronJobType.SAVINGS_ACCOUNT_PAYROLL,
                    JobName = payrollDeductionSchedule.ScheduleName,
                    ComputationStartDate = payrollDeductionSchedule.PayrollDate,
                    JobStatus = CronJobStatus.PENDING,
                    DeductionScheduleId = payrollDeductionSchedule.Id,
                    JobDate = payrollDeductionSchedule.PayrollDate,
                    Description = "SAVINGS PAYROLL DEDUCTION " + DateTime.Today.ToString()
                };
                _dbContext.PayrollCronJobConfigs.Add(savingsCronJobConfig);

                PayrollCronJobConfig specialDepositCronJobConfig = new()
                {
                    CronJobType = CronJobType.SPECIAL_DEPOSIT_PAYROLL,
                    JobName = payrollDeductionSchedule.ScheduleName,
                    ComputationStartDate = payrollDeductionSchedule.PayrollDate,
                    JobStatus = CronJobStatus.PENDING,
                    DeductionScheduleId = payrollDeductionSchedule.Id,
                    JobDate = payrollDeductionSchedule.PayrollDate,
                    Description = "SPECIAL DEPOSIT PAYROLL DEDUCTION " + DateTime.Today.ToString()
                };
                _dbContext.PayrollCronJobConfigs.Add(specialDepositCronJobConfig);

                PayrollCronJobConfig loanRepaymentCronJobConfig = new()
                {
                    CronJobType = CronJobType.LOAN_REPAYMENT_PAYROLL,
                    JobName = payrollDeductionSchedule.ScheduleName,
                    ComputationStartDate = payrollDeductionSchedule.PayrollDate,
                    JobStatus = CronJobStatus.PENDING,
                    DeductionScheduleId = payrollDeductionSchedule.Id,
                    JobDate = payrollDeductionSchedule.PayrollDate,
                    Description = "LOAN REPAYMENT FROM PAYROLL DEDUCTION " + DateTime.Today.ToString()
                };
                _dbContext.PayrollCronJobConfigs.Add(loanRepaymentCronJobConfig);

            }
            else if (payrollDeductionSchedule.ScheduleType == PayrollScheduleType.INTEREST_COMPUTATION)
            {
                // create 2 jobs

                PayrollCronJobConfig specialDepositInterestCronJobConfig = new()
                {
                    CronJobType = CronJobType.MONTHLY_INTEREST_COMPUTATION_SPECIAL_DEPOSIT,
                    JobName = payrollDeductionSchedule.ScheduleName,
                    ComputationStartDate = payrollDeductionSchedule.PayrollDate,
                    JobStatus = CronJobStatus.PENDING,
                    DeductionScheduleId = payrollDeductionSchedule.Id,
                    JobDate = payrollDeductionSchedule.PayrollDate,
                    Description = "SPECIAL DEPOSIT INTEREST " + DateTime.Today.ToString()
                };
                _dbContext.PayrollCronJobConfigs.Add(specialDepositInterestCronJobConfig);

                PayrollCronJobConfig fixedDepositInterestCronJobConfig = new()
                {
                    CronJobType = CronJobType.INTEREST_COMPUTATION_FIXED_DEPOSIT,
                    JobName = payrollDeductionSchedule.ScheduleName,
                    ComputationStartDate = payrollDeductionSchedule.PayrollDate,
                    JobStatus = CronJobStatus.PENDING,
                    DeductionScheduleId = payrollDeductionSchedule.Id,
                    JobDate = payrollDeductionSchedule.PayrollDate,
                    Description = "FIXED DEPOSIT INTEREST " + DateTime.Today.ToString()
                };
                _dbContext.PayrollCronJobConfigs.Add(fixedDepositInterestCronJobConfig);
            }

            return await _dbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        ///  Matching Payroll 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> MatchDeductionAndPayrollData([Required] string scheduleId)
        {
            if (string.IsNullOrEmpty(scheduleId))
            {
                await _logger.LogInfo(nameof(PayrollScheduleBackgroundService), "MatchDeductionAndPayrollData", $"Invalid scheduleId :{scheduleId}");
                return false;
            }

            // to validate that we have a schedule for the month 
            var payrollScheduleItem = await
               _dbContext.PayrollDeductionSchedules
               .FirstOrDefaultAsync(c => !c.IsPosted
               && c.Id == scheduleId);

            if (payrollScheduleItem == null)
                return false;

            DateTime payrollDate = payrollScheduleItem.PayrollDate;

            // savings
            await MatchSavingAccountDeductionToPayroll(payrollDate.Month, payrollDate.Year, payrollScheduleItem);

            // special deposit
            await MatchSpecialDepositAccountDeductionToPayroll(payrollDate.Month, payrollDate.Year, payrollScheduleItem);

            // loan repayment
            await MatchLoanDeductionToPayroll(payrollDate.Month, payrollDate.Year, payrollScheduleItem);

            return true;
        }

        /// <summary>
        /// Service runs everyday
        /// Get the last payroll day on job table
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetSavingDepositDeductions()
        {
            string batchRef = "";
            var today = DateTime.Today;
            int count = 0;
            // get payroll config for savings account ...

            var jobConfiguration = await _dbContext.PayrollCronJobConfigs
                                    .Include(c => c.DeductionSchedule)
                                    .Where(c => c.CronJobType == CronJobType.SAVINGS_ACCOUNT_PAYROLL
                                    && c.ComputationStartDate.Date == today
                                    && c.JobStatus == CronJobStatus.PENDING)
                                    .FirstOrDefaultAsync(cts.Token);


            if (jobConfiguration != null)
            {
                // get all saving account

                var savingAccounts = await _dbContext.SavingsAccounts
                                        .Include(c => c.Customer)
                                        .Include(c => c.DepositProduct)
                                        .Where(c => c.IsActive
                                            && !c.IsClosed
                                            && !c.IsDeleted
                                            && c.PayrollAmount > 0)
                                          .ToListAsync(cts.Token);

                batchRef = "SV" + NHiloHelper.GetNextKey(nameof(SavingsAccountDeductionSchedule))
                       .ToString();

                PayrollDeductionSchedule payrollDeduction = jobConfiguration.DeductionSchedule;

                var ScheduleItems = new List<PayrollDeductionScheduleItem>();

                if (payrollDeduction == null)
                    return false;

                foreach (var savingAccount in savingAccounts)
                {
                    var savingAccountSchedule = new SavingsAccountDeductionSchedule()
                    {
                        AccountNo = savingAccount.AccountNo,
                        Amount = Math.Round(savingAccount.PayrollAmount, 2),
                        BatchRefNo = batchRef,
                        CurrentStatus = Enum.GetName(PayrollErrorType.PENDING),
                        DateCreated = DateTime.Now,
                        EmployeeNo = savingAccount.Customer?.MemberId,
                        MemberId = savingAccount.Customer?.MemberId,
                        MemberName = savingAccount.Customer?.FirstName + " " + savingAccount.Customer?.LastName,
                        Description = $"Saving Account Payroll Deduction for :{savingAccount.Customer?.MemberId} " + today.ToString("dd-MM-yyyy"),
                        DueDate = DateTime.Now,
                        Narration = $"Saving Account Payroll Deduction for :{savingAccount.Customer?.MemberId}",
                        PayrollCode = savingAccount.DepositProduct.Code,
                        CreatedByUserId = "1",
                        SavingsAccountId = savingAccount.Id,
                    };
                    _dbContext.SavingsAccountDeductionSchedules.Add(savingAccountSchedule);

                    await _dbContext.SaveChangesAsync();

                    var ScheduleItem = new PayrollDeductionScheduleItem()
                    {
                        MemberId = savingAccount.Customer?.MemberId,
                        MemberName = savingAccount.Customer?.FirstName + " " + savingAccount.Customer?.LastName,
                        Narration = $"SAVINGS_ACCOUNT_PAYROLL FOR " + today.ToString("dd-MM-yyyy"),
                        AccountNo = savingAccount.AccountNo,
                        AccountDueDate = DateTime.Now,
                        Amount = Math.Round(savingAccount.PayrollAmount, 2),
                        BatchRefNo = batchRef,
                        CurrentStatus = Enum.GetName(PayrollErrorType.PENDING),
                        DeductionType = PayrollDeductionType.SAVINGS,
                        PayrollCode = savingAccount.DepositProduct.Code,
                        PayrollDate = DateTime.Now,
                        Description = $"SAVINGS_ACCOUNT_PAYROLL FOR " + today.ToString("dd-MM-yyyy"),
                        DateCreated = DateTime.Now,
                        CreatedByUserId = "1",
                        PayrollDeductionScheduleId = payrollDeduction.Id,
                        SavingsAccountDeductionScheduleId = savingAccountSchedule.Id,
                        PayrollCronJobConfigId = jobConfiguration.Id
                    };
                    count++;
                    ScheduleItems.Add(ScheduleItem);
                }

                jobConfiguration.TotalAmount = ScheduleItems.Sum(c => c.Amount);
                jobConfiguration.TotalCount = count;
                _dbContext.PayrollCronJobConfigs.Update(jobConfiguration);

                payrollDeduction.DeductionsCount += count;
                payrollDeduction.TotalDeductions += ScheduleItems.Sum(c => c.Amount);

                payrollDeduction.ScheduleItems.AddRange(ScheduleItems);
                _dbContext.PayrollDeductionSchedules.Update(payrollDeduction);

                jobConfiguration.JobStatus = CronJobStatus.COMPLETED;
                _dbContext.PayrollCronJobConfigs.Update(jobConfiguration);
                int savingResult = await _dbContext.SaveChangesAsync();

                return savingResult > 0;
            }
            return false;

        }
        /// <summary>
        /// Special Deposit Deduction
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetSpecialDepositDeductions()
        {
            string batchRef = "";
            var today = DateTime.Today;
            int count = 0;
            // get payroll config for savings account ...

            var jobConfiguration = await _dbContext.PayrollCronJobConfigs
                                        .Include(c => c.DeductionSchedule)
                                        .Where(c => c.CronJobType == CronJobType.SPECIAL_DEPOSIT_PAYROLL
                                          && c.ComputationStartDate.Date == today
                                          && c.JobStatus == CronJobStatus.PENDING)
                                          .FirstOrDefaultAsync(cts.Token);


            if (jobConfiguration != null)
            {
                // get all special deposit account

                var specialDepositAccounts =
                    (from specialDeposit in _dbContext.SpecialDepositAccounts
                     join depositProduct in _dbContext.DepositProducts
                     on specialDeposit.DepositProductId equals depositProduct.Id
                     join increaseDecrease in _dbContext.SpecialDepositIncreaseDecreases on specialDeposit.Id equals increaseDecrease.SpecialDepositAccountId
                     join customer in _dbContext.Customers on specialDeposit.CustomerId equals customer.Id
                     where (specialDeposit.IsActive == true
                     && specialDeposit.IsClosed == false
                     && increaseDecrease.Amount > 0)
                     select new
                     {
                         specialDeposit,
                         increaseDecrease,
                         depositProduct,
                         customer
                     }).ToList();


                var ScheduleItems = new List<PayrollDeductionScheduleItem>();

                PayrollDeductionSchedule payrollDeduction = jobConfiguration.DeductionSchedule;

                if (payrollDeduction == null)
                    return false;

                batchRef = "SD" + NHiloHelper.GetNextKey(nameof(SpecialDepositAccountDeductionSchedule)).ToString();

                string desc = "";
                foreach (var item in specialDepositAccounts)
                {
                    desc = $"SPECIAL_DEPOSIT_PAYROLL Deduction for :{item.customer?.MemberId} @ " + today.ToString("dd-MM-yyyy");
                    var specialDepositAccountSchedule = new SpecialDepositAccountDeductionSchedule();

                    specialDepositAccountSchedule.AccountNo = item.specialDeposit.AccountNo;
                    specialDepositAccountSchedule.Amount = Math.Round(item.increaseDecrease.Amount, 2);
                    specialDepositAccountSchedule.BatchRefNo = batchRef;
                    specialDepositAccountSchedule.CurrentStatus = Enum.GetName(PayrollErrorType.PENDING);
                    specialDepositAccountSchedule.DateCreated = DateTime.Now;
                    specialDepositAccountSchedule.EmployeeNo = item.customer?.MemberId;
                    specialDepositAccountSchedule.MemberId = item.customer?.MemberId;
                    specialDepositAccountSchedule.MemberName = item.customer?.FirstName + " " + item.customer?.LastName;
                    specialDepositAccountSchedule.Description = desc;
                    specialDepositAccountSchedule.DueDate = DateTime.Now;
                    specialDepositAccountSchedule.Narration = desc;
                    specialDepositAccountSchedule.PayrollCode = item.depositProduct.Code;
                    specialDepositAccountSchedule.CreatedByUserId = "1";
                    specialDepositAccountSchedule.SpecialDepositAccountId = item.specialDeposit.Id;

                    _dbContext.SpecialDepositAccountDeductionSchedules.Add(specialDepositAccountSchedule);
                    await _dbContext.SaveChangesAsync();

                    var ScheduleItem = new PayrollDeductionScheduleItem()
                    {
                        MemberId = item.customer?.MemberId,
                        MemberName = item.customer?.FirstName + " " + item.customer?.LastName,
                        Narration = $"SPECIAL_DEPOSIT_PAYROLL FOR " + today.ToString("dd-MM-yyyy"),
                        AccountNo = item.specialDeposit.AccountNo,
                        AccountDueDate = DateTime.Now,
                        Amount = Math.Round(item.increaseDecrease.Amount, 2),
                        BatchRefNo = batchRef,
                        CurrentStatus = Enum.GetName(PayrollErrorType.PENDING),
                        DeductionType = PayrollDeductionType.SPECIAL_DEPOSIT,
                        PayrollCode = item.depositProduct.Code,
                        PayrollDate = DateTime.Now,
                        Description = $"SPECIAL_DEPOSIT_PAYROLL FOR " + today.ToString("dd-MM-yyyy"),
                        DateCreated = DateTime.Now,
                        CreatedByUserId = "1",
                        PayrollDeductionScheduleId = payrollDeduction.Id,
                        SpecialDepositAccountDeductionScheduleId = specialDepositAccountSchedule.Id,
                        PayrollCronJobConfigId = jobConfiguration.Id

                    };
                    ScheduleItems.Add(ScheduleItem);
                    count++;

                }


                jobConfiguration.TotalAmount = ScheduleItems.Sum(c => c.Amount);
                jobConfiguration.TotalCount = count;
                _dbContext.PayrollCronJobConfigs.Update(jobConfiguration);

                payrollDeduction.DeductionsCount += count;
                payrollDeduction.TotalDeductions += ScheduleItems.Sum(c => c.Amount);

                payrollDeduction.ScheduleItems.AddRange(ScheduleItems);
                _dbContext.PayrollDeductionSchedules.Update(payrollDeduction);

                jobConfiguration.JobStatus = CronJobStatus.COMPLETED;
                _dbContext.PayrollCronJobConfigs.Update(jobConfiguration);
                int response = await _dbContext.SaveChangesAsync();
                return response > 0;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetLoanRepaymentDeductions()
        {

            var today = DateTime.Today;
            int totalCount = 0;

            // get payroll config for savings account ...

            var jobConfiguration = await _dbContext.PayrollCronJobConfigs
                                .Include(c => c.DeductionSchedule)
                                .Where(c => c.CronJobType == CronJobType.LOAN_REPAYMENT_PAYROLL
                                && c.ComputationStartDate.Date == today
                                && c.JobStatus == CronJobStatus.PENDING)
                                .FirstOrDefaultAsync(cts.Token);


            if (jobConfiguration != null)
            {
                // get all saving account

                //var loanRepaymentSchedules = await _dbContext.LoanRepaymentSchedules
                //                            .Include(c => c.LoanAccount)
                //                            .ThenInclude(c => c.Customer)
                //                            .Include(c => c.LoanAccount)
                //                            .ThenInclude(c => c.LoanApplication)
                //                            .ThenInclude(c => c.LoanProduct)
                //                            .Where(c => c.IsActive
                //                            && !c.IsDeleted
                //                            && !c.LoanAccount.IsClosed
                //                            && c.IsPaid == false
                //                            && c.DueDate.Month == DateTime.Today.Month
                //                            && c.DueDate.Year == DateTime.Today.Year)
                //                              .ToListAsync(cts.Token);


                var loanRepaymentSchedules = await _dbContext.LoanRepaymentSchedules
                                           .Include(c => c.LoanAccount)
                                           .ThenInclude(c => c.Customer)
                                           .Include(c => c.LoanAccount)
                                           .ThenInclude(c => c.LoanApplication)
                                           .ThenInclude(c => c.LoanProduct)
                                           .Where(c => c.IsActive == true
                                           && c.IsDeleted == false
                                           && c.LoanAccount.IsClosed == false
                                           && c.IsPaid == false)
                                          .GroupBy(c => c.LoanAccount.AccountNo)
                                          .Select(c => c.OrderBy(e => e.RepaymentNo)
                                           .FirstOrDefault())
                                          .ToListAsync(cts.Token);


                var ScheduleItems = new List<PayrollDeductionScheduleItem>();

                PayrollDeductionSchedule payrollDeduction = jobConfiguration.DeductionSchedule;

                if (payrollDeduction == null)
                    return false;

                string batchRef = "LN" + NHiloHelper.GetNextKey(nameof(PayrollDeductionScheduleItem)).ToString();

                foreach (var loanSchedule in loanRepaymentSchedules)
                {
                    var customer = loanSchedule?.LoanAccount?.Customer;
                    var ScheduleItem = new PayrollDeductionScheduleItem()
                    {
                        PayrollDeductionScheduleId = payrollDeduction.Id,
                        LoanRepaymentScheduleId = loanSchedule?.Id,
                        MemberId = customer?.MemberId,
                        MemberName = customer?.FirstName + " " + customer?.LastName,
                        Narration = $"LOAN_REPAYMENT_PAYROLL FOR " + today.ToString("dd-MM-yyyy"),
                        AccountNo = loanSchedule?.LoanAccount?.AccountNo,
                        AccountDueDate = DateTime.Now,
                        Amount = loanSchedule?.PeriodPayment ?? 0m,
                        BatchRefNo = batchRef,
                        CurrentStatus = Enum.GetName(PayrollErrorType.PENDING),
                        DeductionType = PayrollDeductionType.LOAN,
                        PayrollCode = loanSchedule?.LoanAccount.LoanApplication?.LoanProduct.Code,
                        PayrollDate = DateTime.Now,
                        Description = $"LOAN_REPAYMENT_PAYROLL FOR " + today.ToString("dd-MM-yyyy"),
                        DateCreated = DateTime.Now,
                        CreatedByUserId = "1",
                        PayrollCronJobConfigId = jobConfiguration.Id
                    };
                    totalCount++;
                    ScheduleItems.Add(ScheduleItem);
                }

                jobConfiguration.TotalAmount = ScheduleItems.Sum(c => c.Amount);
                _dbContext.PayrollCronJobConfigs.Update(jobConfiguration);

                payrollDeduction.DeductionsCount += totalCount;
                payrollDeduction.TotalDeductions += ScheduleItems.Sum(c => c.Amount);

                payrollDeduction.ScheduleItems.AddRange(ScheduleItems);
                _dbContext.PayrollDeductionSchedules.Update(payrollDeduction);

                jobConfiguration.JobStatus = CronJobStatus.COMPLETED;
                _dbContext.PayrollCronJobConfigs.Update(jobConfiguration);
                int response = await _dbContext.SaveChangesAsync();

                return response > 0;
            }
            return false;
        }

        #region private methods...

        /// <summary>
        /// Process un matched transaction for loan 
        /// </summary>
        /// <returns></returns>
        private async Task<List<ApprovalGroupMember>> GetComplianceMembers()
        {

            var approvalGroup = await _dbContext.ApprovalGroupWorkflows
                                       .Include(c => c.ApprovalWorkflow)
                                      .Where(x => x.ApprovalWorkflow.WorkflowName == complianceDeptName)
                                      .OrderBy(o => o.Sequence)
                                      .FirstOrDefaultAsync(cancellationToken: cts.Token);

            var members = await _dbContext.ApprovalGroupMembers
                                .Include(c => c.ApplicationUser)
                                .ThenInclude(c => c.MemberProfile)
                                .Where(x => x.ApprovalGroupId == approvalGroup!.Id)
                                .ToListAsync(cancellationToken: cts.Token);


            return members;


        }

        private async Task SendEmailToComplainceTeamAsync(NotificationMessageType notificationType, List<ApprovalGroupMember> members, decimal scheduledCoopAmount,
            decimal deductionCNLAmount, decimal diffInAmount, bool IsOverPayment)
        {

            ////var props_ = new GeneralEmailDto
            ////{
            ////    Link = $"{_options.WebBaseUrl}",
            ////    Name = $"abu"
            ////};
            ////await SendNotificationAsync(notificationType, "abudotnet@gmail.com" ?? "",
            ////scheduledCoopAmount, deductionCNLAmount, diffInAmount,
            ////            "12345", IsOverPayment, props_);

            foreach (var approvalGrpMembers in members)
            {
                if (approvalGrpMembers == null) continue;

                var profile = approvalGrpMembers.ApplicationUser.MemberProfile;
                if (profile == null) continue;

                var props = new GeneralEmailDto
                {
                    Link = $"{_options.WebBaseUrl}",
                    Name = $"{profile.FirstName} {profile.LastName}"
                };

                if (!string.IsNullOrWhiteSpace(profile?.PrimaryEmail))
                    await SendNotificationAsync(notificationType, profile?.PrimaryEmail ?? "",
                        scheduledCoopAmount, deductionCNLAmount, diffInAmount,
                        profile?.MembershipId ?? "", IsOverPayment, props);

            }

            return;
        }

        private async Task<bool> SendNotificationAsync(NotificationMessageType notificationType, string email,
            decimal coopScheduleAmount,
            decimal cnlScheduledAmount, decimal diffAmount,
            string memberId, bool isOverPayment, GeneralEmailDto props)
        {

            string payment;
            if (isOverPayment)
            {
                payment = $"Overpayment amount: {diffAmount}";
            }
            else
            {
                payment = $"Underpayment amount: {diffAmount}";
            }

            var emailBody = @$"
                    <html>
                        <body>
                            <p>Kindly note that Cooperative member specified below has scheduled amount on coop app different from deduction amount from CNL. Please find details below. 
                            member id :{memberId} ,Cooperative Amount :{coopScheduleAmount}, CNL deduction amount: {cnlScheduledAmount}, {payment} , Product Type: {Enum.GetName(notificationType)} </p>
                        </body>
                    </html>
                ";

            await _emailService.SendCEMCAsync(new EmailRequest()
            {
                html = emailBody,
                subject = "CEMCS Payroll Matching Notification",
                to = email
            });


            return true;
        }

        public async Task TestEmailService(string email)
        {


            var emailBody = @$"
                    <html>
                        <body>
                            <p>Kindly note that Cooperative member specified below has scheduled amount on coop app different from deduction amount from CNL. Please find details below. 
                            member id :12345 ,Cooperative Amount :38,930, CNL deduction amount: 28942 , Product Type: SAVINGS </p>
                        </body>
                    </html>
                ";

            //  var message = new Message(email, "CEMCS Approval Notification", emailBody);

            await _emailService.SendCEMCAsync(new EmailRequest()
            {
                html = emailBody,
                subject = "CEMCS Approval Notification",
                to = email
            });
        }

        private static (bool, PayrollErrorType) CheckAmountDifference(decimal payrollAmount, decimal deductionAmount, PayrollDeductionSchedule scheduleDeduction)
        {
            if (payrollAmount == deductionAmount) return (true, PayrollErrorType.MATCHED);

            if (payrollAmount != deductionAmount)
            {
                decimal balance;
                if (payrollAmount - deductionAmount > 0)
                {
                    balance = payrollAmount - deductionAmount;
                    if (balance >= scheduleDeduction.MinDecimalPlace || balance <= scheduleDeduction.MaxDecimalPlace) return (true, PayrollErrorType.UNDER_PAYMENT);
                }
                else if (deductionAmount - payrollAmount > 0)
                {
                    balance = deductionAmount - payrollAmount;
                    if (balance >= scheduleDeduction.MinDecimalPlace || balance <= scheduleDeduction.MaxDecimalPlace) return (true, PayrollErrorType.OVER_PAYMENT);
                }
            }

            return (false, PayrollErrorType.GENRIC_ERROR);
        }

        /// <summary>
        /// Match savings account decution to payroll
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private async Task<bool> MatchSavingAccountDeductionToPayroll(int month, int year, PayrollDeductionSchedule scheduleDeduction)
        {
            string desc = string.Empty;
            // payroll items from coop
            var payrollSchedules = await _dbContext.PayrollDeductionScheduleItems
                                .Include(c => c.SavingsAccountDeductionSchedule)
                                .ThenInclude(c => c.SavingsAccount)
                                .Where(c => c.IsActive
                                && !c.IsDeleted
                                && c.PayrollDate.Month == month
                                && c.PayrollDate.Year == year
                                && c.DeductionType == PayrollDeductionType.SAVINGS)
                                .ToListAsync(cts.Token);

            // Deduction items from CNL
            var payrollDeductionSchedulesFromCNL = await _dbContext.PayrollDeductionItems
                                                .Where(c => c.IsActive
                                                 && !c.IsDeleted
                                                 && c.PayrollDate.Month == month
                                                 && c.PayrollDate.Year == year)
                                                .ToListAsync(cts.Token);

            if (!payrollSchedules.Any() || !payrollDeductionSchedulesFromCNL.Any())
            {
                await _logger.LogInfo(nameof(PayrollScheduleBackgroundService), "MatchDeductionAndPayrollData", $"Data set not found for either payrollSchedules or payrollSchedules");

                return false;
            }

            var complianceMembers = await GetComplianceMembers();

            foreach (var item in payrollSchedules)
            {
                var payrollDeduction = payrollDeductionSchedulesFromCNL
                            .FirstOrDefault(c => c.MemberId == item.MemberId
                            && c.PayrollCode == item.PayrollCode
                            && c.PayrollDate.Month == item.PayrollDate.Month
                            && c.PayrollDate.Year == item.PayrollDate.Year);

                if (payrollDeduction == null)
                {
                    await _logger.LogInfo(nameof(PayrollScheduleBackgroundService), "MatchDeductionAndPayrollData", $"payrollDeduction is currently null.. ");
                    continue;
                }

                (bool status, PayrollErrorType payrollType) StatusRefCheck = CheckAmountDifference(item.Amount,
                        payrollDeduction.Amount,
                        scheduleDeduction
                        );

                if (!StatusRefCheck.status)
                {
                    await _logger.LogInfo(nameof(PayrollScheduleBackgroundService), "MatchDeductionAndPayrollData", $"Invalid amount between payroll amount :{payrollDeduction.Amount} and deduction amount: {item.Amount}");
                }

                if (complianceMembers.Any())
                {
                    if (item.Amount > payrollDeduction.Amount)
                    {
                        await SendEmailToComplainceTeamAsync(NotificationMessageType.SAVINGS, complianceMembers, item.Amount,
                           payrollDeduction.Amount, decimal.Subtract(item.Amount, payrollDeduction.Amount), true);
                    }
                    else if (item.Amount < payrollDeduction.Amount)
                    {
                        await SendEmailToComplainceTeamAsync(NotificationMessageType.SAVINGS, complianceMembers, item.Amount,
                          payrollDeduction.Amount, decimal.Subtract(payrollDeduction.Amount, item.Amount), false);
                    }
                }


                payrollDeduction.CurrentStatus = Enum.GetName(PayrollErrorType.MATCHED);

                desc = $"PAYROLL AMOUNT :{item.Amount} , DEDUCTION AMOUNT: {payrollDeduction.Amount}";
                payrollDeduction.Description = desc;

                _dbContext.PayrollDeductionItems.Update(payrollDeduction);

                item.CurrentStatus = Enum.GetName(PayrollErrorType.MATCHED);
                item.Description = desc;
                item.OriginalAmount = item.Amount;
                item.Amount = payrollDeduction.Amount;

                _dbContext.PayrollDeductionScheduleItems.Update(item);

                await _dbContext.SaveChangesAsync();

                // trigger
                var transaction = new DepositTransactionCommand()
                {
                    EntityId = item.Id,
                    EntityType = typeof(PayrollDeductionScheduleItem),
                    IsApproved = false,
                    ApprovedOn = DateTime.Now,
                    TransactionAction = TransactionAction.POST,
                    TransactionDate = DateTime.Now,
                    TransactionType = TransactionType.SAVINGS_DEPOSIT_PAYROLL_FUNDING,
                    DepositAccountId = item.SavingsAccountDeductionSchedule?.SavingsAccount.Id,
                    TransactionJournalId = null
                };

                var transactionResponse = await _mediator.Send(transaction);
            }

            return false;
        }


        /// <summary>
        /// Match special deposit account deduction to payroll
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private async Task<bool> MatchSpecialDepositAccountDeductionToPayroll(int month, int year, PayrollDeductionSchedule scheduleDeduction)
        {
            // payroll items
            var payrollSchedules = await _dbContext.PayrollDeductionScheduleItems.Include(c => c.SpecialDepositAccountDeductionSchedule).ThenInclude(c => c.SpecialDepositAccount).Where(c => c.IsActive && !c.IsDeleted && c.PayrollDate.Month == month && c.PayrollDate.Year == year && c.DeductionType == PayrollDeductionType.SPECIAL_DEPOSIT).ToListAsync(cts.Token);

            // Deduction items 
            var payrollDeductionSchedules = await _dbContext.PayrollDeductionItems.Where(c => c.IsActive && !c.IsDeleted && c.PayrollDate.Month == month && c.PayrollDate.Year == year).ToListAsync(cts.Token);

            if (!payrollSchedules.Any() || !payrollDeductionSchedules.Any())
            {
                await _logger.LogInfo(nameof(PayrollScheduleBackgroundService), "MatchSpecialDepositAccountDeductionToPayroll", $"Data set not found for either payrollSchedules or payrollSchedules");
                return false;
            }

            var complianceMembers = await GetComplianceMembers();
            foreach (var item in payrollSchedules)
            {
                var payrollDeduction = payrollDeductionSchedules.FirstOrDefault(c => c.MemberId == item.MemberId
                && c.PayrollCode == item.PayrollCode && c.PayrollDate.Month == item.PayrollDate.Month && c.PayrollDate.Year == item.PayrollDate.Year);

                if (payrollDeduction == null)
                {
                    await _logger.LogInfo(nameof(PayrollScheduleBackgroundService), "MatchDeductionAndPayrollData", $"payrollDeduction is currently null.. ");
                    continue;
                }

                (bool status, PayrollErrorType payrollType) StatusRefCheck = CheckAmountDifference(item.Amount,
                        payrollDeduction.Amount,
                        scheduleDeduction
                        );

                if (!StatusRefCheck.status)
                {
                    await _logger.LogInfo(nameof(PayrollScheduleBackgroundService), "MatchDeductionAndPayrollData", $"Invalid amount between payroll amount :{payrollDeduction.Amount} and deduction amount: {item.Amount}");
                }

                if (complianceMembers.Any())
                {
                    // check if the amount is not valid
                    if (item.Amount > payrollDeduction.Amount)
                    {
                        await SendEmailToComplainceTeamAsync(NotificationMessageType.SPECIAL_DEPOSIT, complianceMembers, item.Amount,
                           payrollDeduction.Amount, decimal.Subtract(item.Amount, payrollDeduction.Amount), true);
                    }
                    else if (item.Amount < payrollDeduction.Amount)
                    {
                        await SendEmailToComplainceTeamAsync(NotificationMessageType.SPECIAL_DEPOSIT, complianceMembers, item.Amount,
                          payrollDeduction.Amount, decimal.Subtract(payrollDeduction.Amount, item.Amount), false);
                    }
                }

                string desc = $"PAYROLL AMOUNT :{item.Amount} , DEDUCTION AMOUNT: {payrollDeduction.Amount}";
                payrollDeduction.CurrentStatus = Enum.GetName(
                    PayrollErrorType.MATCHED);
                payrollDeduction.Description = desc;
                _dbContext.PayrollDeductionItems.Update(payrollDeduction);

                item.CurrentStatus = Enum.GetName(
                    PayrollErrorType.MATCHED);

                item.Amount = payrollDeduction.Amount;
                item.Description = desc;
                _dbContext.PayrollDeductionScheduleItems.Update(item);

                await _dbContext.SaveChangesAsync();

                // trigger
                var transaction = new DepositTransactionCommand()
                {
                    EntityId = item.Id,
                    EntityType = typeof(PayrollDeductionScheduleItem),
                    IsApproved = false,
                    ApprovedOn = DateTime.Now,
                    TransactionAction = TransactionAction.POST,
                    TransactionDate = DateTime.Now,
                    TransactionType = TransactionType.SPECIAL_DEPOSIT_PAYROLL_FUNDING,
                    DepositAccountId = item.SpecialDepositAccountDeductionSchedule?.SpecialDepositAccount.Id,
                    TransactionJournalId = null,
                };

                var transactionResponse = await _mediator.Send(transaction);

            }

            return false;
        }

        /// <summary>
        /// Match loan deduction to payroll
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private async Task<bool> MatchLoanDeductionToPayroll(int month, int year, PayrollDeductionSchedule scheduleDeduction)
        {
            decimal deficitAmount = 0m;

            // payroll items from Coop
            var payrollSchedules = await _dbContext.PayrollDeductionScheduleItems.Include(c => c.LoanRepaymentSchedule).ThenInclude(c => c.LoanAccount).Where(c => c.IsActive && !c.IsDeleted && c.PayrollDate.Month == month && c.PayrollDate.Year == year && c.DeductionType == PayrollDeductionType.LOAN).ToListAsync(cts.Token);

            // Deduction items from CNL
            var payrollDeductionSchedules = await _dbContext.PayrollDeductionItems.Where(c => c.IsActive && !c.IsDeleted && c.PayrollDate.Month == month && c.PayrollDate.Year == year).ToListAsync(cts.Token);

            if (!payrollSchedules.Any() || !payrollDeductionSchedules.Any())
            {
                await _logger.LogInfo(nameof(PayrollScheduleBackgroundService), "MatchDeductionAndPayrollData", $"Data set not found for either payrollSchedules or payrollSchedules");
                return false;
            }
            var repayments = await _dbContext.LoanRepaymentSchedules.Include(c => c.LoanAccount).ThenInclude(c => c.Customer).Where(c => c.IsActive && !c.IsDeleted && (c.ProcessedDate.HasValue ? c.ProcessedDate.Value.Month == month : false)
              && (c.ProcessedDate.HasValue ? c.ProcessedDate.Value.Year == year : false))
                .ToListAsync(cts.Token);

            var complianceMembers = await GetComplianceMembers();

            foreach (var item in payrollSchedules)
            {
                var payrollDeduction = payrollDeductionSchedules.FirstOrDefault(c => c.MemberId == item.MemberId
                && c.PayrollCode == item.PayrollCode && c.PayrollDate.Month == item.PayrollDate.Month && c.PayrollDate.Year == item.PayrollDate.Year);

                if (payrollDeduction == null)
                {
                    await _logger.LogInfo(nameof(PayrollScheduleBackgroundService), "MatchDeductionAndPayrollData", $"payrollDeduction is currently null.. ");
                    continue;
                }

                (bool status, PayrollErrorType payrollType) StatusRefCheck = CheckAmountDifference(item.Amount,
                        payrollDeduction.Amount,
                        scheduleDeduction
                        );

                // deduct deficit payroll amount from SD.
                deficitAmount = item.Amount - payrollDeduction.Amount;

                if (StatusRefCheck.payrollType == PayrollErrorType.UNDER_PAYMENT && deficitAmount > 0)
                {
                    await _logger.LogInfo(nameof(PayrollScheduleBackgroundService), "MatchDeductionAndPayrollData", $"Invalid amount between payroll amount :{payrollDeduction.Amount} and deduction amount: {item.Amount}");

                    var trxn = new LoanTransactionCommand()
                    {
                        EntityId = item.Id,
                        EntityType = typeof(LoanRepaymentSchedule),
                        IsApproved = false,
                        ApprovedOn = DateTime.Now,
                        TransactionAction = TransactionAction.POST,
                        TransactionDate = DateTime.Now,
                        TransactionType = TransactionType.LOAN_PAYROLL_REPAYMENT_PARTIAL,
                        LoanAccountId = item.LoanRepaymentSchedule?.LoanAccountId,
                        TransactionJournalId = null
                    };

                    var trxnResponse = await _mediator.Send(trxn);

                    await _logger.LogInfo("PayrollScheduleBackgroundService", $"{nameof(MatchLoanDeductionToPayroll)}", "");

                }

                if (complianceMembers.Any())
                {
                    // check if the amount is not valid
                    if (item.Amount > payrollDeduction.Amount)
                    {
                        await SendEmailToComplainceTeamAsync(NotificationMessageType.SPECIAL_DEPOSIT, complianceMembers, item.Amount,
                           payrollDeduction.Amount, decimal.Subtract(item.Amount, payrollDeduction.Amount), true);
                    }
                    else if (item.Amount < payrollDeduction.Amount)
                    {
                        await SendEmailToComplainceTeamAsync(NotificationMessageType.SPECIAL_DEPOSIT, complianceMembers, item.Amount,
                          payrollDeduction.Amount, decimal.Subtract(payrollDeduction.Amount, item.Amount), false);
                    }
                }


                payrollDeduction.CurrentStatus = Enum.GetName(PayrollErrorType.MATCHED);
                payrollDeduction.Description = $"PAYROLL AMOUNT :{item.Amount} , DEDUCTION AMOUNT: {payrollDeduction.Amount} - {Enum.GetName(StatusRefCheck.payrollType)} ";
                _dbContext.PayrollDeductionItems.Update(payrollDeduction);

                item.CurrentStatus = Enum.GetName(PayrollErrorType.MATCHED);
                item.Description = $"PAYROLL AMOUNT :{item.Amount} , DEDUCTION AMOUNT: {payrollDeduction.Amount} - {Enum.GetName(StatusRefCheck.payrollType)} ";
                _dbContext.PayrollDeductionScheduleItems.Update(item);

                var repayment = repayments.FirstOrDefault(c => c.LoanAccount.Customer.MemberId == item.MemberId);

                await _dbContext.SaveChangesAsync();

                // trigger


                var deposittransaction = new DepositTransactionCommand()
                {
                    EntityId = item.Id,
                    EntityType = typeof(PayrollDeductionScheduleItem),
                    IsApproved = false,
                    ApprovedOn = DateTime.Now,
                    TransactionAction = TransactionAction.POST,
                    TransactionDate = DateTime.Now,
                    TransactionType = TransactionType.SPECIAL_DEPOSIT_PAYROLL_FUNDING,
                    DepositAccountId = item.SpecialDepositAccountDeductionSchedule?.SpecialDepositAccount.Id,
                    TransactionJournalId = null,
                };

                var transaction_Response = await _mediator.Send(deposittransaction);

                var transaction = new LoanTransactionCommand()
                {
                    EntityId = item.Id,
                    EntityType = typeof(PayrollDeductionScheduleItem),
                    IsApproved = false,
                    ApprovedOn = DateTime.Now,
                    TransactionAction = TransactionAction.POST,
                    TransactionDate = DateTime.Now,
                    TransactionType = TransactionType.LOAN_PAYROLL_REPAYMENT,
                    LoanAccountId = item.LoanRepaymentSchedule?.LoanAccount.Id,
                    TransactionJournalId = null
                };

                var transactionResponse = await _mediator.Send(transaction);
            }

            return false;
        }

        #endregion
    }
}
