using AssetsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetsManager.Repository
{
   public interface IAssetAssignedRepository
    {
        Task<List<AssetsAssignedDetail>> GetAllAssetsAsignedDetailsAsync();
        Task<AssetsAssignedDetail> GetAssetAssignedByIdAsync(int asignid);
        Task<int> AddAssetAssigned(AssetsAssignedDetail assetsAssignedDetail);
        Task UpdateAssetAsignedAsync(int asignid, AssetsAssignedDetail assetsAssignedDetail);
        Task DeleteAssetsAssignedAsync(int assignid);
        Task UpdateAsignstatusAsync(int assetid,bool stat);

    }
}
