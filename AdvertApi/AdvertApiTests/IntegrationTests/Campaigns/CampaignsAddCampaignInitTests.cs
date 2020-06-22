using System;
using AdvertApi.Controllers;
using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace AdvertApiTests.IntegrationTests.Campaigns
{
    [TestFixture]
    class CampaignsAddCampaignInitTests
    {
        [Test]
        public void AddCampaignMethod_CompleteRequest_Correct()
        {
            var dbLayer = new EfAdvertDbService(new s19191Context());
            
            var cont = new CampaignsController(dbLayer);

            var result = cont.AddCampaign(new AddCampaignRequest
            {
                IdClient = 1,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(2),
                PricePerSquareMeter = 35,
                FromIdBuilding = 2,
                ToIdBuilding = 5
            });
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ObjectResult);
            var vr = (ObjectResult) result;
            Assert.IsNotNull(vr.Value);
            var vm = (AddCampaignResponse) vr.Value;
            Assert.IsNotNull(vm);
            Assert.IsTrue(Decimal.Compare(vm.TotalPrice,1837.50m) == 0);
            Assert.IsTrue(vm.Campaign.IdClient == 1);
        }
    }
}
