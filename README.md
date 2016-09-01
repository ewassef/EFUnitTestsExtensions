# EFUnitTestsExtensions [![Build status](https://ci.appveyor.com/api/projects/status/qsvwa43c936vl6pm/branch/master?svg=true)](https://ci.appveyor.com/project/ewassef/efunittestsextensions/branch/master)
Helper methods for unit testing code with Entity Framework contexts and Moq

## Usage

Empty context with no data, to mock a bare bones table


```csharp
var mockedContext = new InjectableMockedContext<AdventureWorksContext>();

mockedContext.MockEntity(x => x.Products); // No seed to mock empty db

var classUnderTest = new SomeDal(mockedContext.MockedContext.Object);
//OR
var classUnderTest = new SomeDal(){
    Context = mockedContext.MockedContext.Object
}

```



Or if you want to seed some data or put in values to test and see if you logic is building the query correctly and passing it along

```csharp
var mockedContext = new InjectableMockedContext<AdventureWorksContext>();

var seedLIst = new List<Product>();
    seedLIst.Add(new Product { ProductId = 1, Transactions = new List<TransactionHistory> { new TransactionHistory { TransactionId = 1 } } });
    seedLIst.Add(new Product { ProductId = 2 });

mockedContext.MockEntity(x => x.Products, seedLIst);

var classUnderTest = new SomeDal(mockedContext.MockedContext.Object);
//OR
var classUnderTest = new SomeDal(){
    Context = mockedContext.MockedContext.Object
}
```


## Issues and feature Requests

Just open an issue above


## Downloads
Download from [NuGet](https://www.nuget.org/packages/EntityFramework.Testing.Helpers/).

Download the continuously integrated Binaries from [AppVeyor](https://ci.appveyor.com/project/ewassef/efunittestsextensions).

