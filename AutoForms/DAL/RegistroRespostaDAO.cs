using AutoForms.ViewModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoForms.DAL
{
    public class RegistroRespostaDAO
    {
        public static void InsertRegistroResposta(int idPergunta, int idDocumento)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("INSERT INTO FORM_REGISTRO_RESPOSTA   ")
                .Append("            (ID_PERGUNTA,   ")
                .Append("             ID_DOCUMENTO,   ")
                .Append("             RESPOSTA,   ")
                .Append("             DATA_REGISTRO)   ")
                .Append("VALUES      (@ID_PERGUNTA,   ")
                .Append("             @ID_DOCUMENTO,   ")
                .Append("             0,   ")
                .Append("             GETDATE())   ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_PERGUNTA", idPergunta),
                    new SqlParameter("@ID_DOCUMENTO", idDocumento)

                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void SalvarRespostaAutomaticamente(int idPergunta, int idDocumento, int resposta)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("UPDATE FORM_REGISTRO_RESPOSTA   ")
                .Append("SET    RESPOSTA = @RESPOSTA   ")
                .Append("WHERE  ID_DOCUMENTO = @ID_DOCUMENTO   ")
                .Append("       AND ID_PERGUNTA = @ID_PERGUNTA ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@RESPOSTA", resposta),
                    new SqlParameter("@ID_DOCUMENTO", idDocumento),
                    new SqlParameter("@ID_PERGUNTA", idPergunta),
                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Este método realiza a exclusão definitiva do documento que esteja atrelado a qualquer tabela
        public static void DeletarRegistroResposta(int idDocumento)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("DELETE FROM FORM_REGISTRO_RESPOSTA   ")
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

        public static List<RegistroRespostaViewModel> RetornarListaDeRespostaIdDocumento(int idDocumento)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("                SELECT *  ")
                .Append("FROM   FORM_REGISTRO_RESPOSTA  ")
                .Append("WHERE  ID_DOCUMENTO = @ID_DOCUMENTO ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_DOCUMENTO",idDocumento)
                };

                return AcessosDAO.ExecutarQueryComRetorno<RegistroRespostaViewModel>(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
