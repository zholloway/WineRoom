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
        public DrinkableStart DrinkableStart { get; set; }
        public DrinkableEnd DrinkableEnd { get; set; }
        public PurchasePrice PurchasePrice { get; set; }
        public MarketPrice MarketPrice { get; set; }
        public bool? Favorite { get; set; }
        public string Region { get; set; }
        public string Color { get; set; }
        public string Tags { get; set; }
    }
}