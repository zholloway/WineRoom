using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WineRoomAPI.DataContext;
using WineRoomAPI.Models;
using System.Linq.Dynamic;

namespace WineRoomAPI.Services.DrankWineServices
{
    public class DrankWineServices
    {
        public WineroomContext Database { get; } = new WineroomContext();

        public List<DrankWine> GetDrankWines()
        {
            return Database.DrankWines.ToList();
        }

        public DrankWine GetIndividualDrankWineByID(int id)
        {
            return Database.DrankWines.First(f => f.ID == id);
        }

        public void AddDrankWine(DrankWine drankWine)
        {
            Database.DrankWines.Add(drankWine);
            Database.SaveChanges();
        }

        public void DeleteDrankWine(int id)
        {
            Database.DrankWines.Remove(GetIndividualDrankWineByID(id));
            Database.SaveChanges();
        }

        public void EditDrankWine(int id, DrankWine drankWine)
        {
            DrankWine editDrankWine = Database.DrankWines.First(f => f.ID == id);

            editDrankWine.DateDrank = drankWine.DateDrank;
            editDrankWine.People = drankWine.People;
            editDrankWine.Location = drankWine.Location;
            editDrankWine.Comments = drankWine.Comments;

            Database.SaveChanges();
        }
    }
}