using Microsoft.AspNetCore.Mvc;
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult InsertUser([FromBody] UserPostDto newUserDto)
        {
            // Check if body object exists
            if (newUserDto == null)
            {
                return BadRequest(ModelState);
            }

            // Check if ModelState is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map body object to Model object
            // Remove Id since it is not needed when inserting
            var newUser = new User {
                UserName = newUserDto.UserName,
                FullName = newUserDto.FullName,
                Email = newUserDto.Email,
                MobileNumber = newUserDto.MobileNumber,
                Language = newUserDto.Language,
                Culture = newUserDto.Culture,
                Password = newUserDto.Password,
            };

            // Add user to database
            if (!_userRepository.AddUser(ref newUser)){
                ModelState.AddModelError("", "Something went wrong.");
                return StatusCode(500, ModelState);
            }

            return Ok(newUser.Id);
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserPostDto updatedUserDto)
        {
            // Check if body object exists
            if (updatedUserDto == null)
            {
                return BadRequest(ModelState);
            }

            // Check if user being updated exists
            if (_userRepository.GetUser(CustomConstants.ID, userId.ToString()) == null)
            {
                return NotFound();
            }

            // Check if ModelState is valid
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Map body object to Model object
            var updatedUser = new User
            {
                UserName = updatedUserDto.UserName,
                FullName = updatedUserDto.FullName,
                Email = updatedUserDto.Email,
                MobileNumber = updatedUserDto.MobileNumber,
                Language = updatedUserDto.Language,
                Culture = updatedUserDto.Culture,
                Password = updatedUserDto.Password,
            };

            // Update user in database
            if (!_userRepository.UpdateUser(updatedUser))
            {
                ModelState.AddModelError("", "Something went wrong updating user.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            // Check if user exists
            var user = _userRepository.GetUser(CustomConstants.ID, userId.ToString());
            if (user == null)
            {
                return NotFound();
            }

            // Delete user from database
            if (!_userRepository.DeleteUser(user))
            {
                ModelState.AddModelError("", "Something went wrong deleting user.");
            }

            return NoContent();
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int userId)
        {
            // Fetch user from database
            var user = _userRepository.GetUser(CustomConstants.ID, userId.ToString());
            if(user == null)
            {
                return NotFound();
            }

            // Check if ModelState is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        [HttpPost("validate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult ValidateUser(UserValidationDto userValidation)
        {
            // Check if userValidation object exists
            if (userValidation == null)
            {
                return BadRequest(ModelState);
            }

            User? user = _userRepository.GetUser(CustomConstants.USERNAME, userValidation.UserName);

            // Check if ModelState is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check user connected to username exists
            if (user == null)
            {
                return NotFound();
            }

            // Validate password with connected user
            var userValidated = _userRepository.ValidateUser(userValidation.Password, user.Id);
            if (!userValidated) 
            {
                return Unauthorized(false);
            }

            return Ok(true);
        }
    }
}
