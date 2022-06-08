using System;
using System.Collections.Generic;

namespace school_itg_shadi.Models
{
    public partial class StudentMark
    {
        public int Id { get; set; }
        public string? StudentName { get; set; }
        public int? StudentId { get; set; }
        public int Physics { get; set; }
        public int Math { get; set; }
        public int Chemistry { get; set; }
        public int English { get; set; }
        public int History { get; set; }

        public virtual Client? Student { get; set; }
        public virtual Client? StudentNameNavigation { get; set; }
    }
}
