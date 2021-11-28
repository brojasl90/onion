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

        //[Required(ErrorMessage = "{0} es un dato requerido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "No ha seleccionado una Categoria.")]
        [Display(Name = "Categoria")]
        public int IdCategoria { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe brindar un nombre")]
        [Display(Name = "Nombre Producto")]
        public string Nombre_Producto { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "No se ha denifido un precio.")]
        [Range(1, 10000000)]
        [Display(Name = "Precio Unitario")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^[0-9]+(\,[0-9]{1,2})?$", ErrorMessage = "{0} debe ser númerico y con dos decimales")]
        public Nullable<decimal> Precio { get; set; }

        //[Required(ErrorMessage = "{0} es un dato requerido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Indique una fecha de ingreso.")]
        [Display(Name = "Fecha de ingreso")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaCreacion { get; set; }

        //[Required(ErrorMessage = "{0} es un dato requerido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Indique una fecha de vencimiento.")]
        [Display(Name = "Vencimiento de garantía")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaVence { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "No se ha asignado un estado.")]
        public Nullable<byte> Estado { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Indica la cantidad disponible.")]
        [Range(1, 999)]
        [Display(Name = "Cantidad Disponible")]
        //[Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<int> CantidadDisponible { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "No se ha denifido una cantidad mínima.")]
        [Range(1, 999)]
        [Display(Name = "Cantidad Minima")]
        //[Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<int> CantidadStockMin { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "No se ha denifido una cantidad máxima.")]
        [Range(1, 999)]
        [Display(Name = "Cantidad Máxima")]
        //[Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<int> CantidadStockMax { get; set; }
    }

    internal partial class BodegaMetadata
    {
        [Display(Name = "Bodega")]
        public int IdBodega { get; set; }
        [Display(Name = "Ubicación")]
        public int IdUbicacion { get; set; }

    }

    internal partial class InventarioMetadada
    {
        [Display(Name ="Código")]
        public int IdGestionInventario { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "No se ha denifido un usuario.")]
        [Display(Name = "Cod. Usuario")]
        public int IdUsuario { get; set; }

        [Display(Name = "Tipo de Gestión")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el tipo de gestión.")]
        public string TipoGestion { get; set; }

        [Display(Name = "Cod. Movimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el tipo de movimiento.")]
        public int IdTipMovimiento { get; set; }

        [Display(Name = "Cantidad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe indicar una cantidad válida.")]
        [Range(1, 100)]
        public Nullable<int> CantidadProductoGestionado { get; set; }

        [Display(Name = "Modificado Por")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "No se ha denifido un usuario.")]
        public int UsuarioGestion { get; set; }
        [Display(Name = "Última Modificación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar una fecha")]
        [DataType(DataType.Date)]
        public System.DateTime FechaGestion { get; set; }

        [Display(Name = "Registrado por")]
        public virtual Usuario Usuario { get; set; }

        [Display(Name = "Tipo Movimiento")]
        public virtual TipoMovimiento TipoMovimiento { get; set; }

        [DataType(DataType.MultilineText)]
        public string Observaciones { get; set; }

        [Display(Name = "Producto Relacionado")]
        [Required(ErrorMessage = "Debe seleccionar un producto.")]
        public virtual ICollection<Producto> Producto { get; set; }
    }

    internal partial class ProveedorMetadata
    {
        [Display(Name = "Código")]
        public int IdProveedor { get; set; }
        [Display(Name = "Proveedor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe brindar el nombre del proveedor")]
        public string Nombre_Proveedor { get; set; }
        [Display(Name = "Descripción")]
        public string Dsc_Proveedor { get; set; }
        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe brindar un nombre de contacto")]
        public string Nombre_Contacto { get; set; }
        [Display(Name = "Teléfono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe brindar un número telefónico")]
        [StringLength(13, MinimumLength = 12, ErrorMessage = "El formato válido es +506 88888888")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }
        [Display(Name = "Correo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe brindar un correo")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }
        [Display(Name = "País")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe Seleccionar un País")]
        public string Pais { get; set; }
        [Display(Name = "Dirección")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe brindar una dirección")]
        [StringLength(120, MinimumLength = 12, ErrorMessage = "La dirección es muy breve, brinde más detalles")]
        [DataType(DataType.MultilineText)]
        public string Direccion { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "No se ha asignado un estado.")]
        public Nullable<byte> Estado { get; set; }  
        public virtual ICollection<Producto> Producto { get; set; }
    }

    public partial class UsuarioMetadata
    {
        [Display(Name = "Código")]
        public int IdUsuario { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe brindar un número de identificación")]
        [StringLength(9, MinimumLength = 4, ErrorMessage = "El formato no es válido")]
        [Display(Name = "Identificación")]
        public string NumeroIdentificacion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe proporcionar un nombre")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe proporcionar el primer apellido")]
        [Display(Name = "Primer Apellido")]
        public string PrimerApellido { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe proporcionar el segundo apellido")]
        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un tipo de rol")]
        [Display(Name = "Tipo Rol")]
        public int IdRol { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe brindar un correo")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} no tiene formato válido")]
        [Display(Name = "Correo")]
        public string Correo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe brindar una palabra secreta")]
        [Display(Name = "Contraseña")]
        public string Clave { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "No se ha asignado un estado.")]
        [Display(Name = "Estado")]
        public Nullable<byte> Estado { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un tipo de rol")]
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

    public partial class RolMetadata
    {
        [Display(Name = "Código")]
        public int IdRol { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe proporcionar un nombre o descripción")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "No se ha asignado un estado.")]
        [Display(Name = "Estado")]
        public Nullable<byte> Estado { get; set; }
    }
}
