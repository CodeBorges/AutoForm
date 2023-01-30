using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.Models
{
    public class RegistroObservacao
    {
        public int ID { get; set; }

        public int ID_DOCUMENTO { get; set; }

        public int ID_OBSERVACAO { get; set; }

        public string TEXTO_OBSERVACAO { get; set; }

        public DateTime DATA_CADASTRO { get; set; }

        public DateTime DATA_ALTERACAO { get; set; }

        public int USUARIO_CADASTRO { get; set; }

        public int USUARIO_ALTERACAO { get; set; }

        public int D_E_L_E_T { get; set; }
    }
}
