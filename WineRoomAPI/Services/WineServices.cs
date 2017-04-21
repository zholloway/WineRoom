﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WineRoomAPI.DataContext;
using WineRoomAPI.Models;
using System.Linq.Dynamic;

namespace WineRoomAPI.Services
{
    public class WineServices
    {
        public WineroomContext Database { get; set; }

        public WineServices(WineroomContext database)
        {
            Database = database;
        }

        public List<Wine> GetAllWine(int pageIndex, int pageSize, string sortBy, string search, FilterParameters filter)
        {
            
            if (filter != null)
            {
                var rv = Database.Wines
                       .Where(w => w.Vineyard.Contains(search)
                                || w.GrapeType.Contains(search)
                                || w.Year.ToString().Contains(search))
                       .OrderBy(sortBy)
                       .Skip((pageIndex - 1) * pageSize)
                       .Take(pageSize)
                       .AsQueryable();

                if (filter.Color != null)
                {
                     rv = rv.Where(w => w.Color == filter.Color);
                }
                if (filter.DrinkableEnd != null)
                {
                    rv = rv.Where(w => w.DrinkableEnd <= filter.DrinkableEnd.LatestDate 
                                    && w.DrinkableEnd >= filter.DrinkableEnd.EarliestDate);
                }
                if (filter.DrinkableStart != null)
                {
                    rv = rv.Where(w => w.DrinkableStart <= filter.DrinkableStart.LatestDate 
                                    && w.DrinkableStart >= filter.DrinkableStart.EarliestDate);
                }
                if (filter.Favorite != null)
                {
                    rv = rv.Where(w => w.Favorite == filter.Favorite);
                }
                if (filter.MarketPrice != null)
                {
                    rv = rv.Where(w => w.MarketPrice <= filter.MarketPrice.MaximumPrice 
                                    && w.MarketPrice >= filter.MarketPrice.MinimumPrice);
                }
                if (filter.PurchasePrice != null)
                {
                    rv = rv.Where(w => w.PurchasePrice <= filter.PurchasePrice.MaximumValue 
                                    && w.PurchasePrice >= filter.PurchasePrice.MinimumValue);
                }
                if (filter.Region != null)
                {
                    rv = rv.Where(w => w.Region == filter.Region);
                }
                if (filter.Tags != null)
                {
                    rv = rv.Where(w => w.Tags == filter.Tags);
                }

                return rv.ToList();

            } else
            {
                return Database.Wines
                       .Where(w => w.Vineyard.Contains(search)
                                || w.GrapeType.Contains(search)
                                || w.Year.ToString().Contains(search))
                       .OrderBy(sortBy)
                       .Skip((pageIndex - 1) * pageSize)
                       .Take(pageSize)
                       .ToList<Wine>();
            }   
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

        public void AddWine(Wine wine)
        {
            wine.DateAdded = DateTime.Now;
            Database.Wines.Add(wine);
            Database.SaveChanges();
        }

        public void EditWine(Wine wine)
        {
            Wine editWine = Database.Wines.First(f => f.ID == wine.ID);

            editWine.Color = wine.Color;
            //editWine.DateAdded = wine.DateAdded;
            editWine.DrinkableEnd = wine.DrinkableEnd;
            editWine.DrinkableStart = wine.DrinkableStart;
            editWine.Favorite = wine.Favorite;
            editWine.Format = wine.Format;
            editWine.GrapeType = wine.GrapeType;
            editWine.Location = wine.Location;
            editWine.MarketPrice = wine.MarketPrice;
            editWine.NumberOfBottles = wine.NumberOfBottles;
            editWine.PurchasePrice = wine.PurchasePrice;
            editWine.Region = wine.Region;
            editWine.Tags = wine.Tags;
            editWine.UserID = wine.UserID;
            editWine.Vineyard = wine.Vineyard;
            editWine.Year = wine.Year;

            Database.SaveChanges();
        }
        
    }
}