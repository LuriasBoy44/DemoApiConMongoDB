using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Sevices
{
    public class ElementoService
    {
        private readonly IMongoCollection<Elementos> _elemento;

        public ElementoService(IBookSettings settings)
        {
                var cliente = new MongoClient(settings.Server);
                var database = cliente.GetDatabase(settings.Database);
                _elemento= database.GetCollection<Elementos>(settings.Collection); 
        }

        public List<Elementos> Get()
        { 
            return  _elemento.Find(d => true).ToList();  
        }

        public Elementos Create(Elementos e)
        {
            _elemento.InsertOne(e);
            return e;
        }
        public void Update(string id, Elementos e)
        {
            _elemento.ReplaceOne(e => e.Id == id, e);
        }

        public void Delete(string id)
        {
            _elemento.DeleteOne(e=>e.Id==id);
        }

    }
}
