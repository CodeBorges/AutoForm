using AutoForms.DAL;
using AutoForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.ViewModel
{
    public class UsuarioViewModel : Usuario
    {
        public string NOME_DEPARTAMENTO { get; set; }

        public string  NOME_TIPO_USUARIO { get; set; }

        public string NOME_CADASTRO { get; set; }

        public string NOME_ALTERACAO { get; set; }

        public static List<UsuarioViewModel> ListaUsuarios
        {
            get
            {
                return UsuarioDAO.RetornarUsuario();
            }
        }

        public static List<DepartamentoViewModel> ListaDepartamentos
        {
            get
            {
                return DepartamentoDAO.RetornarDepartamento();
            }
        }

        public static List<TipoUsuarioViewModel> ListaTipoUsuario
        {
            get
            {
                return TipoUsuarioDAO.RetornarTipoUsuario();
            }
        }
    }
}
