using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Exceptions;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    /// <summary>
    /// Manejador de consulta que devuelve una lista de todos los campos requeridos de un metodo de pago.
    /// </summary>
    internal class AllRequiredFieldsByPaymentOptionQueryHandler : IRequestHandler<AllRequiredFieldsByPaymentOptionQuery, List<PaymentRequiredFieldsResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AllRequiredFieldsByPaymentOptionQueryHandler> _logger;


        /// <summary>
        /// Constructor de la clase PaymentOptionsByServiceIdQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar los campos de la opcion de pago.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public AllRequiredFieldsByPaymentOptionQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AllRequiredFieldsByPaymentOptionQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de la consulta que busca los campos requeridos de una opcion de pago
        /// </summary>
        /// <param name="request">La consulta AllRequiredFieldsByPaymentOptionQuery que especifica el ID dela opcion de pago para buscar sus campos requeridos.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de objetos PaymentRequiredFieldsResponse que contienen información detallada de los campos de la opcion de pago.</returns>
        public Task<List<PaymentRequiredFieldsResponse>> Handle(AllRequiredFieldsByPaymentOptionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("UserLoginQueryHandler.Handle: Request null.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("ConsultarValoresQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }


        /// <summary>
        /// Método privado que maneja la búsqueda de campos requeridos por opcion de pago.
        /// </summary>
        /// <param name="request">La consulta AllRequiredFieldsByPaymentOptionQuery que especifica el ID dela opcion de pago para buscar sus campos requeridos.</param>
        /// <returns>Una lista de objetos PaymentRequiredFieldsResponse que contienen información detallada de los campos de la opcion de pago.</returns>
        private async Task<List<PaymentRequiredFieldsResponse>> HandleAsync(AllRequiredFieldsByPaymentOptionQuery request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("UserLoginQueryHandler.HandleAsync");

                var fields = _dbContext.PaymentRequiredFieldEntities.Where(c => c.PaymentOptionId == request.PaymentOptionId).Select(c => new PaymentRequiredFieldsResponse()
                {
                    PaymentOptionId = c.PaymentOptionId,
                    RequiredFieldId = c.Id,
                    FieldName = c.FieldName,
                    isNumber = c.isNumber,
                    isString = c.isString,
                    Length = c.Length
                });

              //  if (fields.Count() == 0)
             //   {
              //      throw new RequiredFieldsNotFoundException("El servicio no tiene opciones de pago disponibles");
              //  }

                return await fields.ToListAsync();
            }
            catch (RequiredFieldsNotFoundException ex)
            {
                _logger.LogError(ex, "Error: La opcion de pago no tiene configuracion de campos requeridos ", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
