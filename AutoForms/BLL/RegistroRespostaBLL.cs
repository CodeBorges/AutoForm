using AutoForms.DAL;
using AutoForms.Models;
using AutoForms.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.BLL
{
    public class RegistroRespostaBLL
    {
        public static void InsertRegistroRespostaVazio(int idFormulario, int documento)
        {
            try
            {
                foreach (var categoria in FormularioCategoriaDAO.RetornarCategoriaPorIdFormulario(idFormulario))
                {
                    var mediaCategoria = new MediaCategoria();
                    mediaCategoria.ID_DOCUMENTO = documento;
                    mediaCategoria.ID_CATEGORIA = categoria.ID_CATEGORIA;

                    MediaCategoriaDAO.InserirMediaPorCategoria(mediaCategoria);

                    foreach (var pergunta in PerguntaDAO.RetornarPerguntaPorIdCategoria(categoria.ID_CATEGORIA))
                    {
                        RegistroRespostaDAO.InsertRegistroResposta(pergunta.ID, documento);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DocumentoViewModel ValidarCriacaoDeFormulario(int idFormulario, int idUsuario)
        {
            var docPendente = DocumentoDAO.RetornarDocumentoPendentePorIdUsuario(idUsuario);

            if (docPendente != null)
            {
                docPendente.PENDENTE = true;
                return docPendente;
            }

            var idDocumentoGerado = DocumentoDAO.GerarDocumento(idFormulario, idUsuario);

            var docNovo = DocumentoDAO.RetornarDocumentoPorId(idDocumentoGerado);

            InsertRegistroRespostaVazio(docNovo.ID_FORMULARIO, docNovo.ID);

            RegistroObservacaoBLL.InsertRegistroObservacaoVazio(docNovo.ID_FORMULARIO, docNovo.ID_USUARIO, docNovo.ID);

            docNovo.PENDENTE = false;

            return docNovo;
        }

    }
}
