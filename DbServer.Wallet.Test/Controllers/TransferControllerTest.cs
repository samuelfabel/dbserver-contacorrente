using DbServer.Wallet.Api.ViewModels.Transfer;
using DbServer.Wallet.Test.Constants;
using System.Net;
using Xunit;

namespace DbServer.Wallet.Test.Controllers
{
    public class TransferControllerTest : TestControllerBase
    {
        private const string uri = "/Transfer";

        [Fact]
        public async void Success()
        {
            var result = await PostAsync(AccountConstant.OriginAccountId, uri, new PostTransferViewModel
            {
                DestinationAccount = 2,
                Amount = 100,
                DestinationAgency = 1
            });

            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async void Fail_DestinationNotFound()
        {
            var result = await PostAsync(AccountConstant.OriginAccountId, uri, new PostTransferViewModel
            {
                DestinationAccount = 5,
                Amount = 100,
                DestinationAgency = 1
            });

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async void Fail_InsufficientFunds()
        {
            var result = await PostAsync(AccountConstant.OriginEmptyAccountId, uri, new PostTransferViewModel
            {
                DestinationAccount = 2,
                Amount = 100,
                DestinationAgency = 1
            });

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}