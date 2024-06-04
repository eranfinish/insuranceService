using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;
using ExamTest.Models;
using User = DAL.Models.User;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("All")]
    public async Task<ActionResult<List<User>>> All()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost("AddUser")]    
    public async Task<ActionResult<User>> AddUser(User user)
    {
        var newUser = await _userRepository.AddUserAsync(user);
        return CreatedAtAction(nameof(GetUser), new { id = newUser.ID }, newUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        if (id != user.ID)
        {
            return BadRequest();
        }

        await _userRepository.UpdateUserAsync(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _userRepository.DeleteUserAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> PostUsers(BulkUserDTO bulkUserDTO)
    {
        var users = bulkUserDTO.Users.Select(u => new User
        {
            Name = u.Name,
            Email = u.Email
        }).ToList();

        await _userRepository.AddUsersAsync(users);
        return Created("GetUsers", null);
    }
}
