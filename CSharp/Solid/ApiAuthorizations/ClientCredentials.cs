namespace Solid.ApiAuthorizations
{
    using Solid.Models;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text;
    using System.Threading.Tasks;

    public class ClientCredentials : ISpotifyAuthorization
    {
        private string TOKEN_URL = "https://accounts.spotify.com/api/token";
        public async Task<AccessTokenModel> AuthorizationFlow(string clientId, string clientSecret)
        {
            byte[] clientCredentials = Encoding.UTF8.GetBytes(String.Format("{0}:{1}", clientId, clientSecret));

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(clientCredentials));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
                requestData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

                FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);

                HttpResponseMessage request = await client.PostAsync(this.TOKEN_URL, requestBody);
                string response = await request.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<AccessTokenModel>(response);
            }
        }
    }
}