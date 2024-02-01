using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using UserManagerApp.Dto;
using UserManagerApp.Helpers;
using UserManagerApp.Interfaces;
using UserManagerApp.Models;

namespace UserManagerApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository) 
        { 
            _userRepository = userRepository;
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult InsertUser([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest(ModelState);
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.AddUser(newUser)){
                ModelState.AddModelError("", "Something went wrong.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int pokeId,
            [FromQuery] int userId,
            [FromBody] User updatedUser)
        {
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (pokeId != updatedUser.Id)
                return BadRequest(ModelState);

            if (_userRepository.GetUser(userId) == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_userRepository.UpdateUser(updatedUser))
            {
                ModelState.AddModelError("", "Something went wrong updating user.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            var user = _userRepository.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.DeleteUser(user))
            {
                ModelState.AddModelError("", "Something went wrong deleting user.");
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int id)
        {
            var user = _userRepository.GetUser(id);
            if(user == null)
            {
                return NotFound();
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        [HttpPost("validate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult ValidateUser(UserValidationDto userValidation)
        {
            
            if (userValidation == null)
            {
                return BadRequest(ModelState);
            }

            if (userValidation.Email == null && userValidation.UserName == null)
            {
                return BadRequest("Missing email or username.");
            }

            User user;
            if (userValidation.Email != null)
            {
                user = GetUser();
            }

            if (userValidation.Email == null && userValidation.UserName != null)
            {
                userExists = _userRepository.UserExists(CustomConstants.USERNAME, userValidation.UserName);
            }

            if (!userExists)
            {
                return NotFound();
            }

            var userValidated = _userRepository.ValidateUser(userValidation.Password, )

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
