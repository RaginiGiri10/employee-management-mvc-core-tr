using EmployeeMangementApp.Models;
using EmployeeMangementApp.Repository;
using EmployeeMangementApp.ViewModels.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangementApp.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        IEmployeeRepository _employeeRepository;
        IDepartmentRepository _departmentRepository;
        public EmployeeController(IEmployeeRepository employeeRepository,IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAllEmployees();

            List<EmployeeDetailsViewModel> employeeDetailsListViewModel = new List<EmployeeDetailsViewModel>();
            
           foreach(var employee in employees)
            {
                EmployeeDetailsViewModel employeeDetailsViewModel = new EmployeeDetailsViewModel
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LasttName = employee.LasttName,
                    DepartmentName = employee.Department.DepartmentName
                };
                employeeDetailsListViewModel.Add(employeeDetailsViewModel);
            }

            return View(employeeDetailsListViewModel);
        }


        [HttpGet]
        public IActionResult AddEmployee()
        {
            var addEmployeeViewModel = new AddEmployeeViewModel();

            //Returns the List<Department> from the DB.
            var departments = _departmentRepository.GetAllDepartments();
            List<SelectListItem> departmentSelectListItems = new List<SelectListItem>();

            
            foreach(var dept in departments)
            {
                // Creating a list of SelectListItem inorder to bind the data to a select tag in the view.
                departmentSelectListItems.Add(new SelectListItem { Text = dept.DepartmentName, Value = dept.DepartmentId.ToString() });
            }

            //This code will insert a default selected item as --Select Department-- in the UI.          
           departmentSelectListItems.Insert(0, new SelectListItem { Text = "--Select-Department", Value = string.Empty });

            //Binding the List<SelectListItem> in the DepartmentList field.
            addEmployeeViewModel.DepartmentList = departmentSelectListItems;

            return View(addEmployeeViewModel);
        }


        [HttpPost]      
        public IActionResult AddEmployee(AddEmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    FirstName = employeeViewModel.FirstName,
                    LasttName = employeeViewModel.LasttName,
                    DepartmentId = employeeViewModel.DepartmentId
                };

                var addedEmployee = _employeeRepository.AddEmployee(employee);

                return RedirectToAction("Index");
            }

            return View(employeeViewModel);

        }


     
        [HttpGet]
        public IActionResult UpdateEmployee(int id)
        {
            var updateEmployeeViewModel = new UpdateEmployeeViewModel();
            var employeeToBeEdited = _employeeRepository.GetEmployeeById(id);
            
            
            // Code for filling the departments in the dropdown.
            var departments = _departmentRepository.GetAllDepartments();
            List<SelectListItem> departmentSelectListItems = new List<SelectListItem>();
            foreach (var dept in departments)
            {
                departmentSelectListItems.Add(new SelectListItem { Text = dept.DepartmentName, Value = dept.DepartmentId.ToString() });
            }
            departmentSelectListItems.Insert(0, new SelectListItem { Text = "--Select-Department", Value = string.Empty });
            updateEmployeeViewModel.DepartmentList = departmentSelectListItems;


            //Update the updateEmployeeViewModel fields

            updateEmployeeViewModel.Id = employeeToBeEdited.Id;
            updateEmployeeViewModel.FirstName = employeeToBeEdited.FirstName;
            updateEmployeeViewModel.LasttName = employeeToBeEdited.LasttName;
            updateEmployeeViewModel.DepartmentId = employeeToBeEdited.DepartmentId;

            return View(updateEmployeeViewModel);
        }


        [HttpPost]
        public IActionResult UpdateEmployee(int id,UpdateEmployeeViewModel updateEmployeeViewModel)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee
                {
                    FirstName = updateEmployeeViewModel.FirstName,
                    LasttName = updateEmployeeViewModel.LasttName,
                    DepartmentId = updateEmployeeViewModel.DepartmentId
                };
                _employeeRepository.UpdateEmployee(id, employee);
                return RedirectToAction("Index");
            }

            return View(updateEmployeeViewModel);
                
        }


        [HttpGet]
        public IActionResult DeleteEmployee(int id)
        {
            var employeeToBeDeleted = _employeeRepository.GetFullEmployeeDetailsById(id);
            var deleteEmployeeViewModel = new DeleteEmployeeViewModel
            {
                Id = employeeToBeDeleted.Id,
                FirstName = employeeToBeDeleted.FirstName,
                LasttName = employeeToBeDeleted.LasttName,
                DepartmentName = employeeToBeDeleted.Department.DepartmentName
            };
            return View(deleteEmployeeViewModel);
        }


        [HttpPost]
        public IActionResult DeleteEmployee(DeleteEmployeeViewModel deleteEmployeeViewModel)
        {           
            _employeeRepository.RemoveEmployee(deleteEmployeeViewModel.Id);
            return RedirectToAction("Index");
        }
    }
}
