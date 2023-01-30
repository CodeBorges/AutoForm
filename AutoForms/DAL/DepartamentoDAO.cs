using AutoForms.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoForms.ViewModel;

namespace AutoForms.DAL
{
    public class DepartamentoDAO
    {
        public static List<DepartamentoViewModel> RetornarDepartamento()
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT B.NOME AS NOME_USUARIO, ")
                .Append("       A.*  ")
                .Append("FROM   FORM_DEPARTAMENTO AS A  ")
                .Append("       INNER JOIN FORM_USUARIO AS B  ")
                .Append("               ON A.USUARIO_CADASTRO = B.ID  ")
                .Append("WHERE  A.D_E_L_E_T != '*' ")
                .Append("ORDER BY ID ASC ");

                return AcessosDAO.ExecutarQueryComRetorno<DepartamentoViewModel>(query.ToString());

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Departamento RetornarDepartamentoPorNome(string nome)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT * ")
                .Append("FROM   FORM_DEPARTAMENTO ")
                .Append("WHERE  D_E_L_E_T != '*' ")
                .Append("AND  NOME = @NOME ")
                .Append("ORDER BY ID ASC ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@NOME", nome.Trim().ToUpper())
                };

                return AcessosDAO
                    .ExecutarQueryComRetorno<Departamento>(query.ToString(), parametros).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int CadastrarDepartamento(DepartamentoViewModel departamento, int idUsuario)
        {
            try
            {

                var query = new StringBuilder();

                query
              .Append("INSERT INTO FORM_DEPARTAMENTO  ")
              .Append("            (NOME,  ")
              .Append("             USUARIO_CADASTRO,  ")
              .Append("             DATA_CADASTRO,  ")
              .Append("             D_E_L_E_T)  ")
              .Append("VALUES      (@NOME,  ")
              .Append("             @USUARIO_CADASTRO, ")
              .Append("             GETDATE(),  ")
              .Append("             '')");

                var parametros = new SqlParameter[]
                {
                       new SqlParameter("@NOME", departamento.NOME.Trim().ToUpper()),
                       new SqlParameter("@USUARIO_CADASTRO", idUsuario)
                };

                return AcessosDAO.ExecutarQueryInsertComRetornoDeID(query.ToString(), parametros);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void EditarDepartamento(DepartamentoViewModel departamento, int IdUsuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("UPDATE FORM_DEPARTAMENTO ")
                .Append("SET    NOME = @NOME, ")
                .Append("       USUARIO_ALTERACAO = @USUARIO_ALTERACAO, ")
                .Append("       DATA_ALTERACAO = Getdate() ")
                .Append("WHERE  ID = @ID");

                var parametros = new SqlParameter[]
                {
                        new SqlParameter("@ID", departamento.ID),
                        new SqlParameter("@NOME", departamento.NOME),
                        new SqlParameter("@USUARIO_ALTERACAO", IdUsuario)

                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void ExcluirDepartamento(int Id, int idUsuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                    .Append("UPDATE FORM_DEPARTAMENTO ")
                    .Append("SET    D_E_L_E_T = '*', ")
                    .Append("       USUARIO_ALTERACAO = @USUARIO_ALTERACAO, ")
                    .Append("       DATA_ALTERACAO = GETDATE() ")
                    .Append("WHERE  ID = @ID");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@USUARIO_ALTERACAO", idUsuario),
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
