using System;
using System.Net.Http;
using Backend.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
// Tymczasowo wyłącza równoległe uruchamianie testów
// do momentu rozwiązania przypisywania różnych baz danych dla różnych testów
[assembly: CollectionBehavior(DisableTestParallelization = true)]

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

