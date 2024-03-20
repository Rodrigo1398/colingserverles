using Coling.API.Curriculum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.interfaces.Repositorio
{
    public interface IAfiliadoRepositorio
    {
        public Task<bool> Create(Afiliado afiliado);
        public Task<bool> Update(Afiliado afiliado);
        public Task<bool> Delete(string partitionkey, string rowkey);
        public Task<Afiliado> Get(string id);
        public Task<List<Afiliado>> GetAll();
    }
}
