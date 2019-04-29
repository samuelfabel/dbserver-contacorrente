namespace DbServer.Wallet.Application.Services.Model
{
    public interface IValidateAccount
    {
        int Agency { get; set; }

        int Account { get; set; }

        string Password { get; set; }
    }
}