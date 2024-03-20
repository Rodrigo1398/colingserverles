﻿using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.interfaces
{
    public interface IDireccion
    {
        public Task<bool> InsertarDireccion(Direccion direccion);
        public Task<bool> ModificarDireccion(Direccion direccion, int id);
        public Task<bool> EliminarDireccion(int id);
        public Task<List<Direccion>> ListarDirecciones();
        public Task<Direccion> ListarDireccionById(int id);
    }
}
