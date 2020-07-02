using AdvertApi.Controllers;
using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace AdvertApiTests.IntegrationTests.Clients
{
    [TestFixture]
    public class ClientsLoginInitTest
    {
        [Test]
        public void RefreshTokenMethod_CompleteRequest_Correct()
        {
            var dbLayer = new EfAdvertDbService(new s19191Context());
            
            var configuration = new ConfigurationBuilder();
            configuration.AddJsonFile("D:/APBD/AdvertApi/AdvertApiTests/TestSecret.json");
            
            var cont = new ClientsController(configuration.Build(), dbLayer);

            var result = cont.Login(new LoginRequest
            {
                Login = "aaaaa",
                Password = "asd124"
            });
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ObjectResult);
            var vr = (ObjectResult) result;
            Assert.IsNotNull(vr.Value);
            var vm = (TokensReponseViewModel) vr.Value;
        }
    }
}