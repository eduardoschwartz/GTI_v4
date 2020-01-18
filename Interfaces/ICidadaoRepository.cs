using GTI_v4.Models;
using System;
using System.Collections.Generic;

namespace GTI_v4.Interfaces {
    public interface ICidadaoRepository {
        bool ExisteCidadao(int nCodigo);
        List<Profissao> Lista_Profissao();
        CidadaoStruct LoadReg(Int32 nCodigo);

    }
}
