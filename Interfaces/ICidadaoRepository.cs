using GTI_v4.Models;
using System;
using System.Collections.Generic;

namespace GTI_v4.Interfaces {
    public interface ICidadaoRepository {
        Exception Alterar_cidadao(Cidadao reg);
        Exception Excluir_cidadao(int Codigo);
        bool ExisteCidadao(int nCodigo);
        Exception Incluir_cidadao(Cidadao reg);
        Exception Incluir_observacao_cidadao(ObsCidadao reg);
        List<Historico_CidadaoStruct> Lista_Historico(int CodigoCidadao);
        List<Observacao_CidadaoStruct> Lista_Observacao(int CodigoCidadao);
        List<Profissao> Lista_Profissao();
        CidadaoStruct LoadReg(int nCodigo);
        int Retorna_Ultimo_Codigo_Cidadao();
    }
}
