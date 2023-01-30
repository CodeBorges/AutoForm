using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.Models
{
    public class RegistroResposta
    {
        public int ID { get; set; }

        public int ID_PERGUNTA { get; set; }

        public int ID_DOCUMENTO { get; set; }

        public int? RESPOSTA { get; set; }

        public DateTime DATA_REGISTRO { get; set; }

    }
}
