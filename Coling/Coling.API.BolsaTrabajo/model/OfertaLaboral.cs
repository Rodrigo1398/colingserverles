using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.BolsaTrabajo.model
{
    public class OfertaLaboral
    {

        [BsonId]
        [DataMember]
        public MongoDB.Bson.ObjectId Id { get; set; }
        [DataMember]
        public string IdInstitucion { get; set; }
        [DataMember]
        public DateTime FechaOferta { get; set; }
        [DataMember]
        public DateTime FechaLimite { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string TituloCargo { get; set; }
        [DataMember]
        public string TipoContrato { get; set; }
        [DataMember]
        public string TipoTrabajo { get; set; }
        [DataMember]
        public string Area { get; set; }
        [DataMember]
        public string[] Caracteristicas { get; set; }
        [DataMember]
        public bool Estado { get; set; }
    }
}
