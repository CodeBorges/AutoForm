using AutoForms.ViewModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoForms.DAL
{
    public class RegistroObservacaoDAO
    {
        public static List<RegistroObservacaoViewModel> RetornarRegistroObservacaoPorIdDocumento(int idDocumento)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("SELECT *   ")
                .Append("FROM   FORM_REGISTRO_OBSERVACAO   ")
                .Append("WHERE  ID_DOCUMENTO = @ID_DOCUMENTO  ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_DOCUMENTO", idDocumento)
                };

                return AcessosDAO.ExecutarQueryComRetorno<RegistroObservacaoViewModel>(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void InsertRegistroObservacao(int idDocumento, int idObservacao, int IdUsuario)
        {
            try
            {
                var query = new StringBuilder();

                query

                .Append("INSERT INTO FORM_REGISTRO_OBSERVACAO  ")
                .Append("            (ID_DOCUMENTO,  ")
                .Append("             ID_OBSERVACAO,  ")
                .Append("             TEXTO_OBSERVACAO,  ")
                .Append("             DATA_REGISTRO,  ")
                .Append("             USUARIO_CADASTRO)  ")
                .Append("VALUES      (@ID_DOCUMENTO,  ")
                .Append("             @ID_OBSERVACAO,  ")
                .Append("             '',  ")
                .Append("             Getdate(),  ")
                .Append("             @USUARIO_CADASTRO) ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_DOCUMENTO", idDocumento),
                    new SqlParameter("@ID_OBSERVACAO", idObservacao),
                    new SqlParameter("@USUARIO_CADASTRO", IdUsuario)
                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void SalvarObservacao(int idDocumento, int idObservacao, string textoObservacao, int IdUsuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("UPDATE FORM_REGISTRO_OBSERVACAO   ")
                .Append("SET    TEXTO_OBSERVACAO = @TEXTO_OBSERVACAO,   ")
                .Append("       DATA_REGISTRO = GETDATE(),   ")
                .Append("       USUARIO_CADASTRO = @USUARIO_CADASTRO   ")
                .Append("WHERE  ID_DOCUMENTO = @ID_DOCUMENTO  ")
                .Append("AND  ID_OBSERVACAO = @ID_OBSERVACAO  ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_OBSERVACAO",idObservacao),
                    new SqlParameter("@TEXTO_OBSERVACAO",textoObservacao.Trim().ToUpper()),
                    new SqlParameter("@USUARIO_CADASTRO",IdUsuario),
                    new SqlParameter("@ID_DOCUMENTO",idDocumento)
                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void DeletarRegistroObservacao(int idDocumento)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("DELETE FROM FORM_REGISTRO_OBSERVACAO   ")
                .Append("WHERE  ID_DOCUMENTO = @ID_DOCUMENTO  ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_DOCUMENTO", idDocumento)
                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<RegistroObservacaoViewModel> RetornarListaDeRespostaObservacaoIdDocumento(int idDocumento)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("                SELECT *  ")
                .Append("FROM   FORM_REGISTRO_OBSERVACAO  ")
                .Append("WHERE  ID_DOCUMENTO = @ID_DOCUMENTO ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_DOCUMENTO",idDocumento)
                };

                return AcessosDAO.ExecutarQueryComRetorno<RegistroObservacaoViewModel>(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
