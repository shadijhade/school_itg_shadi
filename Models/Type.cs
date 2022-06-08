using System;
using System.Collections.Generic;

namespace school_itg_shadi.Models
{
    public partial class Type
    {
        public Type()
        {
            Clients = new HashSet<Client>();
        }

        public int TypeId { get; set; }
        public string? TypeDesc { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
