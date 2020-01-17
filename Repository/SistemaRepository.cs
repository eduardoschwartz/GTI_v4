using GTI_v4.Classes;
using GTI_v4.Interfaces;
using GTI_v4.Models;
using System;
using System.Linq;

namespace GTI_v4.Repository {
    public class SistemaRepository:ISistemaRepository {
        public string Connection { get; }

        public SistemaRepository(string _connection) {
            Connection = _connection;
        }
        
        public string Retorna_User_Password(string login) {
            using (GTI_Context db = new GTI_Context(Connection)) {
                string Sql = (from u in db.Usuario where u.Nomelogin == login select u.Senha2).FirstOrDefault();
                return Sql;
            }
        }

        public string Retorna_User_FullName(string login) {
            using (GTI_Context db = new GTI_Context(Connection)) {
                string Sql = (from u in db.Usuario where u.Nomelogin == login select u.Senha2).FirstOrDefault();
                return Sql;
            }
        }

        public int Retorna_User_LoginId(string login) {
            using (GTI_Context db = new GTI_Context(Connection)) {
                int Sql = (from u in db.Usuario where u.Nomelogin == login select (int)u.Id).FirstOrDefault();
                return Sql;
            }
        }


        public Exception Alterar_Usuario(Usuario reg) {
            using (GTI_Context db = new GTI_Context(Connection)) {
                Usuario b = db.Usuario.First(i => i.Id == reg.Id);
                b.Nomecompleto = reg.Nomecompleto;
                b.Nomelogin = reg.Nomelogin;
                b.Setor_atual = reg.Setor_atual;
                b.Ativo = reg.Ativo;
                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public Exception Alterar_Senha(Usuario reg) {
            using (GTI_Context db = new GTI_Context(Connection)) {
                string sLogin = reg.Nomelogin;
                Usuario b = db.Usuario.First(i => i.Nomelogin == sLogin);
                b.Senha2 = reg.Senha2;
                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public UsuarioStruct Retorna_Usuario(int Id) {
            using (GTI_Context db = new GTI_Context(Connection)) {
                var reg = (from t in db.Usuario
                           join cc in db.Centrocusto on t.Setor_atual equals cc.Codigo into tcc from cc in tcc.DefaultIfEmpty()
                           where t.Id == Id
                           orderby t.Nomelogin select new UsuarioStruct {
                               Nome_login = t.Nomelogin, Nome_completo = t.Nomecompleto, Ativo = t.Ativo,
                               Id = t.Id, Senha = t.Senha, Senha2 = t.Senha2, Setor_atual = t.Setor_atual, Nome_setor = cc.Descricao
                           }).FirstOrDefault();
                UsuarioStruct Sql = new UsuarioStruct {
                    Id = reg.Id,
                    Nome_completo = reg.Nome_completo,
                    Nome_login = reg.Nome_login,
                    Senha = reg.Senha,
                    Senha2 = reg.Senha2,
                    Setor_atual = reg.Setor_atual,
                    Nome_setor = reg.Nome_setor,
                    Ativo = reg.Ativo
                };
                return Sql;
            }
        }

        public int GetSizeofBinary() {
            using (GTI_Context db = new GTI_Context(Connection)) {
                int nSize = (from t in db.Security_Event orderby t.Id descending select t.Id).FirstOrDefault();
                return nSize;
            }
        }

        public string GetUserBinary(int id) {
            using (GTI_Context db = new GTI_Context(Connection)) {
                string Sql = (from u in db.Usuario where u.Id == id select u.Userbinary).FirstOrDefault();
                if (Sql == null) {
                    Sql = "0";
                    int nSize = GetSizeofBinary();
                    int nDif = nSize - Sql.Length;
                    string sZero = new string('0', nDif);
                    Sql += sZero;
                    return GtiCore.Encrypt(Sql);
                }
                return Sql;
            }
        }


    }
}
