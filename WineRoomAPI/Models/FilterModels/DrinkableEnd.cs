using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineRoomAPI.Models.FilterModels
{
    public class DrinkableEnd
    {
        public DateTime EarliestDate { get; set; }
        public DateTime LatestDate { get; set; }
    }
}