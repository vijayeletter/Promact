using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using Promact.Models;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using System.Configuration;
using DAL;
using BAL;
namespace Promact.Controllers
{
    [RequestResponseLog]
    public class HomeController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            //Log the error!!       

            

            //Redirect or return a view, but not both.
            filterContext.Result = RedirectToAction("Index", "ErrorHandler");
            // OR 
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/ErrorHandler/Index.cshtml"
            };

            base.OnException(filterContext);
        }
        
       
        public ActionResult CreateDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDepartment(Department newDept)
        {
            if (ModelState.IsValid)
            {
                BusinessLayer bl = new BusinessLayer();
                bl.InsertDepartment(newDept);
                return RedirectToAction("Department");
            }
            else
            {
                return View(newDept);
            }
        }


        

        

        [RequestResponseLog]
        public ActionResult Department()
        {
            return View();
        }
        [RequestResponseLog]
        public ActionResult LoadDepartments()
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (DatabaseContext _context = new DatabaseContext())
                {
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                    //Paging Size (10,20,50,100)    
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;


                    var deptData = (from tempdept in _context.DbDepartment
                                    select tempdept);

                    //Sorting    
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        deptData = deptData.OrderBy(sortColumn + " " + sortColumnDir);
                    }
                    //Search    
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        deptData = deptData.Where(m => m.DepartmentName.Contains(searchValue));
                    }

                    //total number of rows count     
                    recordsTotal = deptData.Count();
                    //Paging     
                    var data = deptData.Skip(skip).Take(pageSize).ToList();
                    //Returning Json Data    
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        [RequestResponseLog]
        public ActionResult EditDepartment(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Department", "Home");
            }
            else
            {
                BusinessLayer bl = new BusinessLayer();
                return View(bl.GetDepartment(id));
            }
        }

        [HttpPost]
        [RequestResponseLog]
        public ActionResult EditDepartment(Department oObj)
        {
            try
            {
                BusinessLayer bl = new BusinessLayer();
                bl.UpdateDepartment(oObj);
                return RedirectToAction("Department", "Home");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        [RequestResponseLog]
        public ActionResult Index()
        {

            return View();

        }
        public ActionResult Create()
        {
            List<Department> depList = new List<Department>();
            BusinessLayer bl = new BusinessLayer();
            depList = bl.GetDepartments();
            ViewBag.Department = depList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee newEmp)
        {
            if (ModelState.IsValid)
            {
                BusinessLayer bl = new BusinessLayer();
                bl.InsertEmployee(newEmp);
                return RedirectToAction("Index");
            }
            else
            {
                return View(newEmp);
            }
        }

        [HttpGet]
        [RequestResponseLog]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
               return RedirectToAction("Index", "Home");
            }
              else
            {
                List<Department> depList = new List<Department>();
                BusinessLayer bl = new BusinessLayer();
                depList = bl.GetDepartments();                
                ViewBag.Department = depList;
                
                return View(bl.GetEmployee(id));
            }
            
            
        }
        [HttpPost]
        [RequestResponseLog]
        public ActionResult Edit(Employee oObj)
        {
            try
            {
                BusinessLayer bl = new BusinessLayer();
                bl.UpdateEmployee(oObj);
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        [RequestResponseLog]
        public ActionResult LoadData()
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (DatabaseContext _context = new DatabaseContext())
                {
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                    //Paging Size (10,20,50,100)    
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    var EmplData = (from tempcustomer in _context.DbEmployee
                                    join cs in _context.DbDepartment on tempcustomer.DeptId equals cs.DeptId
                                    select new
                                    {
                                        ID = tempcustomer.ID,
                                        FirstName = tempcustomer.FirstName,
                                        Surname = tempcustomer.Surname,
                                        Address = tempcustomer.Address,
                                        ContactNo = tempcustomer.ContactNo,
                                        DepartmentName = cs.DepartmentName,
                                        Qualification = tempcustomer.Qualification,
                                    });

                    //var EmplData = (from tempcustomer in _context.DbEmployee
                    //                select tempcustomer);

                    //Sorting    
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        EmplData = EmplData.OrderBy(sortColumn + " " + sortColumnDir);
                    }
                    //Search    
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        EmplData = EmplData.Where(m => m.FirstName.Contains(searchValue) || m.Surname.Contains(searchValue));
                    }

                    //total number of rows count     
                    recordsTotal = EmplData.Count();
                    //Paging     
                    var data = EmplData.Skip(skip).Take(pageSize).ToList();
                    //Returning Json Data    
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [RequestResponseLog]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [RequestResponseLog]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}