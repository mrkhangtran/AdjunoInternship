using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InternProject.ViewModels.PurchaseOrderManager;
using BLL_Layer.BLL.Interface;
using DomainModel.Models;

namespace InternProject.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private IPurchaseOrderRepository PurchaseOrder;

        public PurchaseOrderController() { }

        public PurchaseOrderController(IPurchaseOrderRepository purchaseOrder)
        {
            this.PurchaseOrder = purchaseOrder;
        }

        public ActionResult Create()
        {
            AddModel defaultModel = new AddModel();

            defaultModel = SetDropDownList(defaultModel);

            return View(defaultModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderDate,Buyer,Currency,Season,Department,Vendor,Company,Origin,PortOfLoading,PortOfDelivery,OrderType,Factory,Mode,ShipDate,LatestShipDate,DeliveryDate,Status")] AddModel addModel)
        {
            addModel = SetDropDownList(addModel);

            if (ModelState.IsValid)
            {
                //PurchaseOrder.Add(addModel.Id, addModel.OrderDate, addModel.Buyer, addModel.Currency, addModel.Season, addModel.Department, addModel.Vendor)
                PurchaseOrder.Add(ConverttoOrderModel(addModel));

                return RedirectToAction("Index");
            }

            return View(addModel);
        }

        private AddModel SetDropDownList(AddModel addModel)
        {
            AddModel init = new AddModel();
            init = addModel;

            init.Seasons = GetSelectListItems(SeasonList());
            init.Origins = GetSelectListItems(new List<string> { "HongKong", "Vietnam" });
            init.Ports = GetSelectListItems(new List<string> { "Port 1", "Port 2", "Port 3" });
            init.Modes = GetSelectListItems(new List<string> { "Road", "Sea", "Air" });

            return init;
        }

        private OrderModel ConverttoOrderModel(AddModel addModel)
        {
            OrderModel order = new OrderModel();

            order.Id = addModel.Id;
            order.OrderDate = addModel.OrderDate;
            order.Buyer = addModel.Buyer;
            order.Currency = addModel.Currency;
            order.Season = addModel.Season;
            order.Department = addModel.Department;
            order.Vendor = addModel.Vendor;
            order.Company = addModel.Company;
            order.Origin = addModel.Origin;
            order.PortOfLoading = addModel.PortOfLoading;
            order.PortOfDelivery = addModel.PortOfDelivery;
            order.OrderType = addModel.OrderType;
            order.Factory = addModel.Factory;
            order.Mode = addModel.Mode;
            order.ShipDate = addModel.ShipDate;
            order.LatestShipDate = addModel.LatestShipDate;
            order.DeliveryDate = addModel.DeliveryDate;
            order.Status = addModel.Status;

            order.POQuantity = 0;
            if (addModel.PODetails != null)
            {
                foreach (var i in addModel.PODetails)
                {
                    order.POQuantity += i.Quantity;
                }
            }

            order.Supplier = "";

            return order;
        }

        private IEnumerable<string> SeasonList()
        {
            List<string> seasonList = new List<string>();
            for (int i = 2010; i <= 2020; i++)
            {
                seasonList.Add(i.ToString());
            }
            return seasonList;
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            var selectList = new List<SelectListItem>();

            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }

            return selectList;
        }

        // GET: PurchaseOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int Id = id ?? default(int);
            OrderModel addModel = PurchaseOrder.Find(Id);
            if (addModel == null)
            {
                return HttpNotFound();
            }
            return View(addModel);
        }

        // POST: PurchaseOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderDate,Buyer,Currency,Season,Department,Vendor,Company,Origin,PortOfLoading,PortOfDelivery,OrderType,Factory,Mode,ShipDate,LatestShipDate,DeliveryDate,Status")] AddModel addModel)
        {
            if (ModelState.IsValid)
            {
                /*db.Entry(addModel).State = EntityState.Modified;
                db.SaveChanges();*/
                return RedirectToAction("Index");
            }
            return View(addModel);
        }

        // GET: PurchaseOrder
        /*public ActionResult Index()
        {
            List<AddModel> addModels = new List<AddModel>();
            addModels = db.Orders.ToList();
            return View(db.AddModels.ToList());
        }

        // GET: PurchaseOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddModel addModel = db.Orders.Find(id);
            if (addModel == null)
            {
                return HttpNotFound();
            }
            return View(addModel);
        }

        // GET: PurchaseOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddModel addModel = db.AddModels.Find(id);
            if (addModel == null)
            {
                return HttpNotFound();
            }
            return View(addModel);
        }

        // POST: PurchaseOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AddModel addModel = db.AddModels.Find(id);
            db.AddModels.Remove(addModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
