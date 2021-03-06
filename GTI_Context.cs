﻿using GTI_v4.Models;
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

        public DbSet<Alvara_funcionamento> Alvara_Funcionamento { get; set; }
        public DbSet<Anexo> Anexo { get; set; }
        public DbSet<Anexo_log> Anexo_log { get; set; }
        public DbSet<Areas> Areas { get; set; }
        public DbSet<Assunto> Assunto { get; set; }
        public DbSet<Assuntocc> Assuntocc { get; set; }
        public DbSet<Assuntodoc> Assuntodoc { get; set; }
        public DbSet<Atividade> Atividade { get; set; }
        public DbSet<Atividadeiss> Atividadeiss { get; set; }
        public DbSet<Bairro> Bairro { get; set; }
        public DbSet<Benfeitoria> Benfeitoria { get; set; }
        public DbSet<Cadimob> Cadimob { get; set; }
        public DbSet<Categconstr> Categconstr { get; set; }
        public DbSet<Categprop> Categprop { get; set; }
        public DbSet<Centrocusto> Centrocusto { get; set; }
        public DbSet<Cep> Cep { get; set; }
        public DbSet<Certidao_inscricao> Certidao_Inscricao { get; set; }
        public DbSet<Cidadao> Cidadao { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Cnae> Cnae { get; set; }
        public DbSet<Cnae_criterio> Cnae_criterio { get; set; }
        public DbSet<Cnaecriterio> Cnaecriterio { get; set; }
        public DbSet<Cnaecriteriodesc> Cnaecriteriodesc { get; set; }
        public DbSet<Cnaesubclasse> Cnaesubclasse { get; set; }
        public DbSet<Comunicado_Isencao> Comunicado_Isencao { get; set; }
        public DbSet<Condominio> Condominio { get; set; }
        public DbSet<Condominioarea> Condominioarea { get; set; }
        public DbSet<Condominiounidade> Condominiounidade { get; set; }
        public DbSet<Contribuinte_Header> Contribuinte_Header { get; set; }
        public DbSet<DEmpresa> DEmpresa { get; set; }
        public DbSet<Debitoparcela> Debitoparcela { get; set; }
        public DbSet<Despacho> Despacho { get; set; }
        public DbSet<Documento> Documento { get; set; }
        public DbSet<Endentrega> EndEntrega { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Escritoriocontabil> Escritoriocontabil { get; set; }
        public DbSet<Facequadra> Facequadra { get; set; }
        public DbSet<Foto_imovel> Foto_Imovel { get; set; }
        public DbSet<Historico> Historico { get; set; }
        public DbSet<Historicocidadao> HistoricoCidadao { get; set; }
        public DbSet<Horario_funcionamento> Horario_Funcionamento { get; set; }
        public DbSet<Horariofunc> Horariofunc { get; set; }
        public DbSet<Isencao> Isencao { get; set; }
        public DbSet<Laseriptu> Laseriptu { get; set; }
        public DbSet<Logradouro> Logradouro { get; set; }
        public DbSet<Mobiliario> Mobiliario { get; set; }
        public DbSet<Mobiliarioatividadeiss> Mobiliarioatividadeiss { get; set; }
        public DbSet<Mobiliariocnae> Mobiliariocnae { get; set; }
        public DbSet<Mobiliarioendentrega> MobiliarioEndEntrega { get; set; }
        public DbSet<Mobiliarioevento> Mobiliarioevento { get; set; }
        public DbSet<Mobiliariohist> Mobiliariohist { get; set; }
        public DbSet<mobiliarioplaca> Mobiliarioplaca { get; set; }
        public DbSet<Mobiliarioproprietario> MobiliarioProprietario { get; set; }
        public DbSet<Mobiliariovs> MobiliarioVs { get; set; }
        public DbSet<ObsCidadao> ObsCidadao { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Pedologia> Pedologia { get; set; }
        public DbSet<Periodomei> Periodomei { get; set; }
        public DbSet<Processo_historico> Processo_historico { get; set; }
        public DbSet<Processodoc> Processodoc { get; set; }
        public DbSet<Processoend> Processoend { get; set; }
        public DbSet<Processogti> Processogti { get; set; }
        public DbSet<Processocidadao> ProcessoCidadao { get; set; }
        public DbSet<Profissao> Profissao { get; set; }
        public DbSet<Proprietario> Proprietario { get; set; }
        public DbSet<Security_event> Security_Event { get; set; }
        public DbSet<sil> Sil { get; set; }
        public DbSet<Situacao> Situacao { get; set; }
        public DbSet<SpExtrato> SpExtrato { get; set; }
        public DbSet<Tabelaiss> Tabelaiss { get; set; }
        public DbSet<Testadacondominio> Testadacondominio { get; set; }
        public DbSet<Testada> Testada { get; set; }
        public DbSet<Tipoconstr> Tipoconstr { get; set; }
        public DbSet<Topografia> Topografia { get; set; }
        public DbSet<Tramitacao> Tramitacao { get; set; }
        public DbSet<Tramitacaocc> Tramitacaocc { get; set; }
        public DbSet<Uf> Uf { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Usuariocc> Usuariocc { get; set; }
        public DbSet<Usoconstr> Usoconstr { get; set; }
        public DbSet<Usoterreno> Usoterreno { get; set; }
        public DbSet<Usuariofunc> Usuariofunc { get; set; }
        public DbSet<Vwproprietarioduplicado> Vwproprietarioduplicado { get; set; }
    }
}
