using AutoForms.ViewModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoForms.DAL
{
    public class PerguntaDAO
    {
        public static List<PerguntaViewModel> RetornarPerguntaPorIdCategoria(int IdCategoria)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("                SELECT A.*,   ")
                .Append("C.NOME AS NOME_CADASTRO,   ")
                .Append("D.NOME AS NOME_ALTERACAO,   ")
                .Append("E.NOME AS NOME_CATEGORIA,   ")
                .Append("F.NOME AS NOME_FORMULARIO   ")
                .Append("FROM   FORM_PERGUNTA AS A    ")
                .Append("       INNER JOIN FORM_FORMULARIO_CATEGORIA AS B    ")
                .Append("               ON A.ID_CATEGORIA = B.ID    ")
                .Append("               LEFT JOIN FORM_USUARIO AS C   ")
                .Append("               ON A.USUARIO_CADASTRO = C.ID   ")
                .Append("               LEFT JOIN FORM_USUARIO AS D   ")
                .Append("               ON A.USUARIO_ALTERACAO = D.ID   ")
                .Append("               INNER JOIN FORM_CATEGORIA AS E    ")
                .Append("               ON A.ID_CATEGORIA = E.ID   ")
                .Append("               INNER JOIN FORM_FORMULARIO AS F   ")
                .Append("               ON A.ID_FORMULARIO = F.ID   ")
                .Append("WHERE  A.ID_CATEGORIA = @ID_CATEGORIA ")
                .Append("AND A.D_E_L_E_T != '*' ")
                .Append("AND B.D_E_L_E_T != '*' ")
                .Append("AND E.D_E_L_E_T != '*' ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_CATEGORIA", IdCategoria)
                };

                return AcessosDAO.ExecutarQueryComRetorno<PerguntaViewModel>(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void CadastrarPergunta(PerguntaViewModel pergunta, int idUsuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("INSERT INTO FORM_PERGUNTA  ")
                .Append("            (NOME,  ")
                .Append("             ID_FORMULARIO,  ")
                .Append("             ID_CATEGORIA,  ")
                .Append("             DATA_CADASTRO,  ")
                .Append("             USUARIO_CADASTRO,  ")
                .Append("             D_E_L_E_T)  ")
                .Append("VALUES      (@NOME,  ")
                .Append("             @ID_FORMULARIO,  ")
                .Append("             @ID_CATEGORIA,  ")
                .Append("             Getdate(),  ")
                .Append("             @USUARIO_CADASTRO,  ")
                .Append("             '') ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@NOME", pergunta.NOME.Trim().ToUpper()),
                    new SqlParameter("@ID_FORMULARIO", pergunta.ID_FORMULARIO),
                    new SqlParameter("@ID_CATEGORIA", pergunta.ID_CATEGORIA),
                    new SqlParameter("@USUARIO_CADASTRO", idUsuario)

                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void EditarPergunta(PerguntaViewModel pergunta, int IdUsuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("UPDATE FORM_PERGUNTA   ")
                .Append("SET    NOME = @NOME,  ")
                .Append("        ID_FORMULARIO = @ID_FORMULARIO,  ")
                .Append("        ID_CATEGORIA = @ID_CATEGORIA,  ")
                .Append("       USUARIO_ALTERACAO = @USUARIO_ALTERACAO,   ")
                .Append("       DATA_ALTERACAO = Getdate()   ")
                .Append("WHERE  ID = @ID ");

                var parametros = new SqlParameter[]
                {
                        new SqlParameter("@ID", pergunta.ID),
                        new SqlParameter("@NOME", pergunta.NOME.Trim().ToUpper()),
                        new SqlParameter("@ID_FORMULARIO", pergunta.ID_FORMULARIO),
                        new SqlParameter("@ID_CATEGORIA", pergunta.ID_CATEGORIA),
                        new SqlParameter("@USUARIO_ALTERACAO", IdUsuario)

                };
                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void ExcluirPergunta(int idPergunta, int idUsuario)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("UPDATE FORM_PERGUNTA ")
                .Append("SET    D_E_L_E_T = '*', ")
                .Append("       USUARIO_ALTERACAO = @USUARIO_ALTERACAO, ")
                .Append("       DATA_ALTERACAO = GETDATE() ")
                .Append("WHERE  ID = @ID");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@USUARIO_ALTERACAO", idUsuario),
                    new SqlParameter("@ID", idPergunta)
                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<PerguntaViewModel> RetornarPergunta()
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

                return AcessosDAO.ExecutarQueryComRetorno<PerguntaViewModel>(query.ToString());

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
