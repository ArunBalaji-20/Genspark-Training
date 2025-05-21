using HospitalManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Exceptions;

namespace HospitalManagement.Repositories
{
    public abstract class Repository<K,T> : IRepository<K,T> where T: class
    {
        protected List<T> _items=new List<T>();
        protected abstract K GenerateID();
        public abstract ICollection<T> GetAll();
        public abstract T GetById(K id);

        public T Add(T item)
        {
            var id=GenerateID();
            var property=typeof(T).GetProperty("Id");
            if (property != null)
            {
                property.SetValue(item, id);
            }
            if(_items.Contains(item))
            {
                throw new DuplicateItemException("Appointment already exists.");
            }
            _items.Add(item);
            return item;
        }

       

        public bool Delete(K id)
        {
            var item = GetById(id);
            if (item == null)
            {
                return false;
                
            }
            _items.Remove(item);
            return true;
        }
    }
}
