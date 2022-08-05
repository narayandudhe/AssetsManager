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
    public class AsstesController : ControllerBase
    {
        private readonly IAssetDetilsRepository _assetsDetilsRepository;
        public AsstesController(IAssetDetilsRepository assetsDetilsRepository)
        {
            _assetsDetilsRepository = assetsDetilsRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAssetsDetils()
        {
            var assets = await _assetsDetilsRepository.GetAllAssetsDetailsAsync();
            return Ok(assets);
        }

        [HttpGet("{Assetid}")]
        public async Task<IActionResult> GetAssetById([FromRoute] int Assetid)
        {
            var asset = await _assetsDetilsRepository.GetAssetByIdAsync(Assetid);
            if (asset == null)
            {
                return NotFound();
            }
            return Ok(asset);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewAsset([FromBody] AssetsDetail assets)
        {
            var id = await _assetsDetilsRepository.AddAsset(assets);
            assets.AssetsId = id;
            return Ok(assets);
        }

        [HttpPut("{Assetid}")]
        public async Task<IActionResult> UpdateAssetDetails([FromBody] AssetsDetail assets, [FromRoute] int Assetid)
        {
            await _assetsDetilsRepository.UpdateAssetAsync(Assetid, assets);
            return Ok();
        }

        [HttpDelete("{Assetid}")]
        public async Task<IActionResult> DeleteEmployeeDetails([FromRoute] int Assetid)
        {
            await _assetsDetilsRepository.DeleteAssetAsync(Assetid);
            return Ok();
        }
    }
}
