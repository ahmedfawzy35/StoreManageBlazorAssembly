using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.SellerDato.SellerSettlementDtos
{
    public class SellerAddingSettlementDto
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double Value { get; set; }
        public string? Notes { get; set; } = string.Empty;
        [Required]
        public int SellerId { get; set; }
        public string? SellerName { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }
        [Required]
        public int BrancheId { get; set; }
    }
}
