using System;
using System.Collections.Generic;

namespace SSISPayroll.Models
{
    public partial class PanelLink
    {
        public int PanelLinkId { get; set; }
        public int PanelId { get; set; }
        public int Sequence { get; set; }
        public string Label { get; set; }
        public string Url { get; set; }
        public string Target { get; set; }
        public int Display { get; set; }
    }
}
