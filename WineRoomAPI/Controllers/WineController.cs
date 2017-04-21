using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WineRoomAPI.DataContext;
using WineRoomAPI.Models;
using WineRoomAPI.Models.FilterModels;
using WineRoomAPI.Services;

namespace WineRoomAPI.Controllers
{
    public class WineController : ApiController
    {
        static WineroomContext database = new WineroomContext();

        WineServices wineServices = new WineServices(database);

        //get wine
        [HttpGet]
        public IHttpActionResult Get(int pageIndex = 1, int pageSize = 10, string sortBy = "ID", string search = "", FilterParameters filter = null)
        {
            return Ok(wineServices.GetAllWine(pageIndex, pageSize, sortBy, search, filter));
        }

        //create wine
        [HttpPut]
        public IHttpActionResult Add(Wine wine)
        {
            wineServices.AddWine(wine);
            return Ok("New wine successfully added to the collection.");
        }

        //update wine
        [HttpPost]
        public IHttpActionResult Edit(Wine wine)
        {
            wineServices.EditWine(wine);
            return Ok("The wine was successfully updated.");
        }

        //delete wine
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            wineServices.DeleteWine(id);
            return Ok("The wine was successfully deleted from the collection.");
        }
    }
}
