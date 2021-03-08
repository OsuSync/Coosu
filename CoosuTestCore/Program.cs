﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Coosu.Api.V2;

namespace CoosuTestCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var redirectUri = new Uri("");
            var sb = new AuthorizationLinkBuilder(5044, redirectUri);
            var uri = sb.BuildAuthorizationLink("2241521134", AuthorizationScope.Public
                                                          | AuthorizationScope.Identify);
            var o = uri.ToString();

            var code = "";
            var clientSecret = "";
            var uri2 = sb.BuildAuthorizationTokenLink(clientSecret, code).ToString();

            var result = AuthorizationHelper.GetUserToken(5044, redirectUri, clientSecret, code);
        }
    }
}
