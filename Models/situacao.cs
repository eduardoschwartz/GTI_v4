
using System.ComponentModel.DataAnnotations;

namespace GTI_v4.Models {
    public class Situacao {
        [Key]
        public short Codsituacao { get; set; }
        public string Descsituacao { get; set; }
    }
}
