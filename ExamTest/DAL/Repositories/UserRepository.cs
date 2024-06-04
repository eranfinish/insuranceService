using DAL.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task AddUsersAsync(IEnumerable<User> users);
    }
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.Include(u => u.InsurancePolicies).ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(u => u.InsurancePolicies)
                                       .FirstOrDefaultAsync(u => u.ID == id);
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
           
            if (user == null)
            {
                return false;
            }
 var policy = await _context.InsurancePolicies.FirstOrDefaultAsync(p => p.UserID == id);
            if (policy != null)
            {
                _context.InsurancePolicies.Remove(policy);
                await _context.SaveChangesAsync();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task AddUsersAsync(IEnumerable<User> users)
        {
            _context.Users.AddRange(users);
            await _context.SaveChangesAsync();
        }
    }
}