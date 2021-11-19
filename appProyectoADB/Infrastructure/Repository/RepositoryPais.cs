using Infrastructure.APIs;
using Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryPais : IRepositoryPais
    {
        public IEnumerable<PaisAPI> GetPaisAPIs()
        {
            HttpClient client = new HttpClient();
            string path = "";
            string json = "";
            try
            {

                path = @"https://restcountries.com/v2/all";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());
                json = sr.ReadToEnd();
                IEnumerable<PaisAPI> lista = JSONGeneric<IEnumerable<PaisAPI>>.JSonToObject(json);


                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
    }
}
