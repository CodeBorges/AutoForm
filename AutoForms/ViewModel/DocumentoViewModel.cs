using AutoForms.DAL;
using AutoForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.ViewModel
{
    public class DocumentoViewModel : Documento
    {
        public string NOME_USUARIO { get; set; }

        public string NOME_FORMULARIO { get; set; }

        public string NOME_DEPARTAMENTO { get; set; }

        public string NOME_LOCALIZACAO { get; set; }

        public bool PENDENTE { get; set; }

    }
}
