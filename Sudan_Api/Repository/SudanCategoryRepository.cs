using Sudan_Api.Data;
using Sudan_Api.Interface;
using Sudan_Api.Models;

namespace Sudan_Api.Repository
{
    public class SudanCategoryRepository : ISudanCategoryRepository
    {
        private readonly DataContext _dataContext;

        public SudanCategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreateSudanCategories(SudanCategory sudanCategory)
        {
            _dataContext.Add(sudanCategory);
            return Save();
        }

        public bool DeleteSudanCategories(SudanCategory sudanCategory)
        {
            _dataContext.Remove(sudanCategory);
            return Save();
        }

        public ICollection<SudanCategory> GetSudanCategories()
        {
            return _dataContext.SudanCategories.ToList();
        }

        public SudanCategory GetSudanCategory(int id)
        {
            return _dataContext.SudanCategories.Where(s => s.sudanCatId == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;

        }

        public bool SudanCategoryExist(int Id)
        {
            return _dataContext.SudanCategories.Any(s => s.sudanCatId == Id);
        }

        public bool UpdateSudanCategories(SudanCategory sudanCategory)
        {
            _dataContext.Update(sudanCategory);
            return Save();
        }
    }
}
