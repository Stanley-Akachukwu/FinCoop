using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;
using AP.ChevronCoop.AppDomain.Deposits.DepositTransactions;
using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositLiquidations;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.DepositCronJobConfigurations;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositImmediateLiquidations;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestAdditions;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestSchedules;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositLiquidationCharges;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Connection;

namespace AP.ChevronCoop.AppCore.Services.BackgroundServices
{
    public class FixedDepositInterestComputationService : IFixedDepositInterestComputationService
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly IMediator _mediator;

        public FixedDepositInterestComputationService(ChevronCoopDbContext chevronCoopDb, IMediator mediator)
        {
            _dbContext = chevronCoopDb;
            _mediator = mediator;
        }



        public async Task<FixedDepositInterestSchedule> GetFixedDepositSchedule()
        {

            var sdCronJob = _dbContext.PayrollCronJobConfigs.Where(p => p.CronJobType == CronJobType.INTEREST_COMPUTATION_FIXED_DEPOSIT
                  && p.JobDate.Date == DateTime.Now.Date).Include(p => p.DeductionSchedule).FirstOrDefault();


            if (sdCronJob == null)
            {

                var deductionSchedule = _dbContext.PayrollDeductionSchedules.Where(p => p.DateCreated.Value.Date == DateTime.Now.Date).FirstOrDefault();
                if (deductionSchedule == null)
                {

                    deductionSchedule = new Entities.Payroll.PayrollDeductionSchedules.PayrollDeductionSchedule
                    {
                        ScheduleName = $"Daily FD Interest Computation for {DateTime.Now.ToString("dd-MM-yyyyy hh:tt")}",
                        ScheduleType = PayrollScheduleType.INTEREST_COMPUTATION,
                        TotalDeductions = 0,
                        DeductionsCount = 0,
                        AdviseDate = DateTime.Now.Date,
                        ExpectedDate = DateTime.Now.Date,
                        IsPosted = false,
                        LastUploadedDate = DateTime.Now.Date,
                        PayrollDate = DateTime.Now.Date,
                        MaxDecimalPlace = 0,
                        MinDecimalPlace = 0,
                        IsProcessed = false,
                        IsUploaded = false,
                        IsActive = false,
                        ProcessedDate = DateTime.Now.Date,

                    };

                    _dbContext.PayrollDeductionSchedules.Add(deductionSchedule);
                }

                sdCronJob = new PayrollCronJobConfig()
                {
                    CronJobType = CronJobType.INTEREST_COMPUTATION_FIXED_DEPOSIT,
                    JobName = $"Daily FD Interest Computation for {DateTime.Now.ToString("dd-MM-yyyyy hh:tt")}",
                    ComputationStartDate = DateTime.Now,
                    JobStatus = CronJobStatus.PENDING,
                    JobDate = deductionSchedule.PayrollDate,
                    DeductionSchedule = deductionSchedule,
                    Description = "FIXED DEPOSIT DAILY INTEREST" + DateTime.Today.ToString(),

                };

                _dbContext.PayrollCronJobConfigs.Add(sdCronJob);
            }


            await _dbContext.SaveChangesAsync();


            var schedule = await _dbContext.FixedDepositInterestSchedules.Where(p => p.CronJobConfigId == sdCronJob.Id).FirstOrDefaultAsync();

            if (schedule != null) return schedule;

            schedule = new FixedDepositInterestSchedule()
            {

                CronJobConfig = sdCronJob,
                IsProcessed = false,
                EndDate = sdCronJob.ComputationEndDate,
                ProcessedDate = sdCronJob.JobDate,
                ScheduleName = sdCronJob.JobName,
                StartDate = DateTime.Now,
                Description = $"Fd Interest Computation Schedule({sdCronJob.JobName})- Processing Date - {sdCronJob.JobDate.ToString("dddd, dd MMMM yyyy")}",

            };
            await _dbContext.FixedDepositInterestSchedules.AddAsync(schedule);
            await _dbContext.SaveChangesAsync();

            return schedule;



        }


        public DateTime GetMaturityDate(FixedDepositAccount account)
        {

            // var days = Utility.DateDiff(account.LastInterestComputationDate.Value, account.DateCreated.Value).Days;
            return DateTime.Parse(account.DateCreated.ToString()).AddDays(Convert.ToDouble(account.TenureValue));
        }

