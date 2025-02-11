using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using Microsoft.AspNetCore.Identity;

namespace AppStore.Models.Domain;

public class LoadDatabase
{

    public static async Task InsertData(DataBaseContext context,
                                        UserManager<AplicationUser> usuarioManager,
                                        RoleManager<IdentityRole> roleManager)
    {
        

        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (!usuarioManager.Users.Any())
        {
            var usuario = new AplicationUser
            {
                Nombre = "admin",
                Email = "admin@email.com",
                UserName = "admin.db"
            };

            var result = await usuarioManager.CreateAsync(usuario, "Password123!");
            if (result.Succeeded)
            {
                usuarioManager.AddToRoleAsync(usuario, "Admin").Wait();
                await context.SaveChangesAsync();
            }
            
        }

        if (!context.Categorias!.Any())
        {
            await context.Categorias!.AddRangeAsync(
                new Categoria { Nombre = "Acción" },
                new Categoria { Nombre = "Aventura" },
                new Categoria { Nombre = "Terror" },
                new Categoria { Nombre = "Comedia" },
                new Categoria { Nombre = "Drama" }
            );
            await context.SaveChangesAsync();
        }

        if (!context.Libros.Any())
    {
        await context.Libros.AddRangeAsync(
            new Libro
            {
                Titulo = "Quijote de la Mancha",
                CreateDate = "2020-06-06",
                Imagen = "quijote.jpg",
                Autor = "Miguel de Cervantes"
            },
            new Libro
            {
                Titulo = "Harry Potter",
                CreateDate = "2021-06-01",
                Imagen = "harry.jpg",
                Autor = "Juan de la Vega"
            }
        );
        await context.SaveChangesAsync();
    }

        if (!context.LibroCategorias!.Any())
        {
            await context.LibroCategorias!.AddRangeAsync(
                new LibroCategoria { LibroId = 1, CategoriaId = 1 },
                new LibroCategoria { LibroId = 2, CategoriaId = 2 }
            );
            await context.SaveChangesAsync();
        }

    }
}