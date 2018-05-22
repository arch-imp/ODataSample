using System;
using System.Collections.Generic;

namespace ODataSample.Models
{
    public class Manufacturer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}