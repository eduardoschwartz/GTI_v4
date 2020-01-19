using System.ComponentModel.DataAnnotations;

namespace GTI_v4.Models {
    public class Assunto {
        [Key]
        public short Codigo { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
