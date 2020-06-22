using System;
using AdvertApi.Controllers;
using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AdvertApiTests.UnitTests.Campaigns
{
    [TestFixture]
    class CampaignsAddCampaignUnitTests
    {
        [Test]
        public void AddCampaignMethod_CompleteRequest_Correct()
        {
            var dbLayer = new Mock<IAdvertDbService>();
            AddCampaignRequest request = new AddCampaignRequest
            {
                IdClient = 1,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(2),
                PricePerSquareMeter = 25,
                FromIdBuilding = 1,
                ToIdBuilding = 4
            };
            dbLayer.Setup(d => d.AddCampaign(request))
                .Returns(new AddCampaignResponse
            {
                Campaign = new Campaign
                {
                    IdCampaign = 1,
                    IdClient = 1,
                },
                TotalPrice = 25
            });
            
            var cont = new CampaignsController(dbLayer.Object);

            var result = cont.AddCampaign(request);
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ObjectResult);
            var vr = (ObjectResult) result;
            Assert.IsNotNull(vr.Value);
            var vm = (AddCampaignResponse) vr.Value;
            Assert.IsNotNull(vm);
            Assert.IsTrue(vm.TotalPrice == 25);
            Assert.IsTrue(vm.Campaign.IdCampaign == 1);
            Assert.IsTrue(vm.Campaign.IdClient == 1);
        }
    }
}
