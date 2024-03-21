using Coling.API.Curriculum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.interfaces.Repositorio
{
    public interface IInstitucionRepositorio
    {
        public Task<bool> Create(Institucion institucion);
        public Task<bool> Update(Institucion institucion);
        public Task<bool> Delete(string partitionkey,string rowkey);
        public Task<Institucion> Get(string id);
        public Task<List<Institucion>> GetAll();
    }
}
