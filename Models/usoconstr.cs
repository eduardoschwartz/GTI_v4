﻿
using System.ComponentModel.DataAnnotations;

namespace GTI_v4.Models {
    public class Usoconstr {
        [Key]
        public short Codusoconstr { get; set; }
        public string Descusoconstr { get; set; }
    }
}
