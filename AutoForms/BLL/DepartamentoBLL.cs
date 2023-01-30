using AutoForms.DAL;
using AutoForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.BLL
{
    public class DepartamentoBLL
    {
        public static bool VerificarSeExisteDepartamentoPorNome(string nome)
        {
            try
            {
                var departamento = DepartamentoDAO.RetornarDepartamentoPorNome(nome);

                if (departamento != null)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
