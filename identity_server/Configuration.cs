using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity_server
{
    public static class Configuration
    {
        // identityresources are information about user that are going into the id token
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource()
                {
                    Name = "test.scope",
                    UserClaims =
                    {
                        "test.claim"
                    }
                }
            };
        }

        // here you can add claims to access token
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource> {
                new ApiResource("ApiOne", new string[] { "test.at.claim" }),
                new ApiResource("ApiTwo")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client {
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("client_secret".ToSha256()) },

                    // client credentials is machine-to-machine communication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "ApiOne" }
                },
                new Client {
                    ClientId = "client_id_mvc",
                    ClientSecrets = { new Secret("client_secret_mvc".ToSha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = {"https://localhost:43000/signin-oidc"},
                    PostLogoutRedirectUris = {"https://localhost:43000/Home/Index"},
                    AllowedScopes = { "ApiOne", "ApiTwo",
                        IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                        "test.scope"
                    },

                    /*** configure claims client side via id token, bulks up id token ***/

                    // puts all the claims in the id token
                    //AlwaysIncludeUserClaimsInIdToken = true,
                    
                    // used for refresh token, not implemented
                    //AllowOfflineAccess = true,
                    RequireConsent = false,
                }
            };
        }


        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("ApiOne"),
                new ApiScope("ApiTwo")
            };
        }
    }
}
