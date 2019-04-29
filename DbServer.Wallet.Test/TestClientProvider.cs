﻿using DbServer.Wallet.Api;
using DbServer.Wallet.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.IO;
using System.Net.Http;

namespace DbServer.Wallet.Test
{
    public class TestClientProvider
    {
        private TestServer _server;

        public static TestClientProvider Current { get; } = new TestClientProvider();

        public HttpClient Client { get; }

        private TestClientProvider()
        {
            _server = new TestServer(new WebHostBuilder().UseContentRoot(Directory.GetCurrentDirectory()).UseStartup<Startup>());

            Client = _server.CreateClient();
        }
    }
}
