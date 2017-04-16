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
        public int OwnerID { get; set; }
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

        public Wine() { }
        public Wine(SqlDataReader reader)
        {
            ID = (int)reader["ID"];
            OwnerID = (int)reader["OwnerID"];
            NumberOfBottles = (int)reader["NumberOfBottles"];
            Format = reader["Format"].ToString();
            Year = (int)reader["Year"];
            GrapeType = reader["GrapeType"].ToString();
            Vineyard = reader["Vineyard"].ToString();
            
            //using .Date poperty to set DateAdded
            var dateAdded = (DateTime)reader["DateAdded"];
            var dateString = dateAdded.ToShortDateString();
            DateAdded = dateString;

            Location = reader["Location"].ToString();
            
            //using .Date poperty to set DrinkableStart
            var drinkableStart = (DateTime)reader["DrinkableStart"];
            dateString = drinkableStart.ToShortDateString();
            DrinkableStart = dateString;
            
            //using .Date poperty to set DrinkableEnd
            var drinkableEnd = (DateTime)reader["DrinkableEnd"];
            dateString = drinkableEnd.ToShortDateString();
            DrinkableEnd = dateString;

            PurchasePrice = Convert.ToDecimal(reader["PurchasePrice"]);
            MarketPrice = Convert.ToDecimal(reader["MarketPrice"]);
            Favorite = Convert.ToBoolean(reader["Favorite"]);
            Region = reader["Region"].ToString();
            Color = reader["Color"].ToString();
            Tags = reader["Tags"].ToString();
        }
    }
}