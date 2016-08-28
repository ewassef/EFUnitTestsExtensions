using System.Data.Common;
using System.Data.Entity;

namespace SampleClassWithContext
{
    public class AdventureWorksContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<TransactionHistory> TransactionHistories {get;set;}

        public AdventureWorksContext() : base()
        {
        }

        public AdventureWorksContext(DbConnection connection, bool contextOwnsConnection = false)
            : base(connection, contextOwnsConnection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TransactionHistory>()
                .Map(x => x.ToTable("TransactionHistory", "Production"))
                .HasKey(x => x.TransactionId);


            modelBuilder.Entity<Product>().Map(x => x.ToTable("Product", "Production"))
                .HasKey(x => x.ProductId);

            modelBuilder.Entity<Product>().HasMany(x => x.Transactions).WithOptional();

            modelBuilder.Entity<TransactionHistory>()
                .HasRequired(x => x.Product).WithMany(x => x.Transactions);
 
        }

    }
}