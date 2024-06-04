using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using DAL.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ExamTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancePoliciesController : ControllerBase
    {
        private readonly IInsurancePolicyRepository _insurancePolicyRepository;

        public InsurancePoliciesController(IInsurancePolicyRepository insurancePolicyRepository)
        {
            _insurancePolicyRepository = insurancePolicyRepository;
        }

        // GET: api/InsurancePolicies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InsurancePolicy>>> GetInsurancePolicies()
        {
            return Ok(await _insurancePolicyRepository.GetAllAsync());
        }

        // GET: api/InsurancePolicies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InsurancePolicy>> GetInsurancePolicy(int id)
        {
            var insurancePolicy = await _insurancePolicyRepository.GetByIdAsync(id);

            if (insurancePolicy == null)
            {
                return NotFound();
            }

            return Ok(insurancePolicy);
        }

        // PUT: api/InsurancePolicies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsurancePolicy(int id, InsurancePolicy insurancePolicy)
        {
            if (id != insurancePolicy.ID)
            {
                return BadRequest();
            }

            var updated = await _insurancePolicyRepository.UpdateAsync(insurancePolicy);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/InsurancePolicies
        [HttpPost]
        public async Task<ActionResult<InsurancePolicy>> PostInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            await _insurancePolicyRepository.AddAsync(insurancePolicy);
            return CreatedAtAction(nameof(GetInsurancePolicy), new { id = insurancePolicy.ID }, insurancePolicy);
        }

        // DELETE: api/InsurancePolicies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsurancePolicy(int id)
        {
            var deleted = await _insurancePolicyRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
