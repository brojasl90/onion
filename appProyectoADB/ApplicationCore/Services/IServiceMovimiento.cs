using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceMovimiento
    {
        TipoMovimiento GetTipoMovimientoByID(int pId);

        IEnumerable<TipoMovimiento> GetTipoMovimiento();
    }
}
