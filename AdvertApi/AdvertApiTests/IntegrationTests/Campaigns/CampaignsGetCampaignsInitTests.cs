using System.Collections.Generic;
using System.Linq;
using AdvertApi.Controllers;
using AdvertApi.DTOs.Responses;
using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace AdvertApiTests.IntegrationTests.Campaigns
{
    [TestFixture]
    public class CampaignsGetCampaignsTests
    {
        [Test]
        public void GetCampaignsMethod_CompleteRequest_Correct()
        {
            var dbLayer = new EfAdvertDbService(new s19191Context());

            var cont = new CampaignsController(dbLayer);
            
            var result = cont.GetCampaings();
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ObjectResult);
            var vr = (ObjectResult) result;
            Assert.IsNotNull(vr.Value);
            var vm = (IEnumerable<GetCampaignsResponse>) vr.Value;
            Assert.IsNotNull(vm);
            Assert.IsTrue(vm.Any());
        }
    }
}