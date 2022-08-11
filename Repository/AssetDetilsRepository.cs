using AssetsManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
                AsstsName=x.AsstsName,
                AssetImageUrl = x.AssetImageUrl,
                AssetPurchaseDate = x.AssetPurchaseDate,
                AssetsWarrenty = x.AssetsWarrenty,
                AssetPrice = x.AssetPrice
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
                AsstsName=x.AsstsName,
                AssetImageUrl = x.AssetImageUrl,
                AssetPurchaseDate = x.AssetPurchaseDate,
                AssetsWarrenty = x.AssetsWarrenty,
                AssetPrice = x.AssetPrice
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
                AsstsName = assets.AsstsName,
                AssetImageUrl = assets.AssetImageUrl,
                AssetPurchaseDate = assets.AssetPurchaseDate,
                AssetsWarrenty = assets.AssetsWarrenty,
                AssetPrice = assets.AssetPrice
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
                ass.AssetImageUrl = assets.AssetImageUrl;
                ass.AssetPurchaseDate = assets.AssetPurchaseDate;
                ass.AssetsWarrenty = assets.AssetsWarrenty;
                ass.AssetPrice = assets.AssetPrice;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAssetAsync(int assetid)
        {
            var record = new AssetsDetail() { AssetsId = assetid };
            _context.AssetsDetails.Remove(record);
            await _context.SaveChangesAsync();
        }


        public async Task<string> AddassetPicAsync(IFormFile file)
        {
            string fileName;
            try
            {
                var ext = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks + ext;
                var pathbuilt = Path.Combine(Directory.GetCurrentDirectory(), "AssetPic");
                if (!Directory.Exists(pathbuilt))
                {
                    Directory.CreateDirectory(pathbuilt);
                }
                var pathh = Path.Combine(Directory.GetCurrentDirectory(), "AssetPic", fileName);
                using (var stream = new FileStream(pathh, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return "~/AssetPic/"+fileName;
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }



    }
}
