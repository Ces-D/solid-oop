namespace Solid
{
    using System.IO;
    using System;
    using System.Threading.Tasks;
    using Solid.ApiAuthorizations;
    using Solid.Spotify;

    class Program
    {
        static async Task Main(string[] args)
        {
            string projectRoot = Environment.CurrentDirectory;
            string envPath = Path.GetFullPath(Path.Combine(projectRoot, @"../../.env"));

            DotEnv.Load(envPath);
            string clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
            string clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

            var clientCredentials = new ClientCredentials();
            var accessTokens = await clientCredentials.AuthorizationFlow(clientId, clientSecret);

            var featurePlaylists = new FeaturePlaylists(accessTokens);
            await featurePlaylists.RequestAsync();
        }

    }
}
