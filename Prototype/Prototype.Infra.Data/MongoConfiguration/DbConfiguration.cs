using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Infra.Data.MongoConfiguration
{
    public  class DbConfiguration
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
