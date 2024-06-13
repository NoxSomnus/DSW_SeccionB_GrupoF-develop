using MediatR;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Exceptions;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    internal class AddPaymentRequiredFieldsCommandHandler : IRequestHandler<AddPaymentRequiredFieldsCommand,AddPaymentRequiredFieldsResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AddPaymentRequiredFieldsCommandHandler> _logger;

        public AddPaymentRequiredFieldsCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AddPaymentRequiredFieldsCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<AddPaymentRequiredFieldsResponse> Handle(AddPaymentRequiredFieldsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
                {
                    _logger.LogWarning("ConsultarValoresQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                if (request._request.RequiredFields.Count == 0) 
                {
                    throw new ArgumentNullException(nameof(request)); //cambiar para una excepcion personalizada
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

        private async Task<AddPaymentRequiredFieldsResponse> HandleAsync(AddPaymentRequiredFieldsCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {

                _logger.LogInformation("AgregarValorePruebaCommandHandler.HandleAsync {Request}", request);
                var option = _dbContext.PaymentOptionEntities.FirstOrDefault(s => s.Id == request._request.PaymentOptionId);

                if (option == null)
                {
                    throw new PaymentOptionNotFoundException("No se ha encontrado la opcion de pago");
                }

                foreach (var fields in request._request.RequiredFields)
                {
                    var requiredfields = new PaymentRequiredFieldEntity
                    {
                        Id = new Guid(),
                        FieldName = fields.FieldName,
                        isNumber = fields.isNumber,
                        isString = fields.isString,
                        Length = fields.Length,
                        PaymentOptionId = option.Id,
                        PaymentOption = option
                    };

                    _dbContext.PaymentRequiredFieldEntities.Add(requiredfields);
                    await _dbContext.SaveEfContextChanges("APP");

                };
                transaccion.Commit();
                var response = new AddPaymentRequiredFieldsResponse
                {
                    message = "Configuracion añadida con exito",
                    success = true
                };

                /* _dbContext.PaymentRequiredFieldEntities.Add(requiredfields);
                 await _dbContext.SaveEfContextChanges("APP");
                 transaccion.Commit();*/
                _logger.LogInformation("AddPaymentOptionCommandHandler.HandleAsync {Response}", response);
                return response;
            }
            catch (PaymentOptionNotFoundException ex) 
            {
                _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Mensaje}", ex.Message);
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
