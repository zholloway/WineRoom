using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WineRoomAPI.Models;

namespace WineRoomAPI.Services
{
    public class WineServices
    {
        public SqlConnection Connection { get; set; }

        public WineServices(string connection)
        {
            Connection = new SqlConnection(connection);
        }

        public List<Wine> GetAllWine()
        {
            var wineList = new List<Wine>();

            var query = "SELECT * FROM Wine";
            var cmd = new SqlCommand(query, Connection);

            Connection.Open();
            var reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                wineList.Add(new Wine(reader));
            }
            Connection.Close();

            return wineList;
        }

        public Wine GetIndividualWineByID(int id)
        {
            var wine = new Wine();

            var query = "Select * FROM Wine WHERE ID=@ID";
            var cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@ID", id);

            Connection.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                wine = new Wine(reader);
            }   

            return wine;
        }

        public void DeleteWine(int id)
        {
            var query = "DELETE FROM Wine WHERE ID=@ID";
            var cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@ID", id);

            Connection.Open();
            var reader = cmd.ExecuteNonQuery();
            Connection.Close();
        }

        public void AddWine(FormCollection collection)
        {
            var query = "INSERT INTO Wine (OwnerID, NumberOfBottles, Format, " +
                "Year, GrapeType, Vineyard, DateAdded, Location, DrinkableStart, " +
                "Drinkable End, PurchasePrice, MarketPrice, Favorite, Region, Color, " +
                "Tags) " +
                "Values (@OwnerID, @NumberOfBottles, @Format, @Year, @GrapeType, " +
                "@Vineyard, @DateAdded, @Location, @DrinkableStart, @DrinkableEnd, " +
                "@PurchasePrice, @MarketPrice, @Favorite, @Region, @Color, @Tags)";

            var cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@OwnerID", collection["OwnerID"]);
            cmd.Parameters.AddWithValue("@NumberOfBottles", collection["NumberOfBottles"]);
            cmd.Parameters.AddWithValue("@Format", collection["Format"]);
            cmd.Parameters.AddWithValue("@Year", collection["Year"]);
            cmd.Parameters.AddWithValue("@GrapeType", collection["GrapeType"]);
            cmd.Parameters.AddWithValue("@Vineyard", collection["Vineyard"]);
            cmd.Parameters.AddWithValue("@DateAdded",DateTime.Now);
            cmd.Parameters.AddWithValue("@Location", collection["Location"]);
            cmd.Parameters.AddWithValue("@DrinkableStart", collection["DrinkableStart"]);
            cmd.Parameters.AddWithValue("@DrinkableEnd", collection["DrinkableEnd"]);
            cmd.Parameters.AddWithValue("@PurchasePrice", collection["PurchasePrice"]);
            cmd.Parameters.AddWithValue("@MarketPrice", collection["MarketPrice"]);
            cmd.Parameters.AddWithValue("@Favorite", collection["Favorite"]);
            cmd.Parameters.AddWithValue("@Region", collection["Region"]);
            cmd.Parameters.AddWithValue("@Color", collection["Color"]);
            cmd.Parameters.AddWithValue("@Tags", collection["Tags"]);

            Connection.Open();
            cmd.ExecuteNonQuery();
            Connection.Close();
        }

        public void EditWine(int id)
        {
            var query = "UPDATE Book SET []";
        }
    }
}