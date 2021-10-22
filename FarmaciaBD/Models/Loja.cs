using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaciaBD.Models
{
    public class Loja
    {
        [Required(ErrorMessage = "Preencha esse campo, ele é obrigatório")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "Preencha esse campo, ele é obrigatório")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Preencha esse campo, ele é obrigatório")]
        public string Endereco { get; set; }
    }

}