        public async Task ProcessInterestComputation()
        {


            //don't compute on weekends
            if (IsWeekEnd()) return;

            //get schedule for the current month

            var schedule = await GetFixedDepositSchedule();

            if (schedule == null) return;


            var accounts = await _dbContext.FixedDepositAccounts
                                                   .Include(x => x.Application)
                                                   .Include(x => x.InterestEarnedAccount)
                                                   .Include(x => x.DepositAccount)
                                                   .Where(x =>
                                                     x.HasMature == false && x.IsClosed == false
                                                      )
                                                    .ToListAsync();


            var fixedDepositInterestAdditions = new List<FixedDepositInterestAddition>();
            var scheduleItems = new List<FixedDepositInterestScheduleItem>();



            if (schedule != null && accounts.Any())
            {


                ProcessAccountInterest(schedule, scheduleItems, fixedDepositInterestAdditions, accounts);

                var config = schedule.CronJobConfig;
                config.RecordsProcessed += scheduleItems.Count;
                config.JobStatus = CronJobStatus.COMPLETED;
                _dbContext.PayrollCronJobConfigs.Update(config);

                _dbContext.FixedDepositInterestScheduleItems.AddRange(scheduleItems);
                _dbContext.FixedDepositInterestAdditions.AddRange(fixedDepositInterestAdditions);
                _dbContext.FixedDepositAccounts.UpdateRange(accounts);

                await _dbContext.SaveChangesAsync();

                await PostFixedDepositInterestAdditionTransactions(fixedDepositInterestAdditions);


                var maturedAccounts = accounts.Where(a => a.HasMature == true).ToList();
                await CloseMaturedAcount(maturedAccounts);
                await HandleMaturedAccounts(maturedAccounts);


            }

        }

        private void ProcessAccountInterest(FixedDepositInterestSchedule schedule, List<FixedDepositInterestScheduleItem> fixedDepositInterestScheduleItems, List<FixedDepositInterestAddition> fixedDepositInterestAdditions, List<FixedDepositAccount> accounts)
        {


            var scheduleItems = new List<FixedDepositInterestScheduleItem>();


            foreach (var account in accounts)
            {

                var lastComputatedDate = account.LastInterestComputationDate ?? account.DateCreated.Value;

                //Account already computed or created today
                if (IsInterestComputed(lastComputatedDate)) continue;

                var interest = CalculateInterest(account);

                var fixedDepositInterestScheduleItem = new FixedDepositInterestScheduleItem()
                {
                    OldBalance = account.DepositAccount.LedgerBalance,
                    NewBalance = account.DepositAccount.LedgerBalance + interest,
                    InterestRate = account.InterestRate,
                    InterestEarned = interest,
                    FixedDepositAccountId = account.Id,
                    PeriodCashAddition = interest,
                    FixedDepositInterestScheduleId = schedule.Id

                };

                scheduleItems.Add(fixedDepositInterestScheduleItem);

                fixedDepositInterestAdditions.Add(new FixedDepositInterestAddition()
                {
                    InterestScheduleItem = fixedDepositInterestScheduleItem,
                    InterestEarned = interest,
                    IsProcessed = false,
                    FixedDepositAccountId = account.Id,

                });

                account.LastInterestComputationDate = DateTime.UtcNow;

                var tenorValueToDays = ConvertTenorToDays(account);

                account.HasMature =
                   Utility.DateDiff(account.LastInterestComputationDate.Value, account.DateCreated.Value).Days >= tenorValueToDays;

            }


        }

