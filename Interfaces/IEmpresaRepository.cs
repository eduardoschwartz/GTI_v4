using System;


namespace GTI_v4.Interfaces {
    public interface IEmpresaRepository {
        bool Existe_Bairro(string UF, int Cidade, int Bairro);
        bool Existe_Bairro_Entrega(string UF, int Cidade, int Bairro);
    }
}
