using CrudV2.Business.DTOs;
using CrudV2.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CrudV2.WebApi.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserUseCases _userUseCases;

        public UserController(IUserUseCases userUseCases)
        {
            _userUseCases = userUseCases;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userUseCases.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userUseCases.GetUserByIdAsync(id);

                if (user == null)
                    return NotFound($"User with the ID {id} not found");

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            try
            {
                var userId = await _userUseCases.AddUserAsync(userDto);
                return CreatedAtAction(nameof(GetUserById), new { id = userId }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            try
            {
                if (id != userDto.Id)
                    return BadRequest("The user ID in the URL does not match the user ID in the request body.");

                var success = await _userUseCases.UpdateUserAsync(userDto);
                if (!success)
                    return NotFound($"The user ID {id} is not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                // Aqui você pode acessar as informações do usuário autenticado, se necessário.
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Continue com a lógica de exclusão do usuário.
                var success = await _userUseCases.DeleteUserAsync(id);
                if (!success)
                    return NotFound($"The user ID {id} is not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
            //try
            //{
            //    var success = await _userUseCases.DeleteUserAsync(id);
            //    if (!success)
            //        return NotFound($"The user ID {id} is not found");

            //    return NoContent();
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, $"Erro interno: {ex.Message}");
            //}
        }
    }
}