using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.CustomerDato
{
    public class CustomerAccountElementDto
    {
        [DisplayName("رقم العملية")]
        public int Id { get; set; }

        [DisplayName("قيمة العملية")]
        public double Value { get; set; }

        [DisplayName("الملاحظات")]
        public String? Notes { get; set; }
        [DisplayName("تاريخ العملية")]
        public DateTime Date { get; set; }
        [DisplayName("الرصيد قبل العملية العملية")]
        public double AccountBeforElement { get; set; }
        [DisplayName("الرصيد بعد العملية")]
        public double AccountAfterElement => Add ? AccountBeforElement + Value : AccountBeforElement - Value;
        [DisplayName("نوع العملية")]
        public string? Type { get; set; }
        public bool Add { get; set; }
        [DisplayName("الرقم الورقي")]
        public int Number { get; set; } = 0;
    }
}
