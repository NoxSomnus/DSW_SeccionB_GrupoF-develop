using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Exceptions;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    internal class AddDebtToServiceCommandHandler : IRequestHandler<AddDebtToServiceCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AddDebtToServiceCommandHandler> _logger;
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor de la clase AddServiceCommandHandler.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para agregar el servicio.</param>
        /// <param name="logger">El objeto ILogger que se utilizará para registrar la actividad del manejador de comando.</param>
        public AddDebtToServiceCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AddDebtToServiceCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Manejador de comando para agregar un servicio a un proveedor.
        /// </summary>
        /// <param name="request">Objeto de comando de tipo AddServiceCommand.</param>
        /// <param name="cancellationToken">Token de cancelación para cancelar la operación asincrónica.</param>
        /// <exception cref="ArgumentNullException">Se lanza si el objeto de comando es nulo.</exception>
        /// <exception cref="InvalidOperationException">Se lanza si el usuario no existe o no es un proveedor, o si ya existe un servicio con el mismo nombre.</exception>
        /// <returns>Un objeto Guid que indica el identificador del servicio agregado.</returns>
        public async Task<Guid> Handle(AddDebtToServiceCommand request, CancellationToken cancellationToken)
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
        /// Método asincrónico que agrega un servicio a un proveedor.
        /// </summary/// <param name="request">Objeto de comando de tipo AddServiceCommand.</param>
        /// <exception cref="InvalidOperationException">Se lanza si el usuario no existe o no es un proveedor, o si ya existe un servicio con el mismo nombre.</exception>
        /// <returns>Un objeto Guid que indica el identificador del servicio agregado.</returns>
        private async Task<Guid> HandleAsync(AddDebtToServiceCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                var user = _dbContext.UserEntities.FirstOrDefault(c => c.Username == request._request.ProviderUsername);
                if (user == null)
                {
                    throw new UserNotFoundException(request._request.ProviderUsername);
                }
                user.GetType();
                if (!(user is ProviderEntity provider))
                {
                    //throw new InvalidOperationException("Update servicio fallido: el usuario no es un proveedor");
                    throw new UserIsNotProviderException("Error: el usuario no es un proveedor");
                }

                var service = _dbContext.ServiceEntities.FirstOrDefault(c => c.ServiceName == request._request.Service && c.ProviderId == user.Id);
                if (service == null)
                {
                    throw new ServiceNotFoundException("Error: El proveedor no tiene este servicio");
                }

                if (service.TypePayment != "Conciliation")
                {
                    throw new NotByConciliationPaymentException("Error: El pago del servicio no es por deudas");
                }

                var debtor = _dbContext.UserEntities.FirstOrDefault(c => c.Username == request._request.UserName);
                if (debtor == null)
                {
                    throw new UserNotFoundException(request._request.UserName);
                }
                if (debtor is AdminEntity || debtor is ProviderEntity)
                {
                    //throw new InvalidOperationException("Update servicio fallido: el usuario no es un proveedor");
                    throw new UserIsNotCustomerException("Error: el usuario no es un consumidor");
                }

                var entity = new PaymentByConciliationEntity
                {
                    UserId = debtor.Id,
                    ServiceId = service.Id,
                    Debt = request._request.Amount
                };

                _dbContext.PaymentByConciliationEntities.Add(entity);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                return entity.Id;
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogError(ex, "Error AddDebtToServiceCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
            catch (ServiceNotFoundException ex) 
            {
                _logger.LogError(ex, "Error AddDebtToServiceCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
            catch (UserIsNotProviderException ex)
            {
                _logger.LogError(ex, "Error AddDebtToServiceCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
            catch (UserIsNotCustomerException ex)
            {
                _logger.LogError(ex, "Error AddDebtToServiceCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
            catch (NotByConciliationPaymentException ex)
            {
                _logger.LogError(ex, "Error AddDebtToServiceCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AddDebtToServiceCommandHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
        }
    }

}
