using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
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
        public RegionsController(IRegionRepository regionRepository)
        { 
            _regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GettAllRegions()
        {
            var regionsDomainModel = await _regionRepository.GetAllRegionsAsync();

            var regions = new List<RegionDto>();

            foreach( var regionDomain in regionsDomainModel)
            {
                regions.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageURL = regionDomain.RegionImageURL
                });
            }
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

            var region = new RegionDto()
            {
                Id = regionDoman.Id,
                Code = regionDoman.Code,
                Name = regionDoman.Name,
                RegionImageURL = regionDoman.RegionImageURL
            };
            return Ok(region);
        }

        [HttpPost]
        public async Task<IActionResult> GreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomanModel = new Region()
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageURL = addRegionRequestDto.RegionImageURL
            };
            
            regionDomanModel = await _regionRepository.CreateRegionAsync(regionDomanModel);

            var regionDto = new RegionDto()
            {
                Id = regionDomanModel.Id,
                Code = regionDomanModel.Code,
                Name = regionDomanModel.Name,
                RegionImageURL = regionDomanModel.RegionImageURL
            };

            return CreatedAtAction(nameof(GetRegionById), new {id = regionDto.Id}, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateregionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = new Region()
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageURL = updateRegionRequestDto.RegionImageURL
            };
            regionDomainModel = await _regionRepository.UpdateRegionAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            
            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageURL = regionDomainModel.RegionImageURL
            };

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
