﻿using System;

namespace Shop.Order.Model
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
    }
}