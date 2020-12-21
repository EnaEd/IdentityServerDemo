using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServerDemo.Presentation.Config
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("api1", "My API")
            };

        public static IEnumerable<IdentityResource> Ids =>
           new List<IdentityResource>
           {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = IdentityServerConstants.StandardScopes.Address,
                    DisplayName = "Address",
                    UserClaims = new [] { "address", "phone_number" }
                },
                new IdentityResource
                {
                    Name = "api_role",
                    DisplayName = "Api Role",
                    UserClaims = new [] { "api_role" }
                }
           };

        public static IEnumerable<ApiResource> Apis =>

            new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };


        public static IEnumerable<Client> Clients =>

            new List<Client>
            {
                new Client()
                {
                    ClientId="client",
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes={ "api1" }
                },
                 new Client
                {
                    ClientId = "blazor",
                    AllowedGrantTypes = GrantTypes.Code,
                    //RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = {"https://localhost:5001"} ,
                    RedirectUris = {"https://localhost:5001/authentication/login-callback"},
                    PostLogoutRedirectUris = {"https://localhost:5001/"},
                    ClientName = "blazor",
                    AllowOfflineAccess=true, //turn on token

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api1"
                    }
                }
            };


        public static List<TestUser> Users => new List<TestUser>
        {
            new TestUser{SubjectId = "1122Alica", Username = "alice", Password = "alice",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Alice"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                }
            },
            new TestUser{SubjectId = "3344Bob", Username = "bob", Password = "bob",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Bob Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Bob"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim("location", "bob's location"),
                    new Claim("api_role", "Admin"),
                    new Claim("api_role", "User"),
                    new Claim("api_role", "Manager"),
                    new Claim(JwtClaimTypes.PhoneNumber, "456546564"),
                }
            }
        };
    }
}
