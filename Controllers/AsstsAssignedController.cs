using AssetsManager.Models;
using AssetsManager.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetsManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AsstsAssignedController : ControllerBase
    {
        private readonly IAssetAssignedRepository _assetAssignedRepository;
        public AsstsAssignedController(IAssetAssignedRepository assetAssignedRepository)
        {
            _assetAssignedRepository = assetAssignedRepository;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllAssetsAsignedDetils()
        {
            var asigned = await _assetAssignedRepository.GetAllAssetsAsignedDetailsAsync();
            return Ok(asigned);
        }

        [HttpGet("{Asignid}")]
        public async Task<IActionResult> GetAssetAssignedById([FromRoute] int Asignid)
        {
            var asigned = await _assetAssignedRepository.GetAssetAssignedByIdAsync(Asignid);
            if (asigned == null)
            {
                return NotFound();
            }
            return Ok(asigned);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewAssetAsigned([FromBody] AssetsAssignedDetail asigned)
        {
            var id = await _assetAssignedRepository.AddAssetAssigned(asigned);
           
            return Ok(await _assetAssignedRepository.GetAssetAssignedByIdAsync(id));
        }

        [HttpPut("{Asignid}")]
        public async Task<IActionResult> UpdateAssetAsignedDetails([FromBody] AssetsAssignedDetail asigned, [FromRoute] int Asignid)
        {
            await _assetAssignedRepository.UpdateAssetAsignedAsync(Asignid, asigned);
            return Ok();
        }

        [HttpDelete("{Asignid}")]
        public async Task<IActionResult> DeleteAssetAsignedDetails([FromRoute] int Asignid)
        {
            await _assetAssignedRepository.DeleteAssetsAssignedAsync(Asignid);

            return Ok();
        }
        [HttpPut("{assetsid}/{stat}")]
        public async Task<IActionResult> Updatestatus([FromRoute] int assetsid,[FromRoute]bool stat)
        {
            await _assetAssignedRepository.UpdateAsignstatusAsync(assetsid,stat);
            
            var asigned = await _assetAssignedRepository.GetAssetAssignedByIdAsync(assetsid);
            if (asigned == null)
            {
                return NotFound();
            }

            return Ok(asigned);
        }
        
    }
}
