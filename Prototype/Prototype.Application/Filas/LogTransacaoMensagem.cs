using Newtonsoft.Json;
using Prototype.Application.Interfaces.Filas;
using Prototype.Domain.Entities;
using Prototype.Domain.Interfaces.Repositories;
using RabbitMQ.Client;
using System.Text;

namespace Prototype.Application.Filas
{
    public class LogTransacaoMensagem: ILogTransacaoMensagem
    {
        private readonly ITransacaoMongoRepository _mongoRepository;
        private readonly ConnectionFactory _factory;
        private const string QUEUE_NAME = "log_message";
        public LogTransacaoMensagem(ITransacaoMongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
            _factory = new ConnectionFactory() { HostName = "localhost" };
        }

        public void ProduzirTransacao(LogTransacao message)
        {
            using(var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QUEUE_NAME,durable: false,exclusive: false,autoDelete: false,arguments: null);

                    var stringfieldMessage = JsonConvert.SerializeObject(message);
                    var bytesMessage = Encoding.UTF8.GetBytes(stringfieldMessage);

                    channel.BasicPublish( exchange:"",routingKey: QUEUE_NAME, basicProperties: null,body: bytesMessage);
                }
            }
        }

        //public consumirFIla()
        //{
        //    await _mongoRepository.CreateAsync(log);
        //}
    }
}
