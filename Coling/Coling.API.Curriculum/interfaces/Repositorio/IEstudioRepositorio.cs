using Coling.API.Curriculum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.interfaces.Repositorio
{
    public interface IEstudioRepositorio
    {
        public Task<bool> Create(Estudio estudio);
        public Task<bool> Update(Estudio estudio);
        public Task<bool> Delete(string partitionkey, string rowkey);
        public Task<Estudio> Get(string id);
        public Task<List<Estudio>> GetAll();
    }
}
