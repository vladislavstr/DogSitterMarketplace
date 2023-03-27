﻿namespace DogSitterMarketplaceApp.Models.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public int Summ { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool IsDeleted { get; set; }
    }
}
