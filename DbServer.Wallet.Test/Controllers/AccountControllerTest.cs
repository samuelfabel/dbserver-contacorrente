using DbServer.Wallet.Api.ViewModels.Auth;
using System.Net;
using Xunit;

namespace DbServer.Wallet.Test.Controllers
{
    public class AccountControllerTest : TestControllerBase
    {
        private const string uri = "/Auth";

        [Fact]
        public async void Success()
        {
            var result = await PostAsync<ResponsePostAuthViewModel>(0, uri, new PostAuthViewModel
            {
                Account = 1,
                Agency = 1,
                Password = "102030"
            });

            Assert.NotNull(result.Token);
        }

        [Fact]
        public async void Fail_NotFound()
        {
            var result = await PostAsync(0, uri, new PostAuthViewModel
            {
                Account = 5,
                Agency = 1,
                Password = "102030"
            });

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async void Fail_Disabled()
        {
            var result = await PostAsync(0, uri, new PostAuthViewModel
            {
                Account = 4,
                Agency = 1,
                Password = "102030"
            });

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async void Fail_WrongPassword()
        {
            var result = await PostAsync(0, uri, new PostAuthViewModel
            {
                Account = 1,
                Agency = 1,
                Password = "102031"
            });

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}