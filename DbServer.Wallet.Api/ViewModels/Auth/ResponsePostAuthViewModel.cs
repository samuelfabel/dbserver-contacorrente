namespace DbServer.Wallet.Api.ViewModels.Auth
{
    public class ResponsePostAuthViewModel
    {
        public string Token { get; set; }

        public int Validity { get; set; }
    }
}