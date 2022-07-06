using EmployeeMangementApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeMangementApp.ViewModels.Employee
{
    public class AddEmployeeViewModel
    {
        [Required(ErrorMessage ="First Name is mandatory")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is mandatory")]
        public string LasttName { get; set; }

        [Required(ErrorMessage="Department is mandatory")]       
        public int DepartmentId { get; set; }

        public List<SelectListItem> DepartmentList { get; set; }
    }
}
