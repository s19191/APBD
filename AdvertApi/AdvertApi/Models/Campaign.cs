using System;
using System.Collections.Generic;

namespace AdvertApi.Models
{
    public partial class Campaign
    {
        public Campaign()
        {
            Banner = new HashSet<Banner>();
        }

        public int IdCampaign { get; set; }
        public int IdClient { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PricePerSquareMeter { get; set; }
        public int FromBuilding { get; set; }
        public int ToBuilding { get; set; }

        public virtual Building FromBuildingNavigation { get; set; }
        public virtual Client IdClientNavigation { get; set; }
        public virtual Building ToBuildingNavigation { get; set; }
        public virtual ICollection<Banner> Banner { get; set; }
    }
}
