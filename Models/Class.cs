using System;
using System.Collections.Generic;

namespace school_itg_shadi.Models
{
    public partial class Class
    {
        public Class()
        {
            Associations = new HashSet<Association>();
        }

        public int ClassId { get; set; }
        public string ClassName { get; set; } = null!;

        public virtual ICollection<Association> Associations { get; set; }
    }
}
