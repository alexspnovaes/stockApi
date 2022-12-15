using Stock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteAPI.Domain.Interfaces.ExternalServices
{
    public interface IStooqExternalService
    {
        Task<Stock.Domain.Models.Stock> GetAsync(string StockCode);
    }
}
