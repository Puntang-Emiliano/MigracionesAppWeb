﻿using pasajeApp.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pasajeApp.Datos.Data.Repository.IRepository
{
    public interface ICategoriaRepository
    {
        public interface ICategoriaRepository : IRepository<Categoria> 
        {
            void Update(Categoria categoria);
        }

    }
}
