using AutoForms.DAL;
using AutoForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.ViewModel
{
    public class  DepartamentoViewModel : Departamento
    {
        public string NOME_USUARIO { get; set; }

        public static List<DepartamentoViewModel> ListaDepartamento
        {
            get
            {
                return DepartamentoDAO.RetornarDepartamento();
            }
        }
    }
}
