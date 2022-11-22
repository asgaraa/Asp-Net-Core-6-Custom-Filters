using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTOs.AppUser
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Phone Number is required.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "FullName is required.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }
    }
}
