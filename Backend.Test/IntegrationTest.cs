using System;
using System.Configuration;
using System.Net.Http;
using Backend.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Backend.Test
{
    public class IntegrationTest : IDisposable

    {
        protected readonly HttpClient TestClient;
        private readonly IServiceProvider _serviceProvider;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _serviceProvider = appFactory.Services;
            TestClient = appFactory.CreateClient();
        }

        public IConfiguration Configuration { get; }

        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<BackendContext>();
            context.Database.EnsureDeleted();
        }
    }
}

