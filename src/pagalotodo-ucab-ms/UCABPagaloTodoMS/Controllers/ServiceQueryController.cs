using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Base;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Exceptions;

namespace UCABPagaloTodoMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceQueryController : BaseController<ServiceQueryController>
    {
        private readonly IMediator _mediator;

        public ServiceQueryController(ILogger<ServiceQueryController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Endpoint para la consulta de todos los servicios.
        /// </summary>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON que contiene todos los servicios.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// 

        [HttpGet("AllServices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AllServices()
        {
            _logger.LogInformation("Entrando al metodo que consulta todos los servicios");
            try
            {
                var query = new AllServicesQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (AllDataNotFoundException ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los servicios: No hay servicios registrados" + ex);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                throw;
            }
        }

		[HttpPost("ByServiceName")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> byServiceName([FromBody] OneServiceRequest ServiceName)
		{
			_logger.LogInformation("Entrando al metodo que consulta los servicios por nombre");
            try
            {
                var query = new ServiceByServiceNameQuery(ServiceName);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UserIsNotProviderException ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los servicios: El usuario no es un proveedor de servicios" + ex);
                return BadRequest(ex.Message);
            }
            catch (ServiceNotFoundException ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los servicios: El proveedor no tiene este servicio" + ex);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                throw;
            }
		}

        /// <summary>
        /// Endpoint para la consulta de un servicio por identificador único.
        /// </summary>
        /// <param name="id">Identificador único del servicio a consultar.</param>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON que contiene el servicio consultado.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// 

        [HttpGet("ByGuid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> byGuid([FromQuery] Guid id)
        {
            _logger.LogInformation("Entrando al metodo que consulta los servicios por nombre");
            try
            {
                var query = new ServiceByGuidQuery(id);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (ServiceNotFoundException ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los servicios: No se encontró el servicio" + ex);
                return NotFound(ex.Message);
            }
            catch (UserIdNotFoundException ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los servicios: No se encontró el usuario" + ex);
                return NotFound(ex.Message);
            }
            catch (UserIsNotProviderException ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los servicios: El usuario no es un proveedor de servicios" + ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                throw;
            }
        }
        [HttpGet("AllbyProviderId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AllbyProviderId([FromQuery] Guid id)
        {
            _logger.LogInformation("Entrando al metodo que consulta los servicios por nombre");
            try
            {
                var query = new AllServicesByProviderIdQuery(id);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (UserIdNotFoundException ex) {
                _logger.LogError("Ocurrio un error en la consulta de los servicios: No se encontró el proveedor de servicios" + ex);
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                throw;
            }
        }
    }
}