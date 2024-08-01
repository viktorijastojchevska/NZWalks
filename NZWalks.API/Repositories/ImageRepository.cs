using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly NZWalksDbContext _dbContext;

        public ImageRepository(IWebHostEnvironment environment, IHttpContextAccessor contextAccessor, NZWalksDbContext nZWalksDbContext)
        {
            _environment = environment;
            _contextAccessor = contextAccessor;
            _dbContext = nZWalksDbContext;
        }

        public async Task<Image> UploadImage(Image image)
        {
            var localFilePath = Path.Combine(_environment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}{_contextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            await _dbContext.AddAsync(image);
            await _dbContext.SaveChangesAsync();
            return image;
        }
    }
}
