using AuthenticationApp.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
    
namespace AuthenticationApp.DB
{
    public class AuthenticationAPIContext : DbContext
    {
        public AuthenticationAPIContext(DbContextOptions<AuthenticationAPIContext> options)
          : base(options)
        {
        }

        public DbSet<User>? User { get; set; }



    }
}
