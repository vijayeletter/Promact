using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    
        [Table("EmployeeTable")]
        public class Employee
        {

            public Employee()
            {

            }
            [Key]
            public int ID { get; set; }

            [StringLength(100, ErrorMessage = "Max length 100 characters")]
            [Required(ErrorMessage = "Employee's FirstName is required")]
            public string FirstName { get; set; }

            [StringLength(100, ErrorMessage = "Max length 100 characters")]
            [Required(ErrorMessage = "Employee's Surname is required")]
            public string Surname { get; set; }

            public string Qualification { get; set; }

            //public string City { get; set; }

            public string Address { get; set; }

            [StringLength(10, ErrorMessage = "Max length 10 characters")]
            [Required(ErrorMessage = "Employee's Contact# is required")]
            [Display(Name = "Contact No")]
            public string ContactNo { get; set; }

            [Display(Name = "Department")]
            public int DeptId { get; set; }


            public void Validate()
            {
                if (FirstName == "")
                {
                    throw new Exception("Employee FirstName can not be left blank.");
                }
                if (Surname == "")
                {
                    throw new Exception("Surname can not be left blank.");
                }
                if (ContactNo == null)
                {
                    throw new Exception("Contact Number could not be left blank.");
                }
            }
        }
        [Table("Department")]
        public class Department
        {
            [Key]
            public int DeptId { get; set; }
            
            [Required]
            [StringLength(75, ErrorMessage = "Max length 75 characters")]
            [Display(Name = "Department Name")]
            public string DepartmentName { get; set; }

        }
   
}
