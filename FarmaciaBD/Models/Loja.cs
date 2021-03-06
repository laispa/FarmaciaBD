using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaciaBD.Models
{
    public class Loja
    {
        public int Numero { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Endereco { get; set; }
    }

}
