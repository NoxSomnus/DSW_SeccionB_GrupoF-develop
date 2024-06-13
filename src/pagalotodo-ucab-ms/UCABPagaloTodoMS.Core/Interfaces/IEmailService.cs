using Org.BouncyCastle.Asn1.Ocsp;
using System.Net.Mail;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Core.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
       
        Task SendEmailWithAttachmentAsync(string to, string subject, string body, Stream attachmentStream, string attachmentName);
    }
}
