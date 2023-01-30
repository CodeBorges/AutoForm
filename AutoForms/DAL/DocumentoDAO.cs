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
    public class DocumentoDAO
    {
        public static int GerarDocumento(int idFormulario, int idUsuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("INSERT INTO FORM_DOCUMENTO  ")
                .Append("            (ID_FORMULARIO,  ")
                .Append("             ID_USUARIO,  ")
                .Append("             MEDIA_GERAL,  ")
                .Append("             ID_DEPARTAMENTO,  ")
                .Append("             ID_LOCALIZACAO,  ")
                .Append("             DATA_CADASTRO,  ")
                .Append("             FINALIZADO,  ")
                .Append("             D_E_L_E_T)  ")
                .Append("output         inserted.ID  ")
                .Append("VALUES      (@ID_FORMULARIO,  ")
                .Append("             @ID_USUARIO,  ")
                .Append("             0, ")
                .Append("             0, ")
                .Append("             0, ")
                .Append("             Getdate(),  ")
                .Append("             0, ")
                .Append("             ''); ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_FORMULARIO", idFormulario),
                    new SqlParameter("@ID_USUARIO", idUsuario)
                };

                return AcessosDAO.ExecutarQueryInsertComRetornoDeID(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static void SalvarInformacoes(DocumentoViewModel documento)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("UPDATE FORM_DOCUMENTO   ")
                .Append("SET    ID_DEPARTAMENTO = @ID_DEPARTAMENTO,   ")
                .Append("       ID_LOCALIZACAO = @ID_LOCALIZACAO,   ")
                .Append("       USUARIO_AUDITADO = @USUARIO_AUDITADO   ")
                .Append("WHERE  ID = @ID ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_DEPARTAMENTO", documento.ID_DEPARTAMENTO),
                    new SqlParameter("@ID_LOCALIZACAO", documento.ID_LOCALIZACAO),
                    new SqlParameter("@USUARIO_AUDITADO", documento.USUARIO_AUDITADO.ToUpper().Trim()),
                    new SqlParameter("@ID", documento.ID),
                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void SalvarMediaGeral(DocumentoViewModel documento)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("UPDATE FORM_DOCUMENTO   ")
                .Append("SET    MEDIA_GERAL = @MEDIA_GERAL   ")
                .Append("WHERE  ID = @ID ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@MEDIA_GERAL", documento.MEDIA_GERAL),
                    new SqlParameter("@ID", documento.ID),
                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void DeletarDocumento(int idDocumento)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("DELETE FROM FORM_DOCUMENTO   ")
                .Append("WHERE  ID = @ID_DOCUMENTO  ");

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

        public static void FinalizarDocumento(int idDocumento)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("UPDATE FORM_DOCUMENTO   ")
                .Append("SET    FINALIZADO = 1   ")
                .Append("WHERE  ID = @ID ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID", idDocumento)
                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DocumentoViewModel RetornarDocumentoPorId(int idDocumento)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("                SELECT *  ")
                .Append("FROM   FORM_DOCUMENTO  ")
                .Append("WHERE  ID = @ID  ")
                .Append("       AND D_E_L_E_T != '*' ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID", idDocumento)
                };

                return AcessosDAO.ExecutarQueryComRetorno<DocumentoViewModel>(query.ToString(), parametros).FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DocumentoViewModel RetornarDocumentoPendentePorIdUsuario(int idUsuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT TOP 1 *  ")
                .Append("FROM   FORM_DOCUMENTO  ")
                .Append("WHERE  ID_USUARIO = @ID_USUARIO  ")
                .Append("AND FINALIZADO = 0  ")
                .Append("ORDER BY ID DESC  ");


                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_USUARIO", idUsuario)
                };

                return AcessosDAO.ExecutarQueryComRetorno<DocumentoViewModel>(query.ToString(), parametros).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<DocumentoViewModel> RetornarDocumentoFinalizadoPorIdFormulario(int idFormulario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT B.NOME AS NOME_USUARIO,   ")
                .Append("       A.*   ")
                .Append("FROM FORM_DOCUMENTO AS A   ")
                .Append("INNER JOIN FORM_USUARIO AS B ON A.ID_USUARIO = B.ID   ")
                .Append("WHERE ID_FORMULARIO = @ID_FORMULARIO   ")
                .Append("  AND FINALIZADO = 0 ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_FORMULARIO", idFormulario)
                };

                return AcessosDAO.ExecutarQueryComRetorno<DocumentoViewModel>(query.ToString(), parametros);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<DocumentoViewModel> RetornarDocumentoPorIdUsuario(int idUsuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT A.*,   ")
                .Append("B.NOME AS NOME_FORMULARIO   ")
                .Append("FROM FORM_DOCUMENTO AS A    ")
                .Append("INNER JOIN FORM_FORMULARIO AS B   ")
                .Append("ON A.ID_FORMULARIO = B.ID   ")
                .Append("WHERE ID_USUARIO = @ID_USUARIO  ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_USUARIO", idUsuario)
                };

                return AcessosDAO.ExecutarQueryComRetorno<DocumentoViewModel>(query.ToString(), parametros);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Documento> RetornarDocumentoFinalizadoIdUsuario(int idUsuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT *   ")
                .Append("FROM FORM_DOCUMENTO   ")
                .Append("WHERE ID_USUARIO = @ID_USUARIO ")
                .Append("AND FINALIZADO = 1 ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_USUARIO", idUsuario)
                };

                return AcessosDAO.ExecutarQueryComRetorno<Documento>(query.ToString(), parametros);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<DocumentoViewModel> RetornarDocumentoFinalizadoPorIdUsuario(int idUsuario)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("SELECT A.*,   ")
                .Append("       B.NOME AS NOME_FORMULARIO,   ")
                .Append("       C.NOME AS NOME_DEPARTAMENTO,   ")
                .Append("       D.NOME AS NOME_LOCALIZACAO   ")
                .Append("FROM   FORM_DOCUMENTO AS A   ")
                .Append("       INNER JOIN FORM_FORMULARIO AS B   ")
                .Append("               ON A.ID_FORMULARIO = B.ID   ")
                .Append("       INNER JOIN FORM_DEPARTAMENTO AS C   ")
                .Append("               ON A.ID_DEPARTAMENTO = C.ID   ")
                .Append("       INNER JOIN FORM_LOCALIZACAO AS D   ")
                .Append("               ON A.ID_LOCALIZACAO = D.ID   ")
                .Append("WHERE  ID_USUARIO = @ID_USUARIO   ")
                .Append("       AND FINALIZADO = 1  ")
                .Append(" ORDER BY A.ID DESC");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_USUARIO", idUsuario)
                };

                return AcessosDAO.ExecutarQueryComRetorno<DocumentoViewModel>(query.ToString(), parametros);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
