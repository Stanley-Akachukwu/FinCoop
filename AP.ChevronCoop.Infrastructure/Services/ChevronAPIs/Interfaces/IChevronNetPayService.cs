using System;
using AP.ChevronCoop.Infrastructure.Services.ChevronAPIs.Dto;

namespace AP.ChevronCoop.AppCore.ChevronAPIs.Interfaces
{
    public interface IChevronNetPayService
    {
        Task<bool> CanEmployeeCollectLoanAsync(EmployeeCollectLoanRequestDto employeeCollectLoanRequestDto);
        Task<bool> CanEmployeeCollecTargetLoanAsync(EmployeeCollectTargetLoanRequestDto employeeCollectTargetLoanRequestDto);
        Task<bool> CanEmployeeCollecOneTimeIncreaseAsync(EmployeeCollectOneTimeIncrease employeeCollectOneTimeIncrease);
    }
}

