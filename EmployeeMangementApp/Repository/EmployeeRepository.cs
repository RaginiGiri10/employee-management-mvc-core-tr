using EmployeeMangementApp.EFDBContext;
using EmployeeMangementApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangementApp.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        EmployeeDbContext _employeeDbContext;
        public EmployeeRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public Employee AddEmployee(Employee employee)
        {
            _employeeDbContext.Employees.Add(employee);
            _employeeDbContext.SaveChanges();
            return employee;
        }

        public List<Employee> GetAllEmployees()
        {
           
            return _employeeDbContext.Employees.Include(d => d.Department).ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            var existingEmployee = _employeeDbContext.Employees.FirstOrDefault(e => e.Id == id);
            return existingEmployee;
        }

        public Employee GetFullEmployeeDetailsById(int id)
        {
            var existingEmployee = _employeeDbContext.Employees.Include(d=>d.Department).FirstOrDefault(e => e.Id == id);
            return existingEmployee;
        }

        public void RemoveEmployee(int id)
        {
            var existingEmployee = _employeeDbContext.Employees.FirstOrDefault(e => e.Id == id);
            if (existingEmployee == null)
            {
                throw new Exception($"Employee with Id ={id} is not found for deletion");
            }
            _employeeDbContext.Employees.Remove(existingEmployee);
            _employeeDbContext.SaveChanges();
        }

        public void UpdateEmployee(int id, Employee employee)
        {
            var existingEmployee = _employeeDbContext.Employees.FirstOrDefault(e => e.Id == id);
            if (existingEmployee != null)
            {
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LasttName = employee.LasttName;
                existingEmployee.DepartmentId = employee.DepartmentId;
                _employeeDbContext.SaveChanges();
            }
            throw new Exception($"Employee with Id ={id} is not found for updation");
        }
    }
}
