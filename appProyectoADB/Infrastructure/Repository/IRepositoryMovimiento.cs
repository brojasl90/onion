using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryMovimiento
    {
        TipoMovimiento GetTipoMovimientoByID(int pId);

        IEnumerable<TipoMovimiento> GetTipoMovimiento();
    }
}
