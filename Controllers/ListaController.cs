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
    public IActionResult GetLista(string quando)
    {
      if (quando == "hoje") {
        var Hoje = _context.Listas.Where(x => x.created_at.Date == DateTime.Today).FirstOrDefault();
        var Afazeres = _context.Afazeres.Where(x => x.ListaId == Hoje.Id && x.deleted_at == null);
        return Ok(Afazeres);
      } else if (quando == "amanha") {
        var Amanha = _context.Listas.Where(x => x.created_at.Date == DateTime.Today.AddDays(1)).FirstOrDefault();
        var Afazeres = _context.Afazeres.Where(x => x.ListaId == Amanha.Id && x.deleted_at == null);
        return Ok(Afazeres);
      } else {
        _ = int.TryParse(quando.Split('-')[0], out int ano);
        _ = int.TryParse(quando.Split('-')[1], out int mes);
        _ = int.TryParse(quando.Split('-')[2], out int data);
        DateTime OutraData = new(ano, mes, data);
        var OutraDataDb = _context.Listas.Where(x => x.created_at.Date == OutraData.Date).FirstOrDefault();

        if (OutraDataDb == null)
        {
          Lista NovaLista = new()
          {
            created_at = OutraData
          };
          _context.Add(NovaLista);
          _context.SaveChanges();
          return Ok(NovaLista);
        }

        var Afazeres = _context.Afazeres.Where(x => x.ListaId == OutraDataDb.Id && x.deleted_at == null);
        return Ok(Afazeres);
      }
    }
  }
}