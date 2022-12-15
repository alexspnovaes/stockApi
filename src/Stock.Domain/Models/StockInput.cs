using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Models
{
    public class StockInput
    {
        public string StockCode { get; set;  }
        public string User { get; set; }
        public string RoomId { get; set; }
    }
}
