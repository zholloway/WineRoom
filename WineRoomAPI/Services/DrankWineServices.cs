using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WineRoomAPI.DataContext;
using WineRoomAPI.Models;
using System.Linq.Dynamic;
using WineRoomAPI.Models.JsonReturnModels;

namespace WineRoomAPI.Services.DrankWineServices
{
    public class DrankWineServices
    {
        public WineroomContext Database { get; } = new WineroomContext();

        public List<DrankWine> GetDrankWines()
        {
            return Database.DrankWines.OrderByDescending(o => o.DateDrank).ToList();
        }

        public JsonDrankWineGet JsonDrankWineGet(List<DrankWine> drankWineList)
        {
            return new JsonDrankWineGet {
                Success = true,
                Message = "we did it",
                Data = drankWineList
            };
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

        public DrankWine FindNewDrankWine(DrankWine drankWine)
        {
            return Database.DrankWines
                .Where(w => w.Comments == drankWine.Comments)
                .Where(w => w.DateDrank == drankWine.DateDrank)
                .Where(w => w.Location == drankWine.Location)
                .Where(w => w.People == drankWine.People)
                .Where(w => w.WineID == drankWine.WineID)
                as DrankWine;
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

        public JsonDrankWineCrud JsonDrankWineReturn(int id)
        {
            return new JsonDrankWineCrud {
                Success = true,
                Message = "we did it",
                DrankWineID = id
            };
        }
    }
}