using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private Hashtable _repositories;
        public UnitOfWork(StoreContext context)
        {
            _context = context;
            
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            // free up resources
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if(_repositories == null) 
            {
                _repositories = new Hashtable();
            }

            // entity Name eg. Product
            var type = typeof(TEntity).Name;
            if(!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                // creates instance of generic repository
                // context is shared using late binding reflection
                var respositoryInstance = Activator
                 .CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)),_context);
            
            
                _repositories.Add(type,respositoryInstance);
            }   

            return (IGenericRepository<TEntity>) _repositories[type];

        }
    }
}