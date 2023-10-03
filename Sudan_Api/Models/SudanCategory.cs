using System.ComponentModel.DataAnnotations;

namespace Sudan_Api.Models
{
    public class SudanCategory
    {
        [Key]
        public int sudanCatId { get; set; }
        public string sudanTitle { get; set; }
        public string image { get; set; }
        public ICollection<Sudan> Sudans { get; set; }
    }
}
