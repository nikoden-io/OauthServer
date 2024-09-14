using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace OauthServer.Presentation;

public class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new("api1", "My API")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "client1",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                ClientSecrets = { new Secret("secret".Sha256()) },

                // Enable refresh tokens
                // AllowAccessTokenViaBrowser = false, => Gives an error unknown
                AllowOfflineAccess = true,

                // Enable token revocation
                RequireClientSecret = true,
                
                AccessTokenLifetime = 3600, // 1 hour
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                RefreshTokenExpiration = TokenExpiration.Absolute,
                AbsoluteRefreshTokenLifetime = 2592000, // 30 days in seconds

                AllowedScopes =
                {
                    "api1",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess // Needed for refresh tokens
                }
            }
        };

}