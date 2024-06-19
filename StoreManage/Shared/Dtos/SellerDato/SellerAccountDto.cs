using StoreManage.Shared.Dtos.CustomerDato;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.SellerDato
{
    public class SellerAccountDto
    {
        [DisplayName("رقم المورد")]
        public int SellerId { get; set; }
        [DisplayName("الاسم")]

        public string? Name { get; set; }
        [DisplayName("الرصيد السابق")]

        public double LastAccount { get; set; }
        [DisplayName("رصيد الفترة")]

        public double TimeAccount { get; set; }
        [DisplayName("صافي الرصيد")]

        public double FinalTimeAccount { get; set; }
        [DisplayName("الرصيد الحالي")]

        public double FinalCustomerAccount { get; set; } = 0;
        [DisplayName("تفاصيل العمليات")]

        public List<CustomerAccountElementDto>? elements { get; set; }
    }
}
