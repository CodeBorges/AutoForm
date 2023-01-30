using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.Models
{
    public class Formulario
    {
        public int ID { get; set; }

        [Required]
        public string NOME { get; set; }

        [Required]
        public int ID_DEPARTAMENTO { get; set; }

        [Required]
        public int META { get; set; }

        public DateTime? DATA_CADASTRO { get; set; }

        public DateTime? DATA_ALTERACAO { get; set; }

        public int? USUARIO_CADASTRO { get; set; }

        public int? USUARIO_ALTERACAO { get; set; }

        public string D_E_L_E_T { get; set; }
    }
}
