using DocConnect.Business.Models.Utilities;
using System.ComponentModel.DataAnnotations;

namespace DocConnect.Business.Models.DTOs.User
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = UserDTOMessages.EmptyEmail)]
        [EmailAddress(ErrorMessage = UserDTOMessages.InvalidEmail)]
        public string Email { get; set; }

        [Required(ErrorMessage = UserDTOMessages.EmptyPassword)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
