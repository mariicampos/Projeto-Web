using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Audaz.MVC.Compras.Models
{
    public class Pedido
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Required]
        public string Item1 { get; set; }

        [Required]
        [Range(0, 500, ErrorMessage = "A quantidade deve ser maior que 0")]
        public int Quantidade1 { get; set; }

        [Required]
        public string Item2 { get; set; }

        [Required]
        [Range(0, 500, ErrorMessage = "A quantidade deve ser maior que 0")]
        public int Quantidade2 { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class PedidosDBContext : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }
    }
}