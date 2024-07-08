using Microsoft.EntityFrameworkCore;
using UsersBlazorApp.API.Context;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Services;

public class UserService(UsersDbContext context) : IUserService<AspNetUsers>
{
    public async Task<List<AspNetUsers>> GetAll()
    {
        return await context.AspNetUsers
            .Include(u => u.Role)
            .ThenInclude(r => r.AspNetRoleClaims)
            .ToListAsync();
    }

    public async Task<AspNetUsers> Get(int id)
    {
        return await context.AspNetUsers.FirstAsync(u => u.Id == id);
    }

    public async Task<AspNetUsers> Add(AspNetUsers user)
    {
        if (!await Existe(user.Id))
        {
            context.AspNetUsers.Add(user);
            await context.SaveChangesAsync();
            return user;
        }
        else
        {
            await Update(user);
            return await Get(user.Id);
        }
    }

    public async Task<bool> Update(AspNetUsers user)
    {
        context.Entry(user).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var usuario = await context.AspNetUsers.FirstOrDefaultAsync(u => u.Id == id);
        if (usuario == null)
        {
            return false;
        }
        context.AspNetUsers.Remove(usuario);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int id)
    {
        return await context.AspNetUsers.AnyAsync(u => u.Id == id);
    }
}