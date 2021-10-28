﻿using System;
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

    internal partial class InventarioMetadada
    {
        [Display(Name ="ID")]
        public int IdGestionInventario { get; set; }

        [Display(Name = "Cod. Usuario")]
        public int IdUsuario { get; set; }

        [Display(Name = "Tipo Gestión")]
        public string TipoGestion { get; set; }

        [Display(Name = "Cod. Movimiento")]
        public int IdTipMovimiento { get; set; }

        [Display(Name = "Cantidad")]
        public Nullable<int> CantidadProductoGestionado { get; set; }

        [Display(Name = "Modificado por")]
        public int UsuarioGestion { get; set; }
        [Display(Name = "Modificado")]
        public System.DateTime FechaGestion { get; set; }

        [Display(Name = "Usuario")]
        public virtual Usuario Usuario { get; set; }

        [Display(Name = "Tipo Movimiento")]
        public virtual TipoMovimiento TipoMovimiento { get; set; }

        [Display(Name = "Producto")]
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
