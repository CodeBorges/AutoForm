using AutoForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.ViewModel
{
    public class DashboardViewModel
    {
        public List<Documento> DocumentosFinalizados { get; set; }

        public Documento DocumentoPendente { get; set; }

        public Usuario UsuarioLogado { get; set; }
    }
}
