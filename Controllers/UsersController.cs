using AutoMapper;
using courses_registration.DTO;
using courses_registration.Interfaces;
using courses_registration.Models;
using courses_registration.Repositories;
using courses_registration.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace courses_registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UserDTO> _userValidator;

        public UsersController(IUserRepository UserRepository, IMapper mapper, IValidator<UserDTO> userValidator) 
        {
            _userRepository = UserRepository;
            _mapper = mapper;
            _userValidator = userValidator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult getUser(int id)
        {
            if (!_userRepository.UserExists(id))
                return NotFound();

            var user = _mapper.Map<UserDTO>(_userRepository.GetUser(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDTO user)
        {
            if (user == null)
                return BadRequest(ModelState);

            if (user.UserTypeId == null)
                user.UserTypeId = 1; 

            var validationResult = _userValidator.Validate(user);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            user.PasswordHash= _userRepository.HashPassword(user.PasswordHash);
            

            if (!_userRepository.CreateUser(_mapper.Map<User>(user)))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{id}/updateUsername")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUsername(int id, [FromBody] string username)
        {
            
            if (!_userRepository.UserExists(id))
                return NotFound();

            var user = _userRepository.GetUser(id);
            user.Username= username;

            var validationResult = _userValidator.Validate(_mapper.Map<UserDTO>(user));
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_userRepository.UpdateUser(user))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPut("{id}/updatePassword")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePassword(int id, [FromBody] string password)
        {

            if (!_userRepository.UserExists(id))
                return NotFound();

            var user = _userRepository.GetUser(id);
            user.PasswordHash=_userRepository.HashPassword(password);

              var validationResult = _userValidator.Validate(_mapper.Map<UserDTO>(user));
              if (!validationResult.IsValid)
              {
                  return BadRequest(validationResult.Errors);
              }

            if (!_userRepository.UpdateUser(user))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult SoftDeleteUser(int id)
        {

            if (!_userRepository.UserExists(id))
                return NotFound();

            var userToDelete = _userRepository.GetUser(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.SoftDeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
            }
            return NoContent();
        }
    }
}
