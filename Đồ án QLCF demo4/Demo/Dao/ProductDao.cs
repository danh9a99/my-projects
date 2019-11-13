﻿using Model.EF;
using Model.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        private webQLCFCode db = null;

        public ProductDao()
        {
            db = new webQLCFCode();
        }

        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }
        public bool Delete(int entity)
        {
            try
            {
                var product = db.Products.Find(entity);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(Product entity)
        {
            try
            {
                var product = db.Products.Find(entity.ID);
                product.Name = entity.Name;
                product.Code = entity.Code;
                product.MetaTitle = entity.MetaTitle;
                product.Description = entity.Description;
                product.MoreImages = entity.MoreImages;
                product.Price = entity.Price;
                product.PromotionPrice = entity.PromotionPrice;
                product.IncludedVAT = entity.IncludedVAT;
                product.Quantity = entity.Quantity;
                product.CategoryID = entity.CategoryID;
                product.Detail = entity.Detail;
                product.Warranty = entity.Warranty;
                product.CreatedDate = entity.CreatedDate;
                product.CreatedBy = entity.CreatedBy;
                product.ModifiedBy = entity.ModifiedBy;
                product.ModifiedDate = entity.ModifiedDate;
                product.MetaKeywords = entity.MetaKeywords;
                product.MetaDescriptions = entity.MetaDescriptions;
                product.Status = entity.Status;
                product.TopHot = entity.TopHot;
                product.ViewCount = entity.ViewCount;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }
        }
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        /// <summary>
        /// Get list product by category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        //public List<ProductViewModel> ListByCategoryId(long categoryID, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        //{
        //    totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
        //    var model = (from a in db.Products
        //                 join b in db.ProductCategories
        //                 on a.CategoryID equals b.ID
        //                 where a.CategoryID == categoryID
        //                 select new
        //                 {
        //                     CateMetaTitle = b.MetaTitle,
        //                     CateName = b.Name,
        //                     CreatedDate = a.CreatedDate,
        //                     ID = a.ID,
        //                     Images = a.Image,
        //                     Name = a.Name,
        //                     MetaTitle = a.MetaTitle,
        //                     Price = a.Price
        //                 }).AsEnumerable().Select(x => new ProductViewModel()
        //                 {
        //                     CateMetaTitle = x.MetaTitle,
        //                     CateName = x.Name,
        //                     CreatedDate = x.CreatedDate,
        //                     ID = x.ID,
        //                     Images = x.Images,
        //                     Name = x.Name,
        //                     MetaTitle = x.MetaTitle,
        //                     Price = x.Price
        //                 });
        //    model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //    return model.ToList();
        //}
        //public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        //{
        //    totalRecord = db.Products.Where(x => x.Name == keyword).Count();
        //    var model = (from a in db.Products
        //                 join b in db.ProductCategories
        //                 on a.CategoryID equals b.ID
        //                 where a.Name.Contains(keyword)
        //                 select new
        //                 {
        //                     CateMetaTitle = b.MetaTitle,
        //                     CateName = b.Name,
        //                     CreatedDate = a.CreatedDate,
        //                     ID = a.ID,
        //                     Images = a.Image,
        //                     Name = a.Name,
        //                     MetaTitle = a.MetaTitle,
        //                     Price = a.Price
        //                 }).AsEnumerable().Select(x => new ProductViewModel()
        //                 {
        //                     CateMetaTitle = x.MetaTitle,
        //                     CateName = x.Name,
        //                     CreatedDate = x.CreatedDate,
        //                     ID = x.ID,
        //                     Images = x.Images,
        //                     Name = x.Name,
        //                     MetaTitle = x.MetaTitle,
        //                     Price = x.Price
        //                 });
        //    model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //    return model.ToList();
        //}
        ///// <summary>
        ///// List feature product
        ///// </summary>
        ///// <param name="top"></param>
        ///// <returns></returns>
        //public List<Product> ListFeatureProduct(int top)
        //{
        //    return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        //}
        //public List<Product> ListRelatedProducts(long productId)
        //{
        //    var product = db.Products.Find(productId);
        //    return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        //}

        //public void UpdateImages(long productId, string images)
        //{
        //    var product = db.Products.Find(productId);
        //    product.MoreImages = images;
        //    db.SaveChanges();
        //}
        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
    }
}
