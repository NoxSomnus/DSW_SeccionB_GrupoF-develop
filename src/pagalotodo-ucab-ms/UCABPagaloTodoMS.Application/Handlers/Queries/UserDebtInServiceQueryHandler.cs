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
using UCABPagaloTodoMS.Core.Entities;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    /// <summary>
    /// Manejador de consulta que devuelve la deuda de un usuario consumidor en un servicio.
    /// </summary>
    internal class UserDebtInServiceQueryHandler : IRequestHandler<UserDebtInServiceQuery, UserDebtInServiceResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<UserDebtInServiceQueryHandler> _logger;


        /// <summary>
        /// Constructor de la clase UserDebtInServiceQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar el servicio.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public UserDebtInServiceQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<UserDebtInServiceQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de la consulta que busca un servicio por su nombre y el nombre del proveedor que lo ofrece.
        /// </summary>
        /// <param name="request">La consulta ServiceByServiceNameQuery que especifica el nombre del servicio y el nombre del proveedor que lo ofrece.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Un objeto OneServiceResponse que contiene información detallada del servicio.</returns>
        public Task<UserDebtInServiceResponse> Handle(UserDebtInServiceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("UserDebtInServiceQueryHandler.Handle: Request null.");

                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch
            {
                _logger.LogWarning("UserDebtInServiceQueryHandler.Handle: ArgumentNullException");
                throw;
            }
            throw new NotImplementedException();
        }


        /// <summary>
        /// Método privado que maneja la búsqueda de un servicio por su nombre y el nombre del proveedor que lo ofrece.
        /// </summary>
        /// <param name="request">La consulta ServiceByServiceNameQuery que especifica el nombre del servicio y el nombre del proveedor que lo ofrece.</param>
        /// <returns>Un objeto OneServiceResponse que contiene información detallada del servicio.</returns>
        private async Task<UserDebtInServiceResponse> HandleAsync(UserDebtInServiceQuery request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("UserDebtInServiceQueryHandler.HandleAsync");

                var debt = await _dbContext.PaymentByConciliationEntities.Where(
                    c => c.UserId == request.UserId && c.ServiceId == request.ServiceId).FirstOrDefaultAsync();

                if (debt == null)
                {
                    throw new DebtNotFoundException("Usuario no tiene deudas"); //cambiar esto
                }
                var response = new UserDebtInServiceResponse
                {
                    UserId = debt.UserId,
                    ServiceId = debt.ServiceId,
                    Debt = debt.Debt

                };
                return response;
            }
            catch (DebtNotFoundException ex) 
            {
                _logger.LogError(ex, "Error UserDebtInServiceQueryHandler.HandleAsync. {Usuario sin deudas}", ex.Message);
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
