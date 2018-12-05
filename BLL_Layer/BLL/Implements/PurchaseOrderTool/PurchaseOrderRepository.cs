using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL_Layer.BLL.Interface;
using DomainModel.Models;
using System.Data.Entity;

namespace BLL_Layer.BLL.Implements
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private IPODBContext db;

        public PurchaseOrderRepository()
        {

        }

        public PurchaseOrderRepository(IPODBContext pODB)
        {
            this.db = pODB;
        }

        public void Add(OrderModel order)
        {
            db.GetDB().Orders.Add(order);
            db.GetDB().SaveChanges();
        }

        public void Edit(int Id, OrderModel newOrder)
        {
            //db.GetDB().Orders.Find(Id).
        }

        public OrderModel Find(int Id)
        {
            return db.GetDB().Orders.Find(Id);
        }
    }
}