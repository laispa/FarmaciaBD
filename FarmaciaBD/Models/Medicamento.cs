using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaciaBD.Models
{
    public class Medicamento
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Laboratorio { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Composicao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Tarja { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int Produto_codigo_de_barras { get; set; }
    }
}
