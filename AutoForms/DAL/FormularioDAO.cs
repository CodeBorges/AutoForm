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
    public class FormularioDAO
    {
        public static List<FormularioViewModel> RetornarFormulario()
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT A.*,   ")
                .Append("       B.NOME AS NOME_CADASTRO,   ")
                .Append("       C.NOME AS NOME_ALTERACAO,  ")
                .Append("       D.NOME AS NOME_DEPARTAMENTO  ")
                .Append("FROM   FORM_FORMULARIO AS A   ")
                .Append("       LEFT JOIN FORM_USUARIO AS B   ")
                .Append("              ON A.USUARIO_CADASTRO = B.ID   ")
                .Append("       LEFT JOIN FORM_USUARIO AS C   ")
                .Append("              ON A.USUARIO_ALTERACAO = C.ID  ")
                .Append("        INNER JOIN FORM_DEPARTAMENTO AS D  ")
                .Append("            ON A.ID_DEPARTAMENTO = D.ID  ")
                .Append("WHERE  A.D_E_L_E_T != '*'   ");

                return AcessosDAO.ExecutarQueryComRetorno<FormularioViewModel>(query.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static FormularioViewModel RetornarFormularioParaEdicao(FormularioViewModel formulario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("select * from FORM_FORMULARIO    ")
                .Append("WHERE ID != @ID   ")
                .Append("AND NOME = @NOME ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID", formulario.ID),
                    new SqlParameter("@NOME", formulario.NOME.ToUpper().Trim()),
                };

                return AcessosDAO.ExecutarQueryComRetorno<FormularioViewModel>(query.ToString(), parametros).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Formulario RetornarFormularioPorNome(string nome)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT * ")
                .Append("FROM   FORM_FORMULARIO ")
                .Append("WHERE  D_E_L_E_T != '*' ")
                .Append("AND  NOME = @NOME ")
                .Append("ORDER BY ID ASC ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@NOME", nome.Trim().ToUpper())
                };

                return AcessosDAO
                    .ExecutarQueryComRetorno<Formulario>(query.ToString(), parametros).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static FormularioViewModel RetornarFormularioPorId(int idFormulario)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("SELECT *  ")
                .Append("FROM   FORM_FORMULARIO  ")
                .Append("WHERE  D_E_L_E_T != '*'   ")
                .Append("AND  ID = @ID_FORMULARIO   ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_FORMULARIO", idFormulario)
                };

                return AcessosDAO.ExecutarQueryComRetorno<FormularioViewModel>(query.ToString(),parametros).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void CadastrarFormulario(FormularioViewModel formulario, int IdUsuario)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("INSERT INTO FORM_FORMULARIO  ")
                .Append("            (NOME,  ")
                .Append("            ID_DEPARTAMENTO,  ")
                .Append("            META,  ")
                .Append("             DATA_CADASTRO,  ")
                .Append("             USUARIO_CADASTRO,  ")
                .Append("             D_E_L_E_T)  ")
                .Append("VALUES      (@NOME,  ")
                .Append("             @ID_DEPARTAMENTO,  ")
                .Append("             @META,  ")
                .Append("             Getdate(),  ")
                .Append("             @USUARIO_CADASTRO,  ")
                .Append("             '') ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@NOME", formulario.NOME.Trim().ToUpper()),
                    new SqlParameter("ID_DEPARTAMENTO", formulario.ID_DEPARTAMENTO),
                    new SqlParameter("@META", formulario.META),
                    new SqlParameter("@USUARIO_CADASTRO", IdUsuario)
                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void EditarFormulario(FormularioViewModel formulario, int IdUsuario)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("UPDATE FORM_FORMULARIO ")
                .Append("SET    NOME = @NOME, ")
                .Append("       ID_DEPARTAMENTO = @ID_DEPARTAMENTO, ")
                .Append("       USUARIO_ALTERACAO = @USUARIO_ALTERACAO, ")
                .Append("       META = @META, ")
                .Append("       DATA_ALTERACAO = Getdate() ")
                .Append("WHERE  ID = @ID");

                var parametros = new SqlParameter[]
                {
                        new SqlParameter("@ID", formulario.ID),
                        new SqlParameter("@NOME", formulario.NOME.Trim().ToUpper()),
                        new SqlParameter("@META", formulario.META),
                        new SqlParameter("@ID_DEPARTAMENTO", formulario.ID_DEPARTAMENTO),
                        new SqlParameter("@USUARIO_ALTERACAO", IdUsuario)

                };
                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void ExcluirFormulario(int idFormulario, int idUsuario)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("UPDATE FORM_FORMULARIO ")
                .Append("SET    D_E_L_E_T = '*', ")
                .Append("       USUARIO_ALTERACAO = @USUARIO_ALTERACAO, ")
                .Append("       DATA_ALTERACAO = GETDATE() ")
                .Append("WHERE  ID = @ID");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@USUARIO_ALTERACAO", idUsuario),
                    new SqlParameter("@ID", idFormulario)
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
