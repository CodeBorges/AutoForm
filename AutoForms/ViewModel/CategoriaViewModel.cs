using AutoForms.DAL;
using AutoForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.ViewModel
{
    public class CategoriaViewModel : Categoria
    {
        public string NOME_CATEGORIA { get; set; }

        public string NOME_FORMULARIO { get; set; }

        public string NOME_CADASTRO { get; set; }

        public string NOME_ALTERACAO { get; set; }

        public int ID_FORMULARIO { get; set; }

        public int ID_CATEGORIA { get; set; }

    }
}
