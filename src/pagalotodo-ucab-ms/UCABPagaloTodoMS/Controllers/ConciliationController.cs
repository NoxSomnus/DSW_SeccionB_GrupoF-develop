using MediatR;
using Microsoft.AspNetCore.Mvc;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Exceptions;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Base;
using UCABPagaloTodoMS.Application.Queries;

namespace UCABPagaloTodoMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConciliationController : BaseController<ConciliationController>
    {
        private readonly IMediator _mediator;
        public ConciliationController(ILogger<ConciliationController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        

        [HttpPost("AddConciliationFileConfig")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddConciliationFileConfig([FromBody] AddConciliationFileConfigRequest request)
        {
            _logger.LogInformation("Entrando al metodo que genera la configuracion del archivo de conciliacion y lo guarda en bd");
            try
            {
                var command = new AddConciliationFileConfigCommand (request);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch(UserIdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: 0" + ex);
                throw;
            }
        }

        [HttpPost("CreateConciliationFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateConciliationFile([FromBody] CreateConciliationFileRequest request)
        {
            _logger.LogInformation("Entrando al Controllador que genera el archivo de conciliacion");
            try
            {
                var query = new CreateConciliationFileQuery(request);
                var response = await  _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: 0" + ex);
                throw;
            }
        }
        [HttpPost("SendConciliationFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> SendConciliationFile([FromBody] SendFileConciliationRequest request)
        {
            _logger.LogInformation("Entrando al Controllador que envia el archivo de conciliacion");
            try
            {

                var query = new SendConciliationfileQuery(request );
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: 0" + ex);
                throw;
            }
        }
    }
}
