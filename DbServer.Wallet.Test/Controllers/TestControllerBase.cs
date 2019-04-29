using DbServer.Wallet.Test.Constants;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DbServer.Wallet.Test.Controllers
{
    public abstract class TestControllerBase
    {
        public HttpClient Client => TestClientProvider.Current.Client;

        public async Task<T> PostAsync<T>(long accountId, string uri, object body)
        {
            using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "/api" + uri))
            using (StringContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"))
            {
                message.Content = content;

                var token = GetAuthorizationToken(accountId);

                if (token != null)
                    message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await Client.SendAsync(message);

                result.EnsureSuccessStatusCode();

                var json = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public async Task<HttpResponseMessage> PostAsync(long accountId, string uri, object body)
        {
            using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "/api" + uri))
            using (StringContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"))
            {
                message.Content = content;

                var token = GetAuthorizationToken(accountId);

                if (token != null)
                    message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return await Client.SendAsync(message);
            }
        }

        protected string GetAuthorizationToken(long accountId)
        {
            switch (accountId)
            {
                case AccountConstant.OriginAccountId:
                    return LoginProvider.OrignToken;

                case AccountConstant.DestinyAccountId:
                    return LoginProvider.DestinyToken;

                case AccountConstant.OriginEmptyAccountId:
                    return LoginProvider.OriginEmptyToken;

                default:
                    return null;
            }
        }
    }
}