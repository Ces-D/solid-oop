namespace Solid.Spotify
{
    using Solid.Models;
    using System;
    using System.Threading.Tasks;
    using System.Net.Http;
    using System.Net.Http.Headers;

    public class FeaturePlaylists
    {
        private AccessTokenModel _authorizationTokens;
        private string PLAYLIST_URL = "https://api.spotify.com/v1/browse/featured-playlists";
        public FeaturePlaylists(AccessTokenModel authorization)
        {
            _authorizationTokens = authorization;
        }

        public async Task RequestAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._authorizationTokens.access_token);

                HttpResponseMessage request = await client.GetAsync(this.PLAYLIST_URL);
                string response = await request.Content.ReadAsStringAsync();

                Console.WriteLine(response);
            }
        }
    }
}