        private int ConvertTenorToDays(FixedDepositAccount account)
        {

            var lastInterestDate = account.DateCreated.Value;
            var tenorValue = (int)account.TenureValue;

            switch (account.TenureUnit)
            {


                case Tenure.DAILY_360:
                case Tenure.DAILY_365:
                case Tenure.DAILY_366:
                    lastInterestDate.AddDays(tenorValue);
                    break;

                case Tenure.WEEKLY:
                    lastInterestDate.AddDays(tenorValue * 7);
                    break;

                case Tenure.BI_WEEKLY:
                    lastInterestDate.AddDays(tenorValue * 14);
                    break;

                case Tenure.MONTHLY:
                    lastInterestDate.AddMonths(tenorValue * 12);
                    break;

                case Tenure.QUARTERLY:
                    lastInterestDate.AddMonths(tenorValue * 3);
                    break;


                case Tenure.SEMI_ANNUALLY:
                    lastInterestDate.AddMonths(tenorValue * 6);
                    break;

                case Tenure.ANNUALLY:
                    lastInterestDate.AddMonths(tenorValue * 12);
                    break;


            }

            return Utility.DateDiff(account.DateCreated.Value, lastInterestDate).Days;


        }
        private async Task PostFixedDepositInterestAdditionTransactions(List<FixedDepositInterestAddition> fixedDepositInterestAdditions)
        {


            foreach (var fixedDepositInterestAddition in fixedDepositInterestAdditions)
            {

                var transaction = new DepositTransactionCommand()
                {
                    EntityId = fixedDepositInterestAddition.Id,
                    EntityType = typeof(FixedDepositInterestAddition),
                    IsApproved = false,
                    ApprovedOn = DateTime.Now,
                    TransactionAction = TransactionAction.POST,
                    TransactionDate = DateTime.Now,
                    TransactionType = TransactionType.FIXED_DEPOSIT_INTEREST_ADDITION,
                    DepositAccountId = fixedDepositInterestAddition.FixedDepositAccountId,
                    TransactionJournalId = null
                };

                var transactionResponse = await _mediator.Send(transaction);
            }


        }

        private bool IsInterestComputed(DateTimeOffset lastComputatedDate) => (lastComputatedDate.Date == DateTime.Now.Date);

        private async Task CloseMaturedAcount(List<FixedDepositAccount> accounts)
        {

            foreach (var account in accounts)
            {
                if (!account.HasMature) continue;
                account.IsClosed = true;
            }

            _dbContext.FixedDepositAccounts.UpdateRange(accounts);
            await _dbContext.SaveChangesAsync();
        }

        private async Task HandleMaturedAccounts(List<FixedDepositAccount> accounts)
        {



            foreach (var account in accounts)
            {

                if (!account.HasMature) continue;



                if (account.MaturityInstructionType == MaturityInstructionType.ROLLOVER_PRINCIPAL_AND_INTEREST
                   || account.MaturityInstructionType == MaturityInstructionType.ROLLOVER_PRINCIPAL_AND_LIQUIDATE_INTEREST)
                {

                    var amount = account.MaturityInstructionType == MaturityInstructionType.ROLLOVER_PRINCIPAL_AND_INTEREST ?
                          account.DepositAccount.LedgerBalance + account.InterestEarnedAccount.LedgerBalance :
                          account.DepositAccount.LedgerBalance;


                    var command = new CreateFixedDepositAccountCommand
                    {
                        Amount = amount,
                        MaturityInstructionType = account.MaturityInstructionType,
                        CustomerId = account.CustomerId,
                        DepositProductId = account.DepositProductId,
                        CreatedByUserId = account.CreatedByUserId,
                        ApplicationId = account.ApplicationId,
                        InterestRate = account.InterestRate,
                        LiquidationAccountType = account.LiquidationAccountType,
                        TenureUnit = account.TenureUnit,
                        TenureValue = account.TenureValue,
                        RootParentAccountId = account.RootParentAccountId ?? account.Id,
                        ParentAccountId = account.Id


                    };

                    if (command.LiquidationAccountType == WithdrawalAccountType.SAVINGS_ACCOUNT) command.LiquidationAccountId = account.SavingsLiquidationAccountId;

                    if (command.LiquidationAccountType == WithdrawalAccountType.SPECIAL_DEPOSIT_ACCOUNT) command.LiquidationAccountId = account.SpecialDepositLiquidationAccountId;

                    if (command.LiquidationAccountType == WithdrawalAccountType.EXISTING_BANK_ACCOUNT) command.LiquidationAccountId = account.CustomerBankLiquidationAccountId;

                    await _mediator.Send(command);


                }
                else
                {
                    //Liquidate_Principle_Interest


                    var liquidateCommand = new CreateFixedDepositLiquidationCommand
                    {
                        IsImmediate = false,
                        FixedDepositAccountId = account.Id,
                        CustomerId = account.CustomerId,
                        LiquidationAccountType = account.LiquidationAccountType,
                        Comments = "Account Matured",
                        Description = $"Account Matured",
                        CreatedByUserId = account.CreatedByUserId,
                        Caption = account.Caption,


                    };


                    liquidateCommand.LiquidationAccountId = account.SavingsLiquidationAccountId ??
                                                            account.SpecialDepositLiquidationAccountId ??
                                                            account.CustomerBankLiquidationAccountId;


                    await _mediator.Send(liquidateCommand);



                }



            }
        }

        public async Task ProcessFixedDepositAccountThatIsliquidatedBeforeMaturity(FixedDepositLiquidation fixedDepositLiquidation)
        {

            var account = fixedDepositLiquidation.FixedDepositAccount;

            if (!account.HasMature)
            {

                var schedule = await GetFixedDepositSchedule();

                if (schedule == null) return;

                var lastComputatedDate = account.LastInterestComputationDate ?? account.DateCreated.Value;

                var result = await CalculateInvestmentBeforeLiquidationAndEarlyCharge(account);

                var interest = (decimal)result.interest;
                var charge = (decimal)result.charge;

                var fixedDepositInterestAddition = new FixedDepositInterestAddition();


                if (!IsInterestComputed(lastComputatedDate) && !IsWeekEnd())
                {

                    var fixedDepositInterestScheduleItem = new FixedDepositInterestScheduleItem()
                    {
                        OldBalance = account.DepositAccount.LedgerBalance,
                        NewBalance = account.DepositAccount.LedgerBalance + interest,
                        InterestRate = account.InterestRate,
                        InterestEarned = interest,
                        FixedDepositAccountId = account.Id,
                        PeriodCashAddition = interest,
                        FixedDepositInterestScheduleId = schedule.Id

                    };

                    fixedDepositInterestAddition = new FixedDepositInterestAddition()
                    {
                        InterestScheduleItem = fixedDepositInterestScheduleItem,
                        InterestEarned = interest,
                        IsProcessed = true,
                        ProcessedDate = DateTime.UtcNow,
                        FixedDepositAccountId = account.Id,

                    };


                    account.LastInterestComputationDate = DateTime.UtcNow;
                    account.IsClosed = true;


                    var config = schedule.CronJobConfig;
                    config.RecordsProcessed += 1;

                    _dbContext.PayrollCronJobConfigs.Update(config);
                    _dbContext.FixedDepositAccounts.Update(account);

                    _dbContext.FixedDepositInterestScheduleItems.Add(fixedDepositInterestScheduleItem);
                    _dbContext.FixedDepositInterestAdditions.Add(fixedDepositInterestAddition);

                }

                var fixedDepositLiquidationCharge = new FixedDepositLiquidationCharge()
                {
                    FixedDepositLiquidationId = fixedDepositLiquidation.Id,
                    LiquidationCharge = charge,
                    ChargeType = ChargeType.FIXED_DEPOSIT_LIQUIDATION_CHARGE

                };

                _dbContext.FixedDepositLiquidationCharges.Add(fixedDepositLiquidationCharge);

                await _dbContext.SaveChangesAsync();


                //Interest already computed for account or account created today
                if (!IsInterestComputed(lastComputatedDate) && !IsWeekEnd())
                    await PostFixedDepositInterestAdditionTransactions(new List<FixedDepositInterestAddition> { fixedDepositInterestAddition });



            }

        }


        private decimal CalculateInterest(FixedDepositAccount account)
        {

            decimal totalDaysInYear = DateTime.IsLeapYear(DateTime.UtcNow.Year) ? 366 : 365;

            //var noOfDaysForInterest = GetNoOfDayToCalculateInterestFor(account);
            decimal annualRate = 1 / totalDaysInYear;
            decimal dailyInterest = account.DepositAccount.LedgerBalance * (account.InterestRate / 100) * annualRate;

            //  var interestTillDate = account.InterestRate / 100 * account.DepositAccount.LedgerBalance / totalDaysInYear * account.TenureValue;
            //var interestTillDate = account.InterestRate / 100 * account.DepositAccount.LedgerBalance / totalDaysInYear * noOfDaysForInterest;
            //return interestTillDate;

            return decimal.Round(dailyInterest, 2, MidpointRounding.AwayFromZero);
        }


