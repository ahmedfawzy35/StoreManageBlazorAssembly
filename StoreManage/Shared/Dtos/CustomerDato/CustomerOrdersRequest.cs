using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.CustomerDato
{
    public class CustomerOrdersRequest
    {
        public int BrancheId { get; set; }
        public string? DateFrom { get; set; }
        public string? DateTo { get; set; }
    }
}
