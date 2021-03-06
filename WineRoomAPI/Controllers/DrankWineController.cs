﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WineRoomAPI.DataContext;
using WineRoomAPI.Models;
using WineRoomAPI.Services;
using WineRoomAPI.Services.DrankWineServices;

namespace WineRoomAPI.Controllers
{
    [EnableCors(origins: "http://wineroomonline.gear.host", headers: "*", methods: "*")]
    public class DrankWineController : ApiController
    {
        DrankWineServices drankWineServices = new DrankWineServices();
        WineServices wineServices = new WineServices();
        WineroomContext db = new WineroomContext();

        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        [HttpGet]
        public IHttpActionResult Get(int userID, int pageIndex = 1, int pageSize = 10, string search = "")
        {   
            List<DrankWine> drankWines = drankWineServices.GetDrankWines(userID, pageIndex, pageSize, search);

            return Ok(drankWineServices.JsonDrankWineGet(drankWines, pageIndex, pageSize));
        }
        [HttpPut]
        public IHttpActionResult EditDrankWine([FromUri]int ID)
        {
            var id = ID;

            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            PutDrankWine putDrankWine = JsonConvert.DeserializeObject<PutDrankWine>(jsonContent);
            //string people = string.Empty;

            ////convert list of people to single string for db storage
            //foreach (var person in putDrankWine.People)
            //{
            //    people += $"{person.text} ";
            //}

            drankWineServices.EditDrankWine(id, putDrankWine);

            return Ok(drankWineServices.JsonDrankWineReturn(putDrankWine.ID));
        } 

        [HttpPost]
        public IHttpActionResult AddDrankWine(DrankWine wine)
        {
            //store location temporarily
            var location = wineServices.GetIndividualWineByID(wine.WineID).Location;

            //add drankwine
            DrankWine drunkWine = drankWineServices.AddDrankWine(wine);

            //delete the wine from the wine list
            wineServices.DecrementWineBottles(drunkWine.WineID);

            return Ok(drankWineServices.JsonDrankWineAddReturn(drunkWine.ID, location));
        }   

        [HttpDelete]
        public IHttpActionResult DeleteDrankWine(int id)
        {
            drankWineServices.DeleteDrankWine(id);
            return Ok(drankWineServices.JsonDrankWineReturn(id));
        }
    }
}
