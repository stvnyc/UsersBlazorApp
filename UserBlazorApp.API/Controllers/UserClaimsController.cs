using Microsoft.AspNetCore.Mvc;
using UserBlazorApp.API.DTO.UserClaims;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserClaimsController(IUserClaimsService<AspNetUserClaims> userClaimsService) : ControllerBase
    {

        // GET: api/UserClaims
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AspNetUserClaims>>> GetAspNetUserClaims()
        {
            var usuarios = await userClaimsService.GetAll();

            var userClaimsRespose = usuarios.Select(u => new UserClaimResponse
            {
                Id = u.Id,
                ClaimType = u.ClaimType,
                ClaimValue = u.ClaimValue,
            }).ToList();
            return Ok(userClaimsRespose);

        }

        // GET: api/UserClaims/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserClaimResponse>> GetAspNetUserClaims(int id)
        {
            var usuario = await userClaimsService.Get(id);
            if (usuario == null)
            {
                return NotFound();
            }
            var userClaimResponse = new UserClaimResponse
            {
                Id = usuario.Id,
                ClaimType = usuario.ClaimType,
                ClaimValue = usuario.ClaimValue,
            };
            return userClaimResponse;
        }

        // PUT: api/UserClaims/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAspNetUserClaims(int id, UserClaimRequest userClaimRequest)
        {
            var usuario = await userClaimsService.Get(id);
            if (usuario == null)
            {
                return NotFound();
            }
            usuario.ClaimType = userClaimRequest.ClaimType;
            usuario.ClaimValue = userClaimRequest.ClaimValue;
            var actualizar = await userClaimsService.Update(usuario);
            if (actualizar == false)
                return NotFound();

            return Ok();
        }

        // POST: api/UserClaims
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AspNetUserClaims>> PostAspNetUserClaims(UserClaimRequest userClaimRequest)
        {
            var usuario = new AspNetUserClaims
            {
                ClaimType = userClaimRequest.ClaimType,
                ClaimValue = userClaimRequest.ClaimValue,
            };
            var crearUsuarioClaim = await userClaimsService.Add(usuario);
            var userClaimResponse = new UserClaimResponse
            {
                Id = crearUsuarioClaim.Id,
                ClaimType = crearUsuarioClaim.ClaimType,
                ClaimValue = crearUsuarioClaim.ClaimValue,
            };
            return Ok(userClaimResponse);
        }

        // DELETE: api/UserClaims/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAspNetUserClaims(int id)
        {
            var usuarioClaims = await userClaimsService.Get(id);
            if (usuarioClaims == null)
            {
                return NotFound();
            }
            await userClaimsService.Delete(id);
            return NoContent();
        }
    }
}
