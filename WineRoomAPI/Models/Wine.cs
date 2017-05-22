using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace WineRoomAPI.Models
{
    public class Wine
    {
        public int ID { get; set; }

        public int? NumberOfBottles { get; set; }

        public string Format { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string GrapeType { get; set; }

        [Required]
        public string Vineyard { get; set; }

        public DateTime? DateAdded { get; set; }

        public string Location { get; set; }

        public DateTime? DrinkableStart { get; set; }

        public DateTime? DrinkableEnd { get; set; }

        public decimal? PurchasePrice { get; set; }

        public decimal? MarketPrice { get; set; }

        public bool? Favorite { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string Color { get; set; }

        public string Tags { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
    }
}