using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Exceptions;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Core.Interfaces;
using System.IO;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    class CreteConciliationFileQueryHandler : IRequestHandler<CreateConciliationFileQuery, string>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<CreteConciliationFileQueryHandler> _logger;
        private readonly IConciliationFileBuilder _fileBuilder;

        public CreteConciliationFileQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<CreteConciliationFileQueryHandler> logger, IConciliationFileBuilder fileBuilder)
        {
            _dbContext = dbContext;
            _logger = logger;
            _fileBuilder = fileBuilder;
        }
        public Task<string> Handle(CreateConciliationFileQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("ConciliationFileConfigureEntity.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("ConsultarValoresQueryHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        public async Task<string> HandleAsync(CreateConciliationFileQuery request)
        {
            try
            {
                _logger.LogInformation("ConsultarValoresQueryHandler.HandleAsync");

                string baseFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Archivos de conciliacion");
                string conciliationFolderPath = Path.Combine(baseFolderPath, "ConciliationFiles");

                if (!Directory.Exists(conciliationFolderPath))
                {
                    Directory.CreateDirectory(conciliationFolderPath);
                }

                var config = _dbContext.ConciliationFileConfigureEntities.FirstOrDefault(c => c.Id == request._request.ConfigID);
                if (config == null)
                    throw new NotFoundConfig("ERROR: Esta configuracion no existe ");
                var proveedor = _dbContext.UserEntities.FirstOrDefault(a => a.Id == config.ProviderId);
                if (!(proveedor is ProviderEntity provider))
                {
                    throw new UserIsNotProviderException("Error: el usuarion no es proveedor");
                }
                var bills = await _dbContext.BillEntities.Where(c => c.ServiceId == config.ServiceId && c.IsConciliated == false).ToListAsync();
                

                var model = ConciliationfileConfigMapper.MapentityModel(config);
                var file = _fileBuilder.Build(bills, config.ProviderId, config.ServiceId, model, _dbContext);

                //string folderPath = "C:\\Users\\sedet\\Desktop\\Archivos de conciliacion";
                string fileName = proveedor.Name+ "_" + provider.CompanyName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt"; ; // Generar un nombre de archivo único utilizando la fecha y hora actual
                string filePath = Path.Combine(conciliationFolderPath, fileName);

                // Guardar el contenido del archivo en un archivo de texto plano
                File.WriteAllBytes(filePath, file);

                return filePath.Replace("\\", "\\\\");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }
        }
    }
}

