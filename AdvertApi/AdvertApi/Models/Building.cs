using System.Collections.Generic;

namespace AdvertApi.Models
{
    public partial class Building
    {
        public Building()
        {
            CampaignFromBuildingNavigation = new HashSet<Campaign>();
            CampaignToBuildingNavigation = new HashSet<Campaign>();
        }

        public int IdBuilding { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public string City { get; set; }
        public decimal Height { get; set; }

        public virtual ICollection<Campaign> CampaignFromBuildingNavigation { get; set; }
        public virtual ICollection<Campaign> CampaignToBuildingNavigation { get; set; }
    }
}
