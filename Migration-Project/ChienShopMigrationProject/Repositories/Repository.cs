



using ChienShopMigrationProject.Interfaces;
using ChienVHShopOnline.Models;

namespace ChienShopMigrationProject.Repository;

    public abstract class Repository<K, T> : IRepository<K, T> where T : class
    {
        protected readonly ChienVHShopDBEntities _ChienVHShopDBEntities;

        public Repository(ChienVHShopDBEntities ChienVHShopDBEntities)
        {
            _ChienVHShopDBEntities = ChienVHShopDBEntities;
        }
        public async Task<T> Add(T item)
        {
            _ChienVHShopDBEntities.Add(item);
            await _ChienVHShopDBEntities.SaveChangesAsync();
            return item;
        }

        public async Task<T> Delete(K key)
        {
            var item = await Get(key);
            if (item != null)
            {
                _ChienVHShopDBEntities.Remove(item);
                await _ChienVHShopDBEntities.SaveChangesAsync();
                return item;
            }
            throw new Exception("No such item found for deleting");
        }


        public async Task<T> Update(K key, T item)
        {
            var myItem = await Get(key);
            if (myItem != null)
            {
                _ChienVHShopDBEntities.Entry(myItem).CurrentValues.SetValues(item);
                await _ChienVHShopDBEntities.SaveChangesAsync();
                return item;
            }
            throw new Exception("No such item found for updation");
        }
        
        public abstract Task<T> Get(K key);


        public abstract Task<IEnumerable<T>> GetAll();
    }
