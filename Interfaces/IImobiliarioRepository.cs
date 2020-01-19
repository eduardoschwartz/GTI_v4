
namespace GTI_v4.Interfaces {
    public interface IImobiliarioRepository {
        bool Existe_Bairro_Condominio(string UF, int Cidade, int Bairro);
        bool Existe_Bairro_Entrega(string UF, int Cidade, int Bairro);
        bool Existe_Bairro_Localizacao(string UF, int Cidade, int Bairro);
    }
}
