using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;

namespace Demo.Peresentation.ViewModels.AccountViewModel
{
    public class ForgetPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
    }
}
