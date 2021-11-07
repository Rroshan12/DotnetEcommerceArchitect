using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification
{
    public class PrductWithTypeAndBrand : BaseSpecification<Product>
    {
        public PrductWithTypeAndBrand( ProductSpecPrams specPrams)
         :base(x =>
            (!specPrams.BrandId.HasValue || x.ProductBrandId == specPrams.BrandId) &&
            (!specPrams.TypeId.HasValue || x.ProductTypeId == specPrams.TypeId)

         
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(specPrams.PageSize*(specPrams.PageIndex - 1), specPrams.PageSize);
            if(!string.IsNullOrEmpty(specPrams.Sort))
            {
                switch(specPrams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n=>n.Name);
                        break;

                }
            }

        }

        public PrductWithTypeAndBrand(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);

        }
    }
}