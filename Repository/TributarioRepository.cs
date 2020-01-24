using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GTI_v4.Interfaces;
using GTI_v4.Models;

namespace GTI_v4.Repository {
   public class TributarioRepository:ITributarioRepository {
        private readonly string connection;

        public TributarioRepository(string Connection) {
            connection = Connection;
        }

        public int DvDocumento(int Numero) {
            string sFromN = Numero.ToString("00000000");
            int nTotPosAtual = 0;

            nTotPosAtual = Convert.ToInt32(sFromN.Substring(0, 1)) * 7;
            nTotPosAtual += Convert.ToInt32(sFromN.Substring(1, 1)) * 3;
            nTotPosAtual += Convert.ToInt32(sFromN.Substring(2, 1)) * 9;
            nTotPosAtual += Convert.ToInt32(sFromN.Substring(3, 1)) * 7;
            nTotPosAtual += Convert.ToInt32(sFromN.Substring(4, 1)) * 3;
            nTotPosAtual += Convert.ToInt32(sFromN.Substring(5, 1)) * 9;
            nTotPosAtual += Convert.ToInt32(sFromN.Substring(6, 1)) * 7;
            nTotPosAtual += Convert.ToInt32(sFromN.Substring(7, 1)) * 3;

            string ret = nTotPosAtual.ToString();
            return Convert.ToInt32(ret.Substring(ret.Length - 1));

        }

        public List<SpExtrato> Lista_Extrato_Tributo(int Codigo, short Ano1 = 1990, short Ano2 = 2050, short Lancamento1 = 1, short Lancamento2 = 99, short Sequencia1 = 0, short Sequencia2 = 9999,
    short Parcela1 = 0, short Parcela2 = 999, short Complemento1 = 0, short Complemento2 = 999, short Status1 = 0, short Status2 = 99, DateTime? Data_Atualizacao = null, string Usuario = "") {
            using (GTI_Context db = new GTI_Context(connection)) {
                db.Database.CommandTimeout = 180;
                var prmCod1 = new SqlParameter { ParameterName = "@CodReduz1", SqlDbType = SqlDbType.Int, SqlValue = Codigo };
                var prmCod2 = new SqlParameter { ParameterName = "@CodReduz2", SqlDbType = SqlDbType.Int, SqlValue = Codigo };
                var prmAno1 = new SqlParameter { ParameterName = "@AnoExercicio1", SqlDbType = SqlDbType.SmallInt, SqlValue = Ano1 };
                var prmAno2 = new SqlParameter { ParameterName = "@AnoExercicio2", SqlDbType = SqlDbType.SmallInt, SqlValue = Ano2 };
                var prmLanc1 = new SqlParameter { ParameterName = "@CodLancamento1", SqlDbType = SqlDbType.SmallInt, SqlValue = Lancamento1 };
                var prmLanc2 = new SqlParameter { ParameterName = "@CodLancamento2", SqlDbType = SqlDbType.SmallInt, SqlValue = Lancamento2 };
                var prmSeq1 = new SqlParameter { ParameterName = "@SeqLancamento1", SqlDbType = SqlDbType.SmallInt, SqlValue = Sequencia1 };
                var prmSeq2 = new SqlParameter { ParameterName = "@SeqLancamento2", SqlDbType = SqlDbType.SmallInt, SqlValue = Sequencia2 };
                var prmPc1 = new SqlParameter { ParameterName = "@NumParcela1", SqlDbType = SqlDbType.SmallInt, SqlValue = Parcela1 };
                var prmPc2 = new SqlParameter { ParameterName = "@NumParcela2", SqlDbType = SqlDbType.SmallInt, SqlValue = Parcela2 };
                var prmCp1 = new SqlParameter { ParameterName = "@CodComplemento1", SqlDbType = SqlDbType.SmallInt, SqlValue = Complemento1 };
                var prmCp2 = new SqlParameter { ParameterName = "@CodComplemento2", SqlDbType = SqlDbType.SmallInt, SqlValue = Complemento2 };
                var prmSta1 = new SqlParameter { ParameterName = "@Status1", SqlDbType = SqlDbType.SmallInt, SqlValue = Status1 };
                var prmSta2 = new SqlParameter { ParameterName = "@Status2", SqlDbType = SqlDbType.SmallInt, SqlValue = Status2 };
                var prmDtA = new SqlParameter { ParameterName = "@DataNow", SqlDbType = SqlDbType.SmallDateTime, SqlValue = Data_Atualizacao == null ? DateTime.Now : Data_Atualizacao };
                var prmUser = new SqlParameter { ParameterName = "@Usuario", SqlDbType = SqlDbType.VarChar, SqlValue = Usuario };

                var result = db.SpExtrato.SqlQuery("EXEC spEXTRATONEW @CodReduz1, @CodReduz2, @AnoExercicio1 ,@AnoExercicio2 ,@CodLancamento1 ,@CodLancamento2, @SeqLancamento1 ,@SeqLancamento2, @NumParcela1, @NumParcela2, @CodComplemento1, @CodComplemento2, @Status1, @Status2, @DataNow, @Usuario ",
                    prmCod1, prmCod2, prmAno1, prmAno2, prmLanc1, prmLanc2, prmSeq1, prmSeq2, prmPc1, prmPc2, prmCp1, prmCp2, prmSta1, prmSta2, prmDtA, prmUser).ToList();

                List<SpExtrato> ListaDebito = new List<SpExtrato>();
                foreach (SpExtrato item in result) {
                    SpExtrato reg = new SpExtrato {
                        Anoexercicio = item.Anoexercicio,
                        Codlancamento = item.Codlancamento,
                        Desclancamento = item.Desclancamento,
                        Seqlancamento = item.Seqlancamento,
                        Numparcela = item.Numparcela,
                        Codcomplemento = item.Codcomplemento,
                        Datavencimento = item.Datavencimento,
                        Datadebase = item.Datadebase,
                        Datapagamento = item.Datapagamento,
                        Codreduzido = item.Codreduzido,
                        Statuslanc = item.Statuslanc,
                        Situacao = item.Situacao,
                        Datainscricao = item.Datainscricao,
                        Certidao = item.Certidao,
                        Numlivro = item.Numlivro,
                        Pagina = item.Pagina,
                        Numdocumento = item.Numdocumento,
                        Dataajuiza = item.Dataajuiza,
                        Valortributo = item.Valortributo,
                        Valormulta = item.Valormulta,
                        Valorjuros = item.Valorjuros,
                        Valorcorrecao = item.Valorcorrecao,
                        Valortotal = item.Valortotal,
                        Valorpago = item.Valorpago,
                        Valorpagoreal = item.Valorpagoreal,
                        Abrevtributo = item.Abrevtributo,
                        Codtributo = item.Codtributo,
                        UserId = item.UserId
                    };
                    reg.Valortributo = item.Valortributo;
                    reg.Anoexecfiscal = item.Anoexecfiscal;
                    reg.Numexecfiscal = item.Numexecfiscal;
                    reg.Processocnj = item.Processocnj;
                    reg.Prot_certidao = item.Prot_certidao;
                    reg.Prot_dtremessa = item.Prot_dtremessa;
                    ListaDebito.Add(reg);
                }
                return ListaDebito;
            }

        }












    }
}
