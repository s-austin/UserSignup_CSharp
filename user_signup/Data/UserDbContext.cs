using Microsoft.EntityFrameworkCore;
using user_signup.Models;

namespace user_signup.Data {
    public class UserDbContext: DbContext  {
        public DbSet<User> Users { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) {
            
        }
    }
}