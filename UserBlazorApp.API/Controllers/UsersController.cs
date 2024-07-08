using Microsoft.AspNetCore.Mvc;
using UserBlazorApp.API.DTO.Role;
using UserBlazorApp.API.DTO.RoleClaims;
using UserBlazorApp.API.DTO.User;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspNetUsersController(IAPIService<AspNetUsers> userService) : ControllerBase
    {

        // GET: api/AspNetUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AspNetUsers>>> GetAspNetUsers()
        {
            var usuarios = await userService.GetAll();

            var userRespose = usuarios.Select(u => new UserResponse
            {
                Id = u.Id,
                Name = u.UserName,
                Email = u.Email,
                PasswordHash = u.PasswordHash,
                PhoneNumber = u.PhoneNumber,
                LockoutEnd = u.LockoutEnd,
                Role = u.Role.Select(r => new RoleResponse
                {
                    Id = r.Id,
                    Name = r.Name,
                    RoleClaims = r.AspNetRoleClaims.Select(c => new RoleClaimResponse
                    {
                        Id = c.Id,
                        RoleId = c.RoleId,
                        ClaimType = c.ClaimType,
                        ClaimValue = c.ClaimValue
                    }).ToList()
                }).ToList()
            }).ToList();
            return Ok(userRespose);
        }

        // GET: api/AspNetUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetAspNetUsers(int id)
        {
            var usuario = await userService.Get(id);
            if (usuario == null)
            {
                return NotFound();
            }
            var userResponse = new UserResponse
            {
                Id = usuario.Id,
                Name = usuario.UserName,
                Email = usuario.Email,
                PasswordHash = usuario.PasswordHash,
                PhoneNumber = usuario.PhoneNumber,
                LockoutEnd = usuario.LockoutEnd,
                Role = usuario.Role.Select(r => new RoleResponse
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList()
            };
            return userResponse;
        }

        // PUT: api/AspNetUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAspNetUsers(int id, UserRequest userRequest)
        {
            var usuario = await userService.Get(id);
            if (usuario == null)
            {
                return NotFound();
            }
            usuario.UserName = userRequest.Name;
            usuario.Email = userRequest.Email;
            usuario.PasswordHash = userRequest.PasswordHash;
            usuario.PhoneNumber = userRequest.PhoneNumber;
            usuario.LockoutEnd = userRequest.LockoutEnd;
            var actualizar = await userService.Update(usuario);
            if (actualizar == false)
                return NotFound();

            return Ok();
        }

        // POST: api/AspNetUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserResponse>> PostAspNetUsers(UserRequest userRequest)
        {
            var usuario = new AspNetUsers
            {
                UserName = userRequest.Name,
                Email = userRequest.Email,
                PasswordHash = userRequest.PasswordHash,
                PhoneNumber = userRequest.PhoneNumber,
                LockoutEnd = DateTime.Now
            };
            var crearUsuario = await userService.Add(usuario);
            var userResponse = new UserResponse
            {
                Id = crearUsuario.Id,
                Name = crearUsuario.UserName,
                Email = crearUsuario.Email,
                PasswordHash = crearUsuario.PasswordHash,
                PhoneNumber = crearUsuario.PhoneNumber,
                LockoutEnd = crearUsuario.LockoutEnd,
                Role = crearUsuario.Role.Select(r => new RoleResponse
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList()
            };
            return Ok(userResponse);
        }

        // DELETE: api/AspNetUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAspNetUsers(int id)
        {
            var usuario = await userService.Get(id);
            if (usuario == null)
            {
                return NotFound();
            }
            await userService.Delete(id);
            return NoContent();
        }
    }
}
