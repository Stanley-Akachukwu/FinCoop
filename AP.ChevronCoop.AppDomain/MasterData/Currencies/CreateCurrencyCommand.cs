using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.MasterData.Currencies;

public class CreateCurrencyCommand : CreateCommand, IRequest<CommandResult<CurrencyViewModel>>
{
  public string Code { get; set; }
  public string Name { get; set; }
  public string Symbol { get; set; }
  public string IsoSymbol { get; set; }
  public int DecimalPlaces { get; set; }
  public string Format { get; set; }
}