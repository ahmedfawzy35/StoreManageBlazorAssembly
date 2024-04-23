using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.CustomerDato
{
    public class CustomerAccountDto
    {
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public double LastAccount { get; set; }
        public double TimeAccount { get; set; }
        public double FinalTimeAccount { get; set; }
        public double FinalCustomerAccount { get; set; } = 0;
        public List<CustomerAccountElementDto>? elements { get; set; }
       
    }
}
