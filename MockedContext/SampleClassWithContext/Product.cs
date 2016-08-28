using System;
using System.Collections.Generic;

namespace SampleClassWithContext
{
    /// <summary>
    /// Contains the products sold or used in the manufacturing of sold products
    /// <seealso cref="https://technet.microsoft.com/en-us/library/ms124719(v=sql.100).aspx"/>
    /// </summary>
    public class Product
    {
        public Product()
        {
            Transactions = new List<TransactionHistory>();
        }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public string ProductNumber { get; set; }

        public bool MakeFlag { get; set; }

        public bool FinishedGoodsFlag { get; set; }

        public string Color { get; set; }

        public short SafetyStockLevel { get; set; }

        public short ReorderPoint { get; set; }

        public decimal StandardCost { get; set; }

        public decimal ListPrice { get; set; }

        public string Size { get; set; }

        public string SizeUnitMeasureCode { get; set; }

        public string WeightUnitMeasureCode { get; set; }

        public decimal? Weight { get; set; }

        public int DaysToManufacture { get; set; }

        public string ProductLine { get; set; }

        public string Class { get; set; }

        public string Style { get; set; }

        public int? ProductSubcategoryId { get; set; }
        public int? ProductModelId { get; set; }

        public DateTime? SellStartDate { get; set; }

        public DateTime? SellEndDate { get; set; }

        public virtual List<TransactionHistory> Transactions { get; set; }
    }
}