using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BAL
{
    
    public interface IEmployee
    {
        List<Department> GetDepartments();
        Department GetDepartment(int? id);
        bool InsertDepartment(Department dept);
        void UpdateDepartment(Department dept);

        Employee GetEmployee(int? id);      
        bool InsertEmployee(Employee emp);
        void UpdateEmployee(Employee emp);
    }

    public class BusinessLayer : IEmployee
    {
        DatabaseContext _context;
        public List<Department> GetDepartments()
        {

            try
            {
                _context = new DatabaseContext();
                return _context.DbDepartment.ToList();               

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Department GetDepartment(int? id)
        {

            try
            {
                _context = new DatabaseContext();
                Department d = _context.DbDepartment.Where(a => a.DeptId == id).FirstOrDefault();
                return d;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertDepartment(Department dept)
        {
            try
            {
                _context = new DatabaseContext();
                _context.DbDepartment.Add(dept);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateDepartment(Department dept)
        {
            try
            {
                _context = new DatabaseContext();
                Department depData = _context.DbDepartment.Where(a => a.DeptId == dept.DeptId).FirstOrDefault();
                if (depData != null)
                {
                    depData.DepartmentName = dept.DepartmentName;
                    _context.Entry(depData).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public Employee GetEmployee(int? id)
        {

            try
            {
                _context = new DatabaseContext();
                Employee s = _context.DbEmployee.Where(a => a.ID == id).FirstOrDefault();
                return s;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        public bool InsertEmployee(Employee emp)
        {
            try
            {
                _context = new DatabaseContext();
                _context.DbEmployee.Add(emp);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateEmployee(Employee emp)
        {
            try
            {
                _context = new DatabaseContext();                
                Employee empData = _context.DbEmployee.Where(a => a.ID == emp.ID).FirstOrDefault();
                if (empData != null)
                {
                    empData.FirstName = emp.FirstName;
                    empData.Surname = emp.Surname;
                    empData.Address = emp.Address;
                    empData.ContactNo = emp.ContactNo;
                    empData.DeptId = emp.DeptId;
                    empData.Qualification = emp.Qualification;
                    _context.Entry(empData).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                }               
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
