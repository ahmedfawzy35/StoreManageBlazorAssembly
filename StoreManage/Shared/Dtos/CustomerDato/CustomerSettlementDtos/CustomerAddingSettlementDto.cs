using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.CustomerDato.CustomerSettlementDtos
{
    public class CustomerAddingSettlementDto
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double Value { get; set; }
        public string? Notes { get; set; } = string.Empty;
        [Required]
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }
        [Required]
        public int BrancheId { get; set; }
    }
}
