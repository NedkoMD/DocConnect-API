using DocConnect.Business.Models.Utilities;
using System.ComponentModel.DataAnnotations;

namespace DocConnect.Business.Models.DTOs.User
{
    public class UserRegistrationDTO
    {
        [Required(ErrorMessage = UserDTOMessages.EmptyEmail)]
        [EmailAddress(ErrorMessage = UserDTOMessages.InvalidEmail)]
        [MaxLength(UserDTOConstrains.MaxLengthEmail, ErrorMessage = UserDTOMessages.InvalidEmail)]
        [RegularExpression(UserDTOConstrains.EmailRegex, ErrorMessage = UserDTOMessages.InvalidEmail)]
        public string Email { get; set; }

        [Required(ErrorMessage = UserDTOMessages.EmptyPassword)]
        [DataType(DataType.Password)]
        [StringLength(UserDTOConstrains.MaxLengthPassword, MinimumLength = UserDTOConstrains.MinLengthPassword, ErrorMessage = UserDTOMessages.InvalidPasswordLength)]
        [RegularExpression(UserDTOConstrains.PasswordRegex, ErrorMessage = UserDTOMessages.WeakPassword)]
        public string Password { get; set; }

        [Required(ErrorMessage = UserDTOMessages.EmptyConfirmPassword)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = UserDTOMessages.PasswordsNoMatch)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = UserDTOMessages.EmptyFirstName)]
        [StringLength(UserDTOConstrains.MaxLengthFirstName, ErrorMessage = UserDTOMessages.InvalidFirstNameLength)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = UserDTOMessages.EmptyLastName)]
        [StringLength(UserDTOConstrains.MaxLengthLastName, ErrorMessage = UserDTOMessages.InvalidLastNameLength)]
        public string LastName { get; set; }
    }
}
