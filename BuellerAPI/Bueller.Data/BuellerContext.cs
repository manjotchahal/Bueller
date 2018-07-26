using Bueller.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Data
{
    public class BuellerContext : DbContext, IDbContext
    {
        public BuellerContext() : base("BuellerDb") { }

        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        IDbSet<TEntity> IDbContext.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public override int SaveChanges()
        {
            var addedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

            addedEntities.ForEach(E =>
            {
                E.Property("Created").CurrentValue = DateTime.Now;
                E.Property("Modified").CurrentValue = DateTime.Now;
            });

            var modifiedEntries = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

            modifiedEntries.ForEach(E =>
            {
                E.Property("Modified").CurrentValue = DateTime.Now;
            });
            return base.SaveChanges();
        }
    }
}
