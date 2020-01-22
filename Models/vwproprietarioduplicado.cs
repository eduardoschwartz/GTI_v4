

using System.ComponentModel.DataAnnotations;

namespace GTI_v4.Models {
    public class Vwproprietarioduplicado {
        [Key]
        public int Qtdeimovel { get; set; }
        public int Codproprietario { get; set; }
    }
}
