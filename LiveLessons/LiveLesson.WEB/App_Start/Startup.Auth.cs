﻿using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using LiveLesson.WEB.Providers;
using LiveLesson.WEB.Models;
using Microsoft.Owin.Cors;

namespace LiveLesson.WEB
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.UseCors(CorsOptions.AllowAll);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };

            app.UseOAuthBearerTokens(OAuthOptions);

            // app.UseMicrosoftAccountAuthentication(
            //     clientId: "",
            //     clientSecret: "");

            // app.UseTwitterAuthentication(
            //     consumerKey: "",
            //     consumerSecret: "");

            // app.UseFacebookAuthentication(
            //     appId: "",
            //     appSecret: "");

            // app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            // {
            //     ClientId = "",
            //     ClientSecret = ""
            // });
        }
    }
}
