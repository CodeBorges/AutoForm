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
    public class CategoriaDAO
    {

        public static CategoriaViewModel RetornarCategoriaPorNome(string nome)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT * ")
                .Append("FROM   FORM_CATEGORIA ")
                .Append("WHERE  D_E_L_E_T != '*' ")
                .Append("AND  NOME = @NOME ")
                .Append("ORDER BY ID ASC ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@NOME", nome.Trim().ToUpper())
                };

                return AcessosDAO
                    .ExecutarQueryComRetorno<CategoriaViewModel>(query.ToString(), parametros).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static CategoriaViewModel RetornarCategoriaParaEdicao(CategoriaViewModel categoria)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT * ")
                .Append("FROM   FORM_CATEGORIA ")
                .Append("WHERE  D_E_L_E_T != '*' ")
                .Append("AND  NOME = @NOME ")
                .Append("AND  ID != @ID ")
                .Append("ORDER BY ID ASC ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID", categoria.ID),
                    new SqlParameter("@NOME", categoria.NOME.Trim().ToUpper())
                };

                return AcessosDAO
                    .ExecutarQueryComRetorno<CategoriaViewModel>(query.ToString(), parametros).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<CategoriaViewModel> RetornarCategoria()
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT A.*,  ")
                .Append("       B.NOME AS NOME_CADASTRO,  ")
                .Append("       C.NOME AS NOME_ALTERACAO  ")
                .Append("FROM   FORM_CATEGORIA AS A  ")
                .Append("       LEFT JOIN FORM_USUARIO AS B  ")
                .Append("              ON A.USUARIO_CADASTRO = B.ID  ")
                .Append("       LEFT JOIN FORM_USUARIO AS C  ")
                .Append("              ON A.USUARIO_ALTERACAO = C.ID  ")
                .Append("WHERE  A.D_E_L_E_T != '*' ");

                return AcessosDAO.ExecutarQueryComRetorno<CategoriaViewModel>(query.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int CadastrarCategoria(CategoriaViewModel categoria, int IdUsuario)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("INSERT INTO FORM_CATEGORIA  ")
                .Append("            (NOME,  ")
                .Append("             CONCEITO,  ")
                .Append("             DATA_CADASTRO,  ")
                .Append("             USUARIO_CADASTRO,  ")
                .Append("             D_E_L_E_T)  ")
                .Append("output         inserted.ID  ")
                .Append("VALUES      (@NOME,  ")
                .Append("             @CONCEITO,  ")
                .Append("             Getdate(),  ")
                .Append("             @USUARIO_CADASTRO,  ")
                .Append("             ''); ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@NOME", categoria.NOME.Trim().ToUpper()),
                    new SqlParameter("@CONCEITO", categoria.CONCEITO.Trim().ToUpper()),
                    new SqlParameter("@USUARIO_CADASTRO", IdUsuario)
                };

                return AcessosDAO.ExecutarQueryInsertComRetornoDeID(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void EditarCategoria(CategoriaViewModel categoria,  int IdUsuario)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("UPDATE FORM_CATEGORIA ")
                .Append("SET    NOME = @NOME, ")
                .Append("       CONCEITO = @CONCEITO, ")
                .Append("       USUARIO_ALTERACAO = @USUARIO_ALTERACAO, ")
                .Append("       DATA_ALTERACAO = Getdate() ")
                .Append("WHERE  ID = @ID");

                var parametros = new SqlParameter[]
                {
                        new SqlParameter("@ID", categoria.ID),
                        new SqlParameter("@NOME", categoria.NOME.Trim().ToUpper()),
                        new SqlParameter("@CONCEITO", categoria.CONCEITO.Trim().ToUpper()),
                        new SqlParameter("@USUARIO_ALTERACAO", IdUsuario)

                };
                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void ExcluirCategoria(int idCategoria, int idUsuario)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("UPDATE FORM_CATEGORIA ")
                .Append("SET    D_E_L_E_T = '*', ")
                .Append("       USUARIO_ALTERACAO = @USUARIO_ALTERACAO, ")
                .Append("       DATA_ALTERACAO = GETDATE() ")
                .Append("WHERE  ID = @ID");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@USUARIO_ALTERACAO", idUsuario),
                    new SqlParameter("@ID", idCategoria)
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
