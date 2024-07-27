using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.CashDtos.CashDayCloseDtos
{
    public class CashBallanceDay
    {
        public List<CashBallanceItem> Items { get; set; }
        public CashBallanceValues Values { get; set; }
        public double RealCash { get; set; }
    }
}
