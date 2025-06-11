

using BugTrackingAPI.Context;
using BugTrackingAPI.Interfaces;

namespace BugTrackingAPI.Repositories
{
    public abstract class Repository<K, T> : IRepository<K, T> where T : class
    {
        protected readonly BugContext _bugContext;

        public Repository(BugContext bugContext)
        {
            _bugContext = bugContext;
        }
        public async Task<T> Add(T item)
        {
            _bugContext.Add(item);
            await _bugContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> Delete(K key)
        {
            var item = await Get(key);
            if (item != null)
            {
                _bugContext.Remove(item);
                await _bugContext.SaveChangesAsync();
                return item;
            }
            throw new Exception("No such item found for deleting");
        }


        public async Task<T> Update(K key, T item)
        {
            var myItem = await Get(key);
            if (myItem != null)
            {
                _bugContext.Entry(myItem).CurrentValues.SetValues(item);
                await _bugContext.SaveChangesAsync();
                return item;
            }
            throw new Exception("No such item found for updation");
        }
        
        public abstract Task<T> Get(K key);


        public abstract Task<IEnumerable<T>> GetAll();
    }
}