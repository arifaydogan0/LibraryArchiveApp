using App.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Context
{
    public class LibDbContext : DbContext
    {
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Kind> Kinds { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }


        /* Yapılandırmayı burada bu kısımda yaptık. */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS; Database=LibraryDb; Trusted_Connection=true;");
            }
        }

        /* Gölge özellikleri tanımlıyoruz. */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<Book>().Property<DateTime>("UpdatedDate");
        }

        /* Gölge özelliklerin güzel bir kullanımı */
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var entry in entries)
            {
                if (entry.Entity is not Book)  //izlenen ve savechanges e (buraya yani) düşen varlık Book değilse ilerle bekleme yapma(bitek Books tablosuna gölge özellik ekledik çünkü)
                    continue;

                entry.Property("UpdatedDate").CurrentValue = DateTime.Now;
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedDate").CurrentValue = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}
