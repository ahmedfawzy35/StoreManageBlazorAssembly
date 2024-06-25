using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.OrderDtos
{
    public class OrderBackDto
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; } = string.Empty;
        [Required]
        public double Total { get; set; }
        public double Paid { get; set; } = 0;
        public double Discount { get; set; } = 0;
        public double RemainingAmount => Total - Discount - Paid;
        [Required]
        public int BrancheId { get; set; }
        public int OrderNumber { get; set; }
        public string? Notes { get; set; } = string.Empty;

    }
}
