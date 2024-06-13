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
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    public class AddConciliationFileConfigCommandHandler : IRequestHandler<AddConciliationFileConfigCommand, Guid>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AddConciliationFileConfigCommandHandler> _logger;


        
        public AddConciliationFileConfigCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AddConciliationFileConfigCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public  Task<Guid> Handle(AddConciliationFileConfigCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("AddConciliationFileConfigCommandHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return HandleAsync(request._request);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Guid> HandleAsync(AddConciliationFileConfigRequest request)
        {
            _logger.LogInformation("AddConciliationFileConfigCommandHandler.HandleAsync {Request}", request);
            var transaccion = _dbContext.BeginTransaction();
            try
            {

                
                //Busca el servico y el proveedro a quien pertenecera la configuracion 
                var user = _dbContext.UserEntities.FirstOrDefault(c => c.Id == request.ProviderId);
                var service = _dbContext.ServiceEntities.FirstOrDefault(d => d.Id == request.ServiceId);

                if (user == null || service == null)
                    throw new UserIdNotFoundException("Erro: El proveedor o servicio no existe");

                var config = ConciliationfileConfigMapper.MapRequestToEntity(request);
                _dbContext.ConciliationFileConfigureEntities.Add(config);
                var id = config.Id;
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Response}", id);
                return id;
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
