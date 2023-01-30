using AutoForms.DAL;
using AutoForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.ViewModel
{
    public class ObservacaoViewModel : Observacao
    {
        public string NOME_FORMULARIO { get; set; }

        public string NOME_CADASTRO { get; set; }

        public string NOME_ALTERACAO { get; set; }
        
    }
}
