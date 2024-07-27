using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StoreManage.Shared.Utilitis.MyTypes;

namespace StoreManage.Shared.Dtos.CashDtos.CashDayCloseDtos
{
    public class CashBallanceItem
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public Double Value { get; set; }
        public string ItemType { get; set; }
        public string? UserCreate { get; set; }
    }
}
