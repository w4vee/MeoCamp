﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MeoCamp.Repository.Models;

public partial class Product
{
    public Guid Id { get; set; }

    public string ProductName { get; set; }

    public string Description { get; set; }

    public decimal? Price { get; set; }

    public decimal? RentalPrice { get; set; }

    public bool? IsRentable { get; set; }

    public Guid? CategoryId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Status { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}