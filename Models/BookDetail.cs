using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationApp.Models
{
    public class BookDetail
    {
        public int Id { get; set; }
        public double Weight { get; set; }

        [ForeignKey("Book")]
        public int Book_Id { get; set; }
        public Book Book { get; set; }

    }
}
