using System;
using System.Linq;
using GTI_v4.Interfaces;

namespace GTI_v4.Repository {
    public class EmpresaRepository:IEmpresaRepository {
        private readonly string _connection;

        public EmpresaRepository(string Connection) {
            _connection = Connection;
        }

        public bool Existe_Bairro(string UF, int Cidade, int Bairro) {
            bool bRet = false;
            using (GTI_Context db = new GTI_Context(_connection)) {
                var existingReg = db.Mobiliario.Count(a => a.Siglauf == UF && a.Codcidade == Cidade && a.Codbairro == Bairro);
                if (existingReg != 0) {
                    bRet = true;
                }
            }
            return bRet;
        }

        public bool Existe_Bairro_Entrega(string UF, int Cidade, int Bairro) {
            bool bRet = false;
            using (GTI_Context db = new GTI_Context(_connection)) {
                var existingReg = db.MobiliarioEndEntrega.Count(a => a.Uf == UF && a.Codcidade == Cidade && a.Codbairro == Bairro);
                if (existingReg != 0) {
                    bRet = true;
                }
            }
            return bRet;
        }

    }
}
