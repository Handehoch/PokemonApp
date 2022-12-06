namespace PokemonApp.Interfaces;

public interface IRepositoryBase: IDisposable
{
    bool Save();
}