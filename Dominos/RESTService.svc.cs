using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using Dominos.Models;

namespace Dominos
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RESTService
    {

        [OperationContract]
        [WebGet(UriTemplate = "/products/list", ResponseFormat = WebMessageFormat.Json)]
        public List<Product> List()
        {
            List<Product> products;

            using (ISession session = NHibernateSession.OpenSession())
            {
                products = session.Query<Product>().ToList();
            }


            return products;
        }

        [OperationContract]
        [WebGet(UriTemplate = "/products/get?id={id}", ResponseFormat = WebMessageFormat.Json)]
        public Product Get(int id)
        {
            Product product;

            using (ISession session = NHibernateSession.OpenSession())
            {
                product = session.Get<Product>(id);
            }


            return product;
        }

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/products/delete?id={id}", ResponseFormat = WebMessageFormat.Json)]
        public void Delete(int id)
        {
            Product product;

            using (ISession session = NHibernateSession.OpenSession())
            {
                product = session.Get<Product>(id);

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(product);
                    transaction.Commit();
                }
            }


            return;
        }


        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/products/create", ResponseFormat = WebMessageFormat.Json)]
        public Product Create(Product product)
        {

            using (ISession session = NHibernateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(product);
                    transaction.Commit();
                }
            }


            return product;
        }


        [OperationContract]
        [WebInvoke(Method = "PATCH", UriTemplate = "/products/update?id={id}", ResponseFormat = WebMessageFormat.Json)]
        public Product Update(int id, Product product)
        {
            Product productFromDb;

            using (ISession session = NHibernateSession.OpenSession())
            {
                productFromDb = session.Get<Product>(id);

                productFromDb.ProductName = product.ProductName;
                productFromDb.ProductPrice = product.ProductPrice;
                productFromDb.ProductImage = product.ProductImage;

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(productFromDb);
                    transaction.Commit();
                }
            }


            return productFromDb;
        }



    }
}
