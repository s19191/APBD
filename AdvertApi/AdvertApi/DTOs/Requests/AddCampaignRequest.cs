using System;
using System.ComponentModel.DataAnnotations;

namespace AdvertApi.DTOs.Requests
{
    public class AddCampaignRequest
    {
        public int IdClient { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PricePerSquareMeter { get; set; }
        [Required]
        public int FromIdBuilding { get; set; }
        [Required]
        public int ToIdBuilding { get; set; }
    }
}