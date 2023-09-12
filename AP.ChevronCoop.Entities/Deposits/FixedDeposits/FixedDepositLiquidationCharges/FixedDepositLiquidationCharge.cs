using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositImmediateLiquidations;
using AP.ChevronCoop.Entities.LoanTopupTransactions;
using AP.ChevronCoop.Entities.MasterData.Charges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositLiquidationCharges
{
    public class FixedDepositLiquidationCharge : BaseEntity<string>
    {

        public FixedDepositLiquidationCharge()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        public string FixedDepositLiquidationId { get; set; }
        public virtual FixedDepositLiquidation FixedDepositLiquidation { get; set; }

        public ChargeType ChargeType { get; set; }
        public decimal LiquidationCharge { get; set; }

        public string? TransactionJournalId { get; set; }
        public virtual TransactionJournal? TransactionJournal { get; set; }


        public override string DisplayCaption { get; }
        public override string DropdownCaption { get; }
        public override string ShortCaption { get; }
    }
}
