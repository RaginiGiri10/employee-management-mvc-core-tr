using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangementApp.ViewModels.Employee
{
    public class EmployeeDetailsViewModel
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
      
        public string LasttName { get; set; }

        public string DepartmentName { get; set; }

    }
}
