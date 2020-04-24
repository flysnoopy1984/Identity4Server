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
        public const string apiName_ContentCenter = "ccApi";
        public const string apiName2_ContentCenter = "ccApi2";
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
                //new ApiResource(apiName_ContentCenter,"API for ContentCenter",new List<string>(){
                //    JwtClaimTypes.Name,
                //    JwtClaimTypes.Email
                  
                //}),
                  new ApiResource(apiName_ContentCenter,"API for ContentCenter"),
                  new ApiResource(apiName2_ContentCenter,"API 2"),

            };
        
        public static IEnumerable<Client> Clients =>
            new Client[] 
            {
                new Client
                {
                    ClientId = "ccClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {apiName_ContentCenter},
                    ClientSecrets =
                    {
                        new Secret("shanghai".Sha256())
                    }
                },
                 new Client
                {
                    ClientId = "ccUserPwd",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = {apiName2_ContentCenter},
                    ClientSecrets =
                    {
                        new Secret("shanghai".Sha256())
                    }
                },
                new Client
                {
                    ClientId = "ccPage",
                    ClientSecrets = { new Secret("shanghai".Sha256()) },
                  
                    AllowedGrantTypes = GrantTypes.Implicit,
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
                        apiName_ContentCenter
                    },
                    AllowOfflineAccess = true
                }
            };
        
    }
}