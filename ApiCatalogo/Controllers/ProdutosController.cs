using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }
    //tipo de retorno ActionResult<T> - T é o tipo do retorno, nesse caso Produto
    //ActionResult é uma classe base para os tipos de retorno de ação, como Ok(), NotFound(), BadRequest(), etc.
    //IEnumerable<Produto> é uma interface que representa uma coleção de objetos do tipo Produto, permitindo a iteração sobre eles.
    //O método Get() retorna uma coleção de produtos, e o método Get(int id, string param2) retorna um produto específico com base no ID fornecido.
    //O método Post(Produto produto) é usado para criar um novo produto, enquanto o método Put(int id, Produto produto) é usado para atualizar um produto existente. O método Delete(int id) é usado para excluir um produto com base no ID fornecido.
    //O método Get() retorna uma coleção de produtos, e o método Get(int id, string param2) retorna um produto específico com base no ID fornecido. 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> Get()
    {
        var produtos = await _context.Produtos!.AsNoTracking().ToListAsync();
        if (produtos is null)
        {
            return NotFound();
        }
        return produtos;
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public async Task<ActionResult<Produto>> Get(int id)
    {
        
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);
        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }
        return produto;
    }

    [HttpPost]
    public async Task<ActionResult> Post(Produto produto)
    {
        if (produto is null) return BadRequest();
        _context.Produtos!.Add(produto);
        await _context.SaveChangesAsync();
        return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId) return BadRequest();
        _context.Entry(produto).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var produto = await _context.Produtos!.FirstOrDefaultAsync(p => p.ProdutoId == id);
        if (produto is null) return NotFound("Produto não localizado...");
        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return Ok(produto);
    }
}