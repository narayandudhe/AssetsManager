using AssetsManager.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetsManager.Repository
{
   public interface IAssetDetilsRepository
    {
        Task<List<AssetsDetail>> GetAllAssetsDetailsAsync();
        Task<AssetsDetail> GetAssetByIdAsync(int Assetid);
        Task<int> AddAsset(AssetsDetail assets);
        Task UpdateAssetAsync(int assetid, AssetsDetail assets);
        Task DeleteAssetAsync(int assetid);

        Task<string> AddassetPicAsync(IFormFile file);


    }
}
