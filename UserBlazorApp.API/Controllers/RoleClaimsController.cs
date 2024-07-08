using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserBlazorApp.API.DTO.Role;
using UserBlazorApp.API.DTO.RoleClaims;
using UserBlazorApp.API.DTO.User;
using UserBlazorApp.API.DTO.UserClaims;
using UserBlazorApp.API.Services;
using UsersBlazorApp.API.Context;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleClaimsController(IRoleClaimsService<AspNetRoleClaims> roleClaimsService) : ControllerBase
    {

        // GET: api/RoleClaims
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AspNetRoleClaims>>> GetAspNetRoleClaims()
        {
            var roles = await roleClaimsService.GetAll();

            var roleClaimRespose = roles.Select(r => new RoleClaimResponse
            {
                Id = r.Id,
                RoleId = r.RoleId,
                ClaimType = r.ClaimType,
                ClaimValue = r.ClaimValue,
            }).ToList();
            return Ok(roleClaimRespose);
        }

        // GET: api/RoleClaims/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleClaimResponse>> GetAspNetRoleClaims(int id)
        {
            var roleClaim = await roleClaimsService.Get(id);
            if (roleClaim == null)
            {
                return NotFound();
            }
            var roleResponse = new RoleClaimResponse
            {
                Id = roleClaim.Id,
                RoleId = roleClaim.RoleId,
                ClaimType = roleClaim.ClaimType,
                ClaimValue = roleClaim.ClaimValue,
            };
            return roleResponse;
        }

        // PUT: api/RoleClaims/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAspNetRoleClaims(int id, RoleClaimRequest roleClaimRequest)
        {
            var roleClaim = await roleClaimsService.Get(id);
            if (roleClaim == null)
            {
                return NotFound();
            }
            roleClaim.ClaimType = roleClaimRequest.ClaimType;
            roleClaim.ClaimValue = roleClaimRequest.ClaimValue;
            var actualizar = await roleClaimsService.Update(roleClaim);
            if (actualizar == false)
                return NotFound();

            return Ok();
        }

        // POST: api/RoleClaims
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AspNetRoleClaims>> PostAspNetRoleClaims(RoleClaimRequest roleClaimRequest)
        {
            var usuario = new AspNetRoleClaims
            {
                ClaimType = roleClaimRequest.ClaimType,
                ClaimValue = roleClaimRequest.ClaimValue,
            };
            var crearRol = await roleClaimsService.Add(usuario);
            var roleResponse = new UserClaimResponse
            {
                Id = crearRol.Id,
            };
            return Ok(roleResponse);
        }

        // DELETE: api/RoleClaims/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAspNetRoleClaims(int id)
        {
            var rol = await roleClaimsService.Get(id);
            if (rol == null)
            {
                return NotFound();
            }
            await roleClaimsService.Delete(id);
            return NoContent();
        }
    }
}
