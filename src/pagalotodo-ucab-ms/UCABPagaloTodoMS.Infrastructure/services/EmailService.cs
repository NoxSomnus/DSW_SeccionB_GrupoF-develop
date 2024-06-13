using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;
using System.IO;
using UCABPagaloTodoMS.Core.Interfaces;
using UCABPagaloTodoMS.Infrastructure.Options;
using System.Net.Sockets;

namespace UCABPagaloTodoMS.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _options;

        public EmailService(IOptions<EmailOptions> options)
        {
            _options = options.Value;
        }
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_options.FromName, _options.FromAddress));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using SmtpClient client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_options.SmtpServer, _options.SmtpPort, SecureSocketOptions.Auto);
                await client.AuthenticateAsync(_options.Username, _options.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar correo electrónico: {ex.ToString()}");
                throw;
            }
        }
        public async Task SendEmailWithAttachmentAsync(string to, string subject, string body, Stream attachmentStream, string attachmentName)
        {
             try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_options.FromName, _options.FromAddress));
                message.To.Add(new MailboxAddress(to, to));
                message.Subject = subject;

                var builder = new BodyBuilder();
                builder.TextBody = body;
                builder.Attachments.Add(attachmentName, attachmentStream);

                message.Body = builder.ToMessageBody();

                using SmtpClient client = new SmtpClient();
           
                await client.ConnectAsync(_options.SmtpServer, _options.SmtpPort, SecureSocketOptions.Auto);
                await client.AuthenticateAsync(_options.Username, _options.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
           // catch (Exception ex)
            //{
           //     Console.WriteLine($"Error al enviar correo electrónico con archivo adjunto: {ex.ToString()}");
             //   throw;
           // }
            catch (SocketException ex)
            {
                Console.WriteLine($"Error de socket: {ex.ToString()}");
                throw;
            }
        }
    }

}
