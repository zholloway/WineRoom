using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WineRoomAPI.Models;
using WineRoomAPI.Models.FilterModels;

namespace WineRoomAPI.Services
{
    public class FilterParameters
    {
        public DateTime DrinkableStart { get; set; }
        public DateTime DrinkableEnd { get; set; }
        public PurchasePrice PurchasePrice { get; set; }
        public MarketPrice MarketPrice { get; set; }
        public bool? Favorite { get; set; }
        public List<string> Region { get; set; }
        public List<string> Color { get; set; }
        public string Tags { get; set; }
        public Rating Rating { get; set; }
        public List<string> Format { get; set; }
    }
}