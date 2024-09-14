using Microsoft.AspNetCore.Mvc;
using Modulo.Data;
using Modulo.Entities;

namespace Modulo.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ListaController : ControllerBase
  {
    private readonly AppDbContext _context;
    public ListaController (AppDbContext context) {
      _context = context;
    }

    [HttpGet("{quando}")]
    public IActionResult Get(string quando)
    {
      if (quando == "hoje") {
        var Hoje = _context.Listas.Where(x => x.created_at.Date == DateTime.Today).FirstOrDefault();
        var Afazeres = _context.Afazeres.Where(x => x.ListaId == Hoje.Id && x.deleted_at == null);
        return Ok(Afazeres);
      } else if (quando == "amanha") {
        var Hoje = _context.Listas.Where(x => x.created_at.Date == DateTime.Today.AddDays(1)).FirstOrDefault();
        var Afazeres = _context.Afazeres.Where(x => x.ListaId == Hoje.Id && x.deleted_at == null);
        return Ok(Afazeres);
      } else {
        _ = int.TryParse(quando.Split('-')[0], out int ano);
        _ = int.TryParse(quando.Split('-')[1], out int mes);
        _ = int.TryParse(quando.Split('-')[2], out int data);
        DateTime OutraData = new(ano, mes, data);
        var Oi = _context.Listas.Where(x => x.created_at.Date == OutraData.Date).FirstOrDefault();
        if (Oi == null)
        {
          Lista NovaLista = new()
          {
            created_at = OutraData
          };
          _context.Add(NovaLista);
          _context.SaveChanges();
          return Ok(NovaLista);
        }
        var Afazeres = _context.Afazeres.Where(x => x.ListaId == Oi.Id && x.deleted_at == null);
        return Ok(Afazeres);
        // return Ok(OutraData);
        // return Ok(Afazeres);
      }
      // return Ok("deu ruim");
      // Lista[] Listas = new Lista[2];
      // Hoje.Afazeres = (ICollection<Afazer>)Afazeres;
      // var Amanha = _context.Listas.Where(x => x.created_at.Date == DateTime.Today.AddDays(1));
      // var[] Listas = {Hoje, Amanha};
      // Listas.Add(Amanha);
      // Lista[] Listas = {(Lista)Hoje, (Lista)Amanha};
    }

    [HttpPost]
    public IActionResult Create([FromBody] Lista lista)
    {
      return Ok(lista);
      lista.created_at = DateTime.Now;

      foreach (var afazer in lista.Afazeres)
      {
        _context.Afazeres.Find(afazer.Id);
        return Ok(afazer);
        afazer.ListaId = lista.Id; // Define a chave estrangeira ListaId
        afazer.created_at = DateTime.Now;
      }

      return Ok(lista);

      // _context.Add(lista);
      // _context.SaveChanges();


      // _context.SaveChanges();

      // return Ok(lista);
      // // return CreatedAtAction(nameof(ObterPorId), new { id = contato.Id }, contato);
    }

    // [HttpPut("{id}")]
    // public IActionResult Atualizar(int id, Contato contato)
    // {
    //   var Contato = _context.Contatos.Find(id);

    //   if (Contato == null) return NotFound();

    //   Contato.Nome = contato.Nome;
    //   Contato.Telefone = contato.Telefone;
    //   Contato.Ativo = contato.Ativo;

    //   _context.Update(Contato);
    //   _context.SaveChanges();
    //   return Ok(Contato);
    // }


    // [HttpGet]
    // public IActionResult Obter()
    // {
    //   var listas = _context.Listas.ToList();

    //   if (listas == null) return NotFound();
      

    //   foreach (var lista in listas)
    //   {
    //     var afazeres = _context.Afazeres.Where(x => x.ListaId == lista.Id).ToList();
    //     if (afazeres != null)
    //     {
    //       lista.Afazeres = afazeres;
    //     }
    //   }

    //   return Ok(listas);
    // }
  }
}