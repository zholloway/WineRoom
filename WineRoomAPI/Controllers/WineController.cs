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
using Newtonsoft.Json;
using System.Web;

namespace WineRoomAPI.Controllers
{
    [EnableCors(origins: "http://wineroomonline.gear.host", headers: "*", methods: "*")]
    public class WineController : ApiController
    {
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        WineServices wineServices = new WineServices();

        //get wine
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(wineServices.GetIndividualWineByID(id));
        }

        [HttpGet]
        public IHttpActionResult Get(FilterParameters filter, int pageIndex = 1, int pageSize = 10, string sortBy = "ID", string search = "")
        {
            var request = Request.RequestUri;
            var uri = new Uri(request.AbsoluteUri);
            var query = uri.ParseQueryString();
            var filters = query.Get("filter");
            var jObj = new FilterParameters();
            if(filters != null)
            {
                jObj = JsonConvert.DeserializeObject<FilterParameters>(filters);
            }          
            
            var list = wineServices.GetAllWine(pageIndex, pageSize, sortBy, search, jObj);
            var count = wineServices.GetWineCount(search, jObj);
            return Ok(wineServices.JsonGetReturn(list, pageIndex, pageSize, count));
        }

        //create wine
        [HttpPost]
        public IHttpActionResult Add(Wine wine)
        {
            var request = Request.RequestUri;
            var uri = new Uri(request.AbsoluteUri);
            var query = uri.ParseQueryString();
            var tags = query.Get("Tags");
            var jObj = new List<string>();
            if (tags != null)
            {
                jObj = JsonConvert.DeserializeObject<List<string>>(tags);
            }
            var tagList = string.Empty;
            foreach (var item in jObj)
            {
                tagList += $"{item},";
            }

            wine.Tags = tagList;

            wineServices.AddWine(wine);
            var newWine = wineServices.FindNewWine(wine);
            return Ok(wineServices.JsonCrudReturn(newWine.ID));
        }

        //update wine
        [HttpPut]
        public IHttpActionResult Edit(int id, Wine wine)
        {
            var request = Request.RequestUri;
            var uri = new Uri(request.AbsoluteUri);
            var query = uri.ParseQueryString();
            var tags = query.Get("Tags");
            var jObj = new List<string>();
            if (tags != null)
            {
                jObj = JsonConvert.DeserializeObject<List<string>>(tags);
            }
            var tagList = string.Empty;
            foreach(var item in jObj)
            {
                tagList += $"{item},";
            }

            wineServices.EditWine(id, wine, tagList);
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
