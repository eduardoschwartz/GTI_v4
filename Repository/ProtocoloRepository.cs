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



    }
}
