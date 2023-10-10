﻿namespace TaskManager.Models.ModelResponse
{
    public class ItemOrderResponse
    {
        //public long ItemOrderId { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double SellPrice { get; set; }
    }
}
