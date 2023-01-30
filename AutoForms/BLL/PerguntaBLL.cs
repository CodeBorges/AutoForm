using AutoForms.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.BLL
{
    public class PerguntaBLL
    {
        public static Dictionary<string, string> ConfigurarListaCategoriaPorIdFormulario(int idFormulario)
        {
            try
            {
                var dicionarioCategorias = new Dictionary<string, string>();

                var listaCategorias = FormularioCategoriaDAO.
                    RetornarCategoriaPorIdFormulario(idFormulario);

                if (listaCategorias.Count > 0)
                    foreach (var categoria in listaCategorias)
                        dicionarioCategorias.Add(categoria.ID_CATEGORIA.ToString(), categoria.NOME_CATEGORIA);

                return dicionarioCategorias;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}