using Quartz;
using Modulo.Data; // Namespace onde está seu DbContext

public class AtualizarRegistroJob : IJob
{
    private readonly AppDbContext _context;

    public AtualizarRegistroJob(AppDbContext context)
    {
        _context = context;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        // Consulta o registro que precisa ser modificado
        // var registro = await _context.YourTable.FindAsync(1); // Exemplo: id = 1
        var lista = _context.Listas.Where(x => x.created_at.Date == DateTime.Today.Date).FirstOrDefault();
        var amanha = _context.Listas.Where(x => x.created_at.Date == DateTime.Today.AddDays(1).Date).FirstOrDefault();
        if (lista != null)
        {
          var afazeres = _context.Afazeres.Where(x => x.ListaId == lista.Id);
          foreach (var afazer in afazeres)
          {
            if (afazer.Status == false)
            {
            afazer.ListaId = amanha.Id;
            afazer.Descricao += "*";
            _context.Update(afazer);
            }
          }
            // // Faz a modificação desejada
            // registro.SeuCampo = "Novo valor";
            // registro.UltimaModificacao = DateTime.Now; // Exemplo de campo de data

            // // Salva as alterações no banco de dados
            // _context.SaveChangesAsync();
            
          _context.SaveChanges();
        }
        Console.WriteLine("Registro atualizado com sucesso à meia-noite!");
    }
}
