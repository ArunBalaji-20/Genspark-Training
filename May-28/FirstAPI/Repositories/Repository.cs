using FirstAPI.Contexts;
using FirstAPI.Interfaces;

namespace FirstAPI.Repositories
{
    public  abstract class Repository<K, T> : IRepository<K, T> where T:class
    {
        protected readonly ClinicContext _clinicContext;

        public Repository(ClinicContext clinicContext)
        {
            _clinicContext = clinicContext;
        }
        public async Task<T> Add(T item)
        {
            _clinicContext.Add(item);
            await _clinicContext.SaveChangesAsync();//generate and execute the DML quries for the objects whse state is in ['added','modified','deleted'],
            return item;
        }

        public async Task<T> Delete(K key)
        {
            var item = await Get(key);
            if (item != null)
            {
                _clinicContext.Remove(item);
                await _clinicContext.SaveChangesAsync();
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
                _clinicContext.Entry(myItem).CurrentValues.SetValues(item);
                await _clinicContext.SaveChangesAsync();
                return item;
            }
            throw new Exception("No such item found for updation");
        }
    }
}




// using System;
// using FirstAPI.Interfaces;

// namespace FirstAPI.Repository;

// public abstract class Repository<K, T> : IRepository<K, T> where T : class
// {
//     protected List<T> _items=new List<T>();
//     protected abstract K GenerateID();
//     public abstract ICollection<T> GetAll();
//     public abstract T GetById(string id);
//     public T Add(T item)
//     {
//         var id = GenerateID();
//         var property = typeof(T).GetProperty("Id");
//         if (property != null)
//         {
//             property.SetValue(item, id);
//         }
//         if (_items.Contains(item))
//         {
//             throw new Exception("Appointment already exists.");
//         }
//         _items.Add(item);
//         return item;
//     }

//     public bool Delete(K id)
//     {
//         var item = GetById(id);
//             if (item == null)
//             {
//                 return false;
                
//             }
//             _items.Remove(item);
//             return true;
//         // throw new NotImplementedException();


//     }

//     public T GetById(K id)
//     {
//         throw new NotImplementedException();
//     }
// }
