using Azure;
using Azure.Data.Tables;
using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Model
{
    public class Estudio : IEstudio, ITableEntity
    {
        public string Estudioo { get ; set ; }
        public string Afiliado { get ; set ; }
        public string Grado { get ; set ; }
        public string Titulo { get ; set ; }
        public string Institucion { get ; set ; }
        public int Anio { get ; set ; }
        public bool Estado { get ; set ; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
