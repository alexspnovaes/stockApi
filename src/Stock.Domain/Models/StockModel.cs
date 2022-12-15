using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Models
{
    public sealed class StockModel : Stock
    {
        public string User { get; set; }
        public string RoomId { get; set; }
    }
}
