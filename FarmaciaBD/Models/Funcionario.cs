using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaciaBD.Models
{
    public class Funcionario
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Matricula { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nascimento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Salario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int Loja_numero { get; set; }
    }
}
