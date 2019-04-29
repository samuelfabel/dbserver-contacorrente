namespace DbServer.Wallet.Api.ViewModels.Shared
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {
        }

        public ErrorViewModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}