using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Mappers;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IRegionMapper _regionMapper;
        public RegionsController(IRegionRepository regionRepository, IRegionMapper regionMapper)
        { 
            _regionRepository = regionRepository;
            _regionMapper = regionMapper;
        }

        [HttpGet]
        public async Task<IActionResult> GettAllRegions()
        {
            var regionsDomainModel = await _regionRepository.GetAllRegionsAsync();

            var regions = regionsDomainModel.Select(_regionMapper.MapToDto).ToList();

            return Ok(regions);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        { 
            var regionDoman = await _regionRepository.GetRegionByIdAsync(id);

            if (regionDoman == null)
            { 
                return NotFound();
            }

            var region = _regionMapper.MapToDto(regionDoman);
            return Ok(region);
        }

        [HttpPost]
        public async Task<IActionResult> GreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomanModel = _regionMapper.MapToDomainModel(addRegionRequestDto);
            
            regionDomanModel = await _regionRepository.CreateRegionAsync(regionDomanModel);

            var regionDto = _regionMapper.MapToDto(regionDomanModel);

            return CreatedAtAction(nameof(GetRegionById), new {id = regionDto.Id}, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = _regionMapper.MapToDomainModel(updateRegionRequestDto);
            regionDomainModel = await _regionRepository.UpdateRegionAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            
            var regionDto = _regionMapper.MapToDto(regionDomainModel);

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        { 
            var regionDomainModel = await _regionRepository.DeleteRegionAsync(id);
            if (regionDomainModel == null) 
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
