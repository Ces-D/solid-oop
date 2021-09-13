namespace Solid.ApiAuthorizations
{
    using System.Threading.Tasks;
    using Solid.Models;


    public interface ISpotifyAuthorization
    {
        public Task<AccessTokenModel> AuthorizationFlow(string clientId, string clientSecret);
    }
}