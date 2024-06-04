using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IInsurancePolicyRepository
    {
        Task<IEnumerable<InsurancePolicy>> GetAllAsync();
        Task<InsurancePolicy> GetByIdAsync(int id);
        Task AddAsync(InsurancePolicy insurancePolicy);
        Task<bool> UpdateAsync(InsurancePolicy insurancePolicy);
        Task<bool> DeleteAsync(int id);
        
    }
    public class InsurancePolicyRepository : IInsurancePolicyRepository
    {
        private readonly AppDbContext _context;

        public InsurancePolicyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InsurancePolicy>> GetAllAsync()
        {
            return await _context.InsurancePolicies.ToListAsync();
        }

        public async Task<InsurancePolicy> GetByIdAsync(int id)
        {
            return await _context.InsurancePolicies.FindAsync(id);
        }

        public async Task AddAsync(InsurancePolicy insurancePolicy)
        {
            await _context.InsurancePolicies.AddAsync(insurancePolicy);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(InsurancePolicy insurancePolicy)
        {
            _context.Entry(insurancePolicy).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsurancePolicyExists(insurancePolicy.ID))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var insurancePolicy = await _context.InsurancePolicies.FindAsync(id);
            if (insurancePolicy == null)
            {
                return false;
            }

            _context.InsurancePolicies.Remove(insurancePolicy);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool InsurancePolicyExists(int id)
        {
            return _context.InsurancePolicies.Any(e => e.ID == id);
        }
    }
}

