﻿namespace TaskManager.Models.ProductImportModel
{
    public class ProductImportResponse
    {
        //public long ProductImportId { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public string ImportBillId { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double PriceOfEachProduct { get; set; }
    }
}
