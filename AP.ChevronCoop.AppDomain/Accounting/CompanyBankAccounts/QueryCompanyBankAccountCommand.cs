using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting;
using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Accounting.CompanyBankAccounts
{
    public class QueryCompanyBankAccountCommand : IRequest<CommandResult<IQueryable<CompanyBankAccount>>>
    {

    }







}
