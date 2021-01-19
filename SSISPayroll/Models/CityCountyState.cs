using System;
using System.Collections.Generic;

namespace SSISPayroll.Models
{
    public partial class CityCountyState
    {
        public int CityCountyStateId { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
    }
}
