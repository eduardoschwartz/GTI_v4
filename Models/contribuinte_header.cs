using GTI_v4.Classes;
using System.ComponentModel.DataAnnotations;

namespace GTI_v4.Models {
    public class Contribuinte_Header {
        public GtiCore.TipoCadastro Tipo { get; set; }
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Cpf_cnpj { get; set; }
        public string Rg { get; set; }
        public string Inscricao { get; set; }
        public string Inscricao_Estadual { get; set; }
        public string Endereco { get; set; }
        public string Endereco_abreviado { get; set; }
        public short Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Nome_bairro { get; set; }
        public string Nome_cidade { get; set; }
        public string Nome_uf { get; set; }
        public string Endereco_entrega { get; set; }
        public string Endereco_entrega_abreviado { get; set; }
        public short Numero_entrega { get; set; }
        public string Complemento_entrega { get; set; }
        public string Cep_entrega { get; set; }
        public string Nome_bairro_entrega { get; set; }
        public string Nome_cidade_entrega { get; set; }
        public string Nome_uf_entrega { get; set; }
        public string Quadra_original { get; set; }
        public string Lote_original { get; set; }
        public string Atividade { get; set; }
        public GtiCore.TipoEndereco TipoEndereco { get; set; }
        public bool Ativo { get; set; }
    }
}
