using System.ComponentModel.DataAnnotations;

namespace GTI_v4.Models {
    public class Horariofunc {
        [Key]
        public short Codhorario { get; set; }
        [StringLength(30)]
        public string Deschorario { get; set; }

    }
}
