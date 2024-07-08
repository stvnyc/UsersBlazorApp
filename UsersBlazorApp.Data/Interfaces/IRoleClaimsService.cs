namespace UsersBlazorApp.Data.Interfaces;

public interface IRoleClaimsService<T>
{
    Task<List<T>> GetAll();
    Task<T> Get(int id);
    Task<T> Add(T role);
    Task<bool> Update(T role);
    Task<bool> Delete(int id);
}