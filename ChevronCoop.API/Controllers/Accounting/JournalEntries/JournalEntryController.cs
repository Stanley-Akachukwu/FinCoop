
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using System.ComponentModel.DataAnnotations;

using AP.ChevronCoop.Commons;
using AP.ChevronCoop.AppDomain.Accounting.JournalEntries;
using ChevronCoop.API.Config;

namespace ChevronCoop.API.Controllers.Accounting.JournalEntries
{
    [ApiController]
    //[ApiVersion("1.0")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = "Accounting")]
    [Route("[controller]")]
    [ODataAttributeRouting]

    public class JournalEntryController : ControllerBase //ODataController
    {

        private readonly IMediator mediator;
        private readonly ILogger<JournalEntryController> logger;
        private readonly IMapper mapper;

        public JournalEntryController(IMediator _mediator,
        ILogger<JournalEntryController> _logger, IMapper _mapper)
        {
            mediator = _mediator;
            logger = _logger;
            mapper = _mapper;
        }

        [EnableQuery]
        [HttpGet]
        //[ODataRoute]
        [ProducesResponseType(typeof(ODataResponse<JournalEntryViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var request = new QueryJournalEntryCommand();

            var rsp = await mediator.Send(request);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
            return result;
        }


        [EnableQuery]
        [HttpGet("({key})")]
        // [ODataRoute("({key})")]
        [ProducesResponseType(typeof(JournalEntryViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<SingleResult<JournalEntryViewModel>> Get([FromODataUri][Required][CustomizeValidator(Skip = true)] string key)
        {
            var request = new QueryJournalEntryCommand();
            var rsp = await mediator.Send(request);
            var queryable = rsp.Response.Where(p => p.Id == key);
            return SingleResult.Create(queryable.ProjectTo<JournalEntryViewModel>(mapper.ConfigurationProvider));

        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(CommandResult<JournalEntryViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateJournalEntryCommand model)
        {

            var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }


        [HttpPost("update")]
        [ProducesResponseType(typeof(CommandResult<JournalEntryViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdateJournalEntryCommand model)
        {
            var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }


        [HttpPost("delete")]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] DeleteJournalEntryCommand model)
        {
            var rsp = await mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;

        }


    }






















}