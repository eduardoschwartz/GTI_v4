using GTI_v4.Forms;
using GTI_v4.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GTI_v4.Classes {
    public static class GtiCore {

        public enum ETweakMode { Normal, AllLetters, AllLettersAllCaps, AllLettersAllSmall, AlphaNumeric, AlphaNumericAllCaps, AlphaNumericAllSmall, IntegerPositive, DecimalPositive };
        public enum LocalEndereco { Imovel, Empresa, Cidadao }
        public enum EventoForm { Nenhum = 0, Insert = 1, Edit = 2, Delete = 3, Print = 4 }

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

        public static bool IsNumeric(System.Object Expression) {
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
            Main f1 = (Main)Application.OpenForms["Main"];
            try {
                BaseDados = f1.DataBaseToolStripStatus.Text;
                connString = CreateConnectionString(Properties.Settings.Default.ServerName, Properties.Settings.Default.DataBaseTeste, Ul, Up);//Base de testes por enquanto
            } catch (Exception) {
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
            SistemaRepository _sistemaRepository = new SistemaRepository();
            string sTmp = _sistemaRepository.GetUserBinary(Properties.Settings.Default.UserId);
            int nSize = _sistemaRepository.GetSizeofBinary();
            GtiTypes.UserBinary = GtiCore.Decrypt(sTmp);
            if (nSize > GtiTypes.UserBinary.Length) {
                int nDif = nSize - GtiTypes.UserBinary.Length;
                sTmp = new string('0', nDif);
                GtiTypes.UserBinary += sTmp;
            }
        }


    }

}

