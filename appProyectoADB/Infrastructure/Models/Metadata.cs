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
        [Display(Name = "Código")]
        public int IdProducto { get; set; }

        [Display(Name = "Categoria")]
        //[Required(ErrorMessage = "{0} es un dato requerido")]
        public int IdCategoria { get; set; }

        [Display(Name = "Nombre Producto")]
        public string Nombre_Producto { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Precio Unitario")]
        public Nullable<decimal> Precio { get; set; }

        [Display(Name = "Fecha de creación")]
        //[Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<System.DateTime> FechaCreacion { get; set; }

        [Display(Name = "Fecha de vencimiento")]
        //[Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<System.DateTime> FechaVence { get; set; }

        public Nullable<byte> Estado { get; set; }

        [Display(Name = "Cantidad Disponible")]
        //[Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<int> CantidadDisponible { get; set; }

        [Display(Name = "Cantidad Minima")]
        //[Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<int> CantidadStockMin { get; set; }

        [Display(Name = "Cantidad Máxima")]
        //[Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<int> CantidadStockMax { get; set; }
    }

    internal partial class InventarioMetadada
    {
        [Display(Name ="Código")]
        public int IdGestionInventario { get; set; }

        [Display(Name = "Cod. Usuario")]
        public int IdUsuario { get; set; }

        [Display(Name = "Tipo de Gestión")]
        public string TipoGestion { get; set; }

        [Display(Name = "Cod. Movimiento")]
        public int IdTipMovimiento { get; set; }

        [Display(Name = "Cantidad")]
        public Nullable<int> CantidadProductoGestionado { get; set; }

        [Display(Name = "Modificado Por")]
        public int UsuarioGestion { get; set; }
        [Display(Name = "Última Modificación")]
        public System.DateTime FechaGestion { get; set; }

        [Display(Name = "Registrado por")]
        public virtual Usuario Usuario { get; set; }

        [Display(Name = "Tipo Movimiento")]
        public virtual TipoMovimiento TipoMovimiento { get; set; }

        [Display(Name = "Producto Relacionado")]
        public virtual ICollection<Producto> Producto { get; set; }
    }

    internal partial class ProveedorMetadata
    {
        [Display(Name = "Código")]
        public int IdProveedor { get; set; }
        [Display(Name = "Proveedor")]
        public string Nombre_Proveedor { get; set; }
        [Display(Name = "Descripción")]
        public string Dsc_Proveedor { get; set; }
        [Display(Name = "Contacto")]
        public string Nombre_Contacto { get; set; }
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        [Display(Name = "Correo")]
        public string Correo { get; set; }
        [Display(Name = "País")]
        public string Pais { get; set; }
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        public Nullable<byte> Estado { get; set; }  
        public virtual ICollection<Producto> Producto { get; set; }
    }

    public partial class UsuarioMetadata
    {
        [Display(Name = "Código")]
        public int IdUsuario { get; set; }
        [Display(Name = "Identificación")]
        public string NumeroIdentificacion { get; set; }
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Primer Apellido")]
        public string PrimerApellido { get; set; }
        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }
        [Display(Name = "Tipo Rol")]
        public int IdRol { get; set; }
        [Display(Name = "Correo")]
        public string Correo { get; set; }
        [Display(Name = "Contraseña")]
        public string Clave { get; set; }
        [Display(Name = "Estado")]
        public Nullable<byte> Estado { get; set; }
        [Display(Name = "Rol")]
        public virtual Rol Rol { get; set; }
    }

    public partial class LoginUserMetadata
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo vacío, proporcione su identificación.")]
        [StringLength(9, MinimumLength = 4, ErrorMessage = "El formato no es válido")]
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo vacío, proporcione su contraseña.")]
        [StringLength(9, MinimumLength = 4, ErrorMessage = "De ser entre 4 a 9 carácteres")]
        [Display(Name = "Contraseña")]
        public string Contrasenia { get; set; }
    }
}
