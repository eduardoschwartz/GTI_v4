using GTI_v4.Models;
using System;
using System.Collections.Generic;

namespace GTI_v4.Interfaces {
    public interface ITributarioRepository {
        int DvDocumento(int Numero);
        List<SpExtrato> Lista_Extrato_Tributo(int Codigo, short Ano1 = 1990, short Ano2 = 2050, short Lancamento1 = 1, short Lancamento2 = 99,
                                              short Sequencia1 = 0, short Sequencia2 = 9999, short Parcela1 = 0, short Parcela2 = 999,
                                              short Complemento1 = 0, short Complemento2 = 999, short Status1 = 0, short Status2 = 99,
                                              DateTime? Data_Atualizacao = null, string Usuario = "");

    }
}
