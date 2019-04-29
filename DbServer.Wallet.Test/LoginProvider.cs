using DbServer.Wallet.Api.ViewModels.Auth;
using DbServer.Wallet.Application;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace DbServer.Wallet.Test
{
    public static class LoginProvider
    {
        private static string _orignToken;
        private static string _destinyToken;
        private static string _originEmptyToken;

        public static string OrignToken => _orignToken ?? (_orignToken = GetToken(1, 1, "102030"));

        public static string DestinyToken => _destinyToken ?? (_destinyToken = GetToken(1, 2, "102030"));

        public static string OriginEmptyToken => _originEmptyToken ?? (_originEmptyToken = GetToken(1, 3, "102030"));

        private static string GetToken(int agency, int account, string password)
        {
            var response = TestClientProvider.Current.Client.PostAsync("/api/Auth", new StringContent(JsonConvert.SerializeObject(new PostAuthViewModel
            {
                Account = account,
                Agency = agency,
                Password = password
            }), Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            response.EnsureSuccessStatusCode();

            var data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            var result = JsonConvert.DeserializeObject<ResponsePostAuthViewModel>(data);

            return result.Token;
        }
    }
}