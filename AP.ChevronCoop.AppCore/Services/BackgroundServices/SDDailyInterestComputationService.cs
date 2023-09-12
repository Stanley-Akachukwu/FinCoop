using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestAdditions;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.DepositCronJobConfigurations;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestSchedules;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionSchedules;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.AppCore.Services.BackgroundServices
{
    public class SDDailyInterestComputationService : ISDDailyInterestComputationService
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly IMediator _mediator;

        public SDDailyInterestComputationService(ChevronCoopDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task ComputeSpecialDepositDailyInterests()
        {
            int totalRecordProcessed = 0;

            var currentDate = DateTime.Now;
            if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday) return;

            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddTicks(-1);
            var sdCronJob = new PayrollCronJobConfig();

            

            var schedule = _dbContext.SpecialDepositInterestSchedules.Where(x => x.StartDate.Date == currentDate.Date).Include(p => p.CronJobConfig).FirstOrDefault();

            if (schedule != null && schedule.IsProcessed == true)
                return;
          


            if (schedule == null)
            {
                 sdCronJob = _dbContext.PayrollCronJobConfigs.Where(p => p.CronJobType == CronJobType.DAILY_INTEREST_COMPUTATION_SPECIAL_DEPOSIT
             && p.JobStatus == CronJobStatus.PENDING).FirstOrDefault();

                if(sdCronJob == null) 
                {

                    var currentPayRoll = _dbContext.PayrollDeductionSchedules.Where(p=>p.DateCreated.Value.Date == DateTime.Now.Date).FirstOrDefault();
                    if (currentPayRoll == null) 
                    {
                        currentPayRoll = new PayrollDeductionSchedule
                        {
                            ScheduleName = "SD Daily Interest Computation.",
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

                        _dbContext.PayrollDeductionSchedules.Add(currentPayRoll);
                    }

                    sdCronJob = new()
                    {
                        CronJobType = CronJobType.MONTHLY_INTEREST_COMPUTATION_SPECIAL_DEPOSIT,
                        JobName = "Daily SD Interest Computation.",
                        ComputationStartDate = firstDayOfMonth,
                        JobStatus = CronJobStatus.PENDING,
                        DeductionScheduleId = currentPayRoll.Id,
                        JobDate = currentPayRoll.PayrollDate,
                        Description = "SPECIAL DEPOSIT DAILY INTEREST " + DateTime.Today.ToString(),
                       ComputationEndDate= lastDayOfMonth
                    };
                    _dbContext.PayrollCronJobConfigs.Add(sdCronJob);
                }


                schedule = new SpecialDepositInterestSchedule()
                {
                    CronJobConfig = sdCronJob,
                    CronJobConfigId = sdCronJob.Id,
                    IsProcessed = false,
                    EndDate = sdCronJob.ComputationEndDate,
                    ProcessedDate = sdCronJob.JobDate,
                    ScheduleName = sdCronJob.JobName,
                    StartDate = currentDate,
                    Description = $"SD Interest Computation Schedule({sdCronJob.JobName})- Processing Date - {sdCronJob.JobDate.ToString("dddd, dd MMMM yyyy")}"
                };


                await _dbContext.SpecialDepositInterestSchedules.AddAsync(schedule);
                await _dbContext.SaveChangesAsync();
            }
            if (schedule == null) return;


            sdCronJob = schedule.CronJobConfig;


            #region Spooling all affected accounts

            var accountsFundedByActions = await _dbContext.SpecialDepositAccounts.
               Where(d => d.Application.ModeOfPayment != DepositFundingSourceType.SPECIAL_DEPOSIT ||
               d.Application.ModeOfPayment != DepositFundingSourceType.PAYROLL && d.IsClosed == false)
               .Include(s => s.Application)
               .Include(s => s.DepositAccount)
               .Include(s => s.Customer).ToListAsync();

            if (accountsFundedByActions.Any())
            {
                foreach (var depositAccount in accountsFundedByActions)
                {
                    await ProcessInterestWithFundingSourceByActionsNoPayroll(depositAccount, schedule.Id);
                    totalRecordProcessed++;
                }
            }

            var accountsFundedByActionsOrPayRoll = await _dbContext.SpecialDepositAccounts.
              Where(d => d.Application.ModeOfPayment == DepositFundingSourceType.BANK_TRANSFER ||
              d.Application.ModeOfPayment == DepositFundingSourceType.PAYROLL &&
               d.Application.ModeOfPayment != DepositFundingSourceType.SPECIAL_DEPOSIT && d.IsClosed == false)
              .Include(s => s.Application)
               .Include(s => s.DepositAccount)
               .Include(s => s.Customer).ToListAsync();

             


            if (accountsFundedByActionsOrPayRoll.Any())
            {
                foreach (var depositAccount in accountsFundedByActionsOrPayRoll)
                {
                    await ProcessInterestWithFundingSourceByActionsOrPayroll(depositAccount, schedule.Id);
                    totalRecordProcessed++;
                }
            }
            #endregion

            if (!accountsFundedByActionsOrPayRoll.Any() && !accountsFundedByActions.Any()) return;


            sdCronJob.RecordsProcessed = totalRecordProcessed;

            if(currentDate.Date == lastDayOfMonth.Date)
                sdCronJob.JobStatus = CronJobStatus.COMPLETED;
                else
                sdCronJob.JobStatus = CronJobStatus.PENDING;


            _dbContext.PayrollCronJobConfigs.Update(sdCronJob);
            schedule.ProcessedDate = DateTime.UtcNow;
            schedule.IsProcessed = true;
            _dbContext.SpecialDepositInterestSchedules.Update(schedule);

            _dbContext.SaveChanges();
        }
         
        private async Task ProcessInterestWithFundingSourceByActionsOrPayroll(SpecialDepositAccount depositAccount, string scheduleId)
        {
            #region                            Chevron Algorithm
            //            For SD that has monthly contribution and has cash addition, SD withdrawal, Deposit transfer and / or loan offset for the month being considered.
            //               (i)Calculate the SD balance as at the previous month end as - SDBalPreviousMonthend
            //               (ii)Calculate daily interest on the month being considered.
            //                     Interest -for-the - first - day - of - month = SDIntRate / 100 * cum - balance -as- at - first - day - of - the - month - being - considered / 365
            //                    Interest -for-the - second - day - of - month = SDIntRate / 100 * cum - balance -as- at - second - day - of - the - month - being - considered / 365
            //              (iii)          Therefore, Interest -for-the - month = (SDIntRate / 100 * SDBalPreviousMonthend / 12) + (Interest -for-the - first - day - of - month + Interest -                     for-the - second - day - of - month + … +Interest -for-the - last - day - of - month) +(SDIntRate / 100 * MonthlyContribAmount / 365 * 7)


            //Note:
            //Cash addition, Fund transfer and withdrawal impact interest computation because one of the parameters for Special deposit interest computation is the SD balance.
            //SD Interest computation is saved monthly and done at the end of the month.  
            //Daily interest calculation for the month being considered helps to duly capture the activity at the submitted date. For example if a cash addition is done on the 10th of the month, interest will not be paid on it until the 10th .

            #endregion

            decimal oldBalance = depositAccount.DepositAccount.LedgerBalance;
            decimal sDIntRate = depositAccount.InterestRate;
            decimal monthlyContributeAmount = depositAccount.FundingAmount;
            decimal calculatedInterest = 0;
            decimal roundedCalculatedInterest = 0;
            decimal interestBasedOnPreviousMonthEnd = 0;
            decimal interestOnMonthlyContribution = 0;


            var totalDaysInYear = DateTime.IsLeapYear(DateTime.UtcNow.Year) ? 366 : 365;
            var currentDate = DateTime.Now;
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddTicks(-1);

            var interestOfTheDay = (sDIntRate / 100) * (oldBalance / totalDaysInYear);
            
            if (currentDate.Date == lastDayOfMonth.Date)
            {
                interestBasedOnPreviousMonthEnd = ((sDIntRate / 100) * oldBalance) / totalDaysInYear;
                interestOnMonthlyContribution = (sDIntRate / 100) * (monthlyContributeAmount / (totalDaysInYear * 7));
            }

            calculatedInterest = interestBasedOnPreviousMonthEnd + interestOfTheDay + interestOnMonthlyContribution;

            var newBalance = oldBalance + calculatedInterest;
            newBalance = Math.Round(newBalance, 2);
            roundedCalculatedInterest = Math.Round(calculatedInterest, 2);


            var scheduleItem = new SpecialDepositInterestScheduleItem
            {
                SpecialDepositAccountId = depositAccount.Id,
                PeriodCashAddition = monthlyContributeAmount,
                InterestEarned = roundedCalculatedInterest,
                InterestRate = sDIntRate,
                OldBalance = oldBalance,
                NewBalance = newBalance,
                SpecialDepositInterestScheduleId = scheduleId,
            };
            _dbContext.SpecialDepositInterestScheduleItems.Add(scheduleItem);

            depositAccount.LastInterestComputationDate = DateTime.Now;
            

            _dbContext.SpecialDepositAccounts.Update(depositAccount);
            _dbContext.SaveChanges();

            var command = new CreateSpecialDepositInterestAdditionCommand
            {
                SpecialDepositAccountId = depositAccount.Id,
                InterestEarned = roundedCalculatedInterest,
                InterestScheduleItemId = scheduleItem.Id,
            };
            await _mediator.Send(command);
        }
        private async Task ProcessInterestWithFundingSourceByActionsNoPayroll(SpecialDepositAccount depositAccount, string scheduleId)
        {
            #region                            Chevron Algorithm
            //(3.) For SD that does not have monthly contribution but has cash addition, SD withdrawal, Deposit transfer and/or loan offset for the month being considered.

            //                (i)Calculate the SD balance as at the previous month end – SDBalPreviousMonthend

            //                (ii)           Calculate daily interest on the month being considered.

            //                              Interest-for-the-first-day-of-month = SDIntRate/100 * cum-balance-as-at-first-day-of-the-month-being-considered /365

            //                                Interest-for-the-second-day-of-month = SDIntRate/100 * cum-balance-as-at-second-day-of-the-month-being-considered /365

            //                (iii)          Therefore, Interest-for-the-month = (SDIntRate/100 * SDBalPreviousMonthend /12) +(Interest -for-the - first - day - of - month + Interest -for-the - second - day - of - month + … +Interest -for-the - last - day - of - month)

            //Note:
            //Cash addition, Fund transfer and withdrawal impact interest computation because one of the parameters for Special deposit interest computation is the SD balance.
            //SD Interest computation is saved monthly and done at the end of the month.  
            #endregion
            decimal oldBalance = depositAccount.DepositAccount.LedgerBalance;
            decimal sDIntRate = depositAccount.InterestRate;
            decimal monthlyContributeAmount = depositAccount.FundingAmount;
            decimal calculatedInterest = 0;
            decimal roundedCalculatedInterest = 0;
            decimal interestBasedOnPreviousMonthEnd = 0;
            decimal interestOnMonthlyContribution = 0;


            var totalDaysInYear = DateTime.IsLeapYear(DateTime.UtcNow.Year) ? 366 : 365;
            var currentDate = DateTime.Now;
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddTicks(-1);

            var interestOfTheDay = (sDIntRate / 100) * (oldBalance / totalDaysInYear);

            if (currentDate.Date == lastDayOfMonth.Date)
            {
                interestBasedOnPreviousMonthEnd = ((sDIntRate / 100) * oldBalance) / totalDaysInYear;
            }

            calculatedInterest = interestBasedOnPreviousMonthEnd + interestOfTheDay + interestOnMonthlyContribution;

            var newBalance = oldBalance + calculatedInterest;
            newBalance = Math.Round(newBalance, 2);
            roundedCalculatedInterest = Math.Round(calculatedInterest, 2);


            var scheduleItem = new SpecialDepositInterestScheduleItem
            {
                SpecialDepositAccountId = depositAccount.Id,
                PeriodCashAddition = 0,
                InterestEarned = roundedCalculatedInterest,
                InterestRate = sDIntRate,
                OldBalance = oldBalance,
                NewBalance = newBalance,
                SpecialDepositInterestScheduleId = scheduleId
            };

            _dbContext.SpecialDepositInterestScheduleItems.Add(scheduleItem);
            depositAccount.LastInterestComputationDate = DateTime.Now;

            _dbContext.SpecialDepositAccounts.Update(depositAccount);
            _dbContext.SaveChanges();

            var command = new CreateSpecialDepositInterestAdditionCommand
            {
                SpecialDepositAccountId = depositAccount.Id,
                InterestEarned = roundedCalculatedInterest,
                InterestScheduleItemId = scheduleItem.Id,
            };
          var response =  await _mediator.Send(command);

        }

    }
}
