using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPORevamp.WebAPI.Models
{
    public class SponsorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AdvertMessage { get; set; }
        public string BannerImage { get; set; }
        public string LogoImage { get; set; }
    }
}
