using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TenaxUtils.Http;

namespace CheckCalculator.BL
{
    public class FnsClient : IDisposable
    {
        

        private readonly HttpClientWrapper _client;

        public FnsClient()
        {
            var httpClient = new HttpClient();
            _client = new HttpClientWrapper(httpClient);
        }

        public async Task<HttpResponse<string>> RegisterAsync(string name, string email, string phone)
        {
            var uri = new Uri("https://proverkacheka.nalog.ru:9999/v1/mobile/users/signup");
            var content = new StringContent(JsonConvert.SerializeObject(
                    new
                    {
                        name,
                        email,
                        phone
                    })
                , Encoding.UTF8
                , "application/json");

            return await _client.SendAsync<string>(HttpMethod.Post, uri, content);
        }

        public async Task<HttpResponse<string>> LoginAsync(string phone, string code)
        {
            AddAuthorizationHeaders(phone, code);

            var uri = new Uri("https://proverkacheka.nalog.ru:9999/v1/mobile/users/login");
            return await _client.SendAsync<string>(HttpMethod.Get, uri);
        }

        public async Task<HttpResponse<string>> VerifyCheck(string fiscalNumber, string fiscalDoc
            , string fiscalSign
            , CheckType checkType
            , DateTime operationDate
            , int operationSum)
        {

            var uri = new Uri("https://proverkacheka.nalog.ru:9999/v1/ofds/*/inns/*/fss/" +
                              $"{fiscalNumber}/operations/{(int)checkType}/tickets/{fiscalDoc}" +
                              $"?fiscalSign={fiscalSign}&date={operationDate:yyyy-MM-ddTHH:mm:ss}&sum={operationSum}");
            return await _client.SendAsync<string>(HttpMethod.Get, uri);
        }

        public async Task<HttpResponse<string>> GetCheckInfo(string fiscalNumber, string fiscalDoc, string fiscalSign)
        {
            var uri = new Uri("https://proverkacheka.nalog.ru:9999/v1/inns/*/kkts/*/fss/" +
                              $"{fiscalNumber}/tickets/{fiscalDoc}?fiscalSign={fiscalSign}&sendToEmail=no");

            return await _client.SendAsync<string>(HttpMethod.Get, uri);
        }

        public void Dispose()
        {
            _client?.Dispose();
        }

        private void AddAuthorizationHeaders(string phone, string code)
        {
            const string authHeaderName = "Authorization";
            const string deviceIdHeaderName = "device-id";
            const string deviceOsHeaderName = "device-os";
            var credentionalBuffer = new UTF8Encoding().GetBytes($"{phone}:{code}");

            _client.AddDefaultHeaders(new Dictionary<string, string>
            {
                {authHeaderName, $"Basic {Convert.ToBase64String(credentionalBuffer)}"},
                {deviceIdHeaderName, ""},
                {deviceOsHeaderName, ""}
            });
        }
    }
}
