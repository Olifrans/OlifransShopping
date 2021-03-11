using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OlifransShopping.Models
{
    public class CategoriaDetalhes
    {
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "O Nome da Categoria é Obrigatório")]
        [StringLength(100, ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 charaters are allwed", MinimumLength = 3)]
        public string CategoriaNome { get; set; }
        public Nullable<bool> StatusAtivo { get; set; }
        public Nullable<bool> StatusDelete { get; set; }
    }

    public class ProdutoDetalhes
    {
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "O Nome do Produto é Obrigatório")]
        [StringLength(100, ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 charaters are allwed", MinimumLength = 3)]
        public string ProdutoNome { get; set; }
        [Required]
        [Range(1, 50)]
        public Nullable<int> CategoriaId { get; set; }
        public Nullable<bool> StatusAtivo { get; set; }
        public Nullable<bool> StatusDelete { get; set; }
        public Nullable<System.DateTime> CriadoData { get; set; }
        public Nullable<System.DateTime> ModificadoData { get; set; }
        [Required(ErrorMessage = "A Descrição é Obrigatório")]
        public string Descricao { get; set; }
        public string ProdutoImage { get; set; }
        public Nullable<bool> EmDestaque { get; set; }
        [Required]
        [Range(typeof(int), "1", "500", ErrorMessage = "Quantidade Invalida")]
        public Nullable<int> Quantidade { get; set; }
        [Required]
        [Range(typeof(double), "1", "200000", ErrorMessage = "Preço Invalido")]
        public Nullable<double> PrecoUnitario { get; set; }
        public Nullable<double> PrecoTotal { get; set; }
        public SelectList Categorias { get; set; }
    }
}