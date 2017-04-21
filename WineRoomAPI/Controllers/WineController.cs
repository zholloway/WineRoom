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

namespace WineRoomAPI.Controllers
{
    [AllowCrossSiteJson]
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
        public IHttpActionResult Get(FilterParameters filter, int pageIndex = 1, int pageSize = 10, string sortBy = "ID", string search = "")
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
