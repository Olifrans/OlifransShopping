using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlifransShopping.Models
{
    public class CompraDetalhes
    {
        public int DetalhesCompraId { get; set; }
        public Nullable<int> ClienteId { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public string Pais { get; set; }
        [Required]
        public string CodigoPostal { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<double> Valorpago { get; set; }
        [Required]
        public string FormaDePagamento { get; set; }
    }
}