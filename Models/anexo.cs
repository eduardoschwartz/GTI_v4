﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTI_v4.Models {
    public class Anexo {
        [Key]
        [Column(Order = 1)]
        public short Ano { get; set; }
        [Key]
        [Column(Order = 2)]
        public int Numero { get; set; }
        public short Anoanexo { get; set; }
        public int Numeroanexo { get; set; }
    }
}
