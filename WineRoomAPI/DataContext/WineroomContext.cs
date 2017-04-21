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
        const string PathToWineroomDatabase = @"Server=mssql2.gear.host;Database=wineroom;Uid=wineroom;Pwd=Em0mC2!SnTo~;";

        public WineroomContext():base(PathToWineroomDatabase)
        {

        } 

        public DbSet<Wine> Wines { get; set; } 
        public DbSet<DrankWine> DrankWines { get; set; }
        public DbSet<User> Users { get; set; }
    }
}