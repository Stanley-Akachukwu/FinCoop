using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositImmediateLiquidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces
{
    public interface IFixedDepositInterestComputationService
    {
        DateTime GetMaturityDate(FixedDepositAccount account);
        Task ProcessFixedDepositAccountThatIsliquidatedBeforeMaturity(FixedDepositLiquidation fixedDepositLiquidation);
        Task ProcessInterestComputation();
        Task RepostFundingTransaction(string fixedDepositId);

        decimal TopedUpInvestment(FixedDepositAccount account, decimal topUpAmount);


    }
}
