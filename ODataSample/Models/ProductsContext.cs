﻿using System.Data.Entity;

namespace ODataSample.Models
{
    public class ProductsContext : DbContext
    {
        public ProductsContext() : base("name=ProductsContext")
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}