using AssetsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetsManager.Repository
{
   public interface IEmployeeDetailsRepository
    {
        Task<List<EmployeeDetail>> GetAllEmployeeDetailsAsync();
        Task<EmployeeDetail> GetEmployeeByIdAsync(int Employeeid);
        Task<int> AddEmployee(EmployeeDetail employee);
        Task UpdateEmployeeAsync(int employeeid, EmployeeDetail employee);
        Task DeleteEmployeeAsync(int Employeeid);
    }
}
