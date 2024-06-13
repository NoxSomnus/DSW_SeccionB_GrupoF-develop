using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Interfaces;
using System.IO;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
   public class SendConciliationFileQueryHandler : IRequestHandler<SendConciliationfileQuery, bool>
    
    {
        private readonly ILogger<SendConciliationFileQueryHandler> _logger;
        private readonly IEmailService _emailService;

        public SendConciliationFileQueryHandler(IEmailService emailService, ILogger<SendConciliationFileQueryHandler> logger)
        {
            _emailService = emailService;
            _logger = logger;

        }
        public Task<bool> Handle(SendConciliationfileQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("Email.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("SendConciliationFileQueryHandler.Handle: ArgumentNullException");
                throw;
            }
        }
       
        public async Task<bool> HandleAsync(SendConciliationfileQuery request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException("");
                var subject = "Archivo de Conciliacion";
                var body = $"Hola";

                if (request._request.email == null && request._request.filePath == null)
                {
                    return false;
                }

                Attachment attachment = new Attachment(request._request.filePath, MediaTypeNames.Text.Plain);
                using (var attachmentStream = new MemoryStream())
                {
                    // Leer los datos del archivo adjunto desde el objeto Attachment y escribirlos en el búfer temporal
                    using (var attachmentContentStream = attachment.ContentStream)
                    {
                        byte[] buffer = new byte[attachmentContentStream.Length];
                        int bytesRead = attachmentContentStream.Read(buffer, 0, (int)attachmentContentStream.Length);

                        // Enviar el correo electrónico con el archivo adjunto
                        await _emailService.SendEmailWithAttachmentAsync(request._request.email, subject, body, new MemoryStream(buffer), Path.GetFileName(request._request.filePath));
                    }
                }

                //await _emailService.SendEmailWithAttachmentAsync(request._email, subject, body, attachment, Path.GetFileName(request._filePath));



                return true;
            }
            catch (Exception)
            {
                _logger.LogWarning("SendConciliationFileQueryHandler.Handle: hola ArgumentNullException");
                throw;
            }
        }
    }
} 
    

