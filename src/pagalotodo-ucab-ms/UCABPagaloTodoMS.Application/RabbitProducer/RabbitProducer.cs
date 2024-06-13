/*using Newtonsoft.Json;
using System.Text;
using RabbitMQ.Client;
using UCABPagaloTodoMS.Core.Interfaces;
using UCABPagaloTodoMS.Infrastructure.services;
namespace UCABPagaloTodoMS.Application.RabbitProducer
{
    public class RabbitProducer : IRabbitProducer
    {
        public async Task SendProductMessage(string message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            string queueName = "lista_deudores";
            bool queueExists = false;

            // Verificar si la cola ya existe
            try
            {
                channel.QueueDeclarePassive(queueName);
                queueExists = true;
            }
            catch (Exception)
            {
                throw;
            }

            // Declarar la cola si no existe
            if (!queueExists)
            {
                channel.QueueDeclare(queueName, exclusive: false);
            }

            var reader = new Firebase();

            var fileContents = await reader.ReadFileContentsAsync(message);

            //Se separa por linea el archivo
            var lines = fileContents.Split('\n');


            foreach (var espacio in lines)
            {
                // Se serializa el mensaje y se le quitan los saltos de linea
                var palabra = espacio.Replace("\r", "");
                var values = palabra.Split(' ');
                var nuevo = new
                {
                    userName = values[0],
                    servicio = values[1],
                    monto = values[2]
                };
                var messageJson = JsonConvert.SerializeObject(nuevo);
                var messageBytes = Encoding.UTF8.GetBytes(messageJson);
                // Se ponen los datos dentro de la cola de producto
                channel.BasicPublish(exchange: "", routingKey: "lista_deudores", basicProperties: null, body: messageBytes);
            }
        }
    }
}*/
