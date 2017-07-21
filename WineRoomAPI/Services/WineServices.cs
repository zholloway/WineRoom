using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WineRoomAPI.DataContext;
using WineRoomAPI.Models;
using System.Linq.Dynamic;
using Newtonsoft.Json;
using WineRoomAPI.Models.JsonReturnModels;
using System.Data.Entity.Validation;

namespace WineRoomAPI.Services
{
    public class WineServices
    {
        public WineroomContext Database { get; } = new WineroomContext();

        public List<Wine> GetAllWine(int pageIndex, int pageSize, string sortBy, string search, FilterParameters filter)
        {
            if (filter != null)
            {
                var rv = Database.Wines
                    .Where(w => w.Vineyard.Contains(search)
                                || w.GrapeType.Contains(search)
                                || w.Year.ToString().Contains(search))
                    .ToList();

                //Color
                if (filter.Color != null && filter.Color.Count != 0)
                {
                    var colorList = new List<string>();
                    foreach (var item in filter.Color)
                    {
                        colorList.Add(item);
                    }
                    foreach (var wine in rv.ToList())
                    {
                        if (!colorList.Contains(wine.Color))
                        {
                            rv.Remove(wine);
                        }
                    }
                }
                //DrinkableEnd
                if (filter.DrinkableEnd != null && filter.DrinkableEnd.ToString() != "1/1/0001 12:00:00 AM")
                {
                    rv = rv.Where(w => w.DrinkableEnd <= filter.DrinkableEnd).ToList();
                }
                //DrinkableStart
                if (filter.DrinkableStart != null && filter.DrinkableStart.ToString() != "1/1/0001 12:00:00 AM")
                {
                    
                    rv = rv.Where(w => w.DrinkableStart >= filter.DrinkableStart).ToList();
                }
                //Favorite
                if (filter.Favorite != null)
                {
                    rv = rv.Where(w => w.Favorite == filter.Favorite).ToList();
                }
                //MarketPrice
                if (!(filter.MarketPrice.MaximumPrice == null && filter.MarketPrice.MinimumPrice == null))
                {
                    if (filter.MarketPrice.MaximumPrice == null && filter.MarketPrice.MinimumPrice != null)
                    {
                        rv = rv.Where(wine => wine.MarketPrice >= filter.MarketPrice.MinimumPrice).ToList();
                    } else if (filter.MarketPrice.MaximumPrice != null && filter.MarketPrice.MinimumPrice == null)
                    {
                        rv = rv.Where(wine => wine.MarketPrice <= filter.MarketPrice.MaximumPrice).ToList();
                    } else
                    {
                        rv = rv.Where(w => w.MarketPrice <= filter.MarketPrice.MaximumPrice
                                    && w.MarketPrice >= filter.MarketPrice.MinimumPrice).ToList();
                    }                   
                }
                //PurchasePrice
                if (!(filter.PurchasePrice.MaximumPrice == null && filter.PurchasePrice.MinimumPrice == null))
                {
                    if (filter.PurchasePrice.MaximumPrice == null && filter.PurchasePrice.MinimumPrice != null)
                    {
                        rv = rv.Where(wine => wine.PurchasePrice >= filter.PurchasePrice.MinimumPrice).ToList();
                    }
                    else if (filter.PurchasePrice.MaximumPrice != null && filter.PurchasePrice.MinimumPrice == null)
                    {
                        rv = rv.Where(wine => wine.PurchasePrice <= filter.PurchasePrice.MaximumPrice).ToList();
                    }
                    else
                    {
                        rv = rv.Where(w => w.MarketPrice <= filter.MarketPrice.MaximumPrice
                                    && w.MarketPrice >= filter.MarketPrice.MinimumPrice).ToList();
                    }
                }
                //Region
                if (filter.Region != null && filter.Region.Count != 0)
                {
                    var regionList = new List<string>();
                    foreach (var item in filter.Region)
                    {
                        regionList.Add(item);
                    }
                    foreach (var wine in rv.ToList())
                    {
                        if (!regionList.Contains(wine.Region))
                        {
                            rv.Remove(wine);
                        }
                    }
                }
                //Tags
                if (filter.Tags != null)
                {
                    rv = rv.Where(w => w.Tags == filter.Tags).ToList();
                }
                //Rating
                if (filter.Rating != null && !(filter.Rating.MinRating == 1 && filter.Rating.MaxRating == 10))
                {
                    rv = rv.Where(w => w.Rating <= filter.Rating.MaxRating
                                    && w.Rating >= filter.Rating.MinRating).ToList();
                }
                //Format
                if (filter.Format != null && filter.Format.Count != 0)
                {
                    var formatList = new List<string>();
                    foreach (var item in filter.Format)
                    {
                        formatList.Add(item);
                    }
                    foreach (var wine in rv.ToList())
                    {
                        if (!formatList.Contains(wine.Format))
                        {
                            rv.Remove(wine);
                        }
                    }
                }

                //paginate the filtered Wines
                return rv.OrderBy(sortBy)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

            } else
            {
                var rv = Database.Wines
                       .Where(w => w.Vineyard.Contains(search)
                                || w.GrapeType.Contains(search)
                                || w.Year.ToString().Contains(search))
                       .OrderBy(sortBy)
                       .Skip((pageIndex - 1) * pageSize)
                       .Take(pageSize)
                       .ToList<Wine>();

                //Tags conversion from good,yummy,dry to JSON string
                //establish tagList object to eventually convert to JSON
                var tagList = new List<string>();

                //if a wine has tags, split the tags on ',' and add them to the tagList
                foreach (var wine in rv)
                {
                    if (wine.Tags != null)
                    {
                        var tagArray = wine.Tags.Split(',');
                        for (int i = 0; i < tagArray.Length; i++)
                        {
                            tagList.Add(tagArray[i]);
                        }
                        //convert the taglist to JSON and save to wine.Tags
                        wine.Tags = JsonConvert.SerializeObject(tagList);
                        //empty the tagList
                        tagList = new List<string>();
                    }
                }

                return rv;
            }
        }

