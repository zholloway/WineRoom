using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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
    }
}