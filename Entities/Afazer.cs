namespace Modulo.Entities
{
  public class Afazer
  {
    // internal string? quando;
    public int Id { get; set; }
    public int ListaId { get; set; }
    public int ElementoId { get; set; }
    public string Descricao { get; set; }
    public bool Status { get; set; }
    public DateTime created_at { get; set; }
    public DateTime? update_at { get; set; }
    public DateTime? deleted_at { get; set; }
    // public Lista Lista { get; set; }
  }
}