﻿

namespace GTI_v4.Models {
    public class Endereco {

        public int Id_pais { get; set; }
        public string Nome_pais { get; set; }
        public string Sigla_uf { get; set; }
        public string Nome_uf { get; set; }
        public int Id_cidade { get; set; }
        public string Nome_cidade { get; set; }
        public int Id_bairro { get; set; }
        public string Nome_bairro { get; set; }
        public int Numero_imovel { get; set; }
        public string Complemento { get; set; }
        public int Id_logradouro { get; set; }
        public string Nome_logradouro { get; set; }
        public int Cep { get; set; }
        public string Email { get; set; }
        public bool Cancelar { get; set; }
        public string Telefone { get; set; }
        public bool? TemFone { get; set; }
        public bool? WhatsApp { get; set; }
    }
}
