using System;
using System.Collections.Generic;
using System.Text;
using AdvertApi.Controllers;
using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace AdvertApiTests.IntegrationTests.Clients
{
    [TestFixture]
    class ClientsRefreshTokenInitTests
    {
        [Test]
        public void RefreshTokenMethod_CompleteRequest_Correct()
        {
            var dbLayer = new EfAdvertDbService(new s19191Context());

            var configuration = new ConfigurationBuilder();
            configuration.AddJsonFile("D:/APBD/AdvertApi/TestSecret.json");
            
            var cont = new ClientsController(configuration.Build(), dbLayer);
            
            var result = cont.RefreshToken("tmp");
            
            Assert.IsNotNull(result);
        }
    }
}
