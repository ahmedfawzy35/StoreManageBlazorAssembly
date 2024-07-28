using StoreManage.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.BrancheMoneySafeDtos
{
    public class BrancheMoneySafeDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Notes { get; set; }
        public double StartAccount { get; set; }
        public int BrancheId { get; set; }

        public  string? BrancheName { get; set; } = null!;
    }
}
