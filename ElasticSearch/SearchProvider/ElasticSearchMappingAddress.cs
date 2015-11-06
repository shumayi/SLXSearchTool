using ElasticSearch.DomainModel;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElasticSearch.SearchProvider
{
    public class ElasticSearchMappingAddress
    {
        private ElasticClient client;

        public ElasticClient createConnection()
        {
            var node = new Uri("http://localhost:9200");

            var settings = new ConnectionSettings(node, defaultIndex: "my-application");
            settings.ExposeRawResponse(true);

            client = new ElasticClient(settings);

            return client;
        }

        public void indexingPerson()
        {
            var person = new Person
            {
                Id = "1",
                Firstname = "Tommy",
                Lastname = "Wu"
            };

            var index = client.Index(person);
        }

        public void indexingProduct()
        {
            using (var db = new SQLDomainModel())
            {
                foreach (var product in db.Products)
                {
                    var index = client.Index(product);
                }
            }
        }
    }
}