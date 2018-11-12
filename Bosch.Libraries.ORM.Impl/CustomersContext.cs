using Bosch.Libraries.Models;
using Bosch.Libraries.ORM.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bosch.Libraries.ORM.Impl
{
    public class CustomersContext : DbContext, ICustomersContext
    {
        private const int MIN_RECORDS = 1;

        public CustomersContext(DbContextOptions<CustomersContext> customersDbContextOptions) : base(customersDbContextOptions)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public bool CommitChanges()
        {
            var noOfRecordsAffected = this.SaveChanges();

            return noOfRecordsAffected >= MIN_RECORDS;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<Customer>(
                new CustomerEntityTypeConfiguration());
        }
    }
}
