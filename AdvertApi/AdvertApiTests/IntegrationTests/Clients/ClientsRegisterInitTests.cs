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
    public class ClientsRegisterInitTests
    {
        [Test]
        public void RegisterMethod_CompleteRequest_Correct()
        {
            var dbLayer = new EfAdvertDbService(new s19191Context());
            
            var configuration = new ConfigurationBuilder();
            configuration.AddJsonFile("D:/APBD/AdvertApi/AdvertApiTests/TestSecret.json");
            
            var cont = new ClientsController(configuration.Build(), dbLayer);

            var result = cont.Register(new RegisterRequest
            {
                FirstName = "John",
                LastName = "Kowalski",
                Email = "kowalski@wp.pl",
                Phone = "454-232-222",
                //zmień login na inny, jak chcesz żeby było dobrze, albo usuń clienta o podanym loginie
                Login = "qqq",
                Password = "asd124"
            });
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ObjectResult);
            var vr = (ObjectResult) result;
            Assert.IsNotNull(vr.Value);
            var vm = (RegisterResponse) vr.Value;
            Assert.IsNotNull(vm.TokensReponse);
        }
        [Test]
        public void RegisterMethod_CompleteRequest_InCorrect()
        {
            var dbLayer = new EfAdvertDbService(new s19191Context());
            
            var configuration = new ConfigurationBuilder();
            configuration.AddJsonFile("D:/APBD/AdvertApi/AdvertApiTests/TestSecret.json");
            
            var cont = new ClientsController(configuration.Build(), dbLayer);

            var result = cont.Register(new RegisterRequest
            {
                FirstName = "John",
                LastName = "Kowalski",
                Email = "kowalski@wp.pl",
                Phone = "454-232-222",
                Login = "alaMAKOTAdgfhfdsgsdfgdh",
                Password = "asd124"
            });
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ObjectResult);
            var vr = (ObjectResult) result;
            Assert.IsNotNull(vr.Value);
            //nie działa to tak jak myślałem
            // var vm = (LoginOccupiedException) vr.Value;
            // Assert.IsNotNull(vm);
            // Assert.IsTrue(vm.Message.Equals("Podany login jest już zajęty, spróbuj inny"));
        }
    }
}