using CarWash.UserService.Application.DTOs;
using CarWash.UserService.Application.IServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.UserService.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public AuthController(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    // -------------------- Register --------------------
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _userService.RegisterAsync(dto.Email, dto.Password);
            return Ok(new { message = "User registered successfully" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // -------------------- Login --------------------
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var isValid = await _userService.ValidateUserAsync(dto.Email, dto.Password);
        if (!isValid)
            return Unauthorized("Invalid credentials");

        var user = await _userService.GetByEmailAsync(dto.Email);
        if (user == null)
            return Unauthorized("User not found");

        var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role.ToString());
        return Ok(new { token });
    }

    // -------------------- Admin-Only --------------------
    [Authorize(Roles = "Admin")]
    [HttpGet("all-users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }
}
