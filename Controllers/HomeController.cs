using CRUD_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            EmployeeDBContext db = new EmployeeDBContext();
            List<Employee> obj = db.GetEmployees();
            return View(obj);
        }

        [HttpPost]
        public ActionResult Add(Employee emp)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    EmployeeDBContext context = new EmployeeDBContext();
                    bool check = context.AddEmployee(emp);
                    if (check == true)
                    {
                        TempData["Insert Message"] = "Data has been inserted successfully";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addEmployee(Employee emp)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    EmployeeDBContext context = new EmployeeDBContext();
                    bool check = context.AddEmployee(emp);
                    if (check == true)
                    {
                        TempData["Insert Message"] = "Data has been inserted successfully";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult getEmployeeData(int id)
        { 
            EmployeeDBContext context = new EmployeeDBContext();
            var row = context.GetEmployees().Find(model => model.Id == id);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult editEmployeeData(int Id, Employee emp)
        {
            if (ModelState.IsValid == true)
            {
                EmployeeDBContext context = new EmployeeDBContext();
                bool check = context.UpdateEmployee(emp);
                if (check == true)
                {
                    TempData["Update Message"] = "Data has been updated successfully";
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }


        public ActionResult removeEmployeeData(int id)
        {
            EmployeeDBContext context = new EmployeeDBContext();
            var row = context.GetEmployees().Find(model => model.Id == id);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Remove(int id, Employee emp)
        {
            EmployeeDBContext context = new EmployeeDBContext();
            bool check = context.DeleteEmployee(id);
            if (check == true) 
            {
                TempData["Delete Message"] = "Data has been deleted successfully";
            }
            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public ActionResult View(int id)
        {
            EmployeeDBContext context = new EmployeeDBContext();
            var row = context.GetEmployees().Find(model => model.Id == id);
            return Json(row, JsonRequestBehavior.AllowGet);
        }
    }
}