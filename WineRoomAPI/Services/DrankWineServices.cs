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

        public List<DrankWine> GetDrankWines(int pageIndex, int pageSize)
        {
            return Database.DrankWines
                .OrderByDescending(o => o.DateDrank)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
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

        public DrankWine AddDrankWine(DrankWine wine)
        {
            //add new drankwine
            DrankWine drunkWine = new DrankWine {
                WineID = wine.WineID,
                DateDrank = DateTime.Now,
            };

            Database.DrankWines.Add(drunkWine);

            Database.SaveChanges();
         
            //return the new drankwine entry
            return drunkWine;
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

        public JsonDrankWineAdd JsonDrankWineAddReturn(int id, string location)
        {
            var data = new JsonDrankWineAddDataObject();
            data.DrankWineID = id;
            data.Location = location;

            var rv = new JsonDrankWineAdd
            {
                Success = true,
                Message = "we did it",
                Data = data
            };

            return rv;
        }
    }
}