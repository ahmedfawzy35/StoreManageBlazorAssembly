using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.CustomerDato
{
    public class CustomerSearchDto
    {
        [DisplayName("رقم العميل")]
        public int Id { get; set; }
        [DisplayName("الاسم")]
        public string? Name { get; set; }
        [DisplayName("العنوان")]
        public string? Adress { get; set; }
        [DisplayName("النوع")]
        public string? Type { get; set; }
        [DisplayName("وقف التعامل")]
        public bool StopDealing { get; set; }
    }
}
