using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaciaBD.Models
{
    public class Fornecedor
    {
        
        public int Matricula { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Localizacao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int Lote { get; set; }
    }
}
