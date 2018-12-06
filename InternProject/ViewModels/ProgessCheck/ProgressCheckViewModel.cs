using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternProject.ViewModels.ProgessCheck
{
    public class ProgressCheckViewModel
    {
        public List<string> Oringins { get; set; }
        public List<string> OriginPorts { get; set; }
        public List<string> Suppliers { get; set; }
        public List<string> Factories { get; set; }
        public List<string> Depts { get; set; }
        [StringLength(30)]
        public string Supplier { get; set; }
        [StringLength(30)]
        public string Factory { get; set; }
        [Display(Name = "PO Number")]
        public int PONumber { get; set; }
        [Display(Name = "PO Quantity")]
        public float POQuantity { get; set; }
        [Display(Name = "PO Ship Date")]
        public DateTime ShipDate { get; set; }
        public List<OrderDetailModel> ListOrderDetail { get; set; }
        [Display(Name = "Inspection Date")]
        public DateTime InspectionDate { get; set; }
        [Display(Name = "Int Ship Date")]
        public DateTime IntendedShipDate { get; set; }
        [Display(Name = "PO Quantity Complete ")]
        public bool Complete { get; set; }
        public string Status { get; set; }
        public string Department { get; set; }









    }
}