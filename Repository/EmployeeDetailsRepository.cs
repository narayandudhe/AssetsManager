using AssetsManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetsManager.Repository
{
    public class EmployeeDetailsRepository: IEmployeeDetailsRepository
    {
        private readonly AssetManagerDBContext _context;

        public EmployeeDetailsRepository(AssetManagerDBContext context)
        {
            _context = context;
        }
        public async Task<List<EmployeeDetail>> GetAllEmployeeDetailsAsync()
        {
            var records = await _context.EmployeeDetails.Select(x=>new EmployeeDetail() 
            {
                //reduce code with automapper external package
                EmployeeId=x.EmployeeId,
                EmployeeAddress=x.EmployeeAddress,
                EmployeeDepName=x.EmployeeDepName,
                EmployeeName=x.EmployeeName
            }).ToListAsync();

            return records;
        }
        public async Task<EmployeeDetail> GetEmployeeByIdAsync( int Employeeid) 
        {
            var record = await _context.EmployeeDetails.Where(x => x.EmployeeId == Employeeid).Select(x=>new EmployeeDetail() 
            {
                //reduce code with automapper external package
                EmployeeId = x.EmployeeId,
                EmployeeAddress=x.EmployeeAddress,
                EmployeeDepName=x.EmployeeDepName,
                EmployeeName=x.EmployeeName
            }).FirstOrDefaultAsync();
            
            return record;
        
        }
        public async Task<int> AddEmployee(EmployeeDetail employee)
        {
            var record = new EmployeeDetail() 
            {
                EmployeeName=employee.EmployeeName,
                EmployeeDepName=employee.EmployeeDepName,
                EmployeeAddress=employee.EmployeeAddress
            };
            _context.EmployeeDetails.Add(record);
            await _context.SaveChangesAsync();
            return record.EmployeeId;

        }
        public async Task UpdateEmployeeAsync(int employeeid,EmployeeDetail employee)
        {
            var emp = await _context.EmployeeDetails.FindAsync(employeeid);
            if (emp != null)
            {
                emp.EmployeeAddress = employee.EmployeeAddress;
                emp.EmployeeDepName = employee.EmployeeDepName;
                emp.EmployeeName = employee.EmployeeName;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteEmployeeAsync(int Employeeid)
        {
            var record = new EmployeeDetail() { EmployeeId=Employeeid};
             _context.EmployeeDetails.Remove(record);
            await _context.SaveChangesAsync ();
        }
    }
}
