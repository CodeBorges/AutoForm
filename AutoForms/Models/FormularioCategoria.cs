using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.Models
{
    public class FormularioCategoria : Categoria
    {
        public int ID_FORMULARIO { get; set; }

        public int ID_DEPARTAMENTO { get; set; }

        public int ID_CATEGORIA { get; set; }

    }
}
