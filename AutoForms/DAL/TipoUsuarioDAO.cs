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
    public class TipoUsuarioDAO
    {
        public static List<TipoUsuarioViewModel> RetornarTipoUsuario()
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT A.*,  ")
                .Append("       B.NOME AS NOME_CADASTRO,  ")
                .Append("       C.NOME AS NOME_ALTERACAO  ")
                .Append("FROM   FORM_TIPO_USUARIO AS A  ")
                .Append("       LEFT JOIN FORM_USUARIO AS B  ")
                .Append("              ON A.USUARIO_CADASTRO = B.ID  ")
                .Append("       LEFT JOIN FORM_USUARIO AS C  ")
                .Append("              ON A.USUARIO_ALTERACAO = C.ID  ")
                .Append("WHERE  A.D_E_L_E_T != '*' ");


                return AcessosDAO.ExecutarQueryComRetorno<TipoUsuarioViewModel>(query.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Tipo_Usuario RetornarTipoUsuarioPorNome(string nome)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT * ")
                .Append("FROM   FORM_TIPO_USUARIO ")
                .Append("WHERE  D_E_L_E_T != '*' ")
                .Append("AND  NOME = @NOME ")
                .Append("ORDER BY ID ASC ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@NOME", nome.Trim().ToUpper())
                };

                return AcessosDAO
                    .ExecutarQueryComRetorno<Tipo_Usuario>(query.ToString(), parametros).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int CadastrarTipoUsuario(TipoUsuarioViewModel tipoUsuario, int IdUsuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("INSERT INTO FORM_TIPO_USUARIO  ")
                .Append("            (NOME,  ")
                .Append("             DATA_CADASTRO,  ")
                .Append("             USUARIO_CADASTRO,  ")
                .Append("             D_E_L_E_T)  ")
                .Append("VALUES      (@NOME,  ")
                .Append("             GETDATE(),  ")
                .Append("             @USUARIO_CADASTRO,  ")
                .Append("             '')");


                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@NOME", tipoUsuario.NOME.ToUpper().Trim()),
                    new SqlParameter("@USUARIO_CADASTRO", IdUsuario)
                };

                return AcessosDAO.ExecutarQueryInsertComRetornoDeID(query.ToString(), parametros);
            }

            catch (Exception)
            {

                throw;
            }
        }

        public static void EditarTipoUsuario(TipoUsuarioViewModel tipoUsuario, int IdUsuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("UPDATE FORM_TIPO_USUARIO  ")
                .Append("SET    NOME = @NOME,")
                .Append("       DATA_ALTERACAO = Getdate(),")
                .Append("       USUARIO_ALTERACAO = @USUARIO_ALTERACAO,")
                .Append("       D_E_L_E_T = ''")
                .Append("WHERE  ID = @ID ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID", tipoUsuario.ID),
                    new SqlParameter("@NOME", tipoUsuario.NOME.ToUpper().Trim()),
                    new SqlParameter("@USUARIO_ALTERACAO", IdUsuario)
                };

                AcessosDAO.ExecutarQueryInsertComRetornoDeID(query.ToString(), parametros);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void ExcluirTipoUsuario(int Id, int IdUsuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("UPDATE FORM_TIPO_USUARIO  ")
                .Append("SET    D_E_L_E_T = '*',  ")
                .Append("       USUARIO_ALTERACAO = @USUARIO_ALTERACAO, ")
                .Append("       DATA_ALTERACAO = GETDATE() ")
                .Append("WHERE  ID = @ID ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@USUARIO_ALTERACAO", IdUsuario),
                    new SqlParameter("@ID", Id)
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
