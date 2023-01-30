using AutoForms.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.BLL
{
    public class TipoUsuarioBLL
    {
        public static bool VerificarSeExisteTipoUsuarioPorNome(string nome)
        {
            try
            {
                var departamento = TipoUsuarioDAO.RetornarTipoUsuarioPorNome(nome);

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
