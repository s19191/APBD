using AdvertApi.Controllers;
using AdvertApi.DTOs.Responses;
using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace AdvertApiTests.UnitTests.Clients
{
    [TestFixture]
    class ClientsRefreshTokenUnitTests
    {
        [Test]
        public void RefreshTokenMethod_CompleteRequest_Correct()
        {
            var dbLayer = new Mock<IAdvertDbService>();
            dbLayer.Setup(d => d.checkRefreshToken("tmp"))
                .Returns(new Client
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

            var configuration = new ConfigurationBuilder();
            configuration.AddJsonFile("D:/APBD/AdvertApi/AdvertApiTests/TestSecret.json");
            
            var cont = new ClientsController(configuration.Build(), dbLayer.Object);

            var result = cont.RefreshToken("tmp");
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ObjectResult);
            var vr = (ObjectResult) result;
            Assert.IsNotNull(vr.Value);
            var vm = (TokensReponseViewModel) vr.Value;
        }
    }
}
