using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {

        public BaseSpecification()
        {
            
        }

        public BaseSpecification(
            Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        
        // for (x => x.id == id || x.Name.Contains("red")) this stuff
        public Expression<Func<T, bool>> Criteria { get;}

        // for including relaed entities
        // .Include(p => p.photos)
        public List<Expression<Func<T, object>>> Includes { get;} =
         new List<Expression<Func<T, object>>>();

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }


    }
}