using AutoForms.DAL;
using AutoForms.ViewModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoForms.BLL
{
    public class RegistroObservacaoBLL
    {
        public static void InsertRegistroObservacaoVazio(int idFormulario, int idUsuario, int documento)
        {
            try
            {
                foreach (var observacao in ObservacaoDAO.RetornarObservacaoPorIdFormulario(idFormulario))
                {
                    RegistroObservacaoDAO.InsertRegistroObservacao(documento, observacao.ID, idUsuario);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void SalvarRegistroObservacao(List<RegistroObservacaoViewModel> registroObservacao, int idUsuario)
        {
            try
            {
                foreach (var observacao in registroObservacao)
                {
                    RegistroObservacaoDAO.SalvarObservacao
                    (observacao.ID_DOCUMENTO, observacao.ID_OBSERVACAO, observacao.TEXTO_OBSERVACAO, idUsuario);
                }
                
                
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
