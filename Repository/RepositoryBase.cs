using PokemonApp.Data;
using PokemonApp.Interfaces;

namespace PokemonApp.Repository;

public abstract class RepositoryBase: IRepositoryBase
{
    protected readonly DataContext Context;

    protected RepositoryBase(DataContext context)
    {
        Context = context;
    }

    public virtual bool Save()
    {
        var saved = Context.SaveChanges();
        return saved > 0;
    }

    public virtual void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }
}