using AdvertApi.Models;

namespace AdvertApi.DTOs.Responses
{
    public class GetCampaignsResponse
    {
        public Client client { get; set; }
        public Campaign campaign { get; set; }
    }
}