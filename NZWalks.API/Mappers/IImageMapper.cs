using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappers
{
    public interface IImageMapper
    {
        Image MapDtoToDomainModel(ImageUploadRequestDto imageUploadRequestDto);
    }
}
