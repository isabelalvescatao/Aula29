using Microsoft.EntityFrameworkCore;

namespace Aula_29.Models;

    public class Catalogo : DbContext
    {
    public Catalogo(DbContextOptions<Catalogo> options) : base(options)
    {

    }

    public DbSet<FilmeModels> Filmes { get; set; }
    public DbSet<AtorModels> Atores { get; set; }
    }
