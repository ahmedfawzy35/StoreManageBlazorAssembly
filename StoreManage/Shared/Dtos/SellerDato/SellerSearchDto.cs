using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.SellerDato
{
    public class SellerSearchDto
    {
        [DisplayName("رقم المورد")]
        public int Id { get; set; }
        [DisplayName("الاسم")]
        public string? Name { get; set; }
        [DisplayName("العنوان")]
        public string? Adress { get; set; }
      
    }
}
