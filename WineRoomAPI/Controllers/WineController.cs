using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using WineRoomAPI.DataContext;
using WineRoomAPI.Models;
using WineRoomAPI.Models.FilterModels;
using WineRoomAPI.Services;
using System.Web.Http.Cors;

namespace WineRoomAPI.Controllers
{
    [EnableCors(origins: "http://wineroomonline.gear.host", headers: "*", methods: "*")]
    public class WineController : ApiController
    {
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        static WineroomContext database = new WineroomContext();

        WineServices wineServices = new WineServices(database);

        //get wine
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(wineServices.GetIndividualWineByID(id));
        }

        [HttpGet]
        public IHttpActionResult Get(FilterParameters filter, int pageIndex = 1, int pageSize = 10, string sortBy = "ID", string search = "")
        {
            var list = wineServices.GetAllWine(pageIndex, pageSize, sortBy, search, filter);          
            return Ok(wineServices.JsonGetReturn(list, pageIndex, pageSize));
        }

        //create wine
        [HttpPost]
        public IHttpActionResult Add(Wine wine)
        {
            wineServices.AddWine(wine);
            var newWine = wineServices.FindNewWine(wine);
            return Ok(wineServices.JsonCrudReturn(newWine.ID));
        }

        //update wine
        [HttpPut]
        public IHttpActionResult Edit(int id, Wine wine)
        {
            wineServices.EditWine(id, wine);
            return Ok(wineServices.JsonCrudReturn(id));
        }

        //delete wine
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            wineServices.DeleteWine(id);
            return Ok(wineServices.JsonCrudReturn(id));
        }
    }
}
