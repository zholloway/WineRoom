using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WineRoomAPI.Models;

namespace WineRoomAPI.DataContext
{
    public class WineroomContext :DbContext
    {
        const string path = "Server=mssql4.gear.host;Database=wineroom;Uid=wineroom;Pwd=Vl9sfc_K!8Na;";
        public WineroomContext():base(path)
        {

        } 

        public DbSet<Wine> Wines { get; set; } 
        public DbSet<DrankWine> DrankWines { get; set; }
        public DbSet<User> Users { get; set; }
    }
}