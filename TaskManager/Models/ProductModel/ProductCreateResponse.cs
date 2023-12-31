﻿namespace TaskManager.Models.ProductModel
{
    public class ProductCreateResponse
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ProductInfo { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
        public string BrandId { get; set; } = string.Empty;
    }
}
