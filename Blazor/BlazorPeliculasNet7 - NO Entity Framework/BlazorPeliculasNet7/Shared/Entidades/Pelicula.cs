using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPeliculasNet7.Shared.Entidades
{
    public class Pelicula
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; } = null!;
        public string? Resumen { get; set; }
        public bool EnCartelera { get; set; }
        public string? Trailer { get; set; }
        public DateTime? Lanzamiento { get; set; }
        public string? Poster { get; set; }
        public List<GeneroPelicula> GeneroPelicula { get; set; } = new List<GeneroPelicula>();
        public string? TituloCortado
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Titulo))
                {
                    return null;
                }
                if (Titulo.Length > 60)
                {
                    return Titulo.Substring(0, 60) + "...";
                }
                else
                {
                    return Titulo;
                }
            }
        }

    }
}
