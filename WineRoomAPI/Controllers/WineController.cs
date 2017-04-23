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
    [AllowCrossSiteJson]
    [EnableCors(origins: "http://wineroomonline.gear.host", headers: "*", methods: "*")]
    public class WineController : ApiController
    {
            /*
            The following class allows the Wine User Site to access everything by 
            setting the header "Access-Control-Allow-Origin" to "*". 
            [AllowCrossSiteJson] above the WineController class applies this status to every method here.
            */
        public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
            {
                if (actionExecutedContext.Response != null)
                    actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                base.OnActionExecuted(actionExecutedContext);
            }
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
            return Ok(wineServices.GetAllWine(pageIndex, pageSize, sortBy, search, filter));
        }

        //create wine
        [HttpPost]
        public IHttpActionResult Add(Wine wine)
        {
            wineServices.AddWine(wine);
            var newWine = wineServices.FindNewWine(wine);
            return Ok($"Wine ID-{newWine.ID} successfully added.");
        }

        //update wine
        [HttpPut]
        public IHttpActionResult Edit(int id, Wine wine)
        {
            wineServices.EditWine(id, wine);
            return Ok($"Wine ID-{id} successfully updated.");
        }

        //delete wine
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            wineServices.DeleteWine(id);
            return Ok($"Wine ID-{id} successfully deleted.");
        }
    }
}
