using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Base;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Exceptions;
using UCABPagaloTodoMS.Application.Commands;

namespace UCABPagaloTodoMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebtsController : BaseController<DebtsController>
    {
        private readonly IMediator _mediator;

        public DebtsController(ILogger<DebtsController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Endpoint para añadir los deudores a la tabla PaymentByConciliation.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite añadir registros a la tabla PaymentByConciliation por medio de un HTTP POST.
        /// </remarks>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON con la información de todas las facturas de servicios.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// <returns>Objeto JSON con la información de los Guids registrados.</returns>

        [HttpPost("AddDebts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddDebts([FromBody] DebtRequest request)
        {
            _logger.LogInformation("Entrando al metodo que consulta todos los servicios");
            try
            {
                var command = new AddDebtToServiceCommand(request);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogError(ex, "Error AddDebtToServiceCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                return NotFound(ex.Message);
            }
            catch (ServiceNotFoundException ex)
            {
                _logger.LogError(ex, "Error AddDebtToServiceCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                return NotFound(ex.Message);
            }
            catch (UserIsNotProviderException ex)
            {
                _logger.LogError(ex, "Error AddDebtToServiceCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (UserIsNotCustomerException ex)
            {
                _logger.LogError(ex, "Error AddDebtToServiceCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (NotByConciliationPaymentException ex) 
            {
                _logger.LogError(ex, "Error AddDebtToServiceCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                throw;
            }
        }

        [HttpGet("UserDebtByService")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UserDebtByService([FromQuery] Guid user, [FromQuery] Guid service)
        {
            _logger.LogInformation("Entrando al metodo que consulta todos los servicios");
            try
            {
                var query = new UserDebtInServiceQuery(user, service);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (DebtNotFoundException ex) 
            {
                _logger.LogError("Ocurrio un error en la consulta de las deudas del usuario en un servicio. Exception: " + ex);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                throw;
            }
        }

        [HttpGet("AllDebtsByUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AllDebtsByUserId([FromQuery] Guid user)
        {
            _logger.LogInformation("Entrando al metodo que consulta todos los servicios");
            try
            {
                var query = new AllDebtsByUserIdQuery(user);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (AllDataNotFoundException ex) 
            {
                _logger.LogError("Ocurrio un error en la consulta de las deudas del usuario. Exception: " + ex);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: " + ex);
                throw;
            }
        }

        [HttpGet("AllDebtorsByServiceId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AllDebtorsByServiceId([FromQuery] Guid service)
        {
            _logger.LogInformation("Entrando al metodo que consulta todos los servicios");
            try
            {
                var query = new AllDebtorsByServiceIdQuery(service);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (ServiceNotFoundException ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de las deudas del servicio. Exception: " + ex);
                return NotFound(ex.Message);
            }
            catch (NotByConciliationPaymentException ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de las deudas del servicio. Exception: " + ex);
                return BadRequest(ex.Message);
            }
            catch (AllDataNotFoundException ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los deudores del servicio. Exception: " + ex);
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
