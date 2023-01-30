using AutoForms.DAL;
using AutoForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.Helpers
{
    public static class Utils
    {
        public enum TipoUsuario
        {
            ADMINISTRADOR = 1,
            USUARIO_PADRÃO = 2,
            USUARIO_AUDITOR = 3
        }

    }
}
