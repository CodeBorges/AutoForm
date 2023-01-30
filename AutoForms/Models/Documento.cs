using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.Models
{
    public class Documento
    {
        public int ID { get; set; }

        public int ID_FORMULARIO { get; set; }

        public int ID_USUARIO { get; set; }

        public double MEDIA_GERAL { get; set; }

        [Required]
        public string USUARIO_AUDITADO { get; set; }

        [Required]
        public int ID_DEPARTAMENTO { get; set; }

        [Required]
        public int ID_LOCALIZACAO { get; set; }

        public DateTime DATA_CADASTRO { get; set; }

        public string D_E_L_E_T { get; set; }

        public bool FINALIZADO { get; set; }
    }
}
