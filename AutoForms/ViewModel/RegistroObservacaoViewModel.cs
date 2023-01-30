using AutoForms.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.ViewModel
{
    public class RegistroObservacaoViewModel : ObservacaoViewModel
    {
        public int ID_DOCUMENTO { get; set; }

        public int ID_OBSERVACAO { get; set; }

        public string TEXTO_OBSERVACAO { get; set; }

        public DateTime DATA_REGISTRO { get; set; }

        public static List<RegistroObservacaoViewModel> RetornarRegistroObservacaoPorIdDocumento(int idDocumento)
        {
            return RegistroObservacaoDAO.RetornarRegistroObservacaoPorIdDocumento(idDocumento);
        }

    }
}