        public int GetWineCount(string search, FilterParameters filter)
        {
            if (filter != null)
            {
                //new List to feed Filtered wines into
                var nrv = new List<Wine>();

                var rv = Database.Wines
                    .Where(w => w.Vineyard.Contains(search)
                                || w.GrapeType.Contains(search)
                                || w.Year.ToString().Contains(search))
                    .AsQueryable();

                //Color
                if (filter.Color != null && filter.Color.Count != 0)
                {
                    foreach (var item in filter.Color)
                    {
                        if (item != string.Empty)
                        {
                            foreach (var wine in rv.Where(w => w.Color == item))
                            {
                                if (!nrv.Contains(wine))
                                {
                                    nrv.Add(wine);
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (var wine in rv)
                    {
                        if (!nrv.Contains(wine))
                        {
                            nrv.Add(wine);
                        }
                    }
                }
                //DrinkableEnd
                var drinkend = filter.DrinkableEnd.ToString();
                if (filter.DrinkableEnd != null && filter.DrinkableEnd.ToString() != "1/1/0001 12:00:00 AM")
                {
                    rv = rv.Where(w => w.DrinkableEnd <= filter.DrinkableEnd);
                }
                //DrinkableStart
                var drinkstart = filter.DrinkableEnd.ToString();
                if (filter.DrinkableStart != null && filter.DrinkableStart.ToString() != "1/1/0001 12:00:00 AM")
                {
                    rv = rv.Where(w => w.DrinkableStart >= filter.DrinkableStart);
                }
                //Favorite
                if (filter.Favorite != null)
                {
                    rv = rv.Where(w => w.Favorite == filter.Favorite);
                }
                //MarketPrice
                if (filter.MarketPrice != null)
                {
                    rv = rv.Where(w => w.MarketPrice <= filter.MarketPrice.MaximumPrice
                                    && w.MarketPrice >= filter.MarketPrice.MinimumPrice);
                }
                //PurchasePrice
                if (filter.PurchasePrice != null)
                {
                    rv = rv.Where(w => w.PurchasePrice <= filter.PurchasePrice.MaximumPrice
                                    && w.PurchasePrice >= filter.PurchasePrice.MinimumPrice);
                }
                //Region
                if (filter.Region != null && filter.Region.Count != 0)
                {
                    foreach (var item in filter.Region)
                    {
                        if (item != string.Empty)
                        {
                            foreach (var wine in rv.Where(w => w.Region == item))
                            {
                                if (!nrv.Contains(wine))
                                {
                                    nrv.Add(wine);
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (var wine in rv)
                    {
                        if (!nrv.Contains(wine))
                        {
                            nrv.Add(wine);
                        }
                    }
                }
                //Tags
                if (filter.Tags != null)
                {
                    rv = rv.Where(w => w.Tags == filter.Tags);
                }
                //Rating
                if (filter.Rating != null && !(filter.Rating.MinRating == 1 && filter.Rating.MaxRating == 10))
                {
                    rv = rv.Where(w => w.Rating <= filter.Rating.MaxRating
                                    && w.Rating >= filter.Rating.MinRating);
                }
                //Format
                if (filter.Format != null && filter.Format.Count != 0)
                {
                    foreach (var item in filter.Format)
                    {
                        if (item != string.Empty)
                        {
                            foreach (var wine in rv.Where(w => w.Format == item))
                            {
                                if (!nrv.Contains(wine))
                                {
                                    nrv.Add(wine);
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (var wine in rv)
                    {
                        if (!nrv.Contains(wine))
                        {
                            nrv.Add(wine);
                        }
                    }
                }

                return nrv.Count;
            }
            else
            {
                return Database.Wines
                       .Where(w => w.Vineyard.Contains(search)
                                || w.GrapeType.Contains(search)
                                || w.Year.ToString().Contains(search))
                       .ToList<Wine>()
                       .Count;
            }
        }
         
        public JsonWineGet JsonGetReturn(List<Wine> list, int pageIndex, int pageSize, int count)
        {
            JsonWineGet jsonObject = new JsonWineGet();
            jsonObject.Success = true;
            jsonObject.Message = "we did it";
            jsonObject.PageIndex = pageIndex;
            jsonObject.PageSize = pageSize;
            jsonObject.Data = list;
            jsonObject.TotalPages = (int)Math.Ceiling((double)count / (double)pageSize); 

            return jsonObject;
        }
        
        public Wine GetIndividualWineByID(int id)
        {
            var wine = Database.Wines.First(w => w.ID == id);

            return wine;
        }

        public void DeleteWine(int id)
        {
            Database.Wines.Remove(GetIndividualWineByID(id));
            Database.SaveChanges();
        }

        public void DecrementWineBottles(int id)
        {
            var wine = Database.Wines.First(f => f.ID == id);
            wine.NumberOfBottles--;
            Database.SaveChanges();
        }

        public void AddWine(Wine wine)
        {
            wine.DateAdded = DateTime.Now;
            Database.Wines.Add(wine);
            Database.SaveChanges();
        }

        public Wine FindNewWine(Wine wine)
        {
            return Database.Wines
                    .Where(w => w.Vineyard == wine.Vineyard
                            && w.GrapeType == wine.GrapeType
                            && w.Year == wine.Year
                            && w.NumberOfBottles == wine.NumberOfBottles
                            && w.Format == wine.Format
                            && w.Location == wine.Location
                            && w.DrinkableStart == wine.DrinkableStart
                            && w.DrinkableEnd == wine.DrinkableEnd
                            && w.PurchasePrice == wine.PurchasePrice
                            && w.MarketPrice == wine.MarketPrice
                            && w.Favorite == wine.Favorite
                            && w.Region == wine.Region
                            && w.Color == wine.Color
                            && w.Tags == wine.Tags  
                            && w.UserID == wine.UserID
                            )
                    .First();
                                
        }

        public void EditWine(int id, Wine wine, string tagList)
        {
            Wine editWine = Database.Wines.First(f => f.ID == id);

            editWine.ID = id;
            editWine.Color = wine.Color;
            editWine.DrinkableEnd = wine.DrinkableEnd;
            editWine.DrinkableStart = wine.DrinkableStart;
            editWine.Favorite = wine.Favorite;
            editWine.Format = wine.Format;
            editWine.GrapeType = wine.GrapeType;
            editWine.Location = wine.Location;
            editWine.MarketPrice = wine.MarketPrice;
            editWine.NumberOfBottles = wine.NumberOfBottles;
            editWine.PurchasePrice = wine.PurchasePrice;
            editWine.Rating = wine.Rating;
            editWine.Region = wine.Region;
            editWine.Tags = tagList;
            editWine.UserID = wine.UserID;
            editWine.Vineyard = wine.Vineyard;
            editWine.Year = wine.Year;

            try
            {
                Database.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }     

        public JsonWineCrud JsonCrudReturn(int id)
        {
            return new JsonWineCrud {
                Success = true,
                Message = "we did it",
                WineID = id
            };
        }
    }
}