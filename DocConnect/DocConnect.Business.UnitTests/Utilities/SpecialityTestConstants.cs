using DocConnect.Business.Models.DTOs.Speciality;
using DocConnect.Data.Models.Entities;

namespace DocConnect.Business.UnitTests.Utilities
{
    public static class SpecialityTestConstants
    {
        public const uint TestExistingSpecialityId = 1;
        public const uint TestNonExistingSpecialityId = 2;

        public const string TestSpecialityName = "Cardiology";
        public const string TestSpecialityImageURL = "https://images.pexels.com/photos/7659564/pexels-photo-7659564.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1";

        public static readonly Speciality TestExistingSpeciality = new Speciality()
        {
            Id = TestExistingSpecialityId,
            Name = TestSpecialityName,
            ImageUrl = TestSpecialityImageURL
        };

        public static readonly Speciality TestNonExistingSpeciality = null;

        public static readonly SpecialityAddDTO TestValidAddDTO = new SpecialityAddDTO()
        {
            Name = TestSpecialityName,
            ImageUrl = TestSpecialityImageURL
        };

        public static readonly SpecialityUpdateDTO TestValidSpecialtyUpdateDTO = new SpecialityUpdateDTO()
        {
            Name = TestSpecialityName,
            ImageUrl = TestSpecialityImageURL
        };

    }
}
