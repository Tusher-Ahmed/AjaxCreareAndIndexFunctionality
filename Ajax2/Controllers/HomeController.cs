using Ajax2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ajax2.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeDbEntities _db;
        public HomeController()
        {
            _db = new EmployeeDbEntities();  
        }
        public ActionResult Index()
        {
            var data=_db.Employees.OrderByDescending(u=>u.Id).ToList();
            return View(data);
        }
        [HttpPost]
        public ActionResult Index(string q)
        {
            if (string.IsNullOrEmpty(q)==false)
            {
                //var emp=_db.Employees.Where(m=>m.Name.Contains(q)).ToList();
                var emp = _db.Employees.Where(m => m.Name.StartsWith(q)).ToList();
                return PartialView("_SearchItem", emp);
            }
            else
            { 

                var data = _db.Employees.ToList();
                return PartialView("_SearchItem", data);
            }

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if(ModelState.IsValid==true)
            {
                _db.Employees.Add(employee);
                int a = _db.SaveChanges();
                if (a > 0)
                {
                    return JavaScript("alert('Data is inserted!')");
                }
                else
                {
                    return Json("Data is not inserted!");
                }
            }
            return View();
        }
    }
}