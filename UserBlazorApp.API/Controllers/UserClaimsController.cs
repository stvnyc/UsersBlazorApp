using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersBlazorApp.API.Context;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserClaimsController : ControllerBase
    {
        private readonly UsersDbContext _context;

        public UserClaimsController(UsersDbContext context)
        {
            _context = context;
        }

        // GET: api/UserClaims
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AspNetUserClaims>>> GetAspNetUserClaims()
        {
            return await _context.AspNetUserClaims.ToListAsync();
        }

        // GET: api/UserClaims/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AspNetUserClaims>> GetAspNetUserClaims(int id)
        {
            var aspNetUserClaims = await _context.AspNetUserClaims.FindAsync(id);

            if (aspNetUserClaims == null)
            {
                return NotFound();
            }

            return aspNetUserClaims;
        }

        // PUT: api/UserClaims/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAspNetUserClaims(int id, AspNetUserClaims aspNetUserClaims)
        {
            if (id != aspNetUserClaims.Id)
            {
                return BadRequest();
            }

            _context.Entry(aspNetUserClaims).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserClaimsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserClaims
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AspNetUserClaims>> PostAspNetUserClaims(AspNetUserClaims aspNetUserClaims)
        {
            _context.AspNetUserClaims.Add(aspNetUserClaims);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAspNetUserClaims", new { id = aspNetUserClaims.Id }, aspNetUserClaims);
        }

        // DELETE: api/UserClaims/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAspNetUserClaims(int id)
        {
            var aspNetUserClaims = await _context.AspNetUserClaims.FindAsync(id);
            if (aspNetUserClaims == null)
            {
                return NotFound();
            }

            _context.AspNetUserClaims.Remove(aspNetUserClaims);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AspNetUserClaimsExists(int id)
        {
            return _context.AspNetUserClaims.Any(e => e.Id == id);
        }
    }
}
