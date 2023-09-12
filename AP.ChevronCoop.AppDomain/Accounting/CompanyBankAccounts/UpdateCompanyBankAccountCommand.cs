using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Accounting.CompanyBankAccounts
{
    public partial class UpdateCompanyBankAccountCommand : UpdateCommand, IRequest<CommandResult<CompanyBankAccountViewModel>>
    {

        //[MaxLength(80)]
        //[Required]
        //public string LedgerAccountId { get; set; }

        [MaxLength(80)]
        [Required]
        public string BankId { get; set; }

        [MaxLength(256)]

        public string BranchName { get; set; }

        [MaxLength(256)]

        public string BranchAddress { get; set; }

        [MaxLength(80)]
        [Required]
        public string CurrencyId { get; set; }

        [MaxLength(256)]
        [Required]
        public string AccountName { get; set; }

        [MaxLength(64)]
        [Required]
        public string AccountNumber { get; set; }

        [MaxLength(64)]

        public string BVN { get; set; }



    }







}
