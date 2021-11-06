using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceMovimiento : IServiceMovimiento
    {
        public IEnumerable<TipoMovimiento> GetTipoMovimiento()
        {
            IRepositoryMovimiento oRepMov = new RepositoryMovimiento();
            return oRepMov.GetTipoMovimiento();
        }

        public TipoMovimiento GetTipoMovimientoByID(int pId)
        {
            IRepositoryMovimiento oRepMov = new RepositoryMovimiento();
            return oRepMov.GetTipoMovimientoByID(pId);
        }
    }
}
