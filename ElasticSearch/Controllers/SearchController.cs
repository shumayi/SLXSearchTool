using ElasticSearch.DomainModel;
using ElasticSearch.SearchProvider;
using ElasticsearchCRUD;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElasticSearch.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchProduct(int Id)
        {
            ElasticClient client;
            ElasticSearchMappingAddress eSearch = new ElasticSearchMappingAddress();

            var productModel = new Product();

            client = eSearch.createConnection();
            //eSearch.indexingProduct();

            var searchResults = client.Search<Product>(s => s
                 .From(0)
                 .Size(10)
                 .Query(q => q
                     .Term(p => p.ProductID, Id)
                 )
             );
            
            if (searchResults.Documents.Count() > 0)
            {
                productModel.ProductID = Id;
                productModel.Name = searchResults.Documents.Single().Name.ToString();
            }
            
            return View(productModel);
        }
    }
}