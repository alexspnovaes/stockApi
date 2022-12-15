using Stock.Domain.Models;
using System.Threading.Tasks;

namespace QuoteAPI.Domain.Interfaces.Services
{
    public interface IStockService
    {
        Task GetStockAsync(StockInput model);
    }
}
