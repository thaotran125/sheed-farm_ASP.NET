using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CayGiong.Models;
using CayGiong.ViewModel;
using PagedList;

namespace CayGiong.Dao
{
    public class DaoProduct
    {
        public List<ProductViewModel> loadProductPageList(int page, int pageSize)
        {
            List<ProductViewModel> listProVM = new List<ProductViewModel>();
            int offset = (page - 1) * pageSize;
            using (var context = new DB_SEED_FARMEntities())
            {
                var pros = context.Products.Include("Category").ToList().Skip(offset).Take(pageSize);
                foreach (var item in pros)
                {
                    listProVM.Add(new ProductViewModel
                    {
                        id = item.id_Product,
                        name = item.name_product,
                        url = item.url_Product,
                        img = item.img_Prodcut,
                        title = item.title,
                        id_cate = item.id_Cate,
                        name_cate = item.Category.name_Cate,
                        price = item.price,
                        status=item.status,
                    });
                }
                return listProVM;
            }
        }

        public List<Product> PagelistAdmin()
        {
            
            using (var context = new DB_SEED_FARMEntities())
            {
                List<Product> pros = (List<Product>)context.Products.Include("Category").OrderByDescending(x=>x.id_Product).ToList();
                
                return pros;
            }
        }

        public List<TopProductViewModel> getIdTopProducct()
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                var result = context.Order_detal.GroupBy(x => x.id_Product).
                    Select(n => new TopProductViewModel
                    {
                        id_Product = n.FirstOrDefault().id_Product,
                        SumQuantity = n.Sum(c => c.quantity),
                    }).OrderBy(x => x.SumQuantity).Take(4).ToList();
                return result;
            }
        }

        public List<Product> LoadTopProduct()
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                List<Product> result = new List<Product>();
                List<TopProductViewModel> listID = getIdTopProducct();
                foreach (var item in listID)
                {
                    Product pro = new Product();
                    pro = context.Products.Where(x => x.id_Product == item.id_Product).FirstOrDefault();
                    result.Add(pro);
                }
                return result;
            }
        }

        public List<Product> loadProdcuctNew()
        {
            using (var contect = new DB_SEED_FARMEntities())
            {
                return contect.Products.OrderByDescending(x => x.id_Product).Take(3).ToList();
            }
        }

        public List<Product> loadAddProduct()
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                return context.Products.OrderByDescending(x => x.id_Product).ToList();
            }
        }

        public List<Product> loadProduct(int limitpage)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                return context.Products.OrderBy(x => x.id_Product).Take(limitpage).ToList();
            }
        }

        public Product getDetail(int id)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                return context.Products.Find(id);
            }
        }

        public ProductViewModel DetailProductVM(int id)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                Product product = new Product();
                ProductViewModel productvm;

                product = context.Products.Include("Category").Where(x => x.id_Product == id).SingleOrDefault();

                productvm = new ProductViewModel()
                {
                    id = product.id_Product,
                    name = product.name_product,
                    url = product.url_Product,
                    img = product.img_Prodcut,
                    title = product.title,
                    id_cate = product.id_Cate,
                    name_cate = product.Category.name_Cate,
                    Group_id = product.Category.Group_id,
                    price = product.price,
                    status=product.status,
                    descript=product.descript,
                };
                var cate = context.Categories.Where(x => x.id_Cate == productvm.Group_id).SingleOrDefault();
                productvm.group_name = cate.name_Cate;
                return productvm;
            }
        }

        public List<Product> loadRelateProduct(int id)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                Product product = context.Products.Find(id);
                return context.Products.Where(x => x.id_Product != id && x.id_Cate == product.id_Cate).ToList();

            }
        }

        public List<Product> PageList(int? page, int limitPage)
        {
            int offset = (int)(page - 1) * limitPage;
            using (var context = new DB_SEED_FARMEntities())
            {
                // random product :OrderBy(x => Guid.NewGuid())
                return context.Products.OrderBy(x => x.id_Product).Skip(offset).Take(limitPage).ToList();

            }
        }

      
        public int Insert_Product(ProductViewModel productvm)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                if (context.Products.Find(productvm.name) == null)
                {
                    Product product = new Product()
                    {
                        name_product = productvm.name,
                        url_Product = productvm.url,
                        img_Prodcut = productvm.img,
                        title = productvm.title,
                        id_Cate = productvm.id_cate,
                        price = productvm.price,
                        status = 1,
                        descript = productvm.descript,
                    };
                    context.Products.Add(product);
                    context.SaveChanges();
                    return 1;
                }
                else
                    return 0;
               
            }
        }

        public int Edit_Product(ProductViewModel productvm)
        {
            try
            {
                using (DB_SEED_FARMEntities context = new DB_SEED_FARMEntities())
                {
                    Product p = context.Products.Find(productvm.id);
                    if (productvm.img == null)
                    {
                        p.name_product = productvm.name;
                        p.url_Product = productvm.url;
                        p.title = productvm.title;
                        p.id_Cate = productvm.id_cate;
                        p.price = productvm.price;
                        p.descript = productvm.descript;
                    }
                    else
                    {
                        p.name_product = productvm.name;
                        p.url_Product = productvm.url;
                        p.img_Prodcut = productvm.img;
                        p.title = productvm.title;
                        p.id_Cate = productvm.id_cate;
                        p.price = productvm.price;
                        p.descript = productvm.descript;
                    }
                    context.SaveChanges();
                    return 1;
                }
            }
            catch 
            {
                return 0;
            }
       
        }

        public void del_Product(int id)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                Product product = context.Products.Find(id);
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }

        public List<Product> search_Product(string textSearch)
        {
            using(var context=new DB_SEED_FARMEntities())
            {
                List<Product> list = new List<Product>();
                list = context.Products.Include("Category").Where(x => x.name_product.Contains(textSearch)).ToList();
                return list;
            }
        }
    }
}