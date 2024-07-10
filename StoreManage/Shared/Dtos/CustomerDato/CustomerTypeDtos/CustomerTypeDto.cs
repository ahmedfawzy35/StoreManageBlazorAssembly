using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.CustomerDato.CustomerTypeDtos
{
    public class CustomerTypeDto
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
