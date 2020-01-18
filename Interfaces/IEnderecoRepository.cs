using GTI_v4.Models;
using System;
using System.Collections.Generic;

namespace GTI_v4.Interfaces {
    public interface IEnderecoRepository {
        Exception Alterar_Bairro(Bairro reg);
        Exception Alterar_Pais(Pais reg);
        bool Bairro_uso_cidadao(string id_UF, int id_cidade, int id_bairro);
        bool Bairro_uso_empresa(string id_UF, int id_cidade, int id_bairro);
        bool Bairro_uso_processo(string id_UF, int id_cidade, int id_bairro);
        Exception Excluir_Bairro(Bairro reg);
        Exception Excluir_Pais(Pais reg);
        Exception Incluir_bairro(Bairro reg);
        Exception Incluir_Pais(Pais reg);
        List<Bairro> Lista_Bairro(string UF, int cidade);
        List<Cidade> Lista_Cidade(string sUF);
        List<Logradouro> Lista_Logradouro(String Filter = "");
        List<Pais> Lista_Pais();
        List<Uf> Lista_UF();
        bool Pais_uso_cidadao(int id_pais);
        string Retorna_Bairro(string UF, int Cidade, int Bairro);
        int RetornaCep(Int32 CodigoLogradouro, Int16 Numero);
        string Retorna_Cidade(string UF, int Codigo);
        string Retorna_Logradouro(int Codigo);
        string Retorna_Pais(int Codigo);

    }
}
