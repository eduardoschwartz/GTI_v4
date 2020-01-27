using GTI_v4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTI_v4.Interfaces {
    public interface ISistemaRepository {
        Exception Alterar_Senha(Usuario reg);
        Exception Alterar_Usuario(Usuario reg);
        Contribuinte_Header Contribuinte_Header(int _codigo, bool _principal = false);
        int GetSizeofBinary();
        string GetUserBinary(int id);
        List<Security_event> Lista_Sec_Eventos();
        List<UsuarioStruct> Lista_Usuarios();
        string Retorna_User_FullName(string login);
        int Retorna_User_LoginId(string login);
        string Retorna_User_LoginName(int idUser);
        string Retorna_User_Password(string login);
        UsuarioStruct Retorna_Usuario(int Id);
        Exception SaveUserBinary(Usuario reg);
    }
}
