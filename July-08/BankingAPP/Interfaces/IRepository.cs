using System;

namespace BankingAPP.Interfaces;

public interface IRepository<K,T> where T:class
{
        public Task<T> Add(T item);
        public Task<T> Update(T item);
        public Task<T> Get(K key);
        public Task<IEnumerable<T>> GetAll();
}
