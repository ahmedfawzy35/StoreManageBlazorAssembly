using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.MasterMoneySafeDtos
{
    public class MasterMoneySafeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Notes { get; set; }
        public double StartAccount { get; set; } = 0;
    }
}
