using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Context
{
    public class MovieInfoContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cast> Casts { get; set; }
    }
}