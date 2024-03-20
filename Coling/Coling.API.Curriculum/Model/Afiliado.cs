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
    public class Afiliado : IAfiliadoo, ITableEntity
    {
        public string Afiliadoo { get ; set ; }
        public string Profesion { get ; set ; }
        public DateTime FechaAsignacion { get ; set ; }
        public string NroSello { get ; set ; }
        public bool Estado { get ; set ; }
        public string PartitionKey { get ; set ; }
        public string RowKey { get ; set ; }
        public DateTimeOffset? Timestamp { get ; set ; }
        public ETag ETag { get ; set ; }
    }
}
