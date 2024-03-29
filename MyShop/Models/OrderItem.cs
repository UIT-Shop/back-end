﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class OrderItem
    {
        public Order? Order { get; set; }
        public int OrderId { get; set; }
        public Product? Product { get; set; }
        public int ProductId { get; set; }
        public string ProductSize { get; set; } = string.Empty;
        public string ProductColor { get; set; } = string.Empty;
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public bool IsCommented { get; set; } = false;
    }
}
