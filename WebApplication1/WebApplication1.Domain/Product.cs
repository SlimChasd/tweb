﻿using System;
using System.Collections.Generic;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }

    public Product()
    {
        OrderItems = new HashSet<OrderItem>();
    }
}