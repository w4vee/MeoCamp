﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MeoCamp.Repository.Models;

public partial class Product
{
    public int Id { get; set; }

    public string ProductName { get; set; }

    public string Description { get; set; }

    public double? Price { get; set; }

    public double? RentalPrice { get; set; }

    public bool? IsRentable { get; set; }

    public int? CategoryId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? Status { get; set; }

    public string Image { get; set; }
    
    public int Quantity { get; set; }

    public double Rate { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}