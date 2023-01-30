using AutoForms.Models;
using AutoForms.ViewModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoForms.DAL
{
    public class ObservacaoDAO
    {
        public static List<ObservacaoViewModel> RetornarObservacaoPorIdFormulario(int idFormulario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT A.*,   ")
                .Append("       B.NOME AS NOME_FORMULARIO,   ")
                .Append("       C.NOME AS NOME_CADASTRO,   ")
                .Append("       D.NOME AS NOME_ALTERACAO   ")
                .Append("FROM   FORM_OBSERVACAO AS A   ")
                .Append("       INNER JOIN FORM_FORMULARIO AS B   ")
                .Append("               ON A.ID_FORMULARIO = B.ID   ")
                .Append("       LEFT JOIN FORM_USUARIO AS C   ")
                .Append("              ON A.USUARIO_CADASTRO = C.ID   ")
                .Append("       LEFT JOIN FORM_USUARIO AS D   ")
                .Append("              ON A.USUARIO_ALTERACAO = D.ID   ")
                .Append("WHERE  A.D_E_L_E_T != '*'  ")
                .Append("AND  A.ID_FORMULARIO = @ID_FORMULARIO ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("ID_FORMULARIO", idFormulario)
                };

                return AcessosDAO.ExecutarQueryComRetorno<ObservacaoViewModel>(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Observacao> RetornarObservacaoIdFormulario(int idFormulario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT *   ")
                .Append("FROM FORM_OBSERVACAO   ")
                .Append("WHERE ID_FORMULARIO = @ID_FORMULARIO ")
                .Append("AND D_E_L_E_T != '*' ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("ID_FORMULARIO", idFormulario)
                };

                return AcessosDAO.ExecutarQueryComRetorno<Observacao>(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<ObservacaoViewModel> RetornarObservacao()
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("    SELECT A.*,   ")
                .Append("       B.NOME AS NOME_CATEGORIA,   ")
                .Append("       C.NOME AS NOME_FORMULARIO,   ")
                .Append("       D.NOME AS NOME_CADASTRO,   ")
                .Append("       E.NOME AS NOME_ALTERACAO   ")
                .Append("FROM   FORM_PERGUNTA AS A   ")
                .Append("       INNER JOIN FORM_CATEGORIA AS B   ")
                .Append("               ON A.ID_CATEGORIA = B.ID   ")
                .Append("       INNER JOIN FORM_FORMULARIO AS C   ")
                .Append("               ON A.ID_FORMULARIO = C.ID   ")
                .Append("       LEFT JOIN FORM_USUARIO AS D   ")
                .Append("              ON A.USUARIO_CADASTRO = D.ID   ")
                .Append("       LEFT JOIN FORM_USUARIO AS E   ")
                .Append("              ON A.USUARIO_ALTERACAO = E.ID   ")
                .Append("WHERE  A.D_E_L_E_T != '*' ");

                return AcessosDAO.ExecutarQueryComRetorno<ObservacaoViewModel>(query.ToString());

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void CadastrarObservacao(ObservacaoViewModel observacao, int IdUsuario)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("INSERT INTO FORM_OBSERVACAO  ")
                .Append("            (NOME,  ")
                .Append("            ID_FORMULARIO,  ")
                .Append("             DATA_CADASTRO,  ")
                .Append("             USUARIO_CADASTRO,  ")
                .Append("             D_E_L_E_T)  ")
                .Append("VALUES      (@NOME,  ")
                .Append("             @ID_FORMULARIO,  ")
                .Append("             Getdate(),  ")
                .Append("             @USUARIO_CADASTRO,  ")
                .Append("             '') ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@NOME", observacao.NOME.Trim().ToUpper()),
                    new SqlParameter("ID_FORMULARIO", observacao.ID_FORMULARIO),
                    new SqlParameter("@USUARIO_CADASTRO", IdUsuario)
                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void EditarObservacao(ObservacaoViewModel observacao, int IdUsuario)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("UPDATE FORM_OBSERVACAO ")
                .Append("SET    NOME = @NOME, ")
                .Append("       USUARIO_ALTERACAO = @USUARIO_ALTERACAO, ")
                .Append("       DATA_ALTERACAO = Getdate() ")
                .Append("WHERE  ID = @ID");

                var parametros = new SqlParameter[]
                {
                        new SqlParameter("@ID", observacao.ID),
                        new SqlParameter("@NOME", observacao.NOME.Trim().ToUpper()),
                        new SqlParameter("@USUARIO_ALTERACAO", IdUsuario)

                };
                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void ExcluirObservacao(int idObservacao, int idUsuario)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("UPDATE FORM_OBSERVACAO ")
                .Append("SET    D_E_L_E_T = '*', ")
                .Append("       USUARIO_ALTERACAO = @USUARIO_ALTERACAO, ")
                .Append("       DATA_ALTERACAO = GETDATE() ")
                .Append("WHERE  ID = @ID");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@USUARIO_ALTERACAO", idUsuario),
                    new SqlParameter("@ID", idObservacao)
                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);

            }
            catch (Exception)
            {

                throw;
            }
        }


    }

}
