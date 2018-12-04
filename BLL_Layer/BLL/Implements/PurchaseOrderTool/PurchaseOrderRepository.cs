﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL_Layer.BLL.Interface.PurchaseOrderTool;
using DAL_Layer.DAL.Models.OrderTrack;
using DAL_Layer.DAL.DBContext;
using System.Data.Entity;

namespace BLL_Layer.BLL.Implements.PurchaseOrderTool
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private PODBContext db = new PODBContext();

        public void Add(OrderModel order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
        }
    }
}