using NPOI.Util;
using System;
using System.Collections.Generic;

namespace school_itg_shadi.Models
{
    public partial class Association
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public int? ClassId { get; set; }
        public virtual Class? Class { get; set; }
        public virtual Client? Client { get; set; }
    }
}
