using AdvertApi.Controllers;
using AdvertApi.DTOs.Responses;
using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.AspNetCore.Mvc;
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
            configuration.AddJsonFile("D:/APBD/AdvertApi/AdvertApiTests/TestSecret.json");
            
            var cont = new ClientsController(configuration.Build(), dbLayer);
            
            //akurat taki refreshToken mam w bazie danych, wiem, że jest błędny, ale to jakiś z wstępnej fazy testów
            //tutaj też trzeba albo znaleść jakiś inny refrehToken albo zmienić jednego refreshToken na "aaa"
            var result = cont.RefreshToken("aaa");
            
            //taki pomysł, ale chyba raczej słaby pomysł
            //var result = cont.RefreshToken(new s19191Context().Client.FirstOrDefault(c=>c.IdClient==1).RefreshToken);
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ObjectResult);
            var vr = (ObjectResult) result;
            Assert.IsNotNull(vr.Value);
            var vm = (TokensReponseViewModel) vr.Value;
        }
    }
}
