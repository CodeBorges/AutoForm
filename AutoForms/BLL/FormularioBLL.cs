using AutoForms.DAL;
using AutoForms.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.BLL
{
    public class FormularioBLL
    {
        public static bool VerificarSeExisteFormularioPorNome(string nome)
        {
            try
            {
                var departamento = FormularioDAO.RetornarFormularioPorNome(nome);

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

        public static bool VerificarSeExisteFormularioParaEditar(FormularioViewModel parametrosFormulario)
        {
            try
            {
                var validarEdicao = FormularioDAO.RetornarFormularioParaEdicao(parametrosFormulario);

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
