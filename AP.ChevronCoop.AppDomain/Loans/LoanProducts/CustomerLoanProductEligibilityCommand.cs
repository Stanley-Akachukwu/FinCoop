using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanProducts
{
    public class CustomerLoanProductEligibilityCommand : IRequest<CommandResult<bool>>
    {
        public string CustomerId { get; set; }
        public string LoanProductId { get; set; }
    }
}
