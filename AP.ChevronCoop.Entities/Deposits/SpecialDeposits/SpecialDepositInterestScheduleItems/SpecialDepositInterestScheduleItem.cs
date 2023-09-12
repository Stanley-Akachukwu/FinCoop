using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestSchedules;

namespace AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestScheduleItems
{
    public class SpecialDepositInterestScheduleItem : BaseEntity<string>
    {

        public SpecialDepositInterestScheduleItem()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        
        public string SpecialDepositAccountId { get; set; }
        public SpecialDepositAccount SpecialDepositAccount { get; set; }
        
        public string SpecialDepositInterestScheduleId { get; set; }
        public SpecialDepositInterestSchedule SpecialDepositInterestSchedule { get; set; } 

        //in actual fact, oldbalance already contains new cash addition/funding
        //existing balance, new funding/cash addition, old balance, new balance, interest earned, 
        //april balance=100k, funding=20k, rate=10%, new balance=interest earned+old balance
        //interest earned=interest computation(old balance+addition)=interest(100k+20k)


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



