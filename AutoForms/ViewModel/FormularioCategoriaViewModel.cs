using AutoForms.DAL;
using AutoForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.ViewModel
{
    public class FormularioCategoriaViewModel : FormularioCategoria
    {
        public string NOME_CADASTRO { get; set; }

        public string NOME_ALTERACAO { get; set; }

        public string NOME_FORMULARIO { get; set; }

        public string NOME_CATEGORIA { get; set; }

        public int TOTAL_PERGUNTAS { get; set; }

        public Usuario UsuarioLogado { get; set; }

        public MediaCategoria MediaCategoria { get; set; }

        public DocumentoViewModel Documento { get; set; }

        public FormularioViewModel Formulario { get; set; }

        public List<Observacao> ListaObservacao { get; set; }

        public List<FormularioCategoriaViewModel> ListaFormularioCategoria { get; set; }

    }
}
