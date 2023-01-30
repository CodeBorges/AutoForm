using AutoForms.DAL;
using AutoForms.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.BLL
{
    public class UsuarioBLL
    {
        public static bool VerificarSeExisteUsuarioParaCadastrar(UsuarioViewModel parametrosUsuario)
        {
            try
            {
                var validarLogin = UsuarioDAO.RetornarUsuarioComParametros(parametrosUsuario);

                if (validarLogin != null)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public static bool VerificarSeExisteUsuarioParaEditar(UsuarioViewModel parametrosUsuario)
        {
            try
            {
                var validarEdicao = UsuarioDAO.RetornarUsuarioParaEdicao(parametrosUsuario);

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
