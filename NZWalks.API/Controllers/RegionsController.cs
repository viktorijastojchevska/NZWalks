using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWaksDbContext _dbContext;
        public RegionsController(NZWaksDbContext nZWaksDbContext)
        { 
            _dbContext = nZWaksDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GettAllRegions()
        {
            var regionsDomainModel = await _dbContext.Regions.ToListAsync();

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
            var regionDoman = await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

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

            await _dbContext.Regions.AddAsync(regionDomanModel);
             await _dbContext.SaveChangesAsync();

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
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateregionRequestDto updateregionRequestDto)
        {
            var regionDomainmodel = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainmodel == null)
            {
                return NotFound();
            }
            
            regionDomainmodel.Code = updateregionRequestDto.Code;
            regionDomainmodel.Name = updateregionRequestDto.Name;
            regionDomainmodel.RegionImageURL = updateregionRequestDto.RegionImageURL;

            await _dbContext.SaveChangesAsync();

            var regionDto = new RegionDto()
            {
                Id = regionDomainmodel.Id,
                Code = regionDomainmodel.Code,
                Name = regionDomainmodel.Name,
                RegionImageURL = regionDomainmodel.RegionImageURL
            };

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        { 
            var regionDomainModel = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainModel == null) 
            {
                return NotFound();
            }

            _dbContext.Regions.Remove(regionDomainModel);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
