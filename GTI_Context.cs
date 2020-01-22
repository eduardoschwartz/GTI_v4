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

        public DbSet<Anexo> Anexo { get; set; }
        public DbSet<Anexo_log> Anexo_log { get; set; }
        public DbSet<Assunto> Assunto { get; set; }
        public DbSet<Assuntocc> Assuntocc { get; set; }
        public DbSet<Assuntodoc> Assuntodoc { get; set; }
        public DbSet<Bairro> Bairro { get; set; }
        public DbSet<Cadimob> Cadimob { get; set; }
        public DbSet<Centrocusto> Centrocusto { get; set; }
        public DbSet<Cep> Cep { get; set; }
        public DbSet<Cidadao> Cidadao { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Condominio> Condominio { get; set; }
        public DbSet<Despacho> Despacho { get; set; }
        public DbSet<Documento> Documento { get; set; }
        public DbSet<Endentrega> EndEntrega { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Historicocidadao> HistoricoCidadao { get; set; }
        public DbSet<Logradouro> Logradouro { get; set; }
        public DbSet<Mobiliario> Mobiliario { get; set; }
        public DbSet<Mobiliarioendentrega> MobiliarioEndEntrega { get; set; }
        public DbSet<Mobiliarioproprietario> MobiliarioProprietario { get; set; }
        public DbSet<ObsCidadao> ObsCidadao { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Processo_historico> Processo_historico { get; set; }
        public DbSet<Processodoc> Processodoc { get; set; }
        public DbSet<Processoend> Processoend { get; set; }
        public DbSet<Processogti> Processogti { get; set; }
        public DbSet<Processocidadao> ProcessoCidadao { get; set; }
        public DbSet<Profissao> Profissao { get; set; }
        public DbSet<Security_event> Security_Event { get; set; }
        public DbSet<Tramitacao> Tramitacao { get; set; }
        public DbSet<Tramitacaocc> Tramitacaocc { get; set; }
        public DbSet<Uf> Uf { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Usuariocc> Usuariocc { get; set; }
        public DbSet<Usuariofunc> Usuariofunc { get; set; }
    }
}
