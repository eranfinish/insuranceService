using ExamTest.DatabaseContext;
using ExamTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamTest.Services
{
    public interface IInsurancePolicyRepository
    {
        Task<IEnumerable<InsurancePolicy>> GetAllPoliciesAsync();
        Task<InsurancePolicy> GetPolicyByIdAsync(int id);
        Task<InsurancePolicy> AddPolicyAsync(InsurancePolicy policy);
        Task<InsurancePolicy> UpdatePolicyAsync(InsurancePolicy policy);
        Task<bool> DeletePolicyAsync(int id);
    }
    public class InsurancePolicyRepository : IInsurancePolicyRepository
    {
        private readonly AppDbContext _context;

        public InsurancePolicyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InsurancePolicy>> GetAllPoliciesAsync()
        {
            return await _context.InsurancePolicies.Include(p => p.User).ToListAsync();
        }

        public async Task<InsurancePolicy> GetPolicyByIdAsync(int id)
        {
            return await _context.InsurancePolicies.Include(p => p.User).FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task<InsurancePolicy> AddPolicyAsync(InsurancePolicy policy)
        {
            _context.InsurancePolicies.Add(policy);
            await _context.SaveChangesAsync();
            return policy;
        }

        public async Task<InsurancePolicy> UpdatePolicyAsync(InsurancePolicy policy)
        {
            _context.Entry(policy).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return policy;
        }

        public async Task<bool> DeletePolicyAsync(int id)
        {
            var policy = await _context.InsurancePolicies.FindAsync(id);
            if (policy == null)
            {
                return false;
            }

            _context.InsurancePolicies.Remove(policy);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
