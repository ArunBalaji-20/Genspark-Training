using DocNotifyAPI.Context;
using DocNotifyAPI.Interfaces;

namespace DocNotifyAPI.Repositories
{
    public  abstract class Repository<K, T> : IRepository<K, T> where T:class
    {
        protected readonly  DocContext _DocContext;

        public Repository(DocContext docContext)
        {
            _DocContext = docContext;
        }
        public async Task<T> Add(T item)
        {
            _DocContext.Add(item);
            await _DocContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> Delete(K key)
        {
            var item = await Get(key);
            if (item != null)
            {
                _DocContext.Remove(item);
                await _DocContext.SaveChangesAsync();
                return item;
            }
            throw new Exception("No such item found for deleting");
        }

        public abstract Task<T> Get(K key);


        public abstract Task<IEnumerable<T>> GetAll();


        public async Task<T> Update(K key, T item)
        {
            var myItem = await Get(key);
            if (myItem != null)
            {
                _DocContext.Entry(myItem).CurrentValues.SetValues(item);
                await _DocContext.SaveChangesAsync();
                return item;
            }
            throw new Exception("No such item found for updation");
        }
    }
}
