﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL_Layer.BLL.Interface;
using DomainModel.Models;
using System.Data.Entity;
using DTOs;
using DatabaseRepo;
using Unity;
using DAL_Layer.DAL.DBContext;
using System.Data.Entity.Migrations;

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

        public OrderModel ConvertToOrderModel(OrderDTO orderDTO)
        {
            OrderModel orderModel = new OrderModel();

            orderModel.Id = orderDTO.Id;
            orderModel.PONumber = orderDTO.PONumber;
            orderModel.OrderDate = orderDTO.OrderDate;
            orderModel.Buyer = orderDTO.Buyer;
            orderModel.Currency = orderDTO.Currency;
            orderModel.Season = orderDTO.Season;
            orderModel.Department = orderDTO.Department;
            orderModel.Vendor = orderDTO.Vendor;
            orderModel.Company = orderDTO.Company;
            orderModel.Origin = orderDTO.Origin;
            orderModel.PortOfLoading = orderDTO.PortOfLoading;
            orderModel.PortOfDelivery = orderDTO.PortOfDelivery;
            orderModel.OrderType = orderDTO.OrderType;
            orderModel.Factory = orderDTO.Factory;
            orderModel.Mode = orderDTO.Mode;
            orderModel.ShipDate = orderDTO.ShipDate;
            orderModel.LatestShipDate = orderDTO.LatestShipDate;
            orderModel.DeliveryDate = orderDTO.DeliveryDate;
            orderModel.Status = orderDTO.Status;

            orderModel.POQuantity = 0;
            if (orderDTO.PODetails != null)
            {
                foreach (var i in orderDTO.PODetails)
                {
                    orderModel.POQuantity += i.Quantity;
                }
            }

            orderModel.Supplier = "";

            return orderModel;
        }

        public OrderDTO ConvertToOrderDTO(OrderModel orderModel)
        {
            OrderDTO orderDTO = new OrderDTO();

            orderDTO.Id = orderModel.Id;
            orderDTO.PONumber = orderModel.PONumber;
            orderDTO.OrderDate = orderModel.OrderDate;
            orderDTO.Buyer = orderModel.Buyer;
            orderDTO.Currency = orderModel.Currency;
            orderDTO.Season = orderModel.Season;
            orderDTO.Department = orderModel.Department;
            orderDTO.Vendor = orderModel.Vendor;
            orderDTO.Company = orderModel.Company;
            orderDTO.Origin = orderModel.Origin;
            orderDTO.PortOfLoading = orderModel.PortOfLoading;
            orderDTO.PortOfDelivery = orderModel.PortOfDelivery;
            orderDTO.OrderType = orderModel.OrderType;
            orderDTO.Factory = orderModel.Factory;
            orderDTO.Mode = orderModel.Mode;
            orderDTO.ShipDate = orderModel.ShipDate;
            orderDTO.LatestShipDate = orderModel.LatestShipDate;
            orderDTO.DeliveryDate = orderModel.DeliveryDate;
            orderDTO.Status = orderModel.Status;
            orderDTO.POQuantity = orderModel.POQuantity;

            List<OrderDetailModel> orderDetailModels = db.GetDB().OrderDetails.Where(p => p.OrderId == orderModel.Id).ToList();
            orderDTO.PODetails = new List<OrderDetailDTO>();

            if (orderDetailModels != null)
            {
                foreach (var i in orderDetailModels)
                {
                    OrderDetailDTO orderDetailDTO = new OrderDetailDTO();

                    orderDetailDTO.Id = i.Id;
                    orderDetailDTO.ItemNumber = i.ItemNumber;
                    orderDetailDTO.Description = i.Description;
                    orderDetailDTO.Tariff = i.Tariff;
                    orderDetailDTO.Quantity = i.Quantity;
                    orderDetailDTO.Cartons = i.Cartons;
                    orderDetailDTO.Cube = i.Cube;
                    orderDetailDTO.KGS = i.KGS;
                    orderDetailDTO.UnitPrice = i.UnitPrice;
                    orderDetailDTO.RetailPrice = i.RetailPrice;
                    orderDetailDTO.Warehouse = i.Warehouse;
                    orderDetailDTO.Size = i.Size;
                    orderDetailDTO.Colour = i.Colour;

                    orderDTO.PODetails.Add(orderDetailDTO);
                }
            }

            return orderDTO;
        }

        public void AddOrUpdate(OrderDTO newOrderDTO)
        {
            db.GetDB().Orders.AddOrUpdate(ConvertToOrderModel(newOrderDTO));
            db.GetDB().SaveChanges();
        }

        public OrderDTO Find(string id)
        {
            OrderModel orderModel = new OrderModel(); //db.GetDB().Orders.Find(id);
            foreach (var item in db.GetDB().Orders.ToList())
            {
                if (item.PONumber == id)
                {
                    orderModel = item;
                }
            }

            if (orderModel == null) { orderModel = new OrderModel(); }

            return ConvertToOrderDTO(orderModel);
        }
        
        public OrderDetailModel ConvertToOrderDetailModel(OrderDetailDTO orderDetailDTO)
        {
            OrderDetailModel orderDetailModel = new OrderDetailModel();

            orderDetailModel.Id = orderDetailDTO.Id;
            orderDetailModel.ItemNumber = orderDetailDTO.ItemNumber;
            orderDetailModel.Description = orderDetailDTO.Description;
            orderDetailModel.Tariff = orderDetailDTO.Tariff;
            orderDetailModel.Quantity = orderDetailDTO.Quantity;
            orderDetailModel.Cartons = orderDetailDTO.Cartons;
            orderDetailModel.Cube = orderDetailDTO.Cube;
            orderDetailModel.KGS = orderDetailDTO.KGS;
            orderDetailModel.UnitPrice = orderDetailDTO.UnitPrice;
            orderDetailModel.RetailPrice = orderDetailDTO.RetailPrice;
            orderDetailModel.Warehouse = orderDetailDTO.Warehouse;
            orderDetailModel.Size = orderDetailDTO.Size;
            orderDetailModel.Colour = orderDetailDTO.Colour;
            orderDetailModel.OrderId = orderDetailDTO.OrderId;

            if (orderDetailModel.OrderId == 0)
            {
                orderDetailModel.OrderId = db.GetDB().Orders.ToList().Last().Id;
            }

            orderDetailModel.Line = "";
            orderDetailModel.Item = "";

            return orderDetailModel;
        }

        public void AddOrUpdateItem(OrderDetailDTO newOrderDetail)
        {
            db.GetDB().OrderDetails.AddOrUpdate(ConvertToOrderDetailModel(newOrderDetail));
            db.GetDB().SaveChanges();
        }

        public OrderDetailDTO ConvertToOrderDetailDTO(OrderDetailModel orderDetail)
        {
            OrderDetailDTO orderDetailDTO = new OrderDetailDTO();

            orderDetailDTO.Id = orderDetail.Id;
            orderDetailDTO.ItemNumber = orderDetail.ItemNumber;
            orderDetailDTO.Description = orderDetail.Description;
            orderDetailDTO.Tariff = orderDetail.Tariff;
            orderDetailDTO.Quantity = orderDetail.Quantity;
            orderDetailDTO.Cartons = orderDetail.Cartons;
            orderDetailDTO.Cube = orderDetail.Cube;
            orderDetailDTO.KGS = orderDetail.KGS;
            orderDetailDTO.UnitPrice = orderDetail.UnitPrice;
            orderDetailDTO.RetailPrice = orderDetail.RetailPrice;
            orderDetailDTO.Warehouse = orderDetail.Warehouse;
            orderDetailDTO.Size = orderDetail.Size;
            orderDetailDTO.Colour = orderDetail.Colour;
            orderDetailDTO.OrderId = orderDetail.OrderId;

            return orderDetailDTO;
        }

        public OrderDetailDTO FindOrderDetail(string id)
        {
            OrderDetailModel orderDetail = new OrderDetailModel(); //db.GetDB().OrderDetails.Find(id);
            foreach (var item in db.GetDB().OrderDetails.ToList())
            {
                if (item.ItemNumber == id)
                {
                    orderDetail = item;
                }
            }

            if (orderDetail == null) { orderDetail = new OrderDetailModel(); }

            return ConvertToOrderDetailDTO(orderDetail);
        }

        public bool UniquePONum(string PONumber, int? id)
        {
            if (id == null) { id = 0; }
            OrderModel findOrder = db.GetDB().Orders.Find(id);
            string oldPONum = "-1";
            if (findOrder != null)
            {
                oldPONum = findOrder.PONumber;
            }

            if (PONumber == oldPONum)
            {
                return true;
            }
            
            List<OrderModel> orders = db.GetDB().Orders.ToList();
            foreach (var i in orders)
            {
                if (i.PONumber == PONumber)
                {
                    return false;
                }
            }

            return true;
        }

        public bool UniqueItemNum(string itemNum, int? id)
        {
            if (id == null) { id = 0; }
            OrderDetailModel findItem = db.GetDB().OrderDetails.Find(id);
            string oldItemNum = "-1";
            if (findItem != null)
            {
                oldItemNum = findItem.ItemNumber;
            }

            if (itemNum == oldItemNum)
            {
                return true;
            }

            List<OrderDetailModel> orderDetailModels = db.GetDB().OrderDetails.ToList();
            foreach (var i in orderDetailModels)
            {
                if (i.ItemNumber == itemNum)
                {
                    return false;
                }
            }

            return true;
        }
    }
}