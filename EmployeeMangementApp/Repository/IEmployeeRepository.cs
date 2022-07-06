using EmployeeMangementApp.Models;
using System.Collections.Generic;

namespace EmployeeMangementApp.Repository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();

        Employee AddEmployee(Employee employee);

        Employee GetEmployeeById(int id);

        Employee GetFullEmployeeDetailsById(int id);

        void UpdateEmployee(int id, Employee employee);

        void RemoveEmployee(int id);
    }
}
