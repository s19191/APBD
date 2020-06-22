using AdvertApi.Controllers;
using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Models;
using AdvertApi.Services;
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
            dbLayer.Setup(d => d.AddCampaign(new AddCampaignRequest
            {
                IdClient = 1,
                PricePerSquareMeter = 25,
                FromIdBuilding = 1,
                ToIdBuilding = 4
            })).Returns(new AddCampaignResponse
            {
                Campaign = new Campaign
                {
                    IdCampaign = 1,
                    IdClient = 1,
                },
                TotalPrice = 25
            });
            
            var cont = new CampaignsController(dbLayer.Object);
            
            var result = cont.AddCampaign(new AddCampaignRequest
            {
                IdClient = 1,
                PricePerSquareMeter = 25,
                FromIdBuilding = 1,
                ToIdBuilding = 4
            });
            
            Assert.IsNotNull(result);
        }
    }
}
