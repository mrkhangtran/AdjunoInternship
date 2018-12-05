using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DomainModel.Models;

namespace DAL_Layer.DAL.DBContext
{
    public class PODBContext : DbContext
    {
        public PODBContext() : base("PODBContext") { }
         
        //OrderTrack
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderDetailModel> OrderDetails { get; set; }
        public DbSet<ProgressCheckModel> ProgressChecks { get; set; }

        //DeliveryTrack
        public DbSet<DCBookingDetailModel> DCBookingDetails { get; set; }
        public DbSet<DCBookingModel> DCBookings { get; set; }
        public DbSet<DCConfirmationDetailModel> DCConfirmationDetails { get; set; }
        public DbSet<DCConfirmationModel> DCConfirmations { get; set; }

        //ShipmentTrack
        public DbSet<ArriveOfDespacthModel> ArriveOfDespacths { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }
        public DbSet<CAModel> CAMs { get; set; }
        public DbSet<ManifestModel> Manifests { get; set; }
    }
}