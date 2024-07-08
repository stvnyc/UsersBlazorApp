using System.Net.Http.Json;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.UI.Services;
public class RoleClienService(HttpClient httpClient) : IRoleService<AspNetRoles>
{
    public async Task<List<AspNetRoles>> GetAll()
    {
        return await httpClient.GetFromJsonAsync<List<AspNetRoles>>("api/AspNetRoles");
    }

    public async Task<AspNetRoles> Get(int id)
    {
        return await httpClient.GetFromJsonAsync<AspNetRoles>("api/AspNetRoles");
    }

    public async Task<AspNetRoles> Add(AspNetRoles property)
    {
        var response = await httpClient.PostAsJsonAsync("api/AspNetRoles", property);
        return await response.Content.ReadFromJsonAsync<AspNetRoles>();
    }

    public async Task<bool> Update(AspNetRoles property)
    {
        var response = await httpClient.PutAsJsonAsync($"api/AspNetRoles/{property.Id}", property);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Delete(int id)
    {
        var response = await httpClient.DeleteAsync($"api/AspNetRoles/{id}");
        return response.IsSuccessStatusCode;
    }
}