using GenericServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace Microsoft.EntityFrameworkCore
{
    public static class DbContextExtensions
    {
        public static List<ValidationResult> ExecuteValidation(this DbContext dbContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            foreach (var entry in dbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                // 需要安装EfCore.GenericServices包
                var provider = new ValidationDbContextServiceProvider(dbContext);
                var valContext = new ValidationContext(entry.Entity, provider, null);
                var errorResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(entry.Entity, valContext, errorResults, true))
                {
                    results.AddRange(errorResults);
                }
            }
            return results;
        }
    }
}
