using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositInterestAdditions;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestSchedules;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.AppCore.Services.BackgroundServices
{
    public class SDMonthlyInterestComputationService : ISDMonthlyInterestComputationService
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly IMediator _mediator;

        public SDMonthlyInterestComputationService(ChevronCoopDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }
        public async Task ComputeSpecialDepositMonthlyInterests()
        {
            int totalRecordProcessed = 0;

            var currentDate = DateTime.Now;


            var schedule = _dbContext.SpecialDepositInterestSchedules.Where(x => x.StartDate.Date == currentDate.Date).Include(x => x.CronJobConfig).ThenInclude(x => x.DeductionSchedule).Include(p => p.CronJobConfig).FirstOrDefault();

          //  var schedule = _dbContext.SpecialDepositInterestSchedules.FirstOrDefault();

            if (schedule != null && schedule.IsProcessed == true)
                return;


            var sdCronJob = schedule?.CronJobConfig;

            if (sdCronJob == null)
            {
                sdCronJob = _dbContext.PayrollCronJobConfigs.Where(p => p.CronJobType == CronJobType.MONTHLY_INTEREST_COMPUTATION_SPECIAL_DEPOSIT
                && p.JobDate.Date == currentDate.Date && p.JobStatus == CronJobStatus.PENDING).Include(p => p.DeductionSchedule).FirstOrDefault();

                if (sdCronJob == null) return;
            }

            if (sdCronJob.CronJobType != CronJobType.MONTHLY_INTEREST_COMPUTATION_SPECIAL_DEPOSIT && sdCronJob.JobDate.Date != currentDate.Date && sdCronJob.JobStatus != CronJobStatus.PENDING) return;


            if (schedule == null)
            {
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

            #region Spooling all affected accounts

            //var accountsFundedByPayRoll = await _dbContext.SpecialDepositAccounts.Where(p => p.AccountNo == "4007")
            //     .Include(s => s.Application)
            //           .Include(s => s.DepositAccount)
            //           .Include(s => s.Customer).ToListAsync();


            var accountsFundedByPayRoll = await _dbContext.SpecialDepositAccounts.
               Where(d => d.Application.ModeOfPayment == DepositFundingSourceType.PAYROLL && d.IsClosed == false)
               .Include(s => s.Application)
               .Include(s => s.DepositAccount)
               .Include(s => s.Customer).ToListAsync();

            if (accountsFundedByPayRoll.Any())
            {
                foreach (var depositAccount in accountsFundedByPayRoll)
                {
                    await ProcessInterestWithFundingSourceByPayrollNoActions(depositAccount, schedule.Id);
                    totalRecordProcessed++;
                }
            }


            var accountsFundedBySpecialDeposit = await _dbContext.SpecialDepositAccounts.
                        Where(d => d.Application.ModeOfPayment == DepositFundingSourceType.SPECIAL_DEPOSIT && d.IsClosed == false)
                        .Include(s => s.Application)
                       .Include(s => s.DepositAccount)
                       .Include(s => s.Customer).ToListAsync();

            //var accountsFundedBySpecialDeposit = await _dbContext.SpecialDepositAccounts.Where(p => p.AccountNo == "4009")
            //            .Include(s => s.Application)
            //           .Include(s => s.DepositAccount)
            //           .Include(s => s.Customer).ToListAsync();

            if (accountsFundedBySpecialDeposit.Any())
            {
               
                foreach (var depositAccount in accountsFundedBySpecialDeposit)
                {
                    await ProcessInterestWithFundingSourceByNoActionsNorPayroll(depositAccount, schedule.Id);
                    totalRecordProcessed++;
                }
            }


            #endregion
            if (accountsFundedByPayRoll.Count == 0 && accountsFundedBySpecialDeposit.Count == 0) return;

            sdCronJob.RecordsProcessed = totalRecordProcessed;
            sdCronJob.JobStatus = CronJobStatus.COMPLETED;
            _dbContext.PayrollCronJobConfigs.Update(sdCronJob);

            schedule.ProcessedDate = DateTime.UtcNow;
            schedule.IsProcessed = true;
            _dbContext.SpecialDepositInterestSchedules.Update(schedule);

            _dbContext.SaveChanges();
        }
         
        private async Task ProcessInterestWithFundingSourceByNoActionsNorPayroll(SpecialDepositAccount depositAccount, string scheduleId)
        {
            #region                            Chevron Algorithm

            //For SD that does not have monthly contribution or any other action for the month being considered.
            //Interest -for-the - month = SDIntRate / 100 * SDBalance -as- at - month - end - being - considered / 12

            //Note:
            //Cash addition, Fund transfer and withdrawal impact interest computation because one of the parameters for Special deposit interest computation is the SD balance.
            //SD Interest computation is saved monthly and done at the end of the month.  

            #endregion

            decimal oldBalance = depositAccount.DepositAccount.LedgerBalance;
            decimal sDIntRate = depositAccount.InterestRate;
            decimal calculatedInterest = 0;
            decimal roundedCalculatedInterest = 0;

            var totalDaysInYear = DateTime.IsLeapYear(DateTime.UtcNow.Year) ? 366 : 365;

            calculatedInterest = ((sDIntRate / 100) * oldBalance)/ totalDaysInYear;

            roundedCalculatedInterest = Math.Round(calculatedInterest, 2);

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
            await _mediator.Send(command);
        }
        private async Task ProcessInterestWithFundingSourceByPayrollNoActions(SpecialDepositAccount depositAccount, string scheduleId)
        {
            #region                            Chevron Algorithm

            //For SD that has only monthly contribution(Payroll) for the month being considered
            //  Interest -for-the - month = (SDIntRate / 100 * SDBalance -as- at - month - end - being - considered / 12) + (SDIntRate / 100 * MonthlyContribAmount / 365 * 7)
            // Please note that the SDBalance -as- at - month - end - being - considered in (b) is less the month’s MonthlyContribAmount.

            //Note:
            //Cash addition, Fund transfer and withdrawal impact interest computation because one of the parameters for Special deposit interest computation is the SD balance.
            //SD Interest computation is saved monthly and done at the end of the month.  
            #endregion

            decimal oldBalance = depositAccount.DepositAccount.LedgerBalance;
            decimal monthlyContributeAmount = depositAccount.FundingAmount;
            decimal sDIntRate = depositAccount.InterestRate;
            decimal calculatedInterest = 0;
            decimal roundedCalculatedInterest = 0;
            var totalDaysInYear = DateTime.IsLeapYear(DateTime.UtcNow.Year) ? 366 : 365;

            decimal monthlyInterest = ((sDIntRate / 100) * oldBalance)/ totalDaysInYear + ((sDIntRate / 100) * monthlyContributeAmount) / (totalDaysInYear * 7);
            calculatedInterest += monthlyInterest;

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
            await _mediator.Send(command);
        }
    }
}
