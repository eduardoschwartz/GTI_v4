using System;
using GTI_v4.Interfaces;

namespace GTI_v4.Repository {
   public class TributarioRepository:ITributarioRepository {
 
        public int DvDocumento(int Numero) {
            string sFromN = Numero.ToString("00000000");
            int nTotPosAtual = 0;

            nTotPosAtual = Convert.ToInt32(sFromN.Substring(0, 1)) * 7;
            nTotPosAtual += Convert.ToInt32(sFromN.Substring(1, 1)) * 3;
            nTotPosAtual += Convert.ToInt32(sFromN.Substring(2, 1)) * 9;
            nTotPosAtual += Convert.ToInt32(sFromN.Substring(3, 1)) * 7;
            nTotPosAtual += Convert.ToInt32(sFromN.Substring(4, 1)) * 3;
            nTotPosAtual += Convert.ToInt32(sFromN.Substring(5, 1)) * 9;
            nTotPosAtual += Convert.ToInt32(sFromN.Substring(6, 1)) * 7;
            nTotPosAtual += Convert.ToInt32(sFromN.Substring(7, 1)) * 3;

            string ret = nTotPosAtual.ToString();
            return Convert.ToInt32(ret.Substring(ret.Length - 1));

        }
    }
}
