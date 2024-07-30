using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappers
{
    public class WalkMapper : IWalkMapper
    {
        public Walk MapToDomainModel(AddWalkRequestDto addWalkRequestDto)
        {
            return new Walk
            {
                Name = addWalkRequestDto.Name,
                Description = addWalkRequestDto.Description,
                LengthInKm = addWalkRequestDto.LengthInKm,
                RegionID = addWalkRequestDto.RegionID,
                DifficultyID = addWalkRequestDto.DifficultyID,
            };
        }

        public Walk MapToDomainModel(UpdateWalkRequestDto updateWalkRequestDto)
        {
            return new Walk
            {
                Name = updateWalkRequestDto.Name,
                Description = updateWalkRequestDto.Description,
                LengthInKm = updateWalkRequestDto.LengthInKm,
                RegionID = updateWalkRequestDto.RegionID,
                DifficultyID = updateWalkRequestDto.DifficultyID,
            };
        }

        public WalkDto MapToDto(Walk walk)
        {
            return new WalkDto 
            {
                Id = walk.Id,
                Name = walk.Name,
                Description = walk.Description,
                LengthInKm = walk.LengthInKm,
                Region = new RegionDto
                {
                    Id = walk.Region.Id,
                    Code = walk.Region.Code,
                    Name = walk.Region.Name,
                    RegionImageURL = walk.Region.RegionImageURL,
                },
                Difficulty = new DifficultyDto
                {
                    Id = walk.Difficulty.Id,
                    Name = walk.Difficulty.Name,
                }
            };
        }
    }
}
