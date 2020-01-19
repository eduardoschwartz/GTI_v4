using GTI_v4.Interfaces;
using System;
using System.Linq;

namespace GTI_v4.Repository {
    public class ImobiliarioRepository:IImobiliarioRepository {
        private readonly string _connection;

        public ImobiliarioRepository(string Connection) {
            _connection = Connection;
        }

        public bool Existe_Bairro_Localizacao(string UF, int Cidade, int Bairro) {
            bool bRet = false;
            using (GTI_Context db = new GTI_Context(_connection)) {
                var existingReg = db.Cadimob.Count(a => a.Li_uf == UF && a.Li_codcidade == Cidade && a.Li_codbairro == Bairro);
                if (existingReg != 0) {
                    bRet = true;
                }
            }
            return bRet;
        }

        public bool Existe_Bairro_Condominio(string UF, int Cidade, int Bairro) {
            bool bRet = false;
            using (GTI_Context db = new GTI_Context(_connection)) {
                var existingReg = db.Condominio.Count(a => a.Cd_uf == UF && a.Cd_codcidade == Cidade && a.Cd_codbairro == Bairro);
                if (existingReg != 0) {
                    bRet = true;
                }
            }
            return bRet;
        }

        public bool Existe_Bairro_Entrega(string UF, int Cidade, int Bairro) {
            bool bRet = false;
            using (GTI_Context db = new GTI_Context(_connection)) {
                var existingReg = db.EndEntrega.Count(a => a.Ee_uf == UF && a.Ee_cidade == Cidade && a.Ee_bairro == Bairro);
                if (existingReg != 0) {
                    bRet = true;
                }
            }
            return bRet;
        }
    }
}
