using AutoForms.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace AutoForms.DAL
{
    public class AcessosDAO
    {
        public static void ExecutarQuerySemRetorno(string query, SqlParameter[] parametros = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConexaoDAO.Formularios()))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parametros != null && parametros.Length > 0)
                        {
                            cmd.Parameters.AddRange(parametros);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<T> ExecutarQueryComRetorno<T>(string query, SqlParameter[] parametros = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConexaoDAO.Formularios()))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parametros != null && parametros.Length > 0)
                        {
                            cmd.Parameters.AddRange(parametros);
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            var listaRetorno = ConversorGenerico.CriarLista<T>(reader);

                            return listaRetorno;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public static string ExecutarQueryComRetornoPorNome(string query, SqlParameter[] parametros = null)
        //{
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(ConexaoDAO.Formularios()))
        //        {
        //            conn.Open();

        //            using (SqlCommand cmd = new SqlCommand(query, conn))
        //            {
        //                if (parametros != null && parametros.Length > 0)
        //                {
        //                    cmd.Parameters.AddRange(parametros);
        //                }

        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        return true.ToString();
        //                    }
        //                    return false.ToString();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public static int ExecutarQueryInsertComRetornoDeID(string query, SqlParameter[] parametros = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConexaoDAO.Formularios()))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parametros != null && parametros.Length > 0)
                        {
                            cmd.Parameters.AddRange(parametros);
                        }

                        
                        return Convert.ToInt32(cmd.ExecuteScalar()); 
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
