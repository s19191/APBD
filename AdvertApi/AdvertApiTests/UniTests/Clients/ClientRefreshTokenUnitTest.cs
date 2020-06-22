using AdvertApi.Controllers;
using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Moq;
using NUnit.Framework;

namespace AdvertApiTests.UniTests.Clients
{
    [TestFixture]
    class ClientRefreshTokenUnitTest
    {
        [Test]
        public void RefreshTokenMethod_CompleteRequest_Correct()
        {
            var dbLayer = new Mock<IAdvertDbService>();
            dbLayer.Setup(d => d.checkRefreshToken("tmp")).Returns(new Client
            {
                IdClient = 1,
                FirstName = "Ala",
                LastName = "MaKota",
                Email = "ala@ma.Kota",
                Phone = "niema",
                Login = "login1",
                Password = "mamKota123",
                Salt = "tmpSalt",
                RefreshToken = "tmpRefreshToken"
            });
            
            //var cont = new ClientsController(new ConfigurationBuilder().Add(new JsonConfigurationSource{}).Build(), dbLayer.Object);
            var cont = new ClientsController(new ConfigurationBuilder().Build(), dbLayer.Object);
            
            var result = cont.RefreshToken("tmp");
            
            Assert.IsNotNull(result);
        }
    }
}
