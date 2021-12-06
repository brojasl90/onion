using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Enum;
using Web.Utils;

namespace Web.ViewModel
{
    public class Movimiento
    {
        public List<ViewModelLineaInventario> Items { get; private set; }

        //Implementación Singleton

        // Las propiedades de solo lectura solo se pueden establecer en la inicialización o en un constructor
        public static readonly Movimiento Instancia;

        // Se llama al constructor estático tan pronto como la clase se carga en la memoria
        static Movimiento()
        {
            // Si el carrito no está en la sesión, cree uno y guarde los items.
            if (HttpContext.Current.Session["carrito"] == null)
            {
                Instancia = new Movimiento();
                Instancia.Items = new List<ViewModelLineaInventario>();
                HttpContext.Current.Session["carrito"] = Instancia;
            }
            else
            {
                // De lo contrario, obténgalo de la sesión.
                Instancia = (Movimiento)HttpContext.Current.Session["carrito"];
            }
        }

        // Un constructor protegido asegura que un objeto no se puede crear desde el exterior
        protected Movimiento() { }

        /**
         * AgregarItem (): agrega un artículo a la compra
         */
        public String AgregarItem(int pIdProd)
        {
            String mensaje = "";
            // Crear un nuevo artículo para agregar al carrito
            ViewModelLineaInventario nuevoItem = new ViewModelLineaInventario(pIdProd);
            // Si este artículo ya existe en lista de libros, aumente la Cantidad
            // De lo contrario, agregue el nuevo elemento a la lista
            if (nuevoItem != null)
            {
                if (Items.Exists(x => x.IdProducto == pIdProd))
                {
                    ViewModelLineaInventario item = Items.Find(x => x.IdProducto == pIdProd);
                    item.Cantidad++;
                }
                else
                {
                    nuevoItem.Cantidad = 1;
                    Items.Add(nuevoItem);
                }
                mensaje = SweetAlertHelper.Mensaje("Producto agregado", "Producto: " + nuevoItem.Producto.Nombre_Producto , SweetAlertMessageType.success);

            }
            else
            {
                mensaje = SweetAlertHelper.Mensaje("El Producto solicitado no existe", "", SweetAlertMessageType.warning);
            }
            return mensaje;
        }


        /**
         * SetItemCantidad(): cambia la Cantidad de un artículo en el carrito
         */
        public String SetItemCantidad(int pIdProd, int Cantidad)
        {
            String mensaje = "";
            // Si estamos configurando la Cantidad a 0, elimine el artículo por completo
            if (Cantidad == 0)
            {
                EliminarItem(pIdProd);
                mensaje = SweetAlertHelper.Mensaje("Producto eliminado", "Producto seleccionado", SweetAlertMessageType.success);

            }
            else
            {
                // Encuentra el artículo y actualiza la Cantidad
                ViewModelLineaInventario actualizarItem = new ViewModelLineaInventario(pIdProd);
                if (Items.Exists(x => x.IdProducto == pIdProd))
                {
                    ViewModelLineaInventario item = Items.Find(x => x.IdProducto == pIdProd);
                    item.Cantidad = Cantidad;
                    mensaje = SweetAlertHelper.Mensaje("Cantidad actualizada", "Producto: " + item.Producto.Nombre_Producto, SweetAlertMessageType.success);

                }
            }
            return mensaje;

        }

        public String SetItemTipoMovi(int pIdProd, int pTipM)
        {
            String mensaje = "";

                ViewModelLineaInventario actualizarItem = new ViewModelLineaInventario(pIdProd);
                if (Items.Exists(x => x.IdProducto == pIdProd))
                {
                    ViewModelLineaInventario item = Items.Find(x => x.IdProducto == pIdProd);
                    item.IdTipMovimiento = pTipM;
                    mensaje = SweetAlertHelper.Mensaje("Tipo de Movimiento actualizado", "Producto: " + item.Producto.Nombre_Producto, SweetAlertMessageType.success);

                }

            return mensaje;
        }

        /**
         * EliminarItem (): elimina un artículo del carrito de compras
         */
        public String EliminarItem(int pIdProd)
        {
            String mensaje = "El Producto no existe";
            if (Items.Exists(x => x.IdProducto == pIdProd))
            {
                var itemEliminar = Items.Single(x => x.IdProducto == pIdProd);
                Items.Remove(itemEliminar);
                mensaje = SweetAlertHelper.Mensaje("Elemento eliminado", "Producto: " + itemEliminar.Producto.Nombre_Producto, SweetAlertMessageType.success);
            }
            return mensaje;

        }

        /**
         * GetTotal() - Devuelve el precio total de todos los libros.
         */
        public decimal GetTotal()
        {
            decimal total = 0;
            total = Items.Sum(x => x.SubTotal);

            return total;
        }
        public int GetCountItems()
        {
            int total = 0;
            total = Items.Sum(x => x.Cantidad);

            return total;
        }
        /**
         * GetTotalPeso() - Devuelve el total de peso de todos los libros.
         */

        public void eliminarCarrito()
        {
            Items.Clear();

        }
    }
}