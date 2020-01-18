using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTI_v4.Models {
    public class Historicocidadao {
        [Key]
        public int Id { get; set; }
        public int Codigo { get; set; }
        public DateTime? Data { get; set; }
        public string Obs { get; set; }
        public int? Userid { get; set; }
    }

    public class Historico_CidadaoStruct {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public DateTime? Data { get; set; }
        public string Obs { get; set; }
        public int? Id_Usuario { get; set; }
        public string Nome_Usuario { get; set; }
    }

}
