using AutoForms.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.BLL
{
    public class LocalizacaoBLL
    {
        public static bool VerificarSeExisteLocalizacaoPorNome(string nome)
        {
            try
            {
                var localizacao = LocalizacaoDAO.RetornarLocalizacaoPorNome(nome);

                if (localizacao != null)
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
