using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SSISPayroll.Models
{
    public partial class SsisRoleBrassConversion
    {
        public int SsisRoleBrassConversionId { get; set; }
        [Required]
        [DisplayName("SSIS Role Id")]
        public int? SsisRoleId { get; set; }
        [Required]
        [DisplayName("Brass")]
        public int? Brass { get; set; }
        [Required]
        [DisplayName("Org Code")]
        public int? OrgCode { get; set; }
    }
}
