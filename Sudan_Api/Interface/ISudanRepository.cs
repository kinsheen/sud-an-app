using Sudan_Api.Models;

namespace Sudan_Api.Interface
{
    public interface ISudanRepository
    {
        ICollection<Sudan> GetSudans();
        bool SudanExist(int Id);
        Sudan GetSudan(int id);
        bool CreateSudan(Sudan sudan);
        bool UpdateSudan(Sudan sudan);
        bool DeleteSudan(Sudan sudan);
        bool Save();
    }
}
