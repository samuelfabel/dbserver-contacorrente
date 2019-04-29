using DbServer.Wallet.Application.Services.Model;
using System.ComponentModel.DataAnnotations;

namespace DbServer.Wallet.Api.ViewModels.Transfer
{
    public class PostTransferViewModel : ITransfer
    {
        [Required]
        public int DestinationAgency { get; set; }

        [Required]
        public int DestinationAccount { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}