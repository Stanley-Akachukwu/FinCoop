using System;
using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Entities.Payroll;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionSchedules;

namespace AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces
{
    public interface IPayrollScheduleBackgroundService
    {
        Task<bool> GetSavingDepositDeductions();
        Task<bool> GetSpecialDepositDeductions();
        Task<bool> GetLoanRepaymentDeductions();

        Task<bool> CreateScheduledJobs(PayrollDeductionSchedule payrollDeductionSchedule);
        Task<bool> MatchDeductionAndPayrollData([Required] string scheduleId);
        Task TestEmailService(string email);

    }
}

