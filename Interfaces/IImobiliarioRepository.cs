using GTI_v4.Classes;
using GTI_v4.Models;
using System.Collections.Generic;

namespace GTI_v4.Interfaces {
    public interface IImobiliarioRepository {
        EnderecoStruct Dados_Endereco(int Codigo, GtiCore.TipoEndereco Tipo);
        ImovelStruct Dados_Imovel(int nCodigo);
        bool Existe_Bairro_Condominio(string UF, int Cidade, int Bairro);
        bool Existe_Bairro_Entrega(string UF, int Cidade, int Bairro);
        bool Existe_Bairro_Localizacao(string UF, int Cidade, int Bairro);
        bool Existe_Imovel(int nCodigo);
        int Existe_Imovel(int distrito, int setor, int quadra, int lote, int unidade, int subunidade);
        int Qtde_Imovel_Cidadao(int CodigoImovel);
        List<FacequadraStruct> Lista_FaceQuadra(int distrito, int setor, int quadra, int face);
        List<LogradouroStruct> Lista_Logradouro(string Filter = "");
        List<ProprietarioStruct> Lista_Proprietario(int CodigoImovel, bool Principal = false);
        int Retorna_Imovel_Inscricao(int distrito, int setor, int quadra, int lote, int face, int unidade, int subunidade);
        decimal Soma_Area(int Codigo);

    }
}
