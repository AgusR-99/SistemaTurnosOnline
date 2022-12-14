﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTurnosOnline.Models
{
    public class Profesor
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public bool Estado { get; set; }
        public string Password { get; set; }
        public List<string> CarrerasId { get; set; }
    }
}
