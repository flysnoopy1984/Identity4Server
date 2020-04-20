// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace Id4Server
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[] 
            { 
                new ApiResource("ccApi","API for ContentCenter",new List<string>(){
                    JwtClaimTypes.Name,
                    JwtClaimTypes.GivenName,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Address,
                    JwtClaimTypes.WebSite
                }),
              
            };
        
        public static IEnumerable<Client> Clients =>
            new Client[] 
            {
                new Client
                {
                    ClientId = "jacky",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"ccApi"},
                    ClientSecrets =
                    {
                        new Secret("shanghai".Sha256())
                    }
                },
                 new Client
                {
                    ClientId = "userPwd",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = {"ccApi"},
                    ClientSecrets =
                    {
                        new Secret("shanghai".Sha256())
                    }
                },
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("shanghai".Sha256()) },
                  
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,

                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5002/signin-oidc" },//

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "ccApi"
                    },
                    AllowOfflineAccess = true
                }
            };
        
    }
}