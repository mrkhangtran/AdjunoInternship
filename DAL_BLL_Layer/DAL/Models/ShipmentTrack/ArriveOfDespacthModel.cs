﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL_BLL_Layer.DAL.Models.ShipmentTrack
{
    public class ArriveOfDespacthModel
    {
        [Key]
        public int Id { get; set; }

        //[ForeignKey("BookingModel")]
        public int BookingId { get; set; }
        public string Carrier { get; set; }
        public string Vessel { get; set; }
        public string Voyage { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CTD { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ETA { get; set; }
        public string OriginPort { get; set; }
        public string DestinationPort { get; set; }
        public string Mode { get; set; }
        public string Confirmed { get; set; }

        public virtual BookingModel Booking { get; set; }

    }
}