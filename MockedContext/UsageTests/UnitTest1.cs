using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockedContext;
using SampleClassWithContext;

namespace UsageTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        [TestCategory("Querying")]
        public void Test_Can_Query_For_Record()
        {

            var mockedContext = new InjectableMockedContext<AdventureWorksContext>();

            var seedLIst = new List<Product>();
            seedLIst.Add(new Product { ProductId= 1, Transactions=new List<TransactionHistory> {new TransactionHistory {TransactionId = 1} } });
            seedLIst.Add(new Product {ProductId = 2});
            mockedContext.MockEntity(x => x.Products,seedLIst);

            var classUnderTest = new SomeDal(mockedContext.MockedContext.Object);

            var results = classUnderTest.GetProductsBasedOnUserEnteredPredicate(x => x.Transactions.Any(t=>t.TransactionId==1));
            Assert.AreEqual(results.Count, 1);
            Assert.AreEqual(results.First().ProductId, 1);

        }

        [TestMethod]
        [TestCategory("Inserting")]
        public void Test_Can_Insert_Record()
        {
            var mockedContext = new InjectableMockedContext<AdventureWorksContext>();

             
            mockedContext.MockEntity(x => x.Products); // No seed to mock empty db 

            var classUnderTest = new SomeDal(mockedContext.MockedContext.Object);

            var initial = classUnderTest.GetProductsBasedOnUserEnteredPredicate(x => x.ProductId == 1);
            Assert.IsNotNull(initial); // verify nothing is there
            Assert.IsFalse(initial.Any()); // verify nothing is there

            var results = classUnderTest.AddProduct(new Product {ProductId = 1});
            Assert.IsNotNull(results);
            Assert.AreEqual(results.ProductId, 1);
            var afterInsert = classUnderTest.GetProductsBasedOnUserEnteredPredicate(x=>x.ProductId == 1);
            Assert.IsNotNull(afterInsert);
            Assert.AreEqual(afterInsert.Count, 1);
            Assert.AreEqual(afterInsert.First().ProductId, 1);
        }

        [TestMethod]
        [TestCategory("Inserting")]
        public void Test_Can_Insert_Multiple_Records()
        {
            var mockedContext = new InjectableMockedContext<AdventureWorksContext>();


            mockedContext.MockEntity(x => x.Products); // No seed to mock empty db 

            var classUnderTest = new SomeDal(mockedContext.MockedContext.Object);

            var initial = classUnderTest.GetProductsBasedOnUserEnteredPredicate(x => x.ProductId == 1);
            Assert.IsNotNull(initial); // verify nothing is there
            Assert.IsFalse(initial.Any()); // verify nothing is there

            var results = classUnderTest.AddProducts(new List<Product>
            {
                new Product { ProductId = 1 },
                new Product { ProductId = 2 }
            });
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 2);
            var afterInsert = classUnderTest.GetProductsBasedOnUserEnteredPredicate(x => true); // This is obviously just for demonstration
            Assert.IsNotNull(afterInsert);
            Assert.AreEqual(afterInsert.Count, 2);
            Assert.AreEqual(afterInsert.First().ProductId, 1);
        }


        [TestMethod]
        [TestCategory("Async")]
        [TestCategory("Inserting")]
        public async Task Test_Can_Insert_RecordAsync()
        {
            var mockedContext = new InjectableMockedContext<AdventureWorksContext>();
            
            mockedContext.MockEntity(x => x.Products); // No seed to mock empty db 

            var classUnderTest = new SomeDal(mockedContext.MockedContext.Object);

            var initial = await classUnderTest.GetProductsBasedOnUserEnteredPredicateAsync(x => x.ProductId == 1);
            Assert.IsNotNull(initial); // verify nothing is there
            Assert.IsFalse(initial.Any()); // verify nothing is there

            var results = await classUnderTest.AddProductAsync(new Product { ProductId = 1 });
            Assert.IsNotNull(results);
            Assert.AreEqual(results.ProductId, 1);

            var afterInsert = await classUnderTest.GetProductsBasedOnUserEnteredPredicateAsync(x => x.ProductId == 1);
            Assert.IsNotNull(afterInsert);
            Assert.AreEqual(afterInsert.Count, 1);
            Assert.AreEqual(afterInsert.First().ProductId, 1);
        }

        [TestMethod]
        [TestCategory("Async")]
        [TestCategory("Inserting")]
        public async Task Test_Can_Insert_Multiple_RecordsAsync()
        {
            var mockedContext = new InjectableMockedContext<AdventureWorksContext>();


            mockedContext.MockEntity(x => x.Products); // No seed to mock empty db 

            var classUnderTest = new SomeDal(mockedContext.MockedContext.Object);

            var initial = classUnderTest.GetProductsBasedOnUserEnteredPredicate(x => x.ProductId == 1);
            Assert.IsNotNull(initial); // verify nothing is there
            Assert.IsFalse(initial.Any()); // verify nothing is there

            var results = await classUnderTest.AddProductsAsync(new List<Product>
            {
                new Product { ProductId = 1 },
                new Product { ProductId = 2 }
            });
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 2);

            var afterInsert = await classUnderTest.GetProductsBasedOnUserEnteredPredicateAsync(x => true); // This is obviously just for demonstration
            Assert.IsNotNull(afterInsert);
            Assert.AreEqual(afterInsert.Count, 2);
            Assert.AreEqual(afterInsert.First().ProductId, 1);
        }

        [TestMethod]
        [TestCategory("Querying")]
        [TestCategory("Async")]
        public async Task Test_Can_Query_For_RecordAsync()
        {

            var mockedContext = new InjectableMockedContext<AdventureWorksContext>();

            var seedLIst = new List<Product>();
            seedLIst.Add(new Product { ProductId = 1 });
            seedLIst.Add(new Product { ProductId = 2 });
            mockedContext.MockEntity(x => x.Products, seedLIst);

            var classUnderTest = new SomeDal(mockedContext.MockedContext.Object);

            var results = await classUnderTest.GetProductsBasedOnUserEnteredPredicateAsync(x => x.ProductId == 1);
            Assert.AreEqual(results.Count, 1);
            Assert.AreEqual(results.First().ProductId, 1);

        }
    }
}
