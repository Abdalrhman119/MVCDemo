using System.ComponentModel.DataAnnotations;

namespace Demo.Peresentation.ViewModels.AccountViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First Name Can't Be Empty")]
        [MaxLength(50, ErrorMessage = "First Name Can't Be More Than 50 Characters")]
        public string FirstName { get; set; }=null!;
        [Required(ErrorMessage = "Last Name Can't Be Empty")]
        [MaxLength(50, ErrorMessage = "Last Name Can't Be More Than 50 Characters")]
        public string LastName { get; set; } = null!;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!; 
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
        public bool IsAgree { get; set; }

    }
}
