using System.ComponentModel.DataAnnotations;

namespace Sudan_Api.Models
{
    public class Sudan
    {
        [Key]
        public int sudanId { get; set; }
        public string sudanName { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public string image { get; set; }
    }
}
