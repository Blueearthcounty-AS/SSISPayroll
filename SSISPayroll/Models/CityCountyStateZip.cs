using System;
using System.Collections.Generic;

namespace SSISPayroll.Models
{
    public partial class CityCountyStateZip
    {
        public int CityCountyStateZipId { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string StateId { get; set; }
        public string CountyFips { get; set; }
    }
}
