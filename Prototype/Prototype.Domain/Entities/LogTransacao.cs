using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Prototype.Shared.Entities;

namespace Prototype.Domain.Entities
{
    public class LogTransacao
    {
        public LogTransacao()
        {
            this.Id = ObjectId.GenerateNewId();
            this.RegisterDate = DateTime.UtcNow;
        }
    
        [BsonId()]
        public ObjectId Id { get; set; }
        public Guid ServidorId { get; set; }

        public DateTime RegisterDate { get; set; }

        public string Observacao { get; set; }
 
    }
}
