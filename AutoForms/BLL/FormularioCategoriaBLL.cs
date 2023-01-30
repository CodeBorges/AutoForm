using AutoForms.DAL;
using AutoForms.Models;
using AutoForms.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.BLL
{
    public class FormularioCategoriaBLL
    {
        public static bool VincularFormularioCategoria(int idCategoria, int idUsuario, CategoriaViewModel categoria)
        {
            try
            {
                if (CategoriaDAO.RetornarCategoriaParaEdicao(categoria) != null)
                {
                    FormularioCategoriaDAO.CadastrarFormularioCategoria(categoria.ID_FORMULARIO, idCategoria, idUsuario);
                    return true;
                }
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
