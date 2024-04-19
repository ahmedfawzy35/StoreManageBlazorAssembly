using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class OrderToReview
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateOrder { get; set; }
        public int OrderId { get; set; }
        public string? Note { get; set; }
        public int OrderNumber { get; set; }
    }
}
