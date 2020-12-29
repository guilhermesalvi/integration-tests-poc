using IntegrPoc.Api;
using System;
using System.Net.Http;
using Xunit;

namespace IntegrPoc.IntegrationTests.Configuration
{
    [CollectionDefinition(nameof(IntegrationTestsApiFixtureCollection))]
    public class IntegrationTestsApiFixtureCollection : ICollectionFixture<IntegrationTestsFixture<Startup>>
    { }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly IntegrPocAppFactory<TStartup> Factory;
        public HttpClient Client;

        public IntegrationTestsFixture()
        {
            Factory = new IntegrPocAppFactory<TStartup>();
            Client = Factory.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
