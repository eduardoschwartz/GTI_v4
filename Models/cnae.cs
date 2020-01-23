
using System.ComponentModel.DataAnnotations;

namespace GTI_v4.Models {
    public class Cnae {
        [Key]
        public string cnae { get; set; }
        public string Descricao { get; set; }
    }
}
