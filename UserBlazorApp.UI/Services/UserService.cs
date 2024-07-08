using System.Net.Http.Json;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.UI.Services;
public class UserClienService(HttpClient httpClient) : IClientService<AspNetUsers>
{
    public async Task<List<AspNetUsers>> GetAll()
    {
        return await httpClient.GetFromJsonAsync<List<AspNetUsers>>("api/AspNetUsers");
    }

    public async Task<AspNetUsers> Get(int id)
    {
        return await httpClient.GetFromJsonAsync<AspNetUsers>("api/AspNetUsers");
    }

    public async Task<AspNetUsers> Add(AspNetUsers property)
    {
        var response = await httpClient.PostAsJsonAsync("api/AspNetUsers", property);
        return await response.Content.ReadFromJsonAsync<AspNetUsers>();
    }

    public async Task<bool> Update(AspNetUsers property)
    {
        var response = await httpClient.PutAsJsonAsync($"api/AspNetUsers/{property.Id}", property);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Delete(int id)
    {
        var response = await httpClient.DeleteAsync($"api/AspNetUsers/{id}");
        return response.IsSuccessStatusCode;
    }
}