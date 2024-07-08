namespace UsersBlazorApp.Data.Interfaces;

public interface IUserService <T>
{
    Task<List<T>> GetAll();
    Task<T> Get(int id);
    Task<T> Add(T user);
    Task<bool> Update(T user);
    Task<bool> Delete(int id);
}
