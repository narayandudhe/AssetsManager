using AssetsManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;

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
                EmployeeName=x.EmployeeName,
                EmployeeProfilePicUrl =x.EmployeeProfilePicUrl,
                EmployeeSalary = x.EmployeeSalary,
                DateOfBirth = x.DateOfBirth,
                DateOfJoining = x.DateOfJoining,
                EmployeeEmailId = x.EmployeeEmailId,
                EmployeeMobileNo = x.EmployeeMobileNo
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
                EmployeeName=x.EmployeeName,
                EmployeeProfilePicUrl=x.EmployeeProfilePicUrl,
                EmployeeSalary = x.EmployeeSalary,
                DateOfBirth = x.DateOfBirth,
                DateOfJoining = x.DateOfJoining,
                EmployeeEmailId = x.EmployeeEmailId,
                EmployeeMobileNo = x.EmployeeMobileNo
            }).FirstOrDefaultAsync();
            
            return record;
        
        }
        public async Task<int> AddEmployee(EmployeeDetail employee)
        {
            var record = new EmployeeDetail() 
            {
                EmployeeName=employee.EmployeeName,
                EmployeeDepName=employee.EmployeeDepName,
                EmployeeAddress=employee.EmployeeAddress,
                EmployeeProfilePicUrl = employee.EmployeeProfilePicUrl,
                EmployeeSalary = employee.EmployeeSalary,
                DateOfBirth = employee.DateOfBirth,
                DateOfJoining = employee.DateOfJoining,
                EmployeeEmailId = employee.EmployeeEmailId,
                EmployeeMobileNo = employee.EmployeeMobileNo

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
                emp.EmployeeProfilePicUrl = employee.EmployeeProfilePicUrl;
                emp.EmployeeSalary = employee.EmployeeSalary;
                emp.DateOfBirth = employee.DateOfBirth;
                emp.DateOfJoining = employee.DateOfJoining;
                emp.EmployeeEmailId = employee.EmployeeEmailId;
                emp.EmployeeMobileNo = employee.EmployeeMobileNo;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteEmployeeAsync(int Employeeid)
        {
            var rec=_context.EmployeeDetails.Where(e=>e.EmployeeId==Employeeid).FirstOrDefault();
            var pathh = Path.Combine(Directory.GetCurrentDirectory(), @"Pictures/EmployeePic", rec.EmployeeProfilePicUrl);

            if (File.Exists(pathh))
            {
                File.Delete(pathh);
            }
            _context.EmployeeDetails.Remove(rec);
            await _context.SaveChangesAsync ();
        }

        public async Task<string> AddempPicAsync(IFormFile file)
        {
            string fileName;
            try
            {
                var ext = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks + ext;
          
                var pathh = Path.Combine(Directory.GetCurrentDirectory(), @"Pictures/EmployeePic", fileName);
                using (var stream = new FileStream(pathh, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return fileName;
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }
      
    }
}
