using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WineRoomAPI.Models;
using WineRoomAPI.Services;

namespace WineRoomAPI.Controllers
{
    public class WineController : ApiController
    {
        const string PathToWineRoomDatabase = @"Server=mssql5.gear.host;Database=wineroom;Uid=wineroom;Pwd=Xp6wb?Pn~ZoF;";

        WineServices wineServices = new WineServices(PathToWineRoomDatabase);

        //get all wine
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetAllWine()
        {
            return Ok(wineServices.GetAllWine());
        }

        //get individual wine
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetIndividualWine(int id)
        {
            return Ok(wineServices.GetIndividualWineByID(id));
        }

        //create wine
        [System.Web.Http.HttpPut]
        public IHttpActionResult AddWine(Wine wine)
        {
            wineServices.AddWine(wine);
            return Ok("New wine successfully added to the collection.");
        }

        //update wine
        [System.Web.Http.HttpPost]
        public IHttpActionResult EditWine(Wine wine)
        {
            wineServices.EditWine(wine);
            return Ok("The wine was successfully updated.");
        }

        //delete wine
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteWine(int id)
        {
            wineServices.DeleteWine(id);
            return Ok("The wine was successfully deleted from the collection.");
        }
    }
}
