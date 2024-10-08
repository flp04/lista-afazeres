using Microsoft.AspNetCore.Mvc;
using Modulo.Data;
using Modulo.Entities;

namespace Modulo.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AfazerController : ControllerBase
  {
    private readonly AppDbContext _context;
    public AfazerController (AppDbContext context) 
    {
      _context = context;
    }

    [HttpPost("{quando}")]
    public IActionResult Create([FromBody] Afazer afazer, string quando)
    {
      int ListaId;
      if (quando == "hoje") {
        DateTime Hoje = DateTime.Today;
        ListaId = _context.Listas
          .Where(x => x.created_at.Date == Hoje)
          .Select(x => x.Id)
          .FirstOrDefault();
      } else if (quando == "amanha") {
        DateTime Amanha = DateTime.Today.AddDays(1);
        ListaId = _context.Listas
          .Where(x => x.created_at.Date == Amanha)
          .Select(x => x.Id)
          .FirstOrDefault();
      } else {
        _ = int.TryParse(quando.Split('-')[0], out int ano);
        _ = int.TryParse(quando.Split('-')[1], out int mes);
        _ = int.TryParse(quando.Split('-')[2], out int data);
        DateTime OutraData = new(ano, mes, data);
        ListaId = _context.Listas
          .Where(x => x.created_at.Date == OutraData.Date)
          .Select(x => x.Id)
          .FirstOrDefault();
      }
      if (ListaId == 0)
      {
        Lista NovaLista;
        if (quando == "hoje") {
          NovaLista = new Lista
          {
            created_at = DateTime.Now
          };
        } else {
          NovaLista = new Lista
          {
            created_at = DateTime.Now.AddDays(1)
          };
        }
        _context.Listas.Add(NovaLista);
        _context.SaveChanges();
        ListaId = NovaLista.Id;
      }

      afazer.ListaId = ListaId;
      afazer.created_at = DateTime.Now;
      afazer.update_at = DateTime.Now;

      _context.Afazeres.Add(afazer);
      _context.SaveChanges();
      return Ok(afazer);
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, [FromBody] string descricao)
    {
      var Afazer = _context.Afazeres.Find(id);
      Afazer.Descricao = descricao;
      Afazer.update_at = DateTime.Now;
      _context.Afazeres.Update(Afazer);
      _context.SaveChanges();
      return Ok(Afazer);
    }

    [HttpPut("ConcluirAfazer/{id}")]
    public IActionResult ConcluirAfazer(int id)
    {
      var Afazer = _context.Afazeres.Find(id);
      Afazer.Status = true;
      Afazer.update_at = DateTime.Now;

      _context.Afazeres.Update(Afazer);
      _context.SaveChanges();
      return Ok(Afazer);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var Afazer = _context.Afazeres.Find(id);

      if (Afazer == null) return NotFound();

      _context.Remove(Afazer);
      // Afazer.update_at = DateTime.Now;
      // Afazer.deleted_at = DateTime.Now;

      // _context.Afazeres.Update(Afazer);
      _context.SaveChanges();
      return Ok(NoContent());
    }
  }
}