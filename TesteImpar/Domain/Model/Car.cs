using System;
using System.Collections.Generic;

#nullable disable

namespace TesteImpar.Domain.Model
{

    public partial class Car
    {
        public long Id { get; set; }
        public long? PhotoId { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }

        public virtual Photo Photo { get; set; }
    }
}
