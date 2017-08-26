
using OSM.Data.Infrastructure;
using OSM.Data.Respositories;
using OSM.Model.Entities;
using System.Collections.Generic;

namespace OSM.Service
{
    public interface IProductCategoryService
    {
        void Add(ProductCategory productCategory);
        void Update(ProductCategory productCategory);
        void Delete(int id);
        IEnumerable<ProductCategory> GetAll();
        IEnumerable<ProductCategory> GetAll(string keyword);
        IEnumerable<ProductCategory> GetAllByParentId(int id);
        ProductCategory GetById(int id);
        void Save();
    }

    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryRepository _ProductCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public ProductCategoryService(IProductCategoryRepository ProductCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._ProductCategoryRepository = ProductCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        //New
        public void Add(ProductCategory productCategory)
        {
            _ProductCategoryRepository.Add(productCategory);
        }

        //New
        public void Update(ProductCategory productCategory)
        {
            _ProductCategoryRepository.Update(productCategory);
        }

        //New - Delete by Id
        public void Delete(int id)
        {
            _ProductCategoryRepository.GetSingle(id).isDeleted = true;
        }

        //New - Get all record
        public IEnumerable<ProductCategory> GetAll()
        {
            return _ProductCategoryRepository.GetMulti(x => x.isDeleted == false);
        }

        //New - Get all record from keyword
        public IEnumerable<ProductCategory> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _ProductCategoryRepository.GetMulti(x => x.isDeleted == false && (x.Name.Contains(keyword) || x.Description.Contains(keyword)));
            else
                return _ProductCategoryRepository.GetMulti(x => x.isDeleted == false);
        }

        //New - Get all record by parent id
        public IEnumerable<ProductCategory> GetAllByParentId(int parentId)
        {
            return _ProductCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
        }

        //New - Get ProCate by Id
        public ProductCategory GetById(int id)
        {
            return _ProductCategoryRepository.GetSingle(id);
        }

        public void Save()
        {
            _ProductCategoryRepository.Commit();
            _unitOfWork.CommitTransaction();
        }
    }
}