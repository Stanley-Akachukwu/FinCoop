using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.MasterData.Country;
using AP.ChevronCoop.AppDomain.MasterData.Currencies;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.MasterData.Currencies;

public class CurrencyCommandHandler :
  IRequestHandler<QueryCurrencyCommand, CommandResult<IQueryable<Currency>>>,
  IRequestHandler<CreateCurrencyCommand, CommandResult<CurrencyViewModel>>,
  IRequestHandler<UpdateCurrencyCommand, CommandResult<CurrencyViewModel>>,
  IRequestHandler<DeleteCurrencyCommand, CommandResult<string>>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger logger;
  private readonly IMapper mapper;

  public CurrencyCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<CurrencyCommandHandler> _logger, IMapper _mapper)
  {
    dbContext = appDbContext;
    logger = _logger;
    mapper = _mapper;
  }


  public async Task<CommandResult<CurrencyViewModel>> Handle(CreateCurrencyCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<CurrencyViewModel>();

    try
    {
      var entity = mapper.Map<Currency>(request);
      //entity.Code = NHiloHelper.GetNextKey(nameof(Currency)).ToString();
      
      dbContext.Currencies.Add(entity);
      await dbContext.SaveChangesAsync();

      rsp.Response = mapper.Map<CurrencyViewModel>(entity);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, ex.Message);
      rsp.StatusCode = 500;
      rsp.ErrorFlag = true;
      rsp.Detail = ex.Message;
    }


    return rsp;
  }

  public async Task<CommandResult<string>> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<string>();
    var entity = await dbContext.Currencies.FindAsync(request.Id);

    dbContext.Currencies.Remove(entity);
    await dbContext.SaveChangesAsync();

    rsp.Response = "Data successfully deleted";

    return rsp;
  }


  public Task<CommandResult<IQueryable<Currency>>> Handle(QueryCurrencyCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<IQueryable<Currency>>();
    rsp.Response = dbContext.Currencies;

    return Task.FromResult(rsp);
  }

  public async Task<CommandResult<CurrencyViewModel>> Handle(UpdateCurrencyCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<CurrencyViewModel>();
    var entity = await dbContext.Currencies.FindAsync(request.Id);

    mapper.Map(request, entity);

    dbContext.Currencies.Update(entity);
    await dbContext.SaveChangesAsync();

    rsp.Response = mapper.Map<CurrencyViewModel>(entity);

    return rsp;
  }
}