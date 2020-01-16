using GTI_v4.Classes;
using GTI_v4.Interfaces;
using System.Linq;

namespace GTI_v4.Repository {
    public class SistemaRepository:ISistemaRepository {

        private static string _connection=GtiCore.Connection_Name();

        public string Retorna_User_Password(string login) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                string Sql = (from u in db.Usuario where u.Nomelogin == login select u.Senha2).FirstOrDefault();
                return Sql;
            }
        }

        public string Retorna_User_FullName(string login) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                string Sql = (from u in db.Usuario where u.Nomelogin == login select u.Senha2).FirstOrDefault();
                return Sql;
            }
        }

        public int Retorna_User_LoginId(string login) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                int Sql = (from u in db.Usuario where u.Nomelogin == login select (int)u.Id).FirstOrDefault();
                return Sql;
            }
        }


    }
}
