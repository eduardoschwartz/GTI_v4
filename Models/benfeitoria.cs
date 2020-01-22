using System.ComponentModel.DataAnnotations;

namespace GTI_v4.Models {
    public class Benfeitoria {
        [Key]
        public short Codbenfeitoria { get; set; }
        public string Descbenfeitoria { get; set; }
    }
}
