using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using MediatR;
using System.Text;
using System.Threading.Tasks;
using MassTransit.Mediator;

namespace UCABPagaloTodoMS.Infrastructure.RabbitConsumer
{
    public abstract class ConsumerAbstract : RabbitMqClient
    {
        private readonly MediatR.IMediator _mediator;
        private readonly ILogger<ConsumerAbstract> _logger;
        protected abstract string QueueName { get; }

        public ConsumerAbstract(
            MediatR.IMediator mediator,
            ConnectionFactory connectionFactory,
            ILogger<ConsumerAbstract> consumerLogger,
            ILogger<RabbitMqClient> logger) :
            base(connectionFactory, logger)
        {
            _mediator = mediator;
            _logger = consumerLogger;
        }

        protected virtual async Task OnEventReceived<T>(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                var body = Encoding.UTF8.GetString(@event.Body.ToArray());
                var message = JsonConvert.DeserializeObject<T>(body);

                await _mediator.Send(message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error while retrieving message from queue.");
            }
            finally
            {
                Channel.BasicAck(@event.DeliveryTag, false);
            }
        }

    }
}
