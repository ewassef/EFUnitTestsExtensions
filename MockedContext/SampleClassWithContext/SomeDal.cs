using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleClassWithContext
{
    public class SomeDal
    {
        /// <summary>
        /// This is a way to make you classes benefit from compiled performance while still allowing your unit testing to override the value
        /// </summary>
        public static Func<DbConnection,bool, AdventureWorksContext> FactoryInjectedContext =
            (connection,ownsConnection) => new AdventureWorksContext(connection,ownsConnection);
        /// <summary>
        /// This is a way to make you classes benefit from compiled performance while still allowing your unit testing to override the value
        /// </summary>
        public static Func<AdventureWorksContext> FactoryInjectedDefaultContext =
            () => new AdventureWorksContext();

        private AdventureWorksContext _adventureWorksContext;

        /// <summary>
        /// This is if you want to use property injection for your code
        /// </summary>
        public AdventureWorksContext PropertyInjectedContext
        {
            get { return _adventureWorksContext; }
            set { _adventureWorksContext = value; }
        }

        /// <summary>
        /// This is if you want to use constructor injection
        /// </summary>
        /// <param name="constructorInjectedContext"></param>
        public SomeDal(AdventureWorksContext constructorInjectedContext)
        {
            _adventureWorksContext = constructorInjectedContext;
        }


        public List<Product> GetProductsBasedOnUserEnteredPredicate(Expression<Func<Product,bool>> theBuiltWhereClause)
        {
            if (_adventureWorksContext == null)
                _adventureWorksContext = FactoryInjectedDefaultContext();

            // i prefer my contexts be in a using statement, so i generally prefer to use my code in the factory pattern
            // so i can say using (var context = FactoryInjectedDefaultContext()) and still have the ability to inject a disposable
            // context for unit testing

            return _adventureWorksContext.Products.Where(theBuiltWhereClause).ToList();
        }
        public async Task<List<Product>> GetProductsBasedOnUserEnteredPredicateAsync(Expression<Func<Product, bool>> theBuiltWhereClause)
        {
            if (_adventureWorksContext == null)
                _adventureWorksContext = FactoryInjectedDefaultContext();

            // i prefer my contexts be in a using statement, so i generally prefer to use my code in the factory pattern
            // so i can say using (var context = FactoryInjectedDefaultContext()) and still have the ability to inject a disposable
            // context for unit testing

            return await _adventureWorksContext.Products.Where(theBuiltWhereClause).ToListAsync();
        }

        public Product AddProduct(Product product)
        {
            if (_adventureWorksContext == null)
                _adventureWorksContext = FactoryInjectedDefaultContext();

            // i prefer my contexts be in a using statement, so i generally prefer to use my code in the factory pattern
            // so i can say using (var context = FactoryInjectedDefaultContext()) and still have the ability to inject a disposable
            // context for unit testing

            var result = _adventureWorksContext.Products.Add(product);
            var records = _adventureWorksContext.SaveChanges();

            if (records == 1)
                return result;
            return null; // indicates nothing inserted
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            if (_adventureWorksContext == null)
                _adventureWorksContext = FactoryInjectedDefaultContext();

            // i prefer my contexts be in a using statement, so i generally prefer to use my code in the factory pattern
            // so i can say using (var context = FactoryInjectedDefaultContext()) and still have the ability to inject a disposable
            // context for unit testing

            var result = _adventureWorksContext.Products.Add(product);
            var records =await  _adventureWorksContext.SaveChangesAsync();

            if (records == 1)
                return result;
            return null; // indicates nothing inserted
        }

        public List<Product> AddProducts(List<Product> products)
        {

            if (_adventureWorksContext == null)
                _adventureWorksContext = FactoryInjectedDefaultContext();

            // i prefer my contexts be in a using statement, so i generally prefer to use my code in the factory pattern
            // so i can say using (var context = FactoryInjectedDefaultContext()) and still have the ability to inject a disposable
            // context for unit testing

            

            var result = _adventureWorksContext.Products.AddRange(products);
            var records = _adventureWorksContext.SaveChanges();

            if (records == products.Count)
                return result.ToList();
            return null;
        }

        public async Task<List<Product>> AddProductsAsync(List<Product> products)
        {

            if (_adventureWorksContext == null)
                _adventureWorksContext = FactoryInjectedDefaultContext();

            // i prefer my contexts be in a using statement, so i generally prefer to use my code in the factory pattern
            // so i can say using (var context = FactoryInjectedDefaultContext()) and still have the ability to inject a disposable
            // context for unit testing



            var result = _adventureWorksContext.Products.AddRange(products);
            var records = await  _adventureWorksContext.SaveChangesAsync();

            if (records == products.Count)
                return result.ToList();
            return null;
        }

    }
}