using AutoMapper;
using Microsoft.Extensions.Options;
using QuoteAPI.Domain.Interfaces.ExternalServices;
using QuoteAPI.Domain.Interfaces.Services;
using RabbitMQ.Client;
using Stock.Domain.Configuration;
using Stock.Domain.Models;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Stock.Domain.Services
{
    public class StockService : IStockService
    {
        private readonly IStooqExternalService _stooqExternalService;
        private readonly ConnectionFactory _factory;
        private readonly IModel _channel;
        private readonly RabbitMqConfiguration _config;
        private readonly IConnection _connection;
        private readonly IMapper _mapper;


        public StockService(IStooqExternalService stooqExternalService, IMapper mapper, IOptions<RabbitMqConfiguration> options)
        {
            _stooqExternalService = stooqExternalService;
            _mapper = mapper;
            _config = options.Value;

            _factory = new ConnectionFactory
            {
                HostName = _config.Host
            };
        }

        public async Task GetStockAsync(StockInput model)
        {
            StockModel stock;
            try
            {
                stock = _mapper.Map<StockModel>(await _stooqExternalService.GetAsync(model.StockCode));
                stock.RoomId = model.RoomId;
                stock.User = model.User;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get stock", ex);
            }

            PostMessage(stock);

        }

        private void PostMessage(StockModel stockModel)
        {
            try
            {
                using var connection = _factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: _config.Queue,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                string message = JsonSerializer.Serialize(stockModel);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                    routingKey: _config.Queue,
                                    basicProperties: null,
                                    body: body);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert message in MQ", ex);
            }
        }
    }
}
