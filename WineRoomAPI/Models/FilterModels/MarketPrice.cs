using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineRoomAPI.Models.FilterModels
{
    public class MarketPrice
    {
        public decimal? MinimumPrice { get; set; }
        public decimal? MaximumPrice { get; set; }
    }
}