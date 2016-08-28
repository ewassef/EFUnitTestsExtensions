using System;

namespace SampleClassWithContext
{
    /// <summary>
    /// Contains a row of each purchase order, sales order, or work order transaction for the current year.
    /// <see cref="https://technet.microsoft.com/en-us/library/ms124844(v=sql.100).aspx"/>
    /// </summary>
    public class TransactionHistory
    {
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public int ReferenceOrderId { get; set; }
        public int ReferenceOrderLineId { get; set; }
        public DateTime TransactionDate { get; set; }
        public char TransactionType { get; set; }
        public int Quantity { get; set; }
        public decimal ActualCost { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Product Product
        {
            get;
            set;
        }
    }
}