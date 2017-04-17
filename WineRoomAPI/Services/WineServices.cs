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

        public void AddWine(Wine wine)
        {
            var query = "INSERT INTO Wine (OwnerID, NumberOfBottles, Format, " +
                "Year, GrapeType, Vineyard, DateAdded, Location, DrinkableStart, " +
                "Drinkable End, PurchasePrice, MarketPrice, Favorite, Region, Color, " +
                "Tags) " +
                "Values (@OwnerID, @NumberOfBottles, @Format, @Year, @GrapeType, " +
                "@Vineyard, @DateAdded, @Location, @DrinkableStart, @DrinkableEnd, " +
                "@PurchasePrice, @MarketPrice, @Favorite, @Region, @Color, @Tags)";

            var cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@OwnerID", wine.ID);
            cmd.Parameters.AddWithValue("@NumberOfBottles", wine.NumberOfBottles);
            cmd.Parameters.AddWithValue("@Format", wine.Format);
            cmd.Parameters.AddWithValue("@Year", wine.Year);
            cmd.Parameters.AddWithValue("@GrapeType", wine.GrapeType);
            cmd.Parameters.AddWithValue("@Vineyard", wine.Vineyard);
            cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
            cmd.Parameters.AddWithValue("@Location", wine.Location);
            cmd.Parameters.AddWithValue("@DrinkableStart", wine.DrinkableStart);
            cmd.Parameters.AddWithValue("@DrinkableEnd", wine.DrinkableEnd);
            cmd.Parameters.AddWithValue("@PurchasePrice", wine.PurchasePrice);
            cmd.Parameters.AddWithValue("@MarketPrice", wine.MarketPrice);
            cmd.Parameters.AddWithValue("@Favorite", wine.Favorite);
            cmd.Parameters.AddWithValue("@Region", wine.Region);
            cmd.Parameters.AddWithValue("@Color", wine.Color);
            cmd.Parameters.AddWithValue("@Tags", wine.Tags);

            Connection.Open();
            cmd.ExecuteNonQuery();
            Connection.Close();
        }

        public void EditWine(Wine wine)
        {
            var query = "UPDATE Book SET [OwnerID] = @OwnerID," +
                        ",[NumberOfBottles] = @NumberOfBottles" +
                        ",[Format] = @Format" +
                        ",[Year] = @Year" +
                        ",[GrapeType] = @GrapeType" +
                        ",[Vineyard] = @Vineyard" +
                        ",[DateAdded] = @DateAdded" +
                        ",[Location] = @Location" +
                        ",[DrinkableStart] = @DrinkableStart" +
                        ",[DrinkableEnd] = @DrinkableEnd" +
                        ",[PurchasePrice] = @PurchasePrice" +
                        ",[MarketPrice] = @MarketPrice" +
                        ",[Favorite] = @Favorite" +
                        ",[Region] = @Region" +
                        ",[Color] = @Color" +
                        ",[Tags] = @Tags " +
                        "WHERE ID=@ID";

            var cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@OwnerID", wine.ID);
            cmd.Parameters.AddWithValue("@NumberOfBottles", wine.NumberOfBottles);
            cmd.Parameters.AddWithValue("@Format", wine.Format);
            cmd.Parameters.AddWithValue("@Year", wine.Year);
            cmd.Parameters.AddWithValue("@GrapeType", wine.GrapeType);
            cmd.Parameters.AddWithValue("@Vineyard", wine.Vineyard);
            cmd.Parameters.AddWithValue("@DateAdded", wine.DateAdded);
            cmd.Parameters.AddWithValue("@Location", wine.Location);
            cmd.Parameters.AddWithValue("@DrinkableStart", wine.DrinkableStart);
            cmd.Parameters.AddWithValue("@PurchasePrice", wine.PurchasePrice);
            cmd.Parameters.AddWithValue("@MarketPrice", wine.MarketPrice);
            cmd.Parameters.AddWithValue("@Favorite", wine.Favorite);
            cmd.Parameters.AddWithValue("@Region", wine.Region);
            cmd.Parameters.AddWithValue("@Color", wine.Color);
            cmd.Parameters.AddWithValue("@Tags", wine.Tags);
            cmd.Parameters.AddWithValue("@ID", wine.ID);

            Connection.Open();
            cmd.ExecuteNonQuery();
            Connection.Close();
        }
    }
}