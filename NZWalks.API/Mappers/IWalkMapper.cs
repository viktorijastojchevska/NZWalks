using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappers
{
    public interface IWalkMapper
    {
        Walk MapToDomainModel(AddWalkRequestDto addWalkRequestDto);
        WalkDto MapToDto(Walk walk);
        Walk MapToDomainModel(UpdateWalkRequestDto updateWalkRequestDto);
    }
}
