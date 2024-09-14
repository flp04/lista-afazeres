using Microsoft.EntityFrameworkCore;
using Modulo.Entities;

namespace Modulo.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
				: base(options)
		{
		}

		// Adicione DbSets para suas entidades
		public DbSet<Lista> Listas { get; set; }
		public DbSet<Afazer> Afazeres { get; set; }
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // base.OnModelCreating(modelBuilder);

				// modelBuilder.Entity<Lista>().HasKey(s => s.Id);

        // // Configurar a relação de chave estrangeira
        // modelBuilder.Entity<Afazer>()
				// 	.HasOne(t => t.Lista)       // Relação de um para muitos
				// 	.WithMany(l => l.Afazeres)   // A lista tem muitas tarefas
				// 	.HasForeignKey(l => l.ListaId); // Tarefa tem a chave estrangeira ListaId
    }
	}
}