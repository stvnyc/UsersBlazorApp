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
    public class RolesController(IRoleService<AspNetRoles> roleService) : ControllerBase
    {

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AspNetRoles>>> GetAspNetRoles()
        {
            var roles = await roleService.GetAll();

            var roleRespose = roles.Select(r => new RoleResponse
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
            }).ToList();
            return Ok(roleRespose);
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleResponse>> GetAspNetRoles(int id)
        {
            var role = await roleService.Get(id);
            if (role == null)
            {
                return NotFound();
            }
            var roleResponse = new RoleResponse
            {
                Id = role.Id,
                Name = role.Name,
            };
            return roleResponse;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAspNetRoles(int id, RoleRequest roleRequest)
        {
            var role = await roleService.Get(id);
            if (role == null)
            {
                return NotFound();
            }
            role.Name = roleRequest.Name;
            var actualizar = await roleService.Update(role);
            if (actualizar == false)
                return NotFound();

            return Ok();
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AspNetRoles>> PostAspNetRoles(RoleRequest roleRequest)
        {
            var usuario = new AspNetRoles
            {
                Name = roleRequest.Name
            };
            var crearRol = await roleService.Add(usuario);
            var roleResponse = new UserResponse
            {
                Id = crearRol.Id,
            };
            return Ok(roleResponse);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAspNetRoles(int id)
        {
            var rol = await roleService.Get(id);
            if (rol == null)
            {
                return NotFound();
            }
            await roleService.Delete(id);
            return NoContent();
        }
    }
}
