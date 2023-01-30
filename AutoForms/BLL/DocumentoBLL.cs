using AutoForms.DAL;
using AutoForms.Models;
using AutoForms.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.BLL
{
    public class DocumentoBLL
    {
        public static Documento VerificarDocumentoFinalizadoPorUsuario(int idUsuario)
        {
            try
            {
                var documento = DocumentoDAO.RetornarDocumentoPendentePorIdUsuario(idUsuario);
                if (documento == null)
                {
                    return documento;
                }
                if (documento.FINALIZADO)
                    return documento;
                else
                return documento;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void ExcluirDocumento(int idDocumento)
        {
            try
            {
                DocumentoDAO.DeletarDocumento(idDocumento);
                RegistroRespostaDAO.DeletarRegistroResposta(idDocumento);
                RegistroObservacaoDAO.DeletarRegistroObservacao(idDocumento);
                MediaCategoriaDAO.DeletarRegistroMediaCategoria(idDocumento);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
