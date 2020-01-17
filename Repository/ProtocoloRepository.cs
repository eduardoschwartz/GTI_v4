using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTI_v4.Classes;
using GTI_v4.Interfaces;
using GTI_v4.Models;

namespace GTI_v4.Repository {
    public class ProtocoloRepository :IProtocoloRepository{
        private static string _connection = GtiCore.Connection_Name();

        public List<Centrocusto> Lista_Local(bool Somente_Ativo, bool Local) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var Sql = (from c in db.Centrocusto select c);
                if (Somente_Ativo)
                    Sql = Sql.Where(c => c.Ativo == true);
                if (Local)
                    Sql = Sql.Where(c => c.Local == true);
                Sql = Sql.OrderBy(c => c.Descricao);
                return Sql.ToList();
            }
        }

        ///<summary> Retorna o dígito verificador de um número de processo.
        ///O dígito verificador é o mesmo para todos os números iguais, independente do ano do processo.
        ///</summary>
        public int DvProcesso(int Numero) {
            int soma = 0, index = 0, Mult = 6;
            string sNumProc = Numero.ToString().PadLeft(5, '0');
            while (index < 5) {
                int nChar = Convert.ToInt32(sNumProc.Substring(index, 1));
                soma += (Mult * nChar);
                Mult--;
                index++;
            }

            int DigAux = soma % 11;
            int Digito = 11 - DigAux;
            if (Digito == 10)
                Digito = 0;
            if (Digito == 11)
                Digito = 1;

            return Digito;
        }

    }
}
