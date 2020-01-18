using GTI_v4.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GTI_v4 {
    public class GTI_Context:DbContext {
        public GTI_Context(string Connection_Name) : base(Connection_Name) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            Database.SetInitializer<GTI_Context>(null);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(14, 4));

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Bairro> Bairro { get; set; }
        public DbSet<Centrocusto> Centrocusto { get; set; }
        public DbSet<Cep> Cep { get; set; }
        public DbSet<Cidadao> Cidadao { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Logradouro> Logradouro { get; set; }
        public DbSet<Mobiliario> Mobiliario { get; set; }
        public DbSet<Mobiliarioproprietario> MobiliarioProprietario { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Processocidadao> ProcessoCidadao { get; set; }
        public DbSet<Profissao> Profissao { get; set; }
        public DbSet<Security_event> Security_Event { get; set; }
        public DbSet<Uf> Uf { get; set; }
        public DbSet<Usuario> Usuario { get; set; }


    }
}
