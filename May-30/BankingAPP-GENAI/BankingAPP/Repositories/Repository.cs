using System;
using BankingAPP.Contexts;
using BankingAPP.Interfaces;

namespace BankingAPP.Repositories;

public abstract class Repository<K, T> : IRepository<K, T> where T : class
{
    protected readonly BankContext _bankContext;

    public Repository(BankContext bankContext)
    {
        _bankContext = bankContext;
    }
    public async Task<T> Add(T item)
    {
        _bankContext.Add(item);
        await _bankContext.SaveChangesAsync();
        return item;
    }

    public  async Task<T> Update(T item)
    {
        _bankContext.Update(item);
        await _bankContext.SaveChangesAsync();
        return item;
    }
    public abstract Task<T> Get(K key);


    public abstract Task<IEnumerable<T>> GetAll();
    
}
