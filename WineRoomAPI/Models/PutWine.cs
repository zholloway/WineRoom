using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineRoomAPI.Models
{
    public class PutWine
    {
        public int ID { get; set; }

        public int? NumberOfBottles { get; set; }

        public string Format { get; set; }

        public int Year { get; set; }

        public string GrapeType { get; set; }

        public string Vineyard { get; set; }

        public DateTime? DateAdded { get; set; }

        public string Location { get; set; }

        public DateTime? DrinkableStart { get; set; }

        public DateTime? DrinkableEnd { get; set; }

        public decimal? PurchasePrice { get; set; }

        public decimal? MarketPrice { get; set; }

        public bool? Favorite { get; set; }

        public string Region { get; set; }

        public string Color { get; set; }

        public int? Rating { get; set; }

        public List<PutTags> Tags { get; set; }

        public int UserID { get; set; }
    }
}