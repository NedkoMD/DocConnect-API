using DocConnect.Business.Models.Utilities;
using System.ComponentModel.DataAnnotations;

namespace DocConnect.Business.Models.DTOs.Speciality
{
    public class SpecialityAddDTO
    {
        [Required]
        [MaxLength(SpecialityDTOConstrains.SpecialityNameMaxLength)]
        public string Name { get; set; }

        [Url]
        [Required]
        [MaxLength(SpecialityDTOConstrains.SpecialityImageUrlMaxLength)]
        public string ImageUrl { get; set; }
    }
}
