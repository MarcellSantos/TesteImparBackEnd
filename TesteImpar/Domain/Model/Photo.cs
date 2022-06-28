using System;
using System.Collections.Generic;

#nullable disable

namespace TesteImpar.Domain.Model
{
    public partial class Photo
    {
        public Photo()
        {
            Cars = new HashSet<Car>();
        }

        public long Id { get; set; }
        public string Base64 { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
