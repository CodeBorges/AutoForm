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
    public class UsuarioDAO
    {

        public static List<UsuarioViewModel> RetornarUsuario()
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("SELECT A.*,  ")
                .Append("       B.NOME AS NOME_CADASTRO,  ")
                .Append("       C.NOME AS NOME_ALTERACAO,  ")
                .Append("       D.NOME AS NOME_TIPO_USUARIO,  ")
                .Append("       E.NOME AS NOME_DEPARTAMENTO  ")
                .Append("FROM   FORM_USUARIO AS A  ")
                .Append("       LEFT JOIN FORM_USUARIO AS B  ")
                .Append("              ON A.USUARIO_CADASTRO = B.ID  ")
                .Append("       LEFT JOIN FORM_USUARIO AS C  ")
                .Append("              ON A.USUARIO_ALTERACAO = C.ID  ")
                .Append("       INNER JOIN FORM_TIPO_USUARIO AS D  ")
                .Append("               ON A.ID_TIPO_USUARIO = D.ID  ")
                .Append("       INNER JOIN FORM_DEPARTAMENTO AS E  ")
                .Append("               ON A.ID_DEPARTAMENTO = E.ID  ")
                .Append("WHERE  A.D_E_L_E_T != '*' ")
                .Append("ORDER BY ID ASC ");

                return AcessosDAO.ExecutarQueryComRetorno<UsuarioViewModel>(query.ToString());
            }
            catch (Exception)
            {
                throw;
            }


        }

        public static UsuarioViewModel RetornarUsuarioPorId(int idUsuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT * FROM    ")
                .Append("FORM_USUARIO    ")
                .Append("WHERE ID = @ID ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID", idUsuario)
                };

                return AcessosDAO.ExecutarQueryComRetorno<UsuarioViewModel>(query.ToString(), parametros).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static UsuarioViewModel RetornarUsuarioComParametros(UsuarioViewModel usuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT *  ")
                .Append("FROM   FORM_USUARIO  ")
                .Append("WHERE  USUARIO = @USUARIO  ")
                .Append("AND  D_E_L_E_T != '*'  ");
               
                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@USUARIO" , usuario.USUARIO)
                };

                return AcessosDAO.ExecutarQueryComRetorno<UsuarioViewModel>(query.ToString(), parametros).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static UsuarioViewModel RetornarUsuarioParaEdicao(UsuarioViewModel usuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT *  ")
                .Append("FROM   FORM_USUARIO  ")
                .Append("WHERE  ID != @ID  ")
                .Append("AND  USUARIO = @USUARIO  ");
               
                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID" , usuario.ID),
                    new SqlParameter("@USUARIO" , usuario.USUARIO)
                };

                return AcessosDAO.ExecutarQueryComRetorno<UsuarioViewModel>(query.ToString(), parametros).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static void AlterarSenha(int idUsuario, string novaSenha)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("UPDATE FORM_USUARIO   ")
                .Append("SET SENHA = @SENHA,   ")
                .Append("MUDAR_SENHA = 0   ")
                .Append("WHERE ID = @ID ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@SENHA", novaSenha.Trim()),
                    new SqlParameter("@ID", idUsuario)
                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Usuario RetornarUsuarioPorLoginESenha(int usuario, string senha)
        {
            try
            {
                var query = new StringBuilder();

                query
                    .Append("SELECT * ")
                    .Append("FROM FORM_USUARIO ")
                    .Append("WHERE USUARIO = @USUARIO ")
                    .Append("AND SENHA = @SENHA ")
                    .Append("AND D_E_L_E_T != '*';");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@USUARIO", usuario),
                    new SqlParameter("@SENHA", senha)
                };

                return AcessosDAO.ExecutarQueryComRetorno<Usuario>(query.ToString(), parametros).FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int CadastrarUsuario(Usuario usuario, int IdUsuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                    .Append("INSERT INTO FORM_USUARIO ")
                    .Append("            (NOME, ")
                    .Append("             USUARIO, ")
                    .Append("             SENHA, ")
                    .Append("             EMAIL, ")
                    .Append("             MUDAR_SENHA, ")
                    .Append("             ID_DEPARTAMENTO, ")
                    .Append("             ID_TIPO_USUARIO, ")
                    .Append("             DATA_CADASTRO, ")
                    .Append("             USUARIO_CADASTRO, ")
                    .Append("             D_E_L_E_T) ")
                    .Append("VALUES      (@NOME, ")
                    .Append("             @USUARIO, ")
                    .Append("             @SENHA, ")
                    .Append("             @EMAIL, ")
                    .Append("             @MUDAR_SENHA, ")
                    .Append("             @ID_DEPARTAMENTO, ")
                    .Append("             @ID_TIPO_USUARIO, ")
                    .Append("             GETDATE(), ")
                    .Append("             @USUARIO_CADASTRO, ")
                    .Append("             '')");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@NOME",usuario.NOME.ToUpper().Trim()),
                    new SqlParameter("@USUARIO", usuario.USUARIO),
                    new SqlParameter("@SENHA", usuario.SENHA),
                    new SqlParameter("@EMAIL", usuario.EMAIL.ToUpper().Trim()),
                    new SqlParameter("@MUDAR_SENHA", usuario.MUDAR_SENHA),
                    new SqlParameter("@ID_DEPARTAMENTO", usuario.ID_DEPARTAMENTO),
                    new SqlParameter("@ID_TIPO_USUARIO", usuario.ID_TIPO_USUARIO),
                    new SqlParameter("@USUARIO_CADASTRO", IdUsuario)
                };

                return AcessosDAO.ExecutarQueryInsertComRetornoDeID(query.ToString(), parametros);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void EditarUsuario(Usuario usuario, int IdUsuario)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("UPDATE FORM_USUARIO  ")
                .Append("SET    NOME = @NOME,  ")
                .Append("       USUARIO = @USUARIO,  ")
                .Append("       SENHA = @SENHA,  ")
                .Append("       EMAIL = @EMAIL,  ")
                .Append("       MUDAR_SENHA = @MUDAR_SENHA,  ")
                .Append("       ID_DEPARTAMENTO = @ID_DEPARTAMENTO,  ")
                .Append("       ID_TIPO_USUARIO = @TIPO_USUARIO,  ")
                .Append("       DATA_ALTERACAO = GETDATE(),  ")
                .Append("       USUARIO_ALTERACAO = @USUARIO_ALTERACAO  ")
                .Append("WHERE  ID = @ID ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID", usuario.ID),
                    new SqlParameter("@NOME", usuario.NOME.ToUpper().Trim()),
                    new SqlParameter("@USUARIO", usuario.USUARIO),
                    new SqlParameter("@SENHA", usuario.SENHA),
                    new SqlParameter("@EMAIL", usuario.EMAIL.ToUpper().Trim()),
                    new SqlParameter("@MUDAR_SENHA", usuario.MUDAR_SENHA),
                    new SqlParameter("@ID_DEPARTAMENTO", usuario.ID_DEPARTAMENTO),
                    new SqlParameter("@TIPO_USUARIO", usuario.ID_TIPO_USUARIO),
                    new SqlParameter("@USUARIO_ALTERACAO", IdUsuario),

                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void ExcluirUsuario(int Id, int IdUsuario)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("UPDATE FORM_USUARIO  ")
                .Append("SET    D_E_L_E_T = '*',  ")
                .Append("       USUARIO_ALTERACAO = @USUARIO_ALTERACAO, ")
                .Append("       DATA_ALTERACAO = GETDATE() ")
                .Append("WHERE  ID = @ID ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID",Id),
                    new SqlParameter("@USUARIO_ALTERACAO", IdUsuario)
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
