using EmployeeMangementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangementApp.Repository
{
    public interface IDepartmentRepository
    {
        List<Department> GetAllDepartments();
    }
}
