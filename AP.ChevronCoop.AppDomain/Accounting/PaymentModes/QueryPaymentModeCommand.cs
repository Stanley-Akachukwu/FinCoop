using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting;
using AP.ChevronCoop.Entities.Accounting.PaymentModes;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Accounting.PaymentModes
{
    public class QueryPaymentModeCommand : IRequest<CommandResult<IQueryable<PaymentMode>>>
    {

    }







}
