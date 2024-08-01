
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.CustomActionFilters
{
    public class ValidateImageFile : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("imageUploadRequestDto", out var obj) && obj is ImageUploadRequestDto imageUploadRequestDto)
            {
                var allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };

                if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName).ToLower()))
                {
                    context.ModelState.AddModelError("file", "Unsupported file extension");
                }

                if (imageUploadRequestDto.File.Length > 10485760)
                {
                    context.ModelState.AddModelError("file", "File size more than 10MB. Please upload a smaller size file.");
                }
            }
        }
    }
}
