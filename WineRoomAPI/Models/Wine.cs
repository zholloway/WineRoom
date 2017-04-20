using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WineRoomAPI.Models
{
    public class Wine
    {
        public int ID { get; set; }
        public int NumberOfBottles { get; set; }
        public string Format { get; set; }
        public int Year { get; set; }
        public string GrapeType { get; set; }
        public string Vineyard { get; set; }
        public string DateAdded { get; set; }
        public string Location { get; set; }
        public string DrinkableStart { get; set; }
        public string DrinkableEnd { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal MarketPrice { get; set; }
        public bool Favorite { get; set; }
        public string Region { get; set; }
        public string Color { get; set; }
        public string Tags { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
    }
}