﻿namespace AdvertApi.Models
{
    public partial class Banner
    {
        public int IdAdvertisement { get; set; }
        public int Name { get; set; }
        public decimal Price { get; set; }
        public int IdCampaign { get; set; }
        public decimal Area { get; set; }

        public virtual Campaign IdCampaignNavigation { get; set; }
    }
}
