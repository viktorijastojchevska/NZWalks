using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateWalkRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Description has to be a maximum of 1000 characters")]
        public string Description { get; set; }

        [Range(1, 50)]
        public double LengthInKm { get; set; }
        public string? WalkImageURL { get; set; }

        [Required]
        public Guid RegionID { get; set; }

        [Required]
        public Guid DifficultyID { get; set; }
    }
}
