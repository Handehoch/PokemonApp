namespace PokemonApp.Interfaces;

public interface IRepository<T>: IRepositoryBase where T: class
{
    bool Create(T dto);
    T GetById(int id);
    ICollection<T> GetAll();
    bool Update(int id, T dto);
    T DeleteById(int id);
    bool Exists(int id);
}
