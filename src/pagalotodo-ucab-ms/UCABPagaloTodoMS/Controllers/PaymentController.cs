using MediatR;
using Microsoft.AspNetCore.Mvc;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Exceptions;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Base;

namespace UCABPagaloTodoMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : BaseController<PaymentController>
    {
        private readonly IMediator _mediator;
        public PaymentController(ILogger<PaymentController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint para agregar un pago al sistema.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite a un usuario agregar un pago al sistema a través de una solicitud HTTP POST.
        /// </remarks>
        /// <param name="request">Objeto JSON en el cuerpo de la solicitud con las propiedades necesarias para agregar el pago.</param>
        /// <response code="200">La solicitud se procesa correctamente y devuelve un objeto JSON con la información del pago agregado.</response>
        /// <response code="400">La solicitud es incorrecta y devuelve un mensaje de error en la respuesta.</response>
        /// <response code="500">Se produjo un error interno en el servidor al procesar la solicitud.</response>
        /// <returns>Objeto JSON con la información del pago agregado.</returns>
        
        [HttpPost("AddPayment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddService([FromBody] AddPaymentRequest request)
        {
            _logger.LogInformation("Entrando al metodo que consulta si el usuario esta registrado");
            try
            {
                var query = new AddPaymentCommand(request);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UserIsNotCustomerException ex)
            {
                _logger.LogError(ex, "Ocurrió un error al registrar el pago: Para hacer un pago debe ser un usuario consumidor", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (ServiceNotFoundException ex)
            {
                _logger.LogError(ex, "Ocurrió un error al registrar el pago: No existe el servicio", ex.Message);
                return NotFound(ex.Message);
            }
            catch (PaymentOptionNotFoundException ex)
            {
                _logger.LogError(ex, "Ocurrió un error al registrar el pago: No existe la opcion de pago", ex.Message);
                return NotFound(ex.Message);
            }
            catch (PaymentOptionIsNotActiveException ex)
            {
                _logger.LogError(ex, "Ocurrió un error al registrar el pago: Opción de pago inactiva", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (InvalidRequestFormatException ex)
            {
                _logger.LogError(ex, "Formato de Request inválido", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error en la consulta de los valores de prueba. Exception: 0" + ex);
                throw;
            }
        }
    }
}
