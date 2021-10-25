using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    internal partial class ProductoMetadata
    {
        public int IdProducto { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IdCategoria { get; set; }

        [Display(Name = "Nombre Producto")]
        public string Nombre_Producto { get; set; }

        public string Descripcion { get; set; }

        public Nullable<decimal> Precio { get; set; }

        [Display(Name = "Fecha de creación")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<System.DateTime> FechaCreacion { get; set; }

        [Display(Name = "Fecha de vencimiento")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<System.DateTime> FechaVence { get; set; }

        public Nullable<byte> Estado { get; set; }

        [Display(Name = "Cantidad Disponible")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<int> CantidadDisponible { get; set; }

        [Display(Name = "Cantidad minima requerida")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<int> CantidadStockMin { get; set; }

        [Display(Name = "Cantidad máxima requerida")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<int> CantidadStockMax { get; set; }
    }
}
