﻿using GTI_v4.Forms;
using GTI_v4.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GTI_v4.Classes {
    public static class GtiCore {

        public enum ETweakMode { Normal, AllLetters, AllLettersAllCaps, AllLettersAllSmall, AlphaNumeric, AlphaNumericAllCaps, AlphaNumericAllSmall, IntegerPositive, DecimalPositive };
        public enum TipoCadastro { Imovel, Empresa, Cidadao }
        public enum EventoForm { Nenhum = 0, Insert = 1, Edit = 2, Delete = 3, Print = 4 }
        public enum TipoEndereco { Local, Proprietario, Entrega }

        private static readonly byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static readonly byte[] iv = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();

        private static string _up;
        private static string _baseDados;
        private static string _ul;
        
        public static string BaseDados { get => _baseDados; set => _baseDados = value; }
        public static string Ul { get => _ul; set => _ul = value; }
        public static string Up { get => _up; set => _up = value; }

        public static List<ToolStripMenuItem> GetItems(MenuStrip menuStrip) {
            List<ToolStripMenuItem> myItems = new List<ToolStripMenuItem>();
            foreach (ToolStripMenuItem i in menuStrip.Items) {
                GetMenuItems(i, myItems);
            }
            return myItems;
        }

        private static void GetMenuItems(ToolStripMenuItem item, List<ToolStripMenuItem> items) {
            items.Add(item);
            foreach (ToolStripItem i in item.DropDownItems) {
                if (i is ToolStripMenuItem) {
                    GetMenuItems((ToolStripMenuItem)i, items);
                }
            }
        }

        public static void Ocupado(Form frm) {
            Forms.Main f1 = (Forms.Main)Application.OpenForms["Main"];
            f1.LedGreen.Enabled = false;
            f1.LedRed.Enabled = true;
            f1.Refresh();
            frm.Cursor = Cursors.WaitCursor;
            frm.Refresh();
            System.Windows.Forms.Application.DoEvents();
        }

        public static void Liberado(Form frm) {
            Forms.Main f1 = (Forms.Main)Application.OpenForms["Main"];
            f1.LedGreen.Enabled = true;
            f1.LedRed.Enabled = false;
            frm.Cursor = Cursors.Default;
            frm.Refresh();
            System.Windows.Forms.Application.DoEvents();
        }

        public static bool IsNumeric(object Expression) {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            } catch { } // just dismiss errors but return false
            return false;
        }

        public static bool IsDate(String date) {
            try {
                DateTime dt = DateTime.Parse(date);
                return true;
            } catch {
                return false;
            }
        }

        public static string ExtractNumber(string original) {
            return new string(original.Where(c => Char.IsDigit(c)).ToArray());
        }

        public static bool IsEmptyDate(string sDate) {
            if (sDate == "__/__/____" | sDate == "  /  /" | sDate == "" | sDate == "  /  /    ")
                return (true);
            else
                return (false);
        }

        public static bool Valida_Email(string Email) {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (rg.IsMatch(Email)) {
                return true;
            } else {
                return false;
            }
        }

        public static string StringRight(string value, int length) {
            return value.Substring(value.Length - length);
        }

        public static string Encrypt(string clearText) {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(clearText);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        public static string Decrypt(string cipherText) {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
            byte[] inputbuffer = Convert.FromBase64String(cipherText);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Encoding.Unicode.GetString(outputBuffer);
        }

        public static string DecryptDatArray(byte[] aArray) {
            string Result = "";
            for (int nPos = 0; nPos < aArray.Length; nPos++) {
                if (aArray[nPos].ToString() != null)
                    Result += Convert.ToChar(aArray[nPos]);
            }
            return Result;
        }

        public static string ValueDatReg(string sReg) {
            int nPos;
            for (nPos = 2; nPos < sReg.Length; nPos++) {
                if (sReg.Substring(nPos, 1) == "#") {
                    if (sReg.Substring(nPos, 2) == "#%")
                        break;
                }
            }
            return sReg.Substring(2, nPos - 2);
        }

        public static ArrayList ParseDatString(string sReg) {
            string sField = "{";
            ArrayList aFields = new ArrayList();
            char[] delimiters = new char[] { '#', '%' };
            string[] aString = sReg.Split(delimiters);
            for (int i = 0; i < aString.Length; i++) {
                if (aString[i].ToString() != "")
                    sField += aString[i].ToString() + ",";
            }
            sField = sField.Substring(0, sField.Length - 1) + "}";
            aFields.Add(sField);
            return aFields;
        }

        public static int GetRandomNumber() {
            lock (syncLock) { // synchronize
                return getrandom.Next(1, 2000000);
            }
        }

        public static string Retorna_Last_User() {
            return Properties.Settings.Default.LastUser;
        }

        public static string Connection_Name() {
            string connString = "";
            Ul = "gtisys"; Up = "everest";
            try {
                BaseDados = "Tributacao";
                connString = CreateConnectionString(Properties.Settings.Default.ServerName, Properties.Settings.Default.DataBaseReal, Ul, Up);//Base de testes por enquanto
            } catch (Exception ex) {
            }
            return connString;
        }

        public static string Connection_Name(string DataBase_Name) {
            string connString = "";
            Ul = "gtisys"; Up = "everest";
            try {
                BaseDados = DataBase_Name;
                connString = CreateConnectionString(Properties.Settings.Default.ServerName, DataBase_Name, Ul, Up);
            } catch (Exception) {
            }
            return connString;
        }

        public static string CreateConnectionString(string ServerName, string DataBaseName, string LoginName, string Pwd) {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = ServerName;
            sqlBuilder.InitialCatalog = DataBaseName;
            sqlBuilder.IntegratedSecurity = false;
            sqlBuilder.UserID = LoginName;
            sqlBuilder.Password = Pwd;
            return sqlBuilder.ConnectionString;
        }

        public static void UpdateUserBinary() {
            string _connection = GtiCore.Connection_Name();
            SistemaRepository _sistemaRepository = new SistemaRepository(_connection);
            string sTmp = _sistemaRepository.GetUserBinary(Properties.Settings.Default.UserId);
            int nSize = _sistemaRepository.GetSizeofBinary();
            GtiTypes.UserBinary = GtiCore.Decrypt(sTmp);
            if (nSize > GtiTypes.UserBinary.Length) {
                int nDif = nSize - GtiTypes.UserBinary.Length;
                sTmp = new string('0', nDif);
                GtiTypes.UserBinary += sTmp;
            }
        }

        public static bool GetBinaryAccess(int index) {
            string _acesso = GtiTypes.UserBinary;
            if (_acesso.Length < index)
                return false;
            else {
                if (_acesso.Substring(index - 1, 1) == "1")
                    return true;
                else
                    return false;
            }
        }

        public static char Tweak(TextBox txt, char sKey, ETweakMode Mode, int DecimalPlaces) {
            int nKey = Convert.ToInt16(sKey);
            char ch = (char)nKey;
            int Curpos = txt.SelectionStart;
            int nPos = 0;

            if (nKey == 8)
                return (ch);

            switch (Mode) {
                case ETweakMode.Normal:
                    return (ch);
                case ETweakMode.AllLetters:
                    if (IsCAPS(nKey) || IsSmall(nKey))
                        return (ch);
                    break;
                case ETweakMode.AllLettersAllCaps:
                    if (IsCAPS(nKey))
                        return (ch);
                    if (IsSmall(nKey)) {
                        nKey -= 32;
                        return (Convert.ToChar(nKey));
                    }
                    break;
                case ETweakMode.AllLettersAllSmall:
                    if (IsSmall(nKey))
                        return (ch);
                    if (IsCAPS(nKey)) {
                        nKey += 32;
                        return (Convert.ToChar(nKey));
                    }
                    break;
                case ETweakMode.AlphaNumeric:
                    if (IsCAPS(nKey) || IsSmall(nKey) || IsDigit(nKey))
                        return (ch);
                    break;
                case ETweakMode.IntegerPositive:
                    if (IsDigit(nKey))
                        return (ch);
                    break;
                case ETweakMode.DecimalPositive:
                    if (IsDigit(nKey)) {
                        if (txt.Text.Length == 0)
                            return (ch);
                        nPos = txt.Text.IndexOf(",", 0);
                        if (nPos == -1)
                            return (ch);
                        else {
                            if (txt.TextLength - txt.Text.IndexOf(",", 1) < DecimalPlaces + 1)
                                return (ch);
                        }
                    }
                    if (txt.Text.Length == 0)
                        return (ch);
                    nPos = txt.Text.IndexOf(",", 0);
                    if (ch == ',' && nPos == -1)
                        return (ch);
                    break;
                default:
                    return (ch);
            }//End switch
            ch = '\0';
            return (ch);
        }//End tweak()

        private static bool IsCAPS(int nKey) {
            bool bRet = false;
            if (nKey > 64 && nKey < 91)
                bRet = true;
            return bRet;
        }

        private static bool IsSmall(int nKey) {
            bool bRet = false;
            if (nKey > 96 && nKey < 123)
                bRet = true;
            return bRet;
        }

        private static bool IsDigit(int nKey) {
            bool bRet = false;
            if (nKey > 47 && nKey < 58)
                bRet = true;
            return bRet;
        }

        public static bool Valida_CPF(string cpf) {

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf, digito;
            int soma, resto;

            if (cpf == "00000000000" || cpf == "11111111111" || cpf == "22222222222" || cpf == "33333333333" || cpf == "44444444444" || cpf == "55555555555" ||
                cpf == "66666666666" || cpf == "77777777777" || cpf == "88888888888" || cpf == "99999999999")
                return false;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11) {
                return false;
            }
            tempCpf = cpf.Substring(0, 9);

            soma = 0;

            for (int i = 0; i < 9; i++) {
                soma += int.Parse(tempCpf[i].ToString()) * (multiplicador1[i]);
            }
            resto = soma % 11;

            if (resto < 2) {
                resto = 0;
            } else {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            int soma2 = 0;

            for (int i = 0; i < 10; i++) {
                soma2 += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma2 % 11;

            if (resto < 2) {
                resto = 0;
            } else {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static bool Valida_CNPJ(string vrCNPJ) {
            string CNPJ = vrCNPJ.Replace(".", "");
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");
            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            try {
                for (nrDig = 0; nrDig < 14; nrDig++) {
                    digitos[nrDig] = int.Parse(
                        CNPJ.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig + 1, 1)));

                    if (nrDig <= 12)

                        soma[1] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++) {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (
                         resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (digitos[12 + nrDig] == 0);
                    else
                        CNPJOk[nrDig] = (digitos[12 + nrDig] == (11 - resultado[nrDig]));
                }

                return (CNPJOk[0] && CNPJOk[1]);
            } catch {
                return false;
            }
        }

        public static List<ArrayList> ReadFromDatFile(string sFile, string sTable, bool bHeader = true) {

            byte[] aHeader = new byte[2];
            byte[] aReg = new byte[0];


            List<ArrayList> aLinhas = new List<ArrayList>();

            //open file
            if (!File.Exists(sFile)) return aLinhas;
            FileStream mFile = File.Open(sFile, FileMode.Open);
            long nSize = mFile.Length;
            while (mFile.Position < nSize) {
                Array.Resize(ref aReg, 0);
                int nChar = mFile.ReadByte();
                //find the begining of a register
                if (nChar == 124) {
                    //We need to find out the size of the Register
                    int nBookMark = Convert.ToInt32(mFile.Position);
                    //walk throught the record
                    while (mFile.Position < nSize) {
                        nChar = mFile.ReadByte();
                        if (nChar == 124) {
                            //we found the begining of the next record, this will be the size of the array aReg
                            int nNewSize = Convert.ToInt32(mFile.Position) - nBookMark - 3;
                            Array.Resize(ref aReg, nNewSize);
                            //back to the last position 
                            mFile.Position = nBookMark;
                            mFile.Read(aHeader, 0, 2);
                            if (DecryptDatArray(aHeader) == sTable) {
                                //read the register
                                mFile.Read(aReg, 0, aReg.Length);
                                string sRegister = DecryptDatArray(aReg);
                                //test the value
                                string sTestValue = ValueDatReg(sRegister);
                                char[] delimiters = new char[] { '#', '%' };
                                string[] aString = sRegister.Split(delimiters);
                                ArrayList aFields = new ArrayList();
                                aFields.AddRange(aString);
                            Inicio:;
                                for (int i = 0; i < aFields.Count; i++) {
                                    if (aFields[i].ToString() == "") {
                                        aFields.RemoveAt(i);
                                        goto Inicio;
                                    }
                                }
                                aLinhas.Add(aFields);
                                if (bHeader)
                                    goto CloseFile;
                            } else if (DecryptDatArray(aHeader) == "XX") {
                                //end of file
                                goto CloseFile;
                            }
                            break;
                        }
                    }
                    goto NextReg;
                }
            NextReg:;
            }
        //close and return
        CloseFile:;
            mFile.Close();
            mFile.Dispose();
            return aLinhas;
        }

        public static void CreateDatFile(string Path, List<string> aArray) {
            Encoding ANSI = Encoding.Default;
            using (StreamWriter sw = new StreamWriter(Path, false, ANSI)) {
                foreach (string item in aArray) {
                    sw.Write(item);
                }
                sw.Write("|XX");
                sw.Flush();
                sw.Close();
            }
        }

        public static string ConvertDatReg(string Prefix, string[] aArray) {
            string Result = "|" + Prefix;
            foreach (string item in aArray) {
                Result += "#%" + item;
            }
            return Result;
        }

        public static FileInfo GetFileInfo(string file, bool deleteIfExists = true) {
            var fi = new FileInfo(file);
            if (deleteIfExists && fi.Exists) {
                fi.Delete();  // ensures we create a new workbook
            }
            return fi;
        }

        public static DateTime Retorna_Data_Base_Sistema() {
            Main f1 = (Main)Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is Forms.Main);
            return f1.ReturnDataBaseValue();
        }

        public static string RetornaNumero(string Numero) {
            if (string.IsNullOrEmpty(Numero))
                return "0";
            else
                return Regex.Replace(Numero, @"[^\d]", "");
        }

        public static string Unifica_Cnae(int _divisao, int _grupo, int _classe, int _subclasse) {
            return _divisao.ToString("00") + _grupo.ToString("0") + _classe.ToString("00").Substring(0, 1) + "-" + _classe.ToString("00").Substring(1, 1) + "/" + _subclasse.ToString("00");
        }

        public static string Mask(string sSqlField) {
            return sSqlField.Replace("'", "''");
        }

        public static string Truncate(string str, int maxLength, string suffix) {
            if (str.Length > maxLength) {
                str = str.Substring(0, maxLength + 1);
                str = str.Substring(0, Math.Min(str.Length, str.LastIndexOf(" ") == -1 ? 0 : str.LastIndexOf(" ")));
                str = str + suffix;
            }
            return str.Trim();
        }

    }

}

