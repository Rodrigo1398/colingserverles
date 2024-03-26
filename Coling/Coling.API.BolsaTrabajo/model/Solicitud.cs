using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.BolsaTrabajo.model
{
    public class Solicitud
    {
        [BsonId]
        [DataMember]
        public MongoDB.Bson.ObjectId Id { get; set; }
        [DataMember]
        public string IdAfiliado { get; set; }
        [DataMember]
        public string NombreCompleto { get; set; }
        [DataMember]
        public DateTime FechaPostulacion { get; set; }
        [DataMember]
        public decimal PretencionSalarial { get; set; }
        [DataMember]
        public string Acercade { get; set; }
        [DataMember]
        public string IdOferta { get; set; }
    }
}
