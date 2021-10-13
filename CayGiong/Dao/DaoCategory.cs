using CayGiong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CayGiong.Dao
{
    public class DaoCategory
    {
        public List<Category> loadCateParent()
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                return context.Categories.Where(x => x.Group_id == null).ToList();
            }
        }

        public List<Category> LoadCateGroup()
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                return context.Categories.Where(x => x.Group_id != null).ToList();
            }
        }

        public int CountCateGroup(int idParent)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                return context.Categories.Where(x => x.Group_id == idParent).Count();
            }
        }

        public List<Category> getCateGroup(int? id)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                return context.Categories.Where(x => x.Group_id == id).ToList();
            }
        }

        public List<Product> loadProductWithIdparent(int idParent)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                List<Product> list = context.Products.Include("Category").OrderByDescending(x => x.id_Product).Where(x => x.Category.Group_id == idParent).ToList();
                return list;
            }
        }

        public List<Product> loadProductWithIdcate(int idCate)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                List<Product> list = context.Products.OrderByDescending(x => x.id_Product).Where(x => x.id_Cate == idCate).ToList();
                return list;
            }
        }

        public int CreateCate(Category category)
        {

            using (var context = new DB_SEED_FARMEntities())
            {
                string name = category.name_Cate;
                if (context.Categories.Where(x => x.name_Cate.Equals(name)).SingleOrDefault() == null)
                {
                    context.Categories.Add(category);
                    context.SaveChanges();
                    return 1;
                }
                else if (category.name_Cate == null || category.url_Cate == null)
                    return -1;
                else
                    return 0;
            }
        }

        public int CreateCateChild(Category category, int idgroup)
        {

            using (var context = new DB_SEED_FARMEntities())
            {
                string name = category.name_Cate;
                if (context.Categories.Where(x => x.name_Cate.Equals(name)).SingleOrDefault() == null)
                {
                    category.Group_id = (int?)idgroup;
                    context.Categories.Add(category);
                    context.SaveChanges();
                    return 1;
                }
                else if (category.name_Cate == null || category.url_Cate == null)
                    return -1;
                else
                    return 0;
            }
        }

        public int EditCate(Category category)
        {
            using (DB_SEED_FARMEntities context = new DB_SEED_FARMEntities())
            {
                if (category.name_Cate == null || category.url_Cate == null)
                {
                    return 0;
                }
                else
                {
                    Category cate = context.Categories.Find(category.id_Cate);
                    cate.name_Cate = category.name_Cate;
                    cate.url_Cate = category.url_Cate;
                    context.SaveChanges();
                    return 1;
                }
            }
        }

        //public void EditCate(Category category)
        //{
        //    using (DB_SEED_FARMEntities context = new DB_SEED_FARMEntities())
        //    {
        //        Category cate = context.Categories.Find(category.id_Cate);
        //        cate.name_Cate = category.name_Cate;
        //        cate.url_Cate = category.url_Cate;
        //        context.SaveChanges();
        //    }
        //}

        public int DelCate(int id)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                var list = context.Categories.Where(x => x.Group_id == id).ToList();
                if (list.Count == 0)// check exeists cate child
                {
                    Category category = context.Categories.Where(x => x.id_Cate == id).SingleOrDefault();
                    context.Categories.Remove(category);
                    context.SaveChanges();
                    return 1;
                }
                else
                    return 0;
            }
        }

        public void CreateCategory(Category category)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                if (context.Categories.Find(category.name_Cate.Trim()) == null)
                {
                    context.Categories.Add(category);
                    context.SaveChanges();
                }
            }
        }

        public int DelCateChild(int id)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                var check = context.CheckDel(id).ToList();
                if (check.Count==0)
                {
                    Category category = context.Categories.Where(x => x.id_Cate == id).SingleOrDefault();
                    context.Categories.Remove(category);
                    context.SaveChanges();
                    return 1;
                }
                else return 0;
            }
        }

        public Category getCateByid(int id)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                return context.Categories.Find(id);
            }
        }

        public string getNameCateParent(int id)
        {
            using(var context=new DB_SEED_FARMEntities())
            {
                var c = context.Categories.Where(x => x.id_Cate == id).SingleOrDefault();
                string name = c.name_Cate;
                return name;
            }
        }

        public int getIdGroup(int id)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                var c = context.Categories.Where(x => x.id_Cate == id).SingleOrDefault();
                int idGroup = (int)c.Group_id;
                return idGroup;
            }
        }
    }
}