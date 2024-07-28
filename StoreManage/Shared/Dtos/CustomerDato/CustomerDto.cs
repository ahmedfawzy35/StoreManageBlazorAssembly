using StoreManage.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.CustomerDato
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Adress { get; set; }
        public double StartAccount { get; set; }
        public int BrancheId { get; set; }
        public int CustomertypeId { get; set; }
        public bool? StopDealing { get; set; }

        public double? CustomerAccount { get; set; } = 0;

        public string?  BrancheName { get; set; } = null!;
        public string?   CustomerTypeName { get; set; } = null!;
    }
}
