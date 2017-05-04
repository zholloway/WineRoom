using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WineRoomAPI.Models;
using WineRoomAPI.Services.DrankWineServices;

namespace WineRoomAPI.Controllers
{
    public class DrankWineController : ApiController
    {
        DrankWineServices drankWineServices = new DrankWineServices();

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(drankWineServices.GetDrankWines());
        }

        [HttpPost]
        public IHttpActionResult EditDrankWine(int id, DrankWine drankWine)
        {
            drankWineServices.EditDrankWine(id, drankWine);
            return Ok($"Wine ID-{drankWine.WineID} successfully edited.");
        } 

        [HttpPost]
        public IHttpActionResult AddDrankWine(DrankWine drankWine)
        {
            drankWineServices.AddDrankWine(drankWine);
            return Ok($"Wine ID-{drankWine.WineID} added to Drank List.");
        }   

        [HttpDelete]
        public IHttpActionResult DeleteDrankWine(int id)
        {
            drankWineServices.DeleteDrankWine(id);
            return Ok($"Wine ID-{id} deleted from Drank List.");
        }
    }
}
