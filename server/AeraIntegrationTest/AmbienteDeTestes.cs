using aera_core;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace AeraIntegrationTest
{
    [SetUpFixture]
    public class AmbienteDeTestes
    {
        public static WebApplicationFactory<Startup> Factory;
        
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Factory = new APIWebApplicationFactory();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Factory.Dispose();
        }
    }
}