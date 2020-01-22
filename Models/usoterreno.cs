

using System.ComponentModel.DataAnnotations;

namespace GTI_v4.Models {
    public class Usoterreno {
        [Key]
        public short Codusoterreno { get; set; }
        public string Descusoterreno { get; set; }
    }
}
