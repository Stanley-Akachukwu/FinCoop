using System;
using AP.ChevronCoop.AppCore.Loans.LoanAccounts;
using AP.ChevronCoop.AppDomain.Loans.LoanAccounts;
using AP.ChevronCoop.AppDomain.NetPay.MemberExposure;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Employees;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AP.ChevronCoop.AppCore.NetPay
{

    public class NetPayCommandHandler : IRequestHandler<CreateMemberExposureCommand, MemberExposureViewModel>,
          IRequestHandler<MonthlyPayrollScheduleCommand,
              MonthlyPayrollScheduleViewModel>
    {

        private readonly ChevronCoopDbContext _dbContext;
        private readonly CoreAppSettings _options;
        private readonly IEmailService _emailService;
        private readonly ILogger<LoanAccountCommandHandler> _logger;
        private readonly IMapper _mapper;


        public NetPayCommandHandler(ChevronCoopDbContext dbContext, IOptions<CoreAppSettings> options, IEmailService emailService,
          ILogger<LoanAccountCommandHandler> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _options = options.Value;
            _emailService = emailService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<MemberExposureViewModel> Handle(CreateMemberExposureCommand request, CancellationToken cancellationToken)
        {
            // get employee total exposure for the year.
            // 1. exposure from savings
            // 2. exposruee from SD
            // 3. loan offset from repayment schedule for the monthe and year.

            decimal totalMemberExposure = 0M;
            var response = new MemberExposureViewModel();

            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.MemberId == request.EmployeeNo && c.IsActive && !c.IsDeleted, cancellationToken: cancellationToken);

            if (customer == null)
            {
                response.Error = true;
                response.Message = $"Customer not found for :{request.EmployeeNo}";
                return response;
            }

            // get exposure on savings 
            var savingAccount = await _dbContext.SavingsAccountDeductionSchedules
               .FirstOrDefaultAsync(c => c.EmployeeNo == request.EmployeeNo && c.DueDate.Year == request.Year && c.DueDate.Month == request.Month,
                 cancellationToken: cancellationToken);

            if (savingAccount != null)
                totalMemberExposure += savingAccount.Amount;

            // get exposure on special deposit

            var specialDeposit = await _dbContext.SpecialDepositAccountDeductionSchedules
             .FirstOrDefaultAsync(c => c.EmployeeNo == request.EmployeeNo && c.DueDate.Year == request.Year && c.DueDate.Month == request.Month, cancellationToken: cancellationToken);

            if (specialDeposit != null)
                totalMemberExposure += specialDeposit.Amount;

            // get exposure on loan offset from Payroll

            var loanRepayment = await _dbContext.LoanRepaymentSchedules.Include(c => c.LoanAccount).Include(c => c.LoanAccount.Customer).FirstOrDefaultAsync(c => c.LoanAccount.Customer.MemberId == request.EmployeeNo && c.DueDate.Year == request.Year
                && c.DueDate.Month == request.Month, cancellationToken: cancellationToken);

            if (loanRepayment != null)
                totalMemberExposure += loanRepayment.TotalBalance;

            if (totalMemberExposure <= 0)
            {
                response.Error = false;
                response.Message = $"Member {request.EmployeeNo} does not have any exposure for month:{request.Month} , year : {request.Year}";

                return response;
            }

            response.Error = false;
            response.Value = Math.Round(totalMemberExposure, 2);
            response.Message = "Success";
            return response;
        }


        public async Task<MonthlyPayrollScheduleViewModel> Handle(MonthlyPayrollScheduleCommand request, CancellationToken cancellationToken)
        {
            var response = new MonthlyPayrollScheduleViewModel();
            var customers = await _dbContext.Customers.Where(c => c.IsActive && !c.IsDeleted).ToListAsync(cancellationToken: cancellationToken);

            if (customers.Count <= 0)
            {
                response.Error = true;
                return response;
            }

            var getSavingProductSchedule = await _dbContext.SavingsAccountDeductionSchedules.Include(c => c.SavingsAccount).Include(c => c.SavingsAccount.DepositProduct).
                Include(c => c.SavingsAccount.Customer).Where(c => c.DueDate.Month == request.Month && c.DueDate.Year == request.Year).ToListAsync(cancellationToken: cancellationToken);


            var getSpecialDepositSchedule = await _dbContext.SpecialDepositAccountDeductionSchedules.Include(c => c.SpecialDepositAccount).Include(c => c.SpecialDepositAccount.DepositProduct)
                .Include(c => c.SpecialDepositAccount.Customer)
                .Where(c => c.DueDate.Month == request.Month && c.DueDate.Year == request.Year).ToListAsync(cancellationToken: cancellationToken);


            var getloanRepaymentSchedule = await _dbContext.LoanRepaymentSchedules.Include(c => c.LoanAccount).Include(c => c.LoanAccount.LoanApplication).Include(c => c.LoanAccount.LoanApplication.LoanProduct).Include(c => c.LoanAccount.Customer)
                .Where(c => c.DueDate.Month == request.Month && c.DueDate.Year == request.Year).ToListAsync(cancellationToken: cancellationToken);

            decimal deductionAmount = 0m;
            foreach (var customer in customers)
            {
                var savingSchedule = getSavingProductSchedule.FirstOrDefault(c => c.SavingsAccount.Customer.Id == customer.Id);


                if (savingSchedule != null)
                {
                    deductionAmount = savingSchedule.Amount;
                    response.Data.Add(new PayrollScheduleDataViewModel()
                    {
                        DbaCode = savingSchedule?.SavingsAccount?.DepositProduct?.Code,
                        DeductionAmount = Math.Round(deductionAmount, 2),
                        EmployeeName = savingSchedule?.MemberName,
                        EmployeeNo = savingSchedule?.MemberId,
                        Voucher = _options.ChevronKey
                    });
                }


                var specialDepositSchedule = getSpecialDepositSchedule.FirstOrDefault(c => c.SpecialDepositAccount.Customer.Id == customer.Id);

                if (specialDepositSchedule != null)
                {
                    deductionAmount = specialDepositSchedule.Amount;
                    response.Data.Add(new PayrollScheduleDataViewModel()
                    {
                        DbaCode = specialDepositSchedule?.SpecialDepositAccount?.DepositProduct?.Code,
                        DeductionAmount = Math.Round(deductionAmount, 2),
                        EmployeeName = specialDepositSchedule?.MemberName,
                        EmployeeNo = specialDepositSchedule?.MemberId,
                        Voucher = _options.ChevronKey
                    });

                }

                var loanRepaymentSchedule = getloanRepaymentSchedule.FirstOrDefault(c => c.LoanAccount.Customer.Id == customer.Id);

                if (loanRepaymentSchedule != null)
                {
                    deductionAmount = loanRepaymentSchedule.TotalBalance;
                    response.Data.Add(new PayrollScheduleDataViewModel()
                    {
                        DbaCode = loanRepaymentSchedule?.LoanAccount?.LoanApplication?.LoanProduct?.Code,
                        DeductionAmount = Math.Round(deductionAmount, 2),
                        EmployeeName = loanRepaymentSchedule?.LoanAccount.Customer.FirstName,
                        EmployeeNo = loanRepaymentSchedule?.LoanAccount.Customer.CustomerNo,
                        Voucher = _options.ChevronKey
                    });
                }


            }

            response.Error = false;

            return response;
        }
    }
}

