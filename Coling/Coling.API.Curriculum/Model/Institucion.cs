﻿using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Azure;

namespace Coling.API.Curriculum.Model
{
    public class Institucion : IInstitucion,ITableEntity
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string TipoInstitucion { get; set; }
        public bool Estado { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
