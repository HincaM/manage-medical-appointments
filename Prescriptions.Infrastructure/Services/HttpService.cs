using Prescriptions.Domain.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Prescriptions.Infrastructure.Services
{
    public sealed class HttpService: IHttpService
    {
        public async Task<T> Get<T>(string url, string endPoint, CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "token");

                HttpResponseMessage response = await client.GetAsync(endPoint, cancellationToken);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseBody);
            }

        }

        public async Task<T> Post<T>(string url, string endPoint, object request, CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "token");

                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(endPoint, content, cancellationToken);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseBody);
            }

        }
    }
}
