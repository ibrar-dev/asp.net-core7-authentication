using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        [ForeignKey("User")]
        public int User_Id { get; set; }
        public User User { get; set; }
        public BookDetail BookDetail { get; set; }
        public virtual ICollection<Author> Author_ { get; set; }

    }
}
