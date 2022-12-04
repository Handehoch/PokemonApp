namespace PokemonApp.Interfaces;

public interface IRepository<T>: IDisposable where T : class
{
    T Create(T dto);
    T GetById(int id);
    ICollection<T> GetAll();
    T UpdateById(int id, T dto);
    T DeleteById(int id);
    void Save();
}
