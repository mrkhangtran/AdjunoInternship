using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL_Layer.BLL.Interface;
using DTOs;
using Unity;

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

        public ActionResult Index(int? id, int? method)
        {
            if ((id != null) && (method != null))
            {
                string message = "The Purchase Order number " + id.ToString() + " is ";
                switch (method)
                {
                    case 1:
                        message += "created";
                        break;
                    case 2:
                        message += "edited";
                        break;
                    case 3:
                        message += "copied";
                        break;

                }
                message += "successfully.Thank you.";
                ViewBag.Message = message;
            }

            return View();
        }

        //Create New Order
        public ActionResult Create()
        {
            ViewBag.Method = "Create Order";
            OrderDTO defaultModel = new OrderDTO();
            defaultModel.orderDetailDTO = new OrderDetailDTO();
            defaultModel = SetDropDownList(defaultModel);
            ViewBag.ItemId = -1;

            return View(defaultModel);
        }

        [HttpPost]
        public ActionResult Create(OrderDTO addModel, string method)
        {
            addModel = SetDropDownList(addModel);
            ViewBag.ItemId = -1;

            switch (method)
            {
                case "Apply":
                    if (addModel.PODetails == null)
                    {
                        ViewBag.OrderDetailError = "Please add at least 1 item detail.";
                        return View(addModel);
                    }

                    if (!(PurchaseOrder.UniquePONum(addModel.PONumber, addModel.Id)))
                    {
                        ViewBag.PONumberError = "PO Number must be unique.";
                        return View(addModel);
                    }

                    if (ModelState.IsValid)
                    {
                        PurchaseOrder.AddOrUpdate(addModel);
                        foreach (var i in addModel.PODetails)
                        {
                            PurchaseOrder.AddOrUpdateItem(i);
                        }

                        return RedirectToAction("Index", new { id = addModel.PONumber, method = 1 });
                    }
                    break;
                case "Save":
                    if (addModel.PODetails == null) { addModel.PODetails = new List<OrderDetailDTO>(); }

                    if ((!UniqueItemNumber(addModel.orderDetailDTO.ItemNumber, addModel.PODetails)) || (!(PurchaseOrder.UniqueItemNum(addModel.orderDetailDTO.ItemNumber, addModel.orderDetailDTO.Id))))
                    {
                        ViewBag.ItemNumberError = "Item Number must be unique.";
                        return View(addModel);
                    }


                    addModel.PODetails.Add(addModel.orderDetailDTO);
                    break;
                default:
                    int itemId = int.Parse(new string(method.Where(char.IsDigit).ToArray()));
                    ModelState.Clear();
                    if (method.Contains("Update"))
                    {
                        addModel.PODetails[itemId] = addModel.orderDetailDTO;
                    }
                    else if (method.Contains("Delete"))
                    {
                        addModel.PODetails.Remove(addModel.PODetails[itemId]);
                    }
                    else
                    {
                        addModel.orderDetailDTO = addModel.PODetails[itemId];
                        ViewBag.ItemId = itemId;
                    }
                    break;
            }

            return View(addModel);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                OrderDTO editModel = new OrderDTO();
                //int Id = id ?? default(int);
                editModel = PurchaseOrder.Find(id);
                editModel.orderDetailDTO = new OrderDetailDTO();
                ViewBag.Method = "Edit Order Number: " + editModel.PONumber;
                editModel = SetDropDownList(editModel);
                ViewBag.ItemId = -1;

                return View(editModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(OrderDTO addModel, string method)
        {
            addModel = SetDropDownList(addModel);
            ViewBag.ItemId = -1;

            switch (method)
            {
                case "Apply":
                    if (!(PurchaseOrder.UniquePONum(addModel.PONumber, addModel.Id)))
                    {
                        ViewBag.PONumberError = "PO Number must be unique.";
                        return View(addModel);
                    }

                    if (ModelState.IsValid)
                    {
                        PurchaseOrder.AddOrUpdate(addModel);
                        foreach (var i in addModel.PODetails)
                        {
                            PurchaseOrder.AddOrUpdateItem(i);
                        }

                        return RedirectToAction("Index", new { id = addModel.PONumber, method = 2 });
                    }
                    break;
                case "Save":
                    if (addModel.PODetails == null) { addModel.PODetails = new List<OrderDetailDTO>(); }

                    if (!UniqueItemNumber(addModel.orderDetailDTO.ItemNumber, addModel.PODetails))
                    {
                        if (!(PurchaseOrder.UniqueItemNum(addModel.orderDetailDTO.ItemNumber, addModel.orderDetailDTO.Id)))
                        {
                            ViewBag.ItemNumberError = "Item Number must be unique.";
                            return View(addModel);
                        }
                    }

                    addModel.PODetails.Add(addModel.orderDetailDTO);
                    break;
                default:
                    int itemId = int.Parse(new string(method.Where(char.IsDigit).ToArray()));
                    ModelState.Clear();
                    if (method.Contains("Update"))
                    {
                        addModel.PODetails[itemId] = addModel.orderDetailDTO;
                    }
                    else if (method.Contains("Delete"))
                    {
                        addModel.PODetails.Remove(addModel.PODetails[itemId]);
                    }
                    else
                    {
                        addModel.orderDetailDTO = addModel.PODetails[itemId];
                        ViewBag.ItemId = itemId;
                    }
                    break;
            }

            return View(addModel);
        }

        public ActionResult Copy(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                OrderDTO editModel = new OrderDTO();
                //int Id = id ?? default(int);
                editModel = PurchaseOrder.Find(id);
                editModel.Id = 0;
                ViewBag.Method = "Copy from Order Number: " + editModel.PONumber;

                editModel.PONumber = "";
                foreach (var item in editModel.PODetails) { item.Id = 0; }

                editModel.orderDetailDTO = new OrderDetailDTO();
                editModel = SetDropDownList(editModel);
                ViewBag.ItemId = -1;

                return View(editModel);
            }
        }

        [HttpPost]
        public ActionResult Copy(OrderDTO addModel, string method)
        {
            addModel = SetDropDownList(addModel);
            ViewBag.ItemId = -1;

            switch (method)
            {
                case "Apply":
                    if (!(PurchaseOrder.UniquePONum(addModel.PONumber, addModel.Id)))
                    {
                        ViewBag.PONumberError = "PO Number must be unique.";
                        return View(addModel);
                    }

                    if (ModelState.IsValid)
                    {
                        PurchaseOrder.AddOrUpdate(addModel);
                        foreach (var i in addModel.PODetails)
                        {
                            PurchaseOrder.AddOrUpdateItem(i);
                        }

                        return RedirectToAction("Index", new { id = addModel.PONumber, method = 3 });
                    }
                    break;
                case "Save":
                    if (addModel.PODetails == null) { addModel.PODetails = new List<OrderDetailDTO>(); }

                    if (!UniqueItemNumber(addModel.orderDetailDTO.ItemNumber, addModel.PODetails))
                    {
                        if (!(PurchaseOrder.UniqueItemNum(addModel.orderDetailDTO.ItemNumber, addModel.orderDetailDTO.Id)))
                        {
                            ViewBag.ItemNumberError = "Item Number must be unique.";
                            return View(addModel);
                        }
                    }

                    addModel.PODetails.Add(addModel.orderDetailDTO);
                    break;
                default:
                    int itemId = int.Parse(new string(method.Where(char.IsDigit).ToArray()));
                    ModelState.Clear();
                    if (method.Contains("Update"))
                    {
                        addModel.PODetails[itemId] = addModel.orderDetailDTO;
                    }
                    else if (method.Contains("Delete"))
                    {
                        addModel.PODetails.Remove(addModel.PODetails[itemId]);
                    }
                    else
                    {
                        addModel.orderDetailDTO = addModel.PODetails[itemId];
                        ViewBag.ItemId = itemId;
                    }
                    break;
            }

            return View(addModel);
        }

        private bool UniqueItemNumber(string itemNum, List<OrderDetailDTO> orderDetails)
        {
            foreach (var item in orderDetails)
            {
                if (itemNum == item.ItemNumber) { return false; }
            }

            return true;
        }

        //Set DropDownList to select on View
        private OrderDTO SetDropDownList(OrderDTO addModel)
        {
            OrderDTO init = new OrderDTO();
            init = addModel;

            init.Seasons = GetSelectListItems(SeasonList());
            init.Origins = GetSelectListItems(new List<string> { "HongKong", "Vietnam" });
            init.Ports = GetSelectListItems(new List<string> { "Port 1", "Port 2", "Port 3" });
            init.Modes = GetSelectListItems(new List<string> { "Road", "Sea", "Air" });

            return init;
        }

        //Set list of years for Season field
        private IEnumerable<string> SeasonList()
        {
            List<string> seasonList = new List<string>();
            for (int i = 2010; i <= 2020; i++)
            {
                seasonList.Add(i.ToString());
            }
            return seasonList;
        }

        //Convert string list to SelectListItem list
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

        // GET: PurchaseOrder/Details/5
        /*public ActionResult Details(int? id)
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
