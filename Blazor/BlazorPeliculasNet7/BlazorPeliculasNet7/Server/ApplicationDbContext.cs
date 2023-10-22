using BlazorPeliculasNet7.Shared.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculasNet7.Server
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GenerosPeliculas>().HasKey(e => new { e.GeneroId, e.PeliculaId});
            modelBuilder.Entity<PeliculasActores>().HasKey(e => new { e.ActorId, e.PeliculaId});
        }

        public DbSet<Genero> Generos => Set<Genero>();
        public DbSet<Actor> Actores => Set<Actor>();
        public DbSet<Pelicula> Peliculas => Set<Pelicula>();
        public DbSet<VotoPelicula> VotoPeliculas => Set<VotoPelicula>();
        public DbSet<GenerosPeliculas> GenerosPeliculas => Set<GenerosPeliculas>();
        public DbSet<PeliculasActores> PeliculasActores => Set<PeliculasActores>();
    }
}
