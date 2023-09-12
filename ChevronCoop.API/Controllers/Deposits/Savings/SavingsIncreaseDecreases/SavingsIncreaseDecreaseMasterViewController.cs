using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChevronCoop.API.Config;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ChevronCoop.API.Controllers.Deposits.Savings.SavingIncreaseDecreases;


[ApiController]
//[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = false, GroupName = "Deposits")]
[Route("[controller]")]
[ODataAttributeRouting]

public class SavingsIncreaseDecreaseMasterViewController : ControllerBase //ODataController
{

    private readonly IMediator mediator;
    private readonly ILogger<SavingsIncreaseDecreaseMasterViewController> logger;
    private readonly IMapper mapper;
    private readonly ChevronCoopDbContext dbContext;
    public SavingsIncreaseDecreaseMasterViewController(IMediator _mediator, ChevronCoopDbContext appDb,
    ILogger<SavingsIncreaseDecreaseMasterViewController> _logger, IMapper _mapper)
    {
        mediator = _mediator;
        logger = _logger;
        mapper = _mapper;
        dbContext = appDb;
    }

    [EnableQuery]
    [HttpGet]
    //[ODataRoute]
    [ProducesResponseType(typeof(ODataResponse<SavingsIncreaseDecreaseMasterView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        return Ok(dbContext.SavingsIncreaseDecreaseMasterView);
    }




}



