using GTI_v4.Models;
using System;
using System.Collections.Generic;

namespace GTI_v4.Interfaces {
    public interface ICidadaoRepository {
        Exception Alterar_cidadao(Cidadao reg);
        Exception Alterar_Profissao(Profissao reg);
        CidadaoStruct Dados_Cidadao(int _codigo);
        Exception Excluir_cidadao(int Codigo);
        Exception Excluir_Profissao(Profissao reg);
        bool Existe_Bairro(string UF, int Cidade, int Bairro);
        bool Existe_Cidadao(int nCodigo);
        bool Existe_Pais(int Pais);
        bool Existe_Profissao(int Profissao);
        Exception Incluir_cidadao(Cidadao reg);
        Exception Incluir_observacao_cidadao(ObsCidadao reg);
        Exception Incluir_profissao(Profissao reg);
        List<Historico_CidadaoStruct> Lista_Historico(int CodigoCidadao);
        List<Observacao_CidadaoStruct> Lista_Observacao(int CodigoCidadao);
        List<Profissao> Lista_Profissao();
        CidadaoStruct LoadReg(int nCodigo);
        bool Profissao_cidadao(int id_profissao);
        Cidadao Retorna_Cidadao(int Codigo);
        int Retorna_Ultimo_Codigo_Cidadao();
    }
}
