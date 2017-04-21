using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineRoomAPI.Models.FilterModels
{
    public class PurchasePrice
    {
        public decimal MaximumValue { get; set; }
        public decimal MinimumValue { get; set; }
    }
}