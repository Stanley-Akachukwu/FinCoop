using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestSchedules;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems
{
    public class FixedDepositInterestScheduleItem : BaseEntity<string>
    {
        public FixedDepositInterestScheduleItem()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }

        public string FixedDepositAccountId { get; set; }
        public FixedDepositAccount FixedDepositAccount { get; set; }

        //in actual fact, oldbalance already contains new cash addition/funding
        //existing balance, new funding/cash addition, old balance, new balance, interest earned, 
        //april balance=100k, funding=20k, rate=10%, new balance=interest earned+old balance
        //interest earned=interest computation(old balance+addition)=interest(100k+20k)


        public string FixedDepositInterestScheduleId { get; set; }
        public FixedDepositInterestSchedule FixedDepositInterestSchedule { get; set; }
        
        public decimal OldBalance { get; set; }
        public decimal PeriodCashAddition { get; set; } //new funding for the period under consideration
        public decimal InterestRate { get; set; } //mandatory
        public decimal InterestEarned { get; set; }//=INTEREST(rate * old balance)
        public decimal NewBalance { get; set; }// == interested earned+old balance
        // public DepositFundingSourceType PaymentMode { get; set; }

        public override string DisplayCaption { get; }
        public override string DropdownCaption { get; }
        public override string ShortCaption { get; }

    }


}
