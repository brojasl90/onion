using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        public Usuario GetTipoUsuarioByID(int pId)
        {
            Usuario oUsuario = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oUsuario = ctx.Usuario.Where(u => u.IdUsuario == pId).Include(r => r.Rol).FirstOrDefault();
            }

            return oUsuario;
        }

        public IEnumerable<Usuario> GetUsuario()
        {
            IEnumerable<Usuario> lista = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Usuario.Include(x => x.Rol).ToList();
            }

            return lista;
        }

        public Usuario GuardarUsuario(Usuario pUsuario)
        {
            int vRetorno = 0;
            Usuario oUsuario = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oUsuario = GetTipoUsuarioByID((int)pUsuario.IdUsuario);
                IRepositoryRol _RepoRol = new RepositoryRol();

                if (oUsuario == null)
                {
                    ctx.Usuario.Add(pUsuario);
                    vRetorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.Usuario.Add(pUsuario);
                    ctx.Entry(pUsuario).State = EntityState.Modified;
                    vRetorno = ctx.SaveChanges();
                }
            }

            if (vRetorno >= 0)
            {
                oUsuario = GetTipoUsuarioByID((int)pUsuario.IdUsuario);
            }

            return oUsuario;
        }
    }
}
