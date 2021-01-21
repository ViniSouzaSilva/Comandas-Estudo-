using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmbiStore.Shared.EFCore.Data
{
    public class AmbiStoreDbContextFactory : IDesignTimeDbContextFactory<AmbiStoreDbContext>
    {
        public AmbiStoreDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<AmbiStoreDbContext>();
            options.EnableSensitiveDataLogging();
            options.UseMySql("server=localhost;userid=root;password=sysdba;database=AmbiStore;ConvertZeroDateTime=True");

            return new AmbiStoreDbContext(options.Options);
        }
    }
}
