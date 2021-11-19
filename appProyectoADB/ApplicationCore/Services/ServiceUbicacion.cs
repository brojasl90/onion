using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUbicacion : IServiceUbicacion
    {
        public IEnumerable<Ubicacion> GetUbicacion()
        {
            IRepositoryUbicacion repository = new RepositoryUbicacion();
            return repository.GetUbicacion();
        }

        public Ubicacion GetUbicacionByID(int id)
        {
            IRepositoryUbicacion repository = new RepositoryUbicacion();
            return repository.GetUbicacionByID(id);
        }
    }
}
