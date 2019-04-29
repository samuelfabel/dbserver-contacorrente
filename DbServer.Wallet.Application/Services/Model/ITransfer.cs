namespace DbServer.Wallet.Application.Services.Model
{
    public interface ITransfer
    {
        int DestinationAgency { get; set; }

        int DestinationAccount { get; set; }

        decimal Amount { get; set; }
    }
}