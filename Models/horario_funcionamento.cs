using System.ComponentModel.DataAnnotations;

namespace GTI_v4.Models {
    public class Horario_funcionamento {
        [Key]
        public int Id { get; set; }
        public string descricao { get; set; }
    }
}
