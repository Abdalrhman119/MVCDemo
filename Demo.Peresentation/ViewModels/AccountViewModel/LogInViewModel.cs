using System.ComponentModel.DataAnnotations;

namespace Demo.Peresentation.ViewModels.AccountViewModel
{
    public class LogInViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
public bool RememberMe { get; set; }
    }
}
