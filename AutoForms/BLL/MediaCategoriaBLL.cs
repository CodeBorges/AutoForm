using AutoForms.DAL;
using AutoForms.Models;
using AutoForms.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.BLL
{
    public class MediaCategoriaBLL
    {
        public static Dictionary<string, string> CalculoMediaPorCategoria(List<RegistroRespostaViewModel> registroResposta)
        {
            try
            {
                var idDocumento = registroResposta.Select(s => s.ID_DOCUMENTO).FirstOrDefault();

                double meta = registroResposta.Select(s => s.META).FirstOrDefault();

                var dicionarioRegistroResposta = new Dictionary<string, string>();

                var quantidadePorCategoria =
                    registroResposta.GroupBy(g => g.ID_CATEGORIA).ToDictionary(d => d.Key, d => d.ToList());

                if (registroResposta.Count <= 0)
                {
                    dicionarioRegistroResposta = null;

                    return (dicionarioRegistroResposta);
                }

                foreach (var quantidade in quantidadePorCategoria)
                {
                    double media;

                    double quantidadeRespostas = quantidade.Value.Select(s => s.RESPOSTA).Count();

                    double somaRespostas = (int)quantidade.Value.Sum(su => su.RESPOSTA);

                    media = somaRespostas / quantidadeRespostas / meta * 100;


                    SalvarMediaCategoria(quantidade.Key, media, idDocumento);

                    dicionarioRegistroResposta.Add(quantidade.Key.ToString(), media.ToString());

                }

                
                return dicionarioRegistroResposta;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void SalvarMediaCategoria(int idCategoria, double media, int idDocumento)
        {
            try
            {
                var mediaCategoria = new MediaCategoria();
                mediaCategoria.ID_CATEGORIA = idCategoria;
                mediaCategoria.ID_DOCUMENTO = idDocumento;
                mediaCategoria.MEDIA = media;

                MediaCategoriaDAO.SalvarMediaPorCategoria(mediaCategoria);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void SalvarMediaGeral(int idDocumento)
        {
            try
            {
                var mediaCategoria = MediaCategoriaDAO.RetornarMediaCategoriaPorIdDocumento(idDocumento);

                double somaMediaCategoria = mediaCategoria.Sum(s => s.MEDIA);

                double mediaGeral = somaMediaCategoria / mediaCategoria.Count;

                var idDoc = new DocumentoViewModel();
                idDoc.MEDIA_GERAL = mediaGeral;
                idDoc.ID = idDocumento;

                DocumentoDAO.SalvarMediaGeral(idDoc);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
