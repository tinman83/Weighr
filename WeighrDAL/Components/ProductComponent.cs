using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeighrDAL.Models;

namespace WeighrDAL.Components
{
  public class ProductComponent
    {

        public void AddProduct(Product product)
        {
            using (var db=new WeighrContext())
            {
                db.Products.Add(product);
                db.SaveChanges();

            }
        }
        public void UpdateProduct(Product product)
        {
            using (var db = new WeighrContext())
            {
                db.Products.Update(product);
                db.SaveChanges();
            }
        }

        public Product GetProduct(string productCode)
        {
            using (var db = new WeighrContext())
            {
                return db.Products
                    .Where(p => p.ProductCode == productCode).FirstOrDefault();

            }
        }

        public Product GetProductById(int id)
        {
            using (var db = new WeighrContext())
            {
                return db.Products
                    .Where(p => p.ProductId == id).FirstOrDefault();

            }
        }

        public List<Product> GetProducts()
        {
            using (var db = new WeighrContext())
            {
                return db.Products.ToList();

            }
        }

        public List<Product> GetProductsByProductCode(string productCode)
        {
            using (var db = new WeighrContext())
            {
                return db.Products
                    .Where(p=>p.ProductCode==productCode)
                    .ToList();

            }
        }


    }
}
