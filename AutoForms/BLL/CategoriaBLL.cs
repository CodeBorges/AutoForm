using AutoForms.DAL;
using AutoForms.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.BLL
{
    public class CategoriaBLL
    {
        public static bool VerificarSeExisteCategoriaPorNome(string nome)
        {
            try
            {
                var categoria = CategoriaDAO.RetornarCategoriaPorNome(nome);

                if (categoria != null)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool VerificarSeExisteCategoriaParaEditar(CategoriaViewModel parametrosCategoria)
        {
            try
            {
                var validarEdicao = CategoriaDAO.RetornarCategoriaParaEdicao(parametrosCategoria);

                if (validarEdicao != null)
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
