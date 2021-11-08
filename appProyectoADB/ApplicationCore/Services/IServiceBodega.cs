using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceBodega
    {
        IEnumerable<Bodega> GetBodega();
        Bodega GetBodegaByID(int id);
        Bodega Save(Bodega pBodega);
        void DeleteBodega(int id);
    }
}
