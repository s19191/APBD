using AdvertApi.Models;

namespace AdvertApi.DTOs.Responses
{
    public class AddCampaignResponse
    {
        public Campaign Campaign { get; set; }
        public decimal TotalPrice { get; set; }
    }
}