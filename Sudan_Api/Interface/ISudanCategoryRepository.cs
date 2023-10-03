using Sudan_Api.Models;

namespace Sudan_Api.Interface
{
    public interface ISudanCategoryRepository
    {
        ICollection<SudanCategory> GetSudanCategories();
        bool SudanCategoryExist(int Id);
        SudanCategory GetSudanCategory(int id);
        bool CreateSudanCategories(SudanCategory sudanCategory);
        bool UpdateSudanCategories(SudanCategory sudanCategory);
        bool DeleteSudanCategories(SudanCategory sudanCategory);
        bool Save();
    }
}
