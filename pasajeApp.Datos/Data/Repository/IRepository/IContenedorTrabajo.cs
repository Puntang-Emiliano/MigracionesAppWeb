﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pasajeApp.Datos.Data.Repository.IRepository
{
    internal interface IContenedorTrabajo: IDisposable
    {
        // Aqui se deben ir agregando los diferentes repositorios
        ICategoriaRepository Categoria {  get; }

        //IArticuloRepository Articulo {get;}
        void Save();

    }
}
