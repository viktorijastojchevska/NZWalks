using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappers
{
    public interface IRegionMapper
    {
        RegionDto MapToDto(Region region);
        Region MapToDomainModel(AddRegionRequestDto addRegionRequestDto);
        Region MapToDomainModel(UpdateRegionRequestDto updateRegionRequestDto);
    }
}
