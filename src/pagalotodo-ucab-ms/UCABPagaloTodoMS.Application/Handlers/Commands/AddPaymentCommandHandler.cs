using Automatonymous.Binders;
using MediatR;
using Microsoft.Extensions.Logging;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Exceptions;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.RefactoringMethods;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{

    /// <summary>
    /// Manejador de comando para agregar un pago.
    /// </summary>
    public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, AddPaymentResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AddPaymentCommandHandler> _logger;


        /// <summary>
        /// Constructor de la clase AddPaymentCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para agregar el pago.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de comando.</param>
        public AddPaymentCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AddPaymentCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de comando para agregar un pago a una factura de servicio.
        /// </summary>
        /// <param name="request">Objeto de comando de tipo AddPaymentCommand.</param>
        /// <param name="cancellationToken">Token de cancelación para cancelar la operación asincrónica.</param>
        /// <exception cref="ArgumentNullException">Se lanza si el objeto de comando es nulo.</exception>
        /// <exception cref="InvalidOperationException">Se lanza si el usuario que hizo el pago no existe, si el usuario no es un usuario consumidor, si el servicio no existe, si la opción de pago no existe o si el método de pago no está activo.</exception>
        /// <returns>Un objeto AddPaymentResponse que indica si el pago se ha agregado correctamente.</returns>
        public async Task<AddPaymentResponse> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("ConsultarValoresQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return await HandleAsync(request);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Método asincrónico que agrega un pago a una factura de servicio.
        /// </summary>
        /// <param name="request">Objeto de comando de tipo AddPaymentCommand.</param>
        /// <exception cref="InvalidOperationException">Se lanza si el usuario que hizo el pago no existe, si el usuario no es un usuario consumidor, si el servicio no existe, si la opción de pago no existe o si el método de pago no está activo.</exception>
        /// <returns>Un objeto AddPaymentResponse que indica si el pago se ha agregado correctamente.</returns>
        private async Task<AddPaymentResponse> HandleAsync(AddPaymentCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Request}", request);
                //busca el usuario que hizo el pago para ver si existe en la BD
                var user = _dbContext.UserEntities.FirstOrDefault(c => c.Id == request._request.UserId);
                if (user == null)
                {
                    throw new UserIdNotFoundException("Error: El usuario no existe");
                }

                if (user is ProviderEntity || user is AdminEntity)
                {
                    throw new UserIsNotCustomerException("Error: Para hacer un pago debe ser un usuario consumidor");
                }

                //busca el servicio para ver si existe en la BD
                var service = _dbContext.ServiceEntities.FirstOrDefault(s => s.Id == request._request.ServiceId);

                if (service == null)
                {
                    throw new ServiceNotFoundException("Error: No existe el servicio");
                }

                var option = _dbContext.PaymentOptionEntities.Where(s => s.Id == request._request.PaymentOptionId && s.ServiceId == request._request.ServiceId)
                    .FirstOrDefault();

                if (option == null)
                {
                    throw new PaymentOptionNotFoundException("Error: No existe la opcion de pago");
                }

                if (option.Status != "Activo")
                {
                    throw new PaymentOptionIsNotActiveException("Error: El metodo de pago no esta activo");
                }

                //Verificaciones para que cuando sea el servicio sea de tipo telefonia, no reciba en el request un contractnumber
                //y si no es un servicio de telefonia entonces que el request no reciba un numero de telefono


                /*if (service.TypeService != "Telefonia")
                {
                    if (request._request.PhoneNumber is null or "")
                    {
                        // Si el servicio no requiere un número de teléfono, establece el valor de PhoneNumber en null o en una cadena vacía
                        request._request.PhoneNumber = ""; // o request._request.PhoneNumber = null;
                    }
                    else
                    {
                        throw new InvalidRequestFormatException("Error: El servicio no requiere un número de teléfono, solo requiere un número de contrato");
                    }
                }

                if (service.TypeService == "Telefonia")
                {
                    if (request._request.ContractNumber == null || request._request.ContractNumber == "")
                    {
                        // Si el servicio de telefonia no tiene un número de contrato, establece el valor de ContractNumber en null o en una cadena vacía
                        request._request.ContractNumber = ""; // o request._request.ContractNumber = null;
                    }
                    else
                    {
                        throw new InvalidRequestFormatException("Error: El servicio de telefonia no tiene un numero de contrato");
                    }
                }*/
                /*try       INTENTO DE REFACTORIZACION FALLIDO, NO SE PROGRAGAN LAS EXCEPCIONES EN EL METODO VALIDATE :((((((
                {
                    if (!new AddPaymentRequestValidations(_dbContext).validate(request))
                    {
                        _logger.LogInformation("Registrar pago fallido: Ocurrio un error en la peticion");
                    }
                }*/
                /*var payment = new BillEntity();
                payment.SetContractNumber(request._request.ContractNumber);
                payment.SetPhoneNumber(request._request.PhoneNumber);
                payment.SetAmount(request._request.Amount);
                payment.SetUserId(request._request.UserId);
                payment.SetServiceId(request._request.ServiceId);
                payment.SetPaymentOptionId(request._request.PaymentOptionId);
                payment.SetDate(DateTime.Now.Date);*/
                var fields = new List<PaymentDetailsEntity>();
                foreach (var field in request._request.Fields)
                {
                    var consult = _dbContext.PaymentRequiredFieldEntities.Where(s => s.Id == field.FieldId 
                        && s.PaymentOptionId == request._request.PaymentOptionId)
                        .FirstOrDefault();
                    if (consult == null)
                    {
                        throw new RequiredFieldsNotFoundException("Campo no encontrado");
                    }
                    
                    fields.Add(new PaymentDetailsEntity{ FieldContent = field.Content, RequiredFieldId = consult.Id });
                }
                var payment = new BillEntity
                {
                    Amount = request._request.Amount,
                    UserId = request._request.UserId,
                    ServiceId = request._request.ServiceId,
                    PaymentOptionId = request._request.PaymentOptionId,
                    Date = DateTime.Now.Date,
                    IsConciliated = false,
                    IsApproved = false
                };

                _dbContext.BillEntities.Add(payment);
                await _dbContext.SaveEfContextChanges("APP");
                // probar esto
                foreach (var field in fields) 
                {
                    field.BillId = payment.Id;
                    _dbContext.PaymentDetailsEntities.Add(field);
                    await _dbContext.SaveEfContextChanges("APP");
                }
                transaccion.Commit();
                var response = new AddPaymentResponse
                {
                    success = true,
                    message = "Pago registrado con exito"
                };
                _logger.LogInformation("AddPaymentCommandHandler.HandleAsync {Response}", response);
                return response;
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogError(ex, "Registrar pago fallido: No se encontró el usuario", ex.Message);
                transaccion.Rollback();
                throw;
            }
            catch (UserIsNotCustomerException ex)
            {
                _logger.LogError(ex, "Registrar pago fallido: Para hacer un pago debe ser un usuario consumidor", ex.Message);
                transaccion.Rollback();
                throw;
            }
            catch (ServiceNotFoundException ex)
            {
                _logger.LogError(ex, "Registrar pago fallido: No existe el servicio", ex.Message);
                transaccion.Rollback();
                throw;
            }
            catch (PaymentOptionNotFoundException ex)
            {
                _logger.LogError(ex, "Registrar pago fallido: No existe la opcion de pago", ex.Message);
                transaccion.Rollback();
                throw;
            }
            catch (PaymentOptionIsNotActiveException ex)
            {
                _logger.LogError(ex, "Registrar pago fallido: Opción de pago inactiva", ex.Message);
                transaccion.Rollback();
                throw;
            }
            catch (InvalidRequestFormatException ex)
            {
                _logger.LogError(ex, "Formato de Request inválido", ex.Message);
                transaccion.Rollback();
                throw;
            }
            catch (RequiredFieldsNotFoundException ex)
            {
                _logger.LogError(ex, "Campo no encontrado", ex.Message);
                transaccion.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
        }
    }
}
