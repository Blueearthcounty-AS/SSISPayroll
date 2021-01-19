using System;
using System.Collections.Generic;

namespace SSISPayroll.Models
{
    public partial class Panel
    {
        public int PanelId { get; set; }
        public string ParentPage { get; set; }
        public int PanelSetNumber { get; set; }
        public int Sequence { get; set; }
        public string Label { get; set; }
        public string Source { get; set; }
        public string RssUrl { get; set; }
        public int? Limit { get; set; }
    }
}
