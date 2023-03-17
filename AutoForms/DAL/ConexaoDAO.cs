using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.DAL
{
    public class ConexaoDAO
    {
        private static readonly string stringFormularioHomologacao = "Data Source=PC_TESTE; Database=programas; Uid=it; Pwd=123456; Trusted_Connection=false; Encrypt=False";

        public static string Formularios()
        {
#if DEBUG
            return stringFormularioHomologacao;

#else
            return stringFormularioHomologacao;
            //return stringFormularioProducao;
#endif
        }
    }
}
