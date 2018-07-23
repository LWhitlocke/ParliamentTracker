﻿using System;

namespace DAL.Models
{
    public class Bill
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string CurrentHouse { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Uri { get; set; }
    }
}