using AssetsManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetsManager.Repository
{
    public class AssetDetilsRepository: IAssetDetilsRepository
    {
        private readonly AssetManagerDBContext _context;

        public AssetDetilsRepository(AssetManagerDBContext context)
        {
            _context = context;
        }
        public async Task<List<AssetsDetail>> GetAllAssetsDetailsAsync()
        {
            var records = await _context.AssetsDetails.Select(x => new AssetsDetail()
            {
                //reduce code with automapper external package
                AssetsId = x.AssetsId,
                AssetAsigned = x.AssetAsigned,
                AssetModel = x.AssetModel,
                AssetsCompanyName = x.AssetsCompanyName,
                AssetsSerialNo=x.AssetsSerialNo,
                AsstsName=x.AsstsName
            }).ToListAsync();

            return records;
        }
        public async Task<AssetsDetail> GetAssetByIdAsync(int Assetid)
        {
            var record = await _context.AssetsDetails.Where(x => x.AssetsId == Assetid).Select(x => new AssetsDetail()
            {
                //reduce code with automapper external package
              AssetsId = x.AssetsId,
                AssetAsigned = x.AssetAsigned,
                AssetModel = x.AssetModel,
                AssetsCompanyName = x.AssetsCompanyName,
                AssetsSerialNo=x.AssetsSerialNo,
                AsstsName=x.AsstsName
            }).FirstOrDefaultAsync();

            return record;

        }
        public async Task<int> AddAsset(AssetsDetail assets)
        {
            var record = new AssetsDetail()
            {
                AssetAsigned = assets.AssetAsigned,
                AssetModel = assets.AssetModel,
                AssetsCompanyName = assets.AssetsCompanyName,
                AssetsSerialNo = assets.AssetsSerialNo,
                AsstsName = assets.AsstsName
            };
            _context.AssetsDetails.Add(record);
            await _context.SaveChangesAsync();
            return record.AssetsId;

        }
        public async Task UpdateAssetAsync(int assetid, AssetsDetail assets)
        {
            var ass = await _context.AssetsDetails.FindAsync(assetid);
            if (ass != null)
            {
                ass.AssetAsigned = assets.AssetAsigned;
                ass.AssetModel = assets.AssetModel;
                ass.AssetsCompanyName = assets.AssetsCompanyName;
                ass.AssetsSerialNo = assets.AssetsSerialNo;
                ass.AsstsName = assets.AsstsName;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAssetAsync(int assetid)
        {
            var record = new AssetsDetail() { AssetsId = assetid };
            _context.AssetsDetails.Remove(record);
            await _context.SaveChangesAsync();
        }

        
    }
}
