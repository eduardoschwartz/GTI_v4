using System.ComponentModel.DataAnnotations;

namespace GTI_v4.Models {
    public class Security_event {
        [Key]
        public int Id { get; set; }
        public int IdMaster { get; set; }
        public string Descricao { get; set; }
    }
}
