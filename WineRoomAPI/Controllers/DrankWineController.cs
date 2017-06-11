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
        public IHttpActionResult Get(int pageIndex = 1, int pageSize = 10, string search = "")
        {   
            List<DrankWine> drankWines = drankWineServices.GetDrankWines(pageIndex, pageSize, search);
            List<DrankWine> drunkWines = db.DrankWines.ToList();

            return Ok(drankWineServices.JsonDrankWineGet(drankWines, pageIndex, pageSize));
        }

        [HttpPut]
        public IHttpActionResult EditDrankWine(int id, DrankWine drankWine)
        {
            drankWineServices.EditDrankWine(id, drankWine);
            return Ok(drankWineServices.JsonDrankWineReturn(id));
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
