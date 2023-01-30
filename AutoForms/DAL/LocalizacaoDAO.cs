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
    public class LocalizacaoDAO
    {
        public static List<LocalizacaoViewModel> RetornarLocalizacao()
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("SELECT A.*,  ")
                .Append("       B.NOME AS NOME_CADASTRO,  ")
                .Append("       C.NOME AS NOME_ALTERACAO  ")
                .Append("FROM   FORM_LOCALIZACAO AS A  ")
                .Append("       LEFT JOIN FORM_USUARIO AS B  ")
                .Append("              ON A.USUARIO_CADASTRO = B.ID  ")
                .Append("       LEFT JOIN FORM_USUARIO AS C  ")
                .Append("              ON A.USUARIO_ALTERACAO = C.ID  ")
                .Append("WHERE  A.D_E_L_E_T != '*' ");

                return AcessosDAO.ExecutarQueryComRetorno<LocalizacaoViewModel>(query.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Localizacao RetornarLocalizacaoPorNome(string nome)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT * ")
                .Append("FROM   FORM_LOCALIZACAO ")
                .Append("WHERE  D_E_L_E_T != '*' ")
                .Append("AND  NOME = @NOME ")
                .Append("ORDER BY ID ASC ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@NOME", nome.Trim().ToUpper())
                };

                return AcessosDAO
                    .ExecutarQueryComRetorno<Localizacao>(query.ToString(), parametros).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void CadastrarLocalizacao(LocalizacaoViewModel localizacao, int IdUsuario)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("INSERT INTO FORM_LOCALIZACAO  ")
                .Append("            (NOME,  ")
                .Append("             DATA_CADASTRO,  ")
                .Append("             USUARIO_CADASTRO,  ")
                .Append("             D_E_L_E_T)  ")
                .Append("VALUES      (@NOME,  ")
                .Append("             Getdate(),  ")
                .Append("             @USUARIO_CADASTRO,  ")
                .Append("             '') ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@NOME", localizacao.NOME.Trim().ToUpper()),
                    new SqlParameter("@USUARIO_CADASTRO", IdUsuario)
                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void EditarLocalizacao(LocalizacaoViewModel localizacao, int IdUsuario)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("UPDATE FORM_LOCALIZACAO ")
                .Append("SET    NOME = @NOME, ")
                .Append("       USUARIO_ALTERACAO = @USUARIO_ALTERACAO, ")
                .Append("       DATA_ALTERACAO = Getdate() ")
                .Append("WHERE  ID = @ID");

                var parametros = new SqlParameter[]
                {
                        new SqlParameter("@ID", localizacao.ID),
                        new SqlParameter("@NOME", localizacao.NOME.Trim().ToUpper()),
                        new SqlParameter("@USUARIO_ALTERACAO", IdUsuario)

                };
                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void ExcluirLocalizacao(int IdLocalizacao, int idUsuario)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("UPDATE FORM_LOCALIZACAO ")
                .Append("SET    D_E_L_E_T = '*', ")
                .Append("       USUARIO_ALTERACAO = @USUARIO_ALTERACAO, ")
                .Append("       DATA_ALTERACAO = GETDATE() ")
                .Append("WHERE  ID = @ID");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@USUARIO_ALTERACAO", idUsuario),
                    new SqlParameter("@ID", IdLocalizacao)
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
