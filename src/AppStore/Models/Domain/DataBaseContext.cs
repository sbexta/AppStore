using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppStore.Models.Domain;
using Microsoft.AspNetCore.Identity;

public class DataBaseContext : IdentityDbContext<AplicationUser>
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {

    }
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //modelBuilder.Entity<Libro>().HasMany(x => x.CategoriaRelationList).WithMany(y => y.LibroRelationList).UsingEntity<LibroCategoria>
    //(
    //    j => j.HasOne(x => x.Categoria)
    //    .WithMany(y => y.LibroCategoriaRelationList)
    //    .HasForeignKey(z => z.CategoriaId),
    //   j => j.HasOne(x => x.Libro)
    //    .WithMany(y => y.LibroCategoriaRelationList)
    //    .HasForeignKey(z => z.LibroId),
    //    j =>
    //    {
    //        j.HasKey(t => new { t.CategoriaId, t.LibroId });
    //    }
    //);
    //}
    protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            // Configuración adicional de entidades
            builder.Entity<Libro>()
                .HasMany(x => x.CategoriaRelationList)
                .WithMany(y => y.LibroRelationList)
                .UsingEntity<LibroCategoria>(
                    j => j.HasOne(x => x.Categoria)
                          .WithMany(y => y.LibroCategoriaRelationList)
                          .HasForeignKey(z => z.CategoriaId),
                    j => j.HasOne(x => x.Libro)
                          .WithMany(y => y.LibroCategoriaRelationList)
                          .HasForeignKey(z => z.LibroId),
                    j =>
                    {
                        j.HasKey(t => new { t.CategoriaId, t.LibroId });
                    }
                );
        }

    //Siempre en plural los nombres de las tablas
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Libro> Libros { get; set; }
    public DbSet<LibroCategoria> LibroCategorias { get; set; }
}
