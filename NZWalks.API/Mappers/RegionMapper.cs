using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappers
{
    public class RegionMapper : IRegionMapper
    {
        public RegionDto MapToDto(Region region)
        {
            return new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageURL = region.RegionImageURL
            };
        }

        public Region MapToDomainModel(AddRegionRequestDto addRegionRequestDto)
        {
            return new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageURL = addRegionRequestDto.RegionImageURL
            };
        }

        public Region MapToDomainModel(UpdateregionRequestDto updateRegionRequestDto)
        {
            return new Region
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageURL = updateRegionRequestDto.RegionImageURL
            };
        }
    }
}
