using AutoMapper;
using BlazorPeliculasNet7.Server.Helpers;
using BlazorPeliculasNet7.Server.Services;
using BlazorPeliculasNet7.Shared.DTOs;
using BlazorPeliculasNet7.Shared.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculasNet7.Server.Controllers
{
    [ApiController]
    [Route("api/actores")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="admin")]
    public class ActoresController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly IMapper mapper;
        private readonly ActorService actorService;
        private readonly string contenedor = "personas";

        public ActoresController(ApplicationDbContext context, IAlmacenadorArchivos almacenadorArchivos, IMapper mapper,
            ActorService actorService )
        {
            this.context = context;
            this.almacenadorArchivos = almacenadorArchivos;
            this.mapper = mapper;
            this.actorService = actorService;
        }

        [HttpGet("buscar/{textoBusqueda}")]
        public async Task<ActionResult<List<Actor>>> Get(string textoBusqueda)
        {
            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                return new List<Actor>();
            }
            textoBusqueda = textoBusqueda.ToLower();
            return await context.Actores
                .Where(actor => actor.Nombre.ToLower().Contains(textoBusqueda))
                .Take(5)
                .ToListAsync();
        }

        // url?pagina1&CantidadRegistros=5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> Get([FromQuery]PaginacionDTO paginacion)
        {
            var queryable = actorService.GetActor();
            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadRegistros);
            return await queryable.OrderBy(x => x.Nombre).Paginar(paginacion).ToListAsync();

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Actor>> Get(int id)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(actor => actor.Id == id);

            if (actor == null)
            {
                return NotFound();
            }

            return actor;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Actor actor)
        {
            if (!string.IsNullOrWhiteSpace(actor.Foto))
            {
                var fotoActor = Convert.FromBase64String(actor.Foto);
                actor.Foto = await almacenadorArchivos.GuardarArchivo(fotoActor, ".jpg", contenedor);
            }

            context.Add(actor);
            await context.SaveChangesAsync();
            return actor.Id;
        }

        public async Task<ActionResult> Put(Actor actor)
        {
            var actorBD = await context.Actores.FirstOrDefaultAsync(a => a.Id == actor.Id);

            if (actorBD == null)
            {
                return NotFound();
            }

            actorBD = mapper.Map(actor, actorBD);

            if (!string.IsNullOrWhiteSpace(actor.Foto))
            {
                var fotoActor = Convert.FromBase64String(actor.Foto);
                actorBD.Foto = await almacenadorArchivos.EditarArchivo(fotoActor, ".jpg", contenedor, actorBD.Foto!);
            }

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(o => o.Id == id);

            if (actor is null) 
            { 
                return NotFound();
            }

            context.Remove(actor);
            await context.SaveChangesAsync();
            await almacenadorArchivos.EliminarArchivo(actor.Foto!, contenedor);

            return NoContent();
        }
    }
}
