using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Commands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Threading.Channels;
using UCABPagaloTodoMS.Infrastructure.RabbitConsumer;

namespace UCABPagaloTodoMS.Application.RabbitConsumer
{
    public class ConsumerValor : ConsumerAbstract, IHostedService
    {
        protected override string QueueName => "lista_deudores";

        public ConsumerValor(
            IMediator mediator,
            ConnectionFactory connectionFactory,
            ILogger<ConsumerValor> logger,
            ILogger<ConsumerAbstract> baseLogger,
            ILogger<RabbitMqClient> clientLogger) :
            base(mediator, connectionFactory, baseLogger, clientLogger)
        {
            try
            {

                var consumer = new EventingBasicConsumer(Channel);

                consumer.Received += async (model, eventArgs) =>
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var request = JsonConvert.DeserializeObject<DebtRequest>(message);
                    await mediator.Send(new AddDebtToServiceCommand(request));
                };

                //read the message
                Channel.BasicConsume(queue: "lista_deudores", autoAck: false, consumer: consumer);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while consuming message");
            }
        }

        public virtual Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }
    }

}
