using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaciaBD.Models 
{ 

    public class Produto
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Codigo_de_Barras { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int Estoque { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public float Valor { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int Fornecedor_Matricula { get; set; }
         

    }
}
