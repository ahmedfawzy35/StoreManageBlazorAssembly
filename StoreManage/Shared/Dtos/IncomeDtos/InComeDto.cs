using StoreManage.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.IncomeDtos
{
    public class InComeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Notes { get; set; }
        public int BrancheId { get; set; }

        public  string BrancheName { get; set; } = null!;
    }
}