        private int GetNoOfDayToCalculateInterestFor(FixedDepositAccount account)
        {
            return 1;


            //return Utility.DateDiff(account.LastInterestComputationDate ?? account.DateCreated.Value, DateTimeOffset.UtcNow).Days;

            //var previousInterestDaysCalculated = 0;

            //if (account.LastInterestComputationDate.HasValue)
            //    previousInterestDaysCalculated = Utility.DateDiff(account.DateCreated.Value, account.LastInterestComputationDate.Value).Days;

            //var tenorValue = (int)account.TenureValue;

            //currentInterestNumOfDays = currentInterestNumOfDays + previousInterestDaysCalculated >= account.TenureValue ? currentInterestNumOfDays - tenorValue : currentInterestNumOfDays;

            //return currentInterestNumOfDays;

        }

        private decimal CalculateRolledOverInvestment(FixedDepositAccount account, decimal intrestAtMaturity)
        {
            var totalDaysInYear = GetTotalDaysInYear();
            var investmentAtMaturity = intrestAtMaturity + account.Application.Amount;

            var fixedDepositInterestPaid = account.InterestRate / 100 * investmentAtMaturity / totalDaysInYear * account.TenureValue;
            return fixedDepositInterestPaid;


        }

        private int GetTotalDaysInYear() => DateTime.IsLeapYear(DateTime.UtcNow.Year) ? 366 : 365;

        private bool IsWeekEnd()
        {

            var dayOfWeek = DateTime.Now.DayOfWeek;

            return dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;

        }


        private async Task<(double interest, double charge)> CalculateInvestmentBeforeLiquidationAndEarlyCharge(FixedDepositAccount account)
        {

            var totalInteresTillDate = (double)account.InterestEarnedAccount.LedgerBalance;

            var totalComputedInterestDays = await _dbContext.FixedDepositInterestAdditions.
                        Where(x => x.FixedDepositAccountId == account.Id).CountAsync();

            var currentInterestPaid = 0.0;

            var lastComputatedDate = account.LastInterestComputationDate ?? account.DateCreated.Value;

            if (!(IsInterestComputed(lastComputatedDate) || IsWeekEnd()))
            {

                currentInterestPaid = Convert.ToDouble(CalculateInterest(account));
                totalInteresTillDate += currentInterestPaid;
                totalComputedInterestDays += 1;
            }


            var earlyLiquidationCharge = 0.0;
            var tenureValue = ConvertTenorToDays(account);

            if (account.TenureValue == 30)
                earlyLiquidationCharge = 0.15 * totalInteresTillDate;

            else if (totalComputedInterestDays < 0.50 * tenureValue)
                earlyLiquidationCharge = 0.20 * totalInteresTillDate;

            else if (totalComputedInterestDays >= 0.50 * tenureValue && totalComputedInterestDays <= 0.60 * tenureValue)
                earlyLiquidationCharge = 0.17 * totalInteresTillDate;

            else if (totalComputedInterestDays >= 0.61 * tenureValue && totalComputedInterestDays <= 0.70 * tenureValue)
                earlyLiquidationCharge = 0.15 * totalInteresTillDate;

            else if (totalComputedInterestDays >= 0.71 * tenureValue && totalComputedInterestDays <= 0.80 * tenureValue)
                earlyLiquidationCharge = 0.13 * totalInteresTillDate;

            else if (totalComputedInterestDays >= 0.81 * tenureValue && totalComputedInterestDays <= 0.99 * tenureValue)
                earlyLiquidationCharge = 0.1 * totalInteresTillDate;


            return await Task.FromResult((currentInterestPaid, earlyLiquidationCharge));

        }

        public decimal TopedUpInvestment(FixedDepositAccount account, decimal topUpAmount)
        {
            var noOfDaysForInterest = GetNoOfDayToCalculateInterestFor(account);
            var interesTillDate = account.InterestRate / 100 * account.Application.Amount / 365 * noOfDaysForInterest;

            var newFXDInvAmount = topUpAmount + interesTillDate;

            return newFXDInvAmount;



        }

        public async Task RepostFundingTransaction(string fixedDepositId)
        {
            var transaction = new DepositTransactionCommand()
            {
                EntityId = fixedDepositId,
                EntityType = typeof(FixedDepositAccount),
                IsApproved = false,
                ApprovedOn = DateTime.Now,
                TransactionAction = TransactionAction.POST,
                TransactionDate = DateTime.Now,
                TransactionType = TransactionType.FIXED_DEPOSIT_APPLICATION_FUNDING,
                DepositAccountId = fixedDepositId,
                TransactionJournalId = null
            };


            var transactionResponse = await _mediator.Send(transaction);
        }
    }
}

