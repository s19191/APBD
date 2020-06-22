using System.Collections.Generic;
using System.Linq;
using AdvertApi.Controllers;
using AdvertApi.DTOs.Responses;
using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AdvertApiTests.UnitTests.Campaigns
{
    [TestFixture]
    public class CampaignsGetCampaignsUnitTests
    {
        [Test]
        public void GetCampaignsMethod_CompleteRequest_Correct()
        {
            var dbLayer = new Mock<IAdvertDbService>();
            dbLayer.Setup(d => d.GetCampaigns()).Returns(new List<GetCampaignsResponse>
            {
                new GetCampaignsResponse
                {
                    campaign = new Campaign
                    {
                        IdCampaign = 2,
                        IdClient = 3,
                        Banner = new List<Banner>
                        {
                            new Banner
                            {
                                IdAdvertisement = 2,
                            }
                        }
                    },
                    client = new Client
                    {
                        IdClient = 3
                    }
                },
                new GetCampaignsResponse
                {
                campaign = new Campaign
                {
                    IdCampaign = 1,
                    IdClient = 1,
                    Banner = new List<Banner>
                    {
                        new Banner
                        {
                            IdAdvertisement = 1,
                        }
                    }
                },
                client = new Client
                {
                    IdClient = 1
                }
                }
            });
            
            var cont = new CampaignsController(dbLayer.Object);
            
            var result = cont.GetCampaings();
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ObjectResult);
            var vr = (ObjectResult) result;
            Assert.IsNotNull(vr.Value);
            var vm = (IEnumerable<GetCampaignsResponse>) vr.Value;
            Assert.IsNotNull(vm);
            Assert.IsTrue(vm.Count() == 2);
            Assert.IsTrue(vm.ElementAt(0).campaign.IdCampaign == 2);
            Assert.IsTrue(vm.ElementAt(1).client.IdClient == 1 && vm.ElementAt(1).campaign.Banner.ElementAt(0).IdAdvertisement == 1);
        }
    }
}