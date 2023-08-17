using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {  

        public ProductsWithTypesAndBrandsSpecification(int id)
            : base(x => x.Id == id) //product Id Criteria
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            
        }

        public ProductsWithTypesAndBrandsSpecification()
        {   
            // for Product Including related entities
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }   
    }
}