using AdvertApi.Controllers;
using AdvertApi.DTOs.Requests;
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
    public class ClientsRegisterUnitTests
    {
        [Test]
        public void RegisterMethod_CompleteRequest_Correct()
        {
            var dbLayer = new Mock<IAdvertDbService>();
            RegisterRequest request = new RegisterRequest
            {
                FirstName = "Ala",
                LastName = "MaKota",
                Email = "ala@ma.Kota",
                Phone = "niema",
                Login = "login1",
                Password = "mamKota123"
            };
            dbLayer.Setup(d => d.Registration(request))
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

            var result = cont.Register(request);
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ObjectResult);
            var vr = (ObjectResult) result;
            Assert.IsNotNull(vr.Value);
            var vm = (RegisterResponse) vr.Value;
            Assert.IsNotNull(vm.TokensReponse);
        }
    }
}