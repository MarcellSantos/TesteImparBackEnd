using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteImpar.Domain.Model;

namespace TesteImpar.Application.InputModel
{
    public class CardInputModel
    {
        public long IdCar { get; set; }
        public long IdPhoto { get; set; }
        public string Titulo { get; set; }
        public string Base64{ get; set; }
        public bool Status { get; set; }
    }
}
