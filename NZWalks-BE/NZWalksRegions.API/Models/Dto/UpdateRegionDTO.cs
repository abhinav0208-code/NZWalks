using System.ComponentModel.DataAnnotations;

namespace NZWalksRegions.API.Models.DTO
{
    public class UpdateRegionDTO
    {
        [Required]
        [MaxLength(3,ErrorMessage =" Length must be maximum of 3")]
        [MinLength(3)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
