using DbServer.Wallet.Application.Services.Model;
using System.ComponentModel.DataAnnotations;

namespace DbServer.Wallet.Api.ViewModels.Auth
{
    public class PostAuthViewModel : IValidateAccount
    {
        [Required]
        public int Agency { get; set; }

        [Required]
        public int Account { get; set; }

        [Required]
        public string Password { get; set; }
    }
}