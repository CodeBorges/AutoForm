using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoForms.Models
{
    public class Usuario
    {
        public int ID { get; set; }

        [Required]
        public string NOME { get; set; }

        [Required]
        public int USUARIO { get; set; }

        [Required]
        public string SENHA { get; set; }

        [Required]
        public string EMAIL { get; set; }

        [Required]
        public bool MUDAR_SENHA { get; set; }

        [Required]
        public int? ID_DEPARTAMENTO { get; set; }

        [Required]
        public int? ID_TIPO_USUARIO { get; set; }

        public DateTime? DATA_CADASTRO { get; set; }

        public DateTime? DATA_ALTERACAO { get; set; }

        public int? USUARIO_CADASTRO { get; set; }

        public int? USUARIO_ALTERACAO { get; set; }

        public string D_E_L_E_T { get; set; }
    }
}
