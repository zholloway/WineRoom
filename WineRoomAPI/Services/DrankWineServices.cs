using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WineRoomAPI.DataContext;
using WineRoomAPI.Models;
using System.Linq.Dynamic;
using WineRoomAPI.Models.JsonReturnModels;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace WineRoomAPI.Services.DrankWineServices
{
    public class DrankWineServices
    {
        public WineroomContext Database { get; } = new WineroomContext();
        public WineServices wineServices = new WineServices();

        public List<DrankWine> GetDrankWines(int userID, int pageIndex, int pageSize, string search)
        {
            var query = Database.DrankWines
                .Include(i => i.Wine)
                .Where(w => w.People.Contains(search)
                         || w.Location.Contains(search)
                         || w.Wine.Vineyard.Contains(search)
                         || w.Wine.Year.ToString().Contains(search)
                         || w.Wine.GrapeType.Contains(search)
                       )
                .OrderByDescending(o => o.DateDrank);

            var rv = query.Where(r => r.Wine.UserID == userID).ToList();
            //var putPeopleList = new List<PutPeople>();
            //foreach (var drankWine in rv.ToList())
            //{
            //    if (drankWine.People != null)
            //    {
            //        var peopleArray = drankWine.People.Split(' ');

            //        for (int i = 0; i < peopleArray.Length; i++)
            //        {
            //            var person = peopleArray[i];

            //            if (person != "")
            //            {
            //                var putPerson = new PutPeople();
            //                putPerson.text = person;
            //                putPeopleList.Add(putPerson);
            //            }
            //        }

            //        drankWine.People = JsonConvert.SerializeObject(putPeopleList);
            //    }

            //    putPeopleList = new List<PutPeople>();
            //}

            for (int i = 0; i < rv.Count; i++)
            {
                if (rv[i].Wine.Tags != "" || rv[i].Wine.Tags != null)
                    rv[i].Wine = wineServices.ConvertWineTagsToJson(rv[i].Wine);
            }

            return rv.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
        }

        public JsonDrankWineGet JsonDrankWineGet(List<DrankWine> drankWineList, int pageIndex, int pageSize)
        {
            return new JsonDrankWineGet {
                Success = true,
                Message = "we did it",
                Data = drankWineList,
                PageIndex = pageIndex,
                PageSize = pageSize,               
                TotalPages = (int)Math.Ceiling((double)Database.DrankWines.Count() / (double)pageSize)
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

        public void EditDrankWine(int id, PutDrankWine putDrankWine)
        {
            DrankWine editDrankWine = Database.DrankWines.First(f => f.ID == id);

            editDrankWine.DateDrank = putDrankWine.DateDrank;
            editDrankWine.People = putDrankWine.People;
            editDrankWine.Location = putDrankWine.Location;
            editDrankWine.Comments = putDrankWine.Comments;

            //edit the associated Wine for tags/rating
            var originalWine = Database.Wines.FirstOrDefault(f => f.ID == putDrankWine.Wine.ID);
            //edit the tags
            if (putDrankWine.Wine.Tags != null)
            {
                originalWine.Tags = putDrankWine.Wine.Tags;
            }
            //edit the rating
            if (putDrankWine.Wine.Rating != null)
            {
                originalWine.Rating = putDrankWine.Wine.Rating;
            }

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