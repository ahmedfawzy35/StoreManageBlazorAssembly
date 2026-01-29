using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.CustomerDato
{
    public class CustomersOrdersDto
    {
        public int CustomerId { get; set; }
        public int Number { get; set; }
        public string? Name { get; set; }
        public double TotalCashOrders { get; set; }
        public double TotalUnCashOrders { get; set; }
        public double TotalOrdersBack { get; set; }
        public double FinalOrders => TotalCashOrders + TotalUnCashOrders - TotalOrdersBack;

    }
}
