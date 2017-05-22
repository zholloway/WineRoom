using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WineRoomAPI.Models;
using WineRoomAPI.Services.DrankWineServices;

namespace WineRoomAPI.Controllers
{
    [EnableCors(origins: "http://wineroomonline.gear.host", headers: "*", methods: "*")]
    public class DrankWineController : ApiController
    {
        DrankWineServices drankWineServices = new DrankWineServices();

        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        [HttpGet]
        public IHttpActionResult Get(int pageIndex = 1, int pageSize = 10)
        {         
            return Ok(drankWineServices.JsonDrankWineGet(drankWineServices.GetDrankWines(pageIndex, pageSize)));
        }

        [HttpPut]
        public IHttpActionResult EditDrankWine(int id, DrankWine drankWine)
        {
            drankWineServices.EditDrankWine(id, drankWine);
            return Ok(drankWineServices.JsonDrankWineReturn(id));
        } 

        [HttpPost]
        public IHttpActionResult AddDrankWine(DrankWine drankWine)
        {
            drankWineServices.AddDrankWine(drankWine);
            var newDrankWine = drankWineServices.FindNewDrankWine(drankWine);
            return Ok(drankWineServices.JsonDrankWineReturn(newDrankWine.ID));
        }   

        [HttpDelete]
        public IHttpActionResult DeleteDrankWine(int id)
        {
            drankWineServices.DeleteDrankWine(id);
            return Ok(drankWineServices.JsonDrankWineReturn(id));
        }
    }
}
