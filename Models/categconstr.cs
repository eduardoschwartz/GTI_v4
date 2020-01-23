
using System.ComponentModel.DataAnnotations;

namespace GTI_v4.Models {
    public class Categconstr {
        [Key]
        public short Codcategconstr { get; set; }
        public string Desccategconstr { get; set; }
    }
}
