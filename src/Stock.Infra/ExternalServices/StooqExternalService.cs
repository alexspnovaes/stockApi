using CsvHelper;
using Microsoft.Extensions.Configuration;
using QuoteAPI.Domain.Interfaces.ExternalServices;
using Stock.Domain.Models;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Stock.Infra.ExternalServices
{
    public class StooqExternalService : IStooqExternalService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        public StooqExternalService(IConfiguration configuration)
        {
            _configuration = configuration;


            _client = new();
            _client.BaseAddress = new Uri(_configuration.GetSection("StooqApi").GetSection("URL").Value);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Domain.Models.Stock> GetAsync(string StockCode)
        {
            Domain.Models.Stock stock = null;
            var response = await _client.GetAsync($"?s={StockCode}&f=sd2t2ohlcv&h&e=csv");
            if (response.IsSuccessStatusCode)
            {                
                var result = await response.Content.ReadAsStreamAsync();
                TextReader reader = new StreamReader(result);
                var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                var records = csvReader.GetRecords<Domain.Models.Stock>();
                try
                {
                    stock = records.FirstOrDefault();
                }
                catch 
                { 
                    throw new HttpRequestException();
                }
            }
            return stock;
        }
    }
}
