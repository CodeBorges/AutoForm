using AutoForms.DAL;
using AutoForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.ViewModel
{
    public class TipoUsuarioViewModel : Tipo_Usuario
    {
        public string NOME_CADASTRO { get; set; }

        public string NOME_ALTERACAO { get; set; }

        public static List<TipoUsuarioViewModel> ListaTipoUsuario
        {
            get
            {
                return TipoUsuarioDAO.RetornarTipoUsuario();
            }
        }

    }
}
