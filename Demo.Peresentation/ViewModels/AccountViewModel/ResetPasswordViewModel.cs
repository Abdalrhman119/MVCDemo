using System.ComponentModel.DataAnnotations;

namespace Demo.Peresentation.ViewModels.AccountViewModel
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
