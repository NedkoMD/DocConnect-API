using DocConnect.Business.Models.Utilities;
using System.ComponentModel.DataAnnotations;

namespace DocConnect.Business.Models.DTOs.User
{
    public class UserResetPasswordDTO
    {
        [Required(ErrorMessage = UserDTOMessages.EmptyPassword)]
        [DataType(DataType.Password)]
        [StringLength(UserDTOConstrains.MaxLengthPassword, MinimumLength = UserDTOConstrains.MinLengthPassword, ErrorMessage = UserDTOMessages.InvalidPasswordLength)]
        [RegularExpression(UserDTOConstrains.PasswordRegex, ErrorMessage = UserDTOMessages.WeakPassword)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = UserDTOMessages.EmptyConfirmPassword)]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = UserDTOMessages.PasswordsNoMatch)]
        public string ConfirmPassword { get; set; }
    }
}
