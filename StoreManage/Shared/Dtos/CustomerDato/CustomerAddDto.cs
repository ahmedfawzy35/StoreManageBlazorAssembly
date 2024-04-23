using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.CustomerDato
{
    public class CustomerAddDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Adress { get; set; }
        public double StartAccount { get; set; } = 0;
        public int BrancheId { get; set; }
        public int CustomertypeId { get; set; }
        public bool? StopDealing { get; set; } = false;
    }
}
