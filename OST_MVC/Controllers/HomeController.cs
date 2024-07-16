using OST_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace OST_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            if (Session["User"] != null)
            {
                List<BaseEquipment> plsData = BaseEquipment.ListEquipmentDate();
                DataTable dtCustEquip = BaseCustomer.ListCustormerEquipment();
                ViewBag.dtCustEquip = dtCustEquip;
                ViewBag.plsData = plsData;
                ViewBag.txtName = "";
                return View();
            }
            else
            {
                return RedirectToAction("Login","Account");
            }
           
        }

        [HttpPost]
        public ActionResult Dashboard(FormCollection frm, string btnSubmit)
        {
            
            if(btnSubmit == "Save Equipment")
            {
                BaseEquipment baseEquipment = new BaseEquipment();
                baseEquipment.Name = frm["ddlEquipmentName"].ToString();
                baseEquipment.EcCount = Convert.ToInt32(frm["txtQuantity"].ToString());
                baseEquipment.EntryDate = Convert.ToDateTime(frm["txtEntryDate"].ToString());

                int returnResult = baseEquipment.SaveEquipment();

                if(returnResult>0) 
                {
                    ViewBag.OperationResult = "Save Successfully";
                }
            }

            if(btnSubmit == "Save Assignment")
            {
                int CustomerID = Convert.ToInt32(frm["ddlpartialCustomer"].ToString());
                int EquipmentID = Convert.ToInt32(frm["ddlpartialEquipment"].ToString());
                int quantity = Convert.ToInt32(frm["txtPartialEquipQuantity"].ToString());

                BaseCustomer.SaveAssignment(CustomerID, EquipmentID, quantity);
                ViewBag.OperationResult = "Save Successfully";
            }

            List<BaseEquipment> plsData = BaseEquipment.ListEquipmentDate();
            ViewBag.plsData = plsData;
            DataTable dtCustEquip = BaseCustomer.ListCustormerEquipment();
            ViewBag.dtCustEquip = dtCustEquip;
            ViewBag.txtName = "";
            if (btnSubmit == "search")
            {
                ViewBag.txtName = frm["txtName"].ToString();
            }
                
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult ListEquipment()
        {
            List<BaseEquipment> plsData = BaseEquipment.ListEquipmentDate();

            var pList = (from p in plsData select new
            {
                EquipmentID = p.EquipmentID,
                Name = p.Name,
                EcCount = p.EcCount.ToString(),
                EntryDate = p.EntryDate.ToString("dd/mm/yyyy"),
            }).ToList();
            return Json(pList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult LstCustomer()
        {
            List<BaseCustomer> plsData = BaseCustomer.ListCustomer();

           
            return Json(plsData, JsonRequestBehavior.AllowGet);
        }
    }
}