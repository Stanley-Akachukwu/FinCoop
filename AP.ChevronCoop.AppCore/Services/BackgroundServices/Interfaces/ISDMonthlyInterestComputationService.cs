
using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;

namespace AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces
{
    public interface ISDMonthlyInterestComputationService
    {
        Task ComputeSpecialDepositMonthlyInterests();
    }
}



