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
    public class ImagesController : ControllerBase
    {

        private readonly IImageMapper _imageMapper;
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageMapper imageMapper, IImageRepository imageRepository)
        {
            _imageMapper = imageMapper;
            _imageRepository = imageRepository;
        }


        [HttpPost]
        [Route("Upload")]
        [ValidateImageFile]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            if (ModelState.IsValid)
            {
                var imageDomainModel = _imageMapper.MapDtoToDomainModel(imageUploadRequestDto);
                await _imageRepository.UploadImage(imageDomainModel);

                return Ok(imageDomainModel);
            }

            return BadRequest(ModelState);
        }
    }
}
