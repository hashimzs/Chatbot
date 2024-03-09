using Chatbot.Api.Data;
using Chatbot.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.Api.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await this.appDbContext.Users
                .FirstOrDefaultAsync(v => v.Email.ToLower().Equals(email));

            return user;
        }

        public async Task<User?> GetUserById(int Id)
        {
            var user = await this.appDbContext.Users
                .FirstOrDefaultAsync(v => v.Id == Id);

            return user;
        }

        public async Task<User> AddUser(User user)
        {
            var result = await this.appDbContext.Users.AddAsync(user);
            await this.appDbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
