using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Mappers;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkMapper _walkMapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IWalkMapper walkMapper, IWalkRepository walkRepository)
        {
            _walkMapper = walkMapper;
            _walkRepository = walkRepository;
        }

        [HttpPost]
        [ValidateModelAttribute]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walkDomaniModel = _walkMapper.MapToDomainModel(addWalkRequestDto);
            await _walkRepository.CreateWalkAsync(walkDomaniModel);
            var walkDto = _walkMapper.MapToDto(walkDomaniModel);

            return Ok(walkDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walkDomainModel = await _walkRepository.GetAllWalksAsync();
            var walkDto = walkDomainModel.Select(_walkMapper.MapToDto).ToList();

            return Ok(walkDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walkDomainModel = await _walkRepository.GetWalkByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            var walkDto = _walkMapper.MapToDto(walkDomainModel);
            return Ok(walkDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModelAttribute]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto) 
        {
            var walkDomainModel = _walkMapper.MapToDomainModel(updateWalkRequestDto);
            walkDomainModel = await _walkRepository.UpdateWalkAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            var walkDto = _walkMapper.MapToDto(walkDomainModel);
            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
           var walkDomainModel = await _walkRepository.DeleteWalkAsync(id);
           if(walkDomainModel == null)
            {
                return NotFound();
            }

           return Ok();
        }
    }
}
