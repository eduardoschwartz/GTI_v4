using System;
using System.ComponentModel.DataAnnotations;

namespace GTI_v4.Models {
    public class sil {
        [Key]
        public int Sid { get; set; }
        public int Codigo { get; set; }
        public string Sil { get; set; }
        public string Protocolo { get; set; }
        public DateTime? Data_emissao { get; set; }
        public DateTime? Data_validade { get; set; }
        public Single? Area_imovel { get; set; }
    }
}
