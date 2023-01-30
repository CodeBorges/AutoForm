using AutoForms.DAL;
using AutoForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.ViewModel
{
    public class FormularioViewModel : Formulario
    {
        public string NOME_DEPARTAMENTO { get; set; }

        public string NOME_CADASTRO { get; set; }

        public string NOME_ALTERACAO { get; set; }

        public bool PENDENTE { get; set; }

        public DocumentoViewModel Documento { get; set; }

        public Usuario UsuarioLogado { get; set; }

        public List<FormularioViewModel> ListaFormulario { get; set; }

    }
}
