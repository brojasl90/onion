//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductoMetadata))]
    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            this.Bodega = new HashSet<Bodega>();
            this.GestionInventario = new HashSet<GestionInventario>();
            this.Proveedor = new HashSet<Proveedor>();
        }
    
        public int IdProducto { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre_Producto { get; set; }
        public string Descripcion { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaVence { get; set; }
        public Nullable<byte> Estado { get; set; }
        public Nullable<int> CantidadDisponible { get; set; }
        public Nullable<int> CantidadStockMin { get; set; }
        public Nullable<int> CantidadStockMax { get; set; }
    
        public virtual Categoria Categoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bodega> Bodega { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GestionInventario> GestionInventario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proveedor> Proveedor { get; set; }
    }
}
