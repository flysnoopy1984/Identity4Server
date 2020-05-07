// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.




using IdentityServer4;
using LYM.AspNetCore.Authentication.WeChat;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Id4Server
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // uncomment, if you want to add an MVC-based UI
            services.AddControllersWithViews();

            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(Config.Ids)
                .AddInMemoryApiResources(Config.Apis)
                .AddInMemoryClients(Config.Clients)
                .AddTestUsers(TestUsers.Users);

            services.AddAuthentication()
                 .AddCookie("Cookies")
                 .AddWeChat(o =>
                 {
                     o.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                     //o.sin
                     o.AppId = "wx0b7a2be2734e3619";
                     o.AppSecret = "75d20b508b2366745002ac3d3eb9886f";
                 //    o.UserInformationEndpoint = "http://id4.iqianba.cn/WX/UserInfo";
                   //  o.CallbackPath = "/WX/LoginCallBack";


                 });
                 //.AddOpenIdConnect("oidc", o =>
                 // {
                 //     o.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                 //     o.SignOutScheme = IdentityServerConstants.SignoutScheme;
                 //     o.Authority = "http://id4.iqianba.cn";
                 //     o.RequireHttpsMetadata = false;
                 //     o.ClientId = "ccPage";
                 //     o.ClientSecret = "shanghai";
                 //     o.ResponseType = "code";

                 //     o.SaveTokens = true;
                 //     o.Scope.Add("ccApi");
                 //     o.Scope.Add("offline_access");

                 //     //用户拒绝
                 //     o.Events = new OpenIdConnectEvents
                 //     {
                 //         OnAccessDenied = cxt => {
                 //             cxt.Response.Redirect(location: "/Home/Error?errorId='拒绝'");
                 //             cxt.HandleResponse();
                 //             return Task.FromResult(0);
                 //         },
                 //         OnRemoteFailure = cxt =>
                 //         {
                 //             cxt.Response.Redirect(location: "/Home/Error?errorId='拒绝'");
                 //             cxt.HandleResponse();
                 //             return Task.FromResult(0);
                 //         }
                 //     };
                 // });

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler("/Home/Error");
            //if (Environment.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            // uncomment if you want to add MVC
            //app.UseStaticFiles();
            //app.UseRouting();

            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });

            // uncomment, if you want to add MVC
            //app.UseAuthorization();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapDefaultControllerRoute();
            //});
        }
    }
}
