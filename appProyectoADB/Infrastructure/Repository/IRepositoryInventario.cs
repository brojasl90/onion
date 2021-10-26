﻿using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryInventario
    {
        IEnumerable<GestionInventario> GetInventario();

        IEnumerable<GestionInventario> GetInventarioPorTipMovimiento(int pTipo);

        IEnumerable<GestionInventario> GetInventarioPorUsuario(int pId);


    }
}
