using AssetsManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetsManager.Repository
{
    public class AssetAssignedRepository: IAssetAssignedRepository
    {
        private readonly AssetManagerDBContext _context;

        public AssetAssignedRepository(AssetManagerDBContext context)
        {
            _context = context;
        }
        public async Task<List<AssetsAssignedDetail>> GetAllAssetsAsignedDetailsAsync()
        {
            var records = await _context.AssetsAssignedDetails.Select(x => new AssetsAssignedDetail()
            {
                //reduce code with automapper external package
               AssetsId=x.AssetsId,
               AsstsName=x.AsstsName,
               AssetModel=x.AssetModel,
               AssetsCompanyName=x.AssetsCompanyName,
               AssetsSerialNo=x.AssetsSerialNo,
               AssignedId=x.AssignedId,
               DateOfAssigned=x.DateOfAssigned,
               EmployeeId=x.EmployeeId,
               EmployeeName=x.EmployeeName
            }).ToListAsync();

            return records;
        }
        public async Task<AssetsAssignedDetail> GetAssetAssignedByIdAsync(int asignid)
        {
            var record = await _context.AssetsAssignedDetails.Where(x => x.AssignedId == asignid).Select(x => new AssetsAssignedDetail()
            {
                //reduce code with automapper external package
                AssetsId = x.AssetsId,
                AsstsName = x.AsstsName,
                AssetModel = x.AssetModel,
                AssetsCompanyName = x.AssetsCompanyName,
                AssetsSerialNo = x.AssetsSerialNo,
                AssignedId = x.AssignedId,
                DateOfAssigned = x.DateOfAssigned,
                EmployeeId = x.EmployeeId,
                EmployeeName = x.EmployeeName
            }).FirstOrDefaultAsync();

            return record;

        }
        public async Task<int> AddAssetAssigned(AssetsAssignedDetail assetsAssignedDetail)
        {
            var ass = await _context.AssetsDetails.FindAsync(Convert.ToInt32(assetsAssignedDetail.AsstsName));
            var emp = await _context.EmployeeDetails.FindAsync(Convert.ToInt32(assetsAssignedDetail.EmployeeName));


            var record = new AssetsAssignedDetail()
            {
                AsstsName = ass.AsstsName,
                AssetModel = ass.AssetModel,
                AssetsCompanyName = ass.AssetsCompanyName,
                AssetsSerialNo = ass.AssetsSerialNo,
                AssetsId = ass.AssetsId,
                DateOfAssigned = assetsAssignedDetail.DateOfAssigned,
                EmployeeId = emp.EmployeeId,
                EmployeeName = emp.EmployeeName
            };
            _context.AssetsAssignedDetails.Add(record);
            await UpdateAsignstatusAsync(ass.AssetsId,true);
            await _context.SaveChangesAsync();
            return record.AssignedId;

        }
        public async Task UpdateAssetAsignedAsync(int asignid, AssetsAssignedDetail assetsAssignedDetail)
        {
            var assignassets = await _context.AssetsAssignedDetails.FindAsync(asignid);
            if (assignassets != null)
            {
                assignassets.AsstsName = assetsAssignedDetail.AsstsName;
                assignassets.AssetModel = assetsAssignedDetail.AssetModel;
                assignassets.AssetsCompanyName = assetsAssignedDetail.AssetsCompanyName;
                assignassets.AssetsSerialNo = assetsAssignedDetail.AssetsSerialNo;
                assignassets.AssignedId = assetsAssignedDetail.AssignedId;
                assignassets.DateOfAssigned = assetsAssignedDetail.DateOfAssigned;
                assignassets.EmployeeId = assetsAssignedDetail.EmployeeId;
                assignassets.EmployeeName = assetsAssignedDetail.EmployeeName;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAssetsAssignedAsync(int assignid)
        {
            var record = new AssetsAssignedDetail() { AssignedId = assignid };
            var ass = await _context.AssetsAssignedDetails.FindAsync(assignid);
            //await UpdateAsignstatusAsync(ass.AssetsId);
            await UpdateAsignstatusAsync(ass.AssetsId, false);
            _context.AssetsAssignedDetails.Remove(ass);

            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsignstatusAsync(int assetid,bool stat)
        {
            var ass = await _context.AssetsDetails.FindAsync(assetid);
            if (ass != null)
            {
                ass.AssetAsigned = stat;
                _context.Update(ass);
                await _context.SaveChangesAsync();
            }
            

        }
    }
}
