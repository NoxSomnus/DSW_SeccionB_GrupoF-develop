using System.Threading.Tasks;
namespace UCABPagaloTodoMS.Core.Interfaces
{
    public interface IRabbitProducer
    {
        Task SendProductMessage(string message);
    }
}
