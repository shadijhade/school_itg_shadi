using System;
using System.Collections.Generic;

namespace school_itg_shadi.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Clients = new HashSet<Client>();
        }

        public int GenderId { get; set; }
        public string? GenderDesc { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
