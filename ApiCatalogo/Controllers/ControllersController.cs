using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ControllersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("categorias")]
        public ActionResult<IEnumerable<Categoria>> get()
        {
            var categorias = _context.Categorias.ToList();

            if(categorias == null)
            {
                return NotFound();
            }
            return Ok(categorias);
        }
        [HttpGet("{id:int}")]
        public ActionResult<Categoria> get(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }
        [HttpPost("categorias")]
        public ActionResult post(Categoria categoria)
        {
            if(categoria is null)
            {
                return BadRequest();
            }
            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("getCategoria", new { id = categoria.CategoriaId }, categoria);
        }
        [HttpDelete("categoria/{int:id}")]
        public ActionResult delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c -> c.categoriaId == id);
            if(categoria is null)
            {
                return NotFound();
            }
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return Ok(categoria);

        }
    }
}
