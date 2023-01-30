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
    public class FormularioCategoriaDAO
    {
        public static List<FormularioCategoriaViewModel> RetornarCategoriaPorIdFormulario(int idFormulario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT A.*,   ")
                .Append("       B.NOME AS NOME_CATEGORIA,   ")
                .Append("       C.NOME AS NOME_FORMULARIO,   ")
                .Append("       B.CONCEITO   ")
                .Append("FROM   FORM_FORMULARIO_CATEGORIA AS A   ")
                .Append("       INNER JOIN FORM_CATEGORIA AS B   ")
                .Append("               ON A.ID_CATEGORIA = B.ID   ")
                .Append("       INNER JOIN FORM_FORMULARIO AS C   ")
                .Append("               ON A.ID_FORMULARIO = C.ID   ")
                .Append("WHERE  A.D_E_L_E_T != '*'   ")
                .Append("       AND A.ID_FORMULARIO = @ID_FORMULARIO ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_FORMULARIO", idFormulario)
                };
                return AcessosDAO.ExecutarQueryComRetorno<FormularioCategoriaViewModel>(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<FormularioCategoriaViewModel> RetornarFormularioCategoriaPorIdFormulario(int idFormulario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT A.*,   ")
                .Append("B.NOME AS NOME_FORMULARIO,   ")
                .Append("C.NOME AS NOME_CATEGORIA,   ")
                .Append("C.CONCEITO,   ")
                .Append("D.NOME AS NOME_CADASTRO,   ")
                .Append("E.NOME AS NOME_ALTERACAO   ")
                .Append("    FROM FORM_FORMULARIO_CATEGORIA AS A   ")
                .Append("    INNER JOIN FORM_FORMULARIO AS B   ")
                .Append("    ON A.ID_FORMULARIO = B.ID   ")
                .Append("    INNER JOIN FORM_CATEGORIA AS C   ")
                .Append("    ON A.ID_CATEGORIA = C.ID   ")
                .Append("    LEFT JOIN FORM_USUARIO AS D    ")
                .Append("              ON A.USUARIO_CADASTRO = D.ID    ")
                .Append("       LEFT JOIN FORM_USUARIO AS E    ")
                .Append("              ON A.USUARIO_ALTERACAO = E.ID    ")
                .Append("    WHERE A.D_E_L_E_T != '*' AND    ")
                .Append("    A.ID_FORMULARIO = @ID_FORMULARIO   ")
                .Append("    ORDER BY A.ID ASC ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_FORMULARIO", idFormulario)
                };

                return AcessosDAO.ExecutarQueryComRetorno<FormularioCategoriaViewModel>(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<FormularioCategoriaViewModel> RetornarTotalPerguntasCategoriaPorIdFormulario(int idFormulario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT FC.ID_CATEGORIA,   ")
                .Append("       TOTAL_PERGUNTAS   ")
                .Append("FROM   FORM_FORMULARIO_CATEGORIA AS FC   ")
                .Append("       CROSS apply (SELECT Count(P.ID) AS TOTAL_PERGUNTAS   ")
                .Append("                    FROM   FORM_PERGUNTA AS P   ")
                .Append("                    WHERE  P.ID_CATEGORIA = FC.ID_CATEGORIA    ")
                .Append("                    AND P.D_E_L_E_T != '*') X   ")
                .Append("WHERE  FC.ID_FORMULARIO = @ID_FORMULARIO and FC.D_E_L_E_T != '*'   ")
                .Append("ORDER  BY FC.ID_CATEGORIA ASC ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_FORMULARIO", idFormulario)
                };

                return AcessosDAO.ExecutarQueryComRetorno<FormularioCategoriaViewModel>(query.ToString(), parametros);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void CadastrarFormularioCategoria(int idFormulario, int IdCategoria, int idUsuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("INSERT INTO FORM_FORMULARIO_CATEGORIA   ")
                .Append("            (ID_FORMULARIO,   ")
                .Append("             ID_CATEGORIA,   ")
                .Append("             DATA_CADASTRO,   ")
                .Append("             USUARIO_CADASTRO,   ")
                .Append("             D_E_L_E_T)   ")
                .Append("VALUES      (@ID_FORMULARIO,   ")
                .Append("             @ID_CATEGORIA,   ")
                .Append("             Getdate(),   ")
                .Append("             @USUARIO_CADASTRO,   ")
                .Append("             '')  ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_FORMULARIO", idFormulario),
                    new SqlParameter("@ID_CATEGORIA", IdCategoria),
                    new SqlParameter("@USUARIO_CADASTRO", idUsuario)
                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void EditarFormularioCategoria(CategoriaViewModel categoria, int idUsuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("UPDATE FORM_FORMULARIO_CATEGORIA   ")
                .Append("SET    ID_FORMULARIO = @ID_FORMULARIO,   ")
                .Append("       ID_CATEGORIA = @ID_CATEGORIA,   ")
                .Append("       DATA_ALTERACAO = GETDATE(),   ")
                .Append("       USUARIO_ALTERACAO = @USUARIO_ALTERACAO   ")
                .Append("WHERE  ID_CATEGORIA = @CATEGORIA_ID  ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_FORMULARIO", categoria.ID_FORMULARIO),
                    new SqlParameter("@ID_CATEGORIA", categoria.ID),
                    new SqlParameter("@CATEGORIA_ID", categoria.ID),
                    new SqlParameter("@USUARIO_ALTERACAO", idUsuario)
                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void ExcluirFormularioCategoria(int idCategoria, int idUsuario)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("UPDATE FORM_FORMULARIO_CATEGORIA ")
                .Append("SET    D_E_L_E_T = '*', ")
                .Append("       USUARIO_ALTERACAO = @USUARIO_ALTERACAO, ")
                .Append("       DATA_ALTERACAO = GETDATE() ")
                .Append("WHERE  ID_CATEGORIA = @ID_CATEGORIA");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@USUARIO_ALTERACAO", idUsuario),
                    new SqlParameter("@ID_CATEGORIA", idCategoria)
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
