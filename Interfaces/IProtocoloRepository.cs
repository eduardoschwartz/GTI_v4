using GTI_v4.Models;
using System.Collections.Generic;

namespace GTI_v4.Interfaces {
    public interface IProtocoloRepository {
        List<Centrocusto> Lista_Local(bool Somente_Ativo, bool Local);
        int DvProcesso(int Numero);
    }
}
