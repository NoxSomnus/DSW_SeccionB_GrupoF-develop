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
    internal class AllDebtsByUserIdQueryHandler : IRequestHandler<AllDebtsByUserIdQuery, List<UserDebtInServiceResponse>>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AllDebtsByUserIdQueryHandler> _logger;


        /// <summary>
        /// Constructor de la clase AllDebtsByUserIdQueryHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para buscar las facturas.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de consulta.</param>
        public AllDebtsByUserIdQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AllDebtsByUserIdQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de consulta que devuelve una lista de todas las deudas de un usuario.
        /// </summary>
        /// <param name="request">La consulta AllDebtsByUserIdQuery que especifica los criterios de búsqueda de las deudas.</param>
        /// <param name="cancellationToken">El token de cancelación que puede detener la operación en cualquier momento.</param>
        /// <returns>Una lista de objetos UserDebtInServiceResponse que contienen información detallada de las facturas.</returns>
        public Task<List<UserDebtInServiceResponse>> Handle(AllDebtsByUserIdQuery request, CancellationToken cancellationToken)
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
        /// Método privado que maneja la búsqueda de todas las facturas en la base de datos.
        /// </summary>
        /// <param name="request">La consulta AllDebtsByUserIdQuery que especifica los criterios de búsqueda de las facturas.</param>
        /// <returns>Una lista de objetos UserDebtInServiceResponse que contienen información detallada de las facturas.</returns>
        private async Task<List<UserDebtInServiceResponse>> HandleAsync(AllDebtsByUserIdQuery request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                _logger.LogInformation("UserLoginQueryHandler.HandleAsync");

                var result = _dbContext.PaymentByConciliationEntities.Where(c => c.UserId == request.UserId)
                    .Select(c => new UserDebtInServiceResponse()
                    {
                        UserId = c.UserId,
                        ServiceId = c.ServiceId,
                        Debt = c.Debt
                    });

                if (result.Count() == 0)
                {
                    throw new AllDataNotFoundException("El usuario no tiene deudas");
                }
                return await result.ToListAsync();
            }
            catch (AllDataNotFoundException ex)
            {
                _logger.LogError(ex, "Error AllDebtsByUserIdQueryHandler.HandleAsync. {No se encontraron deudas}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AllDebtsByUserIdQueryHandler.HandleAsync. {Los datos ingresados no son validos}", ex.Message);
                throw;
            }
        }
    }
}
