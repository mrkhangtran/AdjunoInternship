using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL_Layer.BLL.Interface;
using DomainModel.Models;
using System.Data.Entity;
using DTOs;
using DatabaseRepo;

namespace BLL_Layer.BLL.Implements
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private IPODBContext db;

        public PurchaseOrderRepository()
        {

        }

        public PurchaseOrderRepository(IPODBContext db)
        {
            this.db = db;
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

        public OrderDTO Find(int Id)
        {
            OrderDTO orderDTO = new OrderDTO();
            OrderModel orderModel= db.GetDB().Orders.Find(Id);
            orderDTO.Id = orderModel.Id; //mapping data
            return orderDTO;
        }
        
    }
}