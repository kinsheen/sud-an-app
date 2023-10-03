using Sudan_Api.Data;
using Sudan_Api.Interface;
using Sudan_Api.Models;

namespace Sudan_Api.Repository
{
    public class SudanRepository: ISudanRepository
    {
        private readonly DataContext _dataContext;

        public SudanRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreateSudan(Sudan sudan)
        {
            _dataContext.Add(sudan);
            return Save();
        }

        public bool DeleteSudan(Sudan sudan)
        {
            _dataContext.Remove(sudan);
            return Save();
        }

        public Sudan GetSudan(int id)
        {
            return _dataContext.Sudans.Where(s => s.sudanId == id).FirstOrDefault();
        }

        public ICollection<Sudan> GetSudans()
        {
            return _dataContext.Sudans.ToList();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SudanExist(int Id)
        {
            return _dataContext.Sudans.Any(s => s.sudanId == Id);
        }

        public bool UpdateSudan(Sudan sudan)
        {
            _dataContext.Update(sudan);
            return Save();
        }
    }
}
