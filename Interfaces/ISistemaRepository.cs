using GTI_v4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTI_v4.Interfaces {
    public interface ISistemaRepository {
        string Retorna_User_Password(string login);
        string Retorna_User_FullName(string login);
        int Retorna_User_LoginId(string login);
        Exception Alterar_Senha(Usuario reg);
        Exception Alterar_Usuario(Usuario reg);
        UsuarioStruct Retorna_Usuario(int Id);
        int GetSizeofBinary();
        string GetUserBinary(int id);
    }
}
