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
        public WineroomContext():base()
        {

        } 

        public DbSet<Wine> Wines { get; set; } 
        public DbSet<DrankWine> DrankWines { get; set; }
        public DbSet<User> Users { get; set; }
    }
}