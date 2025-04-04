﻿namespace ItemManagementService.Data.Model;

public class Item
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public Status Status { get; set; } = Status.Available;
    public long? CategoryId { get; set; }
    public string CompanyId { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public Category? Categories { get; set; }
    public Company Companies { get; set; } = null!;
}