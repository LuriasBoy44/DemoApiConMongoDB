using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Elementos
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("nombre")]
        public string Nombre { get; set; }
        [BsonElement("alias")]
        public string Alias { get; set; }
        [BsonElement("debilidad")]
        public string[] Debilidad  { get; set; }
        [BsonElement("resistecia")]
        public string[] Resistecia { get; set; }
        [BsonElement("inmunidad")]
        public string[] Inmunidad { get; set; }
    }
}
