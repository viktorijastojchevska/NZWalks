using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappers
{
    public class ImageMapper : IImageMapper
    {
        public Image MapDtoToDomainModel(ImageUploadRequestDto imageUploadRequestDto)
        {
            return new Image
            {
                File = imageUploadRequestDto.File,
                FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName),
                FileSizeInBytes = imageUploadRequestDto.File.Length,
                FileName = imageUploadRequestDto.FileName,
                FileDescription = imageUploadRequestDto.FileDescription
            };
        }
    }
}
