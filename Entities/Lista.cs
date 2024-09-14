namespace Modulo.Entities
{
  public class Lista
  {
    public int Id { get; set; }
    public DateTime created_at { get; set; }
    public DateTime? update_at { get; set; }
    public DateTime? deleted_at { get; set; }
    public ICollection<Afazer> Afazeres { get; set; } = new List<Afazer>();
  }
}