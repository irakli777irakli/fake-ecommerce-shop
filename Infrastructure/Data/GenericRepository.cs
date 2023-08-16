using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{   
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }


        public async Task<T> GetByIdAsync(int id)
        {
            // T whatever we want to get
            return await _context.Set<T>().FindAsync(id);
        }

        

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            // uses IQuerable returned by evaluator to get data
            return await ApplySpecification(spec).ToListAsync();
        }


        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {   
            // passes `T` IQuerable `product/brand/type`, and expressions of that' type filters
            // to evaluator
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}