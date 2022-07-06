using EmployeeMangementApp.EFDBContext;
using EmployeeMangementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangementApp.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        EmployeeDbContext _employeeDbContext;
        public DepartmentRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public List<Department> GetAllDepartments()
        {
            return _employeeDbContext.Departments.ToList();
        }
    }
}
