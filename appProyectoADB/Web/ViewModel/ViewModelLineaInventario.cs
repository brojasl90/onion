using ApplicationCore.Services;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class ViewModelLineaInventario
    {
        public int IdRegistro { get; set; }
        public int IdProducto { get; set; }
        public int IdGestionInventario { get; set; }
        public int IdUsuario { get; set; }
        public string TipoGestion { get; set; }
        public int IdTipMovimiento { get; set; }
        public Nullable<int> CantidadProductoGestionado { get; set; }
        public int UsuarioGestion { get; set; }
        public System.DateTime FechaGestion { get; set; }
        public string Observaciones { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio
        {
            get { return (decimal)Producto.Precio; }
        }

        public virtual Usuario Usuario { get; set; }
        public virtual TipoMovimiento TipoMovimiento { get; set; }        
        public virtual Producto Producto { get; set; }
        public virtual RegistroInventario RegistroInventario { get; set; }

        public ViewModelLineaInventario(int pIdProd)
        {
            IServiceProducto _ServiceLibro = new ServiceProducto();
            this.IdProducto = pIdProd;
            this.Producto = _ServiceLibro.GetProductoByID(pIdProd);
        }

        public decimal SubTotal
        {
            get
            {
                return calculoSubtotal();
            }
        }
        private decimal calculoSubtotal()
        {
            return this.Precio * this.Cantidad;
        }

    }
}