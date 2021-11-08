using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceBodega : IServiceBodega
    {
        public void DeleteBodega(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bodega> GetBodega()
        {
            IRepositoryBodega oRepBod= new RepositoryBodega();
            return oRepBod.GetBodega();
        }

        public Bodega GetBodegaByID(int id)
        {
            IRepositoryBodega oRepBod = new RepositoryBodega();
            return oRepBod.GetBodegaByID(id);
        }

        public Bodega Save(Bodega pBodega)
        {
            IRepositoryBodega oRepBod = new RepositoryBodega();
            return oRepBod.Save(pBodega);
        }
    }
}
