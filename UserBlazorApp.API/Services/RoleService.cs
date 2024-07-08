using Microsoft.EntityFrameworkCore;
using UsersBlazorApp.API.Context;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Services;

public class RoleService(UsersDbContext context) : IRoleService<AspNetRoles>
{
    public async Task<List<AspNetRoles>> GetAll()
    {
        return await context.AspNetRoles
            .Include(r => r.AspNetRoleClaims)
            .ToListAsync();
    }

    public async Task<AspNetRoles> Get(int id)
    {
        return await context.AspNetRoles.FirstAsync(u => u.Id == id);
    }

    public async Task<AspNetRoles> Add(AspNetRoles role)
    {
        if (!await Existe(role.Id))
        {
            context.AspNetRoles.Add(role);
            await context.SaveChangesAsync();
            return role;
        }
        else
        {
            await Update(role);
            return await Get(role.Id);
        }
    }

    public async Task<bool> Update(AspNetRoles role)
    {
        context.Entry(role).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var rol = await context.AspNetRoles.FirstOrDefaultAsync(u => u.Id == id);
        if (rol == null)
        {
            return false;
        }
        context.AspNetRoles.Remove(rol);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int id)
    {
        return await context.AspNetRoles.AnyAsync(u => u.Id == id);
    }

}