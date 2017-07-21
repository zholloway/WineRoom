using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineRoomAPI.Models.FilterModels
{
    public class PurchasePrice
    {
        public decimal? MaximumPrice { get; set; }
        public decimal? MinimumPrice { get; set; }
    }
}