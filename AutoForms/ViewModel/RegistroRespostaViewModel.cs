using AutoForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.ViewModel
{
    public class RegistroRespostaViewModel : RegistroResposta
    {
        public int ID_CATEGORIA { get; set; }

        public int META { get; set; }

        public List<RegistroRespostaViewModel> RegistroResposta { get; set; }

    }
}
