using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WineRoomAPI.Services;

namespace WineRoomAPI.Controllers
{
    public class WineController : ApiController
    {
        const string PathToWineRoomDatabase = @"Server=mssql5.gear.host;Database=wineroom;Uid=wineroom;Pwd=Xp6wb?Pn~ZoF;";

        WineServices wineServices = new WineServices(PathToWineRoomDatabase);

        //get all wine
        [HttpGet]
        public IHttpActionResult GetAllWine()
        {
            return Ok(wineServices.GetAllWine());
        }
        //get individual wine
        //create wine
        //update wine
        //delete wine
    }
}
