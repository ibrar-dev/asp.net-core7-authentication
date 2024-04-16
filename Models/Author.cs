namespace AuthenticationApp.Models
{
    public class Author
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Book> Book_ { get; set;}

    }
}
