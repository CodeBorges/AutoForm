using AutoForms.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoForms.DAL
{
    public class MediaCategoriaDAO
    {
        public static void SalvarMediaPorCategoria(MediaCategoria mediaCategoria)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("UPDATE FORM_MEDIA_CATEGORIA   ")
                .Append("SET MEDIA = @MEDIA   ")
                .Append("WHERE ID_CATEGORIA = @ID_CATEGORIA ")
                .Append("AND ID_DOCUMENTO = @ID_DOCUMENTO ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@MEDIA", mediaCategoria.MEDIA),
                    new SqlParameter("@ID_CATEGORIA", mediaCategoria.ID_CATEGORIA),
                    new SqlParameter("@ID_DOCUMENTO", mediaCategoria.ID_DOCUMENTO)
                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void InserirMediaPorCategoria(MediaCategoria mediaCategoria)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("INSERT INTO FORM_MEDIA_CATEGORIA (ID_CATEGORIA, ID_DOCUMENTO, MEDIA)   ")
                .Append("VALUES (@ID_CATEGORIA, @ID_DOCUMENTO, 0) ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_DOCUMENTO", mediaCategoria.ID_DOCUMENTO),
                    new SqlParameter("@ID_CATEGORIA", mediaCategoria.ID_CATEGORIA)
                };

                AcessosDAO.ExecutarQuerySemRetorno(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<MediaCategoria> RetornarMediaCategoriaPorIdDocumento(int idDocumento)
        {
            try
            {
                var query = new StringBuilder();
                query
                .Append("SELECT *   ")
                .Append("FROM FORM_MEDIA_CATEGORIA   ")
                .Append("WHERE ID_DOCUMENTO = @ID_DOCUMENTO ");

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ID_DOCUMENTO",idDocumento)
                };

                return AcessosDAO.ExecutarQueryComRetorno<MediaCategoria>(query.ToString(), parametros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void DeletarRegistroMediaCategoria(int idDocumento)
        {
            try
            {
                var query = new StringBuilder();

                query
                .Append("DELETE FROM FORM_MEDIA_CATEGORIA   ")
                .Append("WHERE  ID_DOCUMENTO = @ID_DOCUMENTO  ");

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

    }
}
