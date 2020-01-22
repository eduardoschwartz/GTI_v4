using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTI_v4.Classes;
using GTI_v4.Interfaces;
using GTI_v4.Models;

namespace GTI_v4.Repository {
    public class ProtocoloRepository :IProtocoloRepository{
        ICidadaoRepository _cidadaoRepository = new CidadaoRepository(GtiCore.Connection_Name());
        ISistemaRepository _sistemaRepository = new SistemaRepository(GtiCore.Connection_Name());

        private readonly string _connection;

        public ProtocoloRepository(string Connection) {
            _connection = Connection;
        }

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

        public List<Assunto> Lista_Assunto(bool Somente_Ativo, bool Somente_Inativo, string Filter = "") {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var Sql = (from c in db.Assunto select c);
                Sql = Sql.Where(c => c.Nome.Contains(Filter));
                if (Somente_Ativo)
                    Sql = Sql.Where(c => c.Ativo == true);
                if (Somente_Inativo)
                    Sql = Sql.Where(c => c.Ativo == false);
                Sql = Sql.OrderBy(c => c.Nome);
                return Sql.ToList();
            }
        }

        public int Retorna_Numero_Disponivel(int Ano) {
            int maxCod = 0;
            using (GTI_Context db = new GTI_Context(_connection)) {
                try {
                    var Sql = (from p in db.Processogti where p.Ano == Ano select p.Numero).Max();
                    maxCod = Convert.ToInt32(Sql) + 1;
                } catch  {
                    maxCod = 1;
                }
            }
            return maxCod;
        }

        public Exception Incluir_Processo(Processogti reg) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                object[] Parametros = new object[25];
                Parametros[0] = new SqlParameter { ParameterName = "@Ano", SqlDbType = SqlDbType.SmallInt, SqlValue = reg.Ano };
                Parametros[1] = new SqlParameter { ParameterName = "@Numero", SqlDbType = SqlDbType.Int, SqlValue = reg.Numero };
                Parametros[2] = new SqlParameter { ParameterName = "@Fisico", SqlDbType = SqlDbType.Bit, SqlValue = reg.Fisico };
                Parametros[3] = new SqlParameter { ParameterName = "@Origem", SqlDbType = SqlDbType.SmallInt, SqlValue = reg.Origem };
                Parametros[4] = new SqlParameter { ParameterName = "@Interno", SqlDbType = SqlDbType.Bit, SqlValue = reg.Interno };
                Parametros[5] = new SqlParameter { ParameterName = "@Codassunto", SqlDbType = SqlDbType.SmallInt, SqlValue = reg.Codassunto };
                if (reg.Complemento != null)
                    Parametros[6] = new SqlParameter { ParameterName = "@Complemento", SqlDbType = SqlDbType.VarChar, SqlValue = reg.Complemento };
                else
                    Parametros[6] = new SqlParameter { ParameterName = "@Complemento", SqlValue = DBNull.Value };
                if (reg.Observacao != null)
                    Parametros[7] = new SqlParameter { ParameterName = "@Observacao", SqlDbType = SqlDbType.VarChar, SqlValue = reg.Observacao };
                else
                    Parametros[7] = new SqlParameter { ParameterName = "@Observacao", SqlValue = DBNull.Value };
                if (reg.Dataentrada != null)
                    Parametros[8] = new SqlParameter { ParameterName = "@Dataentrada", SqlDbType = SqlDbType.SmallDateTime, SqlValue = reg.Dataentrada };
                else
                    Parametros[8] = new SqlParameter { ParameterName = "@Dataentrada", SqlValue = DBNull.Value };
                if (reg.Datareativa != null)
                    Parametros[9] = new SqlParameter { ParameterName = "@Datareativa", SqlDbType = SqlDbType.SmallDateTime, SqlValue = reg.Datareativa };
                else
                    Parametros[9] = new SqlParameter { ParameterName = "@Datareativa", SqlValue = DBNull.Value };
                if (reg.Datacancel != null)
                    Parametros[10] = new SqlParameter { ParameterName = "@Datacancel", SqlDbType = SqlDbType.SmallDateTime, SqlValue = reg.Datacancel };
                else
                    Parametros[10] = new SqlParameter { ParameterName = "@Datacancel", SqlValue = DBNull.Value };
                if (reg.Dataarquiva != null)
                    Parametros[11] = new SqlParameter { ParameterName = "@Dataarquiva", SqlDbType = SqlDbType.SmallDateTime, SqlValue = reg.Dataarquiva };
                else
                    Parametros[11] = new SqlParameter { ParameterName = "@Dataarquiva", SqlValue = DBNull.Value };
                if (reg.Datasuspenso != null)
                    Parametros[12] = new SqlParameter { ParameterName = "@Datasuspenso", SqlDbType = SqlDbType.SmallDateTime, SqlValue = reg.Datasuspenso };
                else
                    Parametros[12] = new SqlParameter { ParameterName = "@Datasuspenso", SqlValue = DBNull.Value };
                if (reg.Etiqueta != null)
                    Parametros[13] = new SqlParameter { ParameterName = "@Etiqueta", SqlDbType = SqlDbType.Bit, SqlValue = reg.Etiqueta };
                else
                    Parametros[13] = new SqlParameter { ParameterName = "@Etiqueta", SqlValue = DBNull.Value };
                if (reg.Codcidadao != null)
                    Parametros[14] = new SqlParameter { ParameterName = "@Codcidadao", SqlDbType = SqlDbType.Int, SqlValue = reg.Codcidadao };
                else
                    Parametros[14] = new SqlParameter { ParameterName = "@Codcidadao", SqlValue = DBNull.Value };
                if (reg.Motivocancel != null)
                    Parametros[15] = new SqlParameter { ParameterName = "@Motivocancel", SqlDbType = SqlDbType.VarChar, SqlValue = reg.Motivocancel };
                else
                    Parametros[15] = new SqlParameter { ParameterName = "@Motivocancel", SqlValue = DBNull.Value };
                if (reg.Centrocusto != null)
                    Parametros[16] = new SqlParameter { ParameterName = "@Centrocusto", SqlDbType = SqlDbType.Int, SqlValue = reg.Centrocusto };
                else
                    Parametros[16] = new SqlParameter { ParameterName = "@Centrocusto", SqlValue = DBNull.Value };
                if (reg.Obsa != null)
                    Parametros[17] = new SqlParameter { ParameterName = "@Obsa", SqlDbType = SqlDbType.VarChar, SqlValue = reg.Obsa };
                else
                    Parametros[17] = new SqlParameter { ParameterName = "@Obsa", SqlValue = DBNull.Value };
                if (reg.Obsc != null)
                    Parametros[18] = new SqlParameter { ParameterName = "@Obsc", SqlDbType = SqlDbType.VarChar, SqlValue = reg.Obsc };
                else
                    Parametros[18] = new SqlParameter { ParameterName = "@Obsc", SqlValue = DBNull.Value };
                if (reg.Obss != null)
                    Parametros[19] = new SqlParameter { ParameterName = "@Obss", SqlDbType = SqlDbType.VarChar, SqlValue = reg.Obss };
                else
                    Parametros[19] = new SqlParameter { ParameterName = "@Obss", SqlValue = DBNull.Value };
                if (reg.Obsr != null)
                    Parametros[20] = new SqlParameter { ParameterName = "@Obsr", SqlDbType = SqlDbType.VarChar, SqlValue = reg.Obsr };
                else
                    Parametros[20] = new SqlParameter { ParameterName = "@Obsr", SqlValue = DBNull.Value };
                if (reg.Hora != null)
                    Parametros[21] = new SqlParameter { ParameterName = "@Hora", SqlDbType = SqlDbType.VarChar, SqlValue = reg.Hora };
                else
                    Parametros[21] = new SqlParameter { ParameterName = "@Hora", SqlValue = DBNull.Value };
                if (reg.Insc != null)
                    Parametros[22] = new SqlParameter { ParameterName = "@Insc", SqlDbType = SqlDbType.Int, SqlValue = reg.Insc };
                else
                    Parametros[22] = new SqlParameter { ParameterName = "@Insc", SqlValue = DBNull.Value };
                if (reg.Tipoend != null)
                    Parametros[23] = new SqlParameter { ParameterName = "@Tipoend", SqlDbType = SqlDbType.Char, SqlValue = reg.Tipoend };
                else
                    Parametros[23] = new SqlParameter { ParameterName = "@Tipoend", SqlValue = DBNull.Value };
                if (reg.Userid != null)
                    Parametros[24] = new SqlParameter { ParameterName = "@Userid", SqlDbType = SqlDbType.Int, SqlValue = reg.Userid };
                else
                    Parametros[24] = new SqlParameter { ParameterName = "@Userid", SqlValue = DBNull.Value };

                db.Database.ExecuteSqlCommand("INSERT INTO processogti(ano,numero,fisico,origem,interno,codassunto,complemento,observacao,dataentrada,datareativa," +
                                              "datacancel,dataarquiva,datasuspenso,etiqueta,codcidadao,motivocancel,centrocusto,obsa,obsc,obss,obsr,hora,insc,tipoend," +
                                              "userid) VALUES(@ano,@numero,@fisico,@origem,@interno,@codassunto,@complemento,@observacao,@dataentrada,@datareativa," +
                                              "@datacancel,@dataarquiva,@datasuspenso,@etiqueta,@codcidadao,@motivocancel,@centrocusto,@obsa,@obsc,@obss,@obsr,@hora," +
                                              "@insc,@tipoend,@userid)", Parametros);

                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public Exception Alterar_Processo(Processogti reg) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                short _ano = reg.Ano;
                int _numero = reg.Numero;
                Processogti p = db.Processogti.First(i => i.Ano == _ano && i.Numero == _numero);
                p.Centrocusto = reg.Centrocusto;
                p.Codassunto = reg.Codassunto;
                p.Codcidadao = reg.Codcidadao;
                p.Complemento = reg.Complemento;
                p.Dataarquiva = reg.Dataarquiva;
                p.Datacancel = reg.Datacancel;
                p.Dataentrada = reg.Dataentrada;
                p.Datareativa = reg.Datareativa;
                p.Datasuspenso = reg.Datasuspenso;
                p.Etiqueta = reg.Etiqueta;
                p.Fisico = reg.Fisico;
                p.Hora = reg.Hora;
                p.Insc = reg.Insc;
                p.Interno = reg.Interno;
                p.Motivocancel = reg.Motivocancel;
                p.Numero = reg.Numero;
                p.Obsa = reg.Obsa;
                p.Obsc = reg.Obsc;
                p.Observacao = reg.Observacao;
                p.Obsr = reg.Obsr;
                p.Obss = reg.Obss;
                p.Origem = reg.Origem;
                p.Tipoend = reg.Tipoend;
                p.Userid = reg.Userid;

                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public Exception Incluir_Processo_Endereco(List<Processoend> Lista, int Ano, int Numero) {
            using (GTI_Context db = new GTI_Context(_connection)) {

                try {
                    db.Database.ExecuteSqlCommand("delete from processoend where ano=@ano and numprocesso=@numprocesso",
                        new SqlParameter("@ano", Ano), new SqlParameter("@numprocesso", Numero));
                } catch (Exception ex) {
                    return ex;
                }
                foreach (Processoend item in Lista) {
                    try {
                        db.Database.ExecuteSqlCommand("insert into processoend(ano,numprocesso,codlogr,numero) VALUES(@ano,@numprocesso,@codlogr,@numero)",
                        new SqlParameter("@ano", item.Ano),
                        new SqlParameter("@numprocesso", item.Numprocesso),
                        new SqlParameter("@codlogr", item.Codlogr),
                        new SqlParameter("@numero", item.Numero));
                    } catch (Exception ex) {
                        return ex;
                    }
                }
                return null;
            }
        }

        public Exception Incluir_Processo_Documento(List<Processodoc> Lista, int Ano, int Numero) {
            using (GTI_Context db = new GTI_Context(_connection)) {

                try {
                    db.Database.ExecuteSqlCommand("delete from processodoc where ano=@ano and numero=@numero",
                        new SqlParameter("@ano", Ano), new SqlParameter("@numero", Numero));
                } catch (Exception ex) {
                    return ex;
                }
                foreach (Processodoc item in Lista) {
                    try {
                        object[] Parametros = new object[4];
                        Parametros[0] = new SqlParameter { ParameterName = "@ano", SqlDbType = SqlDbType.SmallInt, SqlValue = item.Ano };
                        Parametros[1] = new SqlParameter { ParameterName = "@numero", SqlDbType = SqlDbType.Int, SqlValue = item.Numero };
                        Parametros[2] = new SqlParameter { ParameterName = "@coddoc", SqlDbType = SqlDbType.SmallInt, SqlValue = item.Coddoc };
                        if (item.Data != null)
                            Parametros[3] = new SqlParameter { ParameterName = "@data", SqlDbType = SqlDbType.SmallDateTime, SqlValue = item.Data };
                        else
                            Parametros[3] = new SqlParameter { ParameterName = "@data", SqlValue = DBNull.Value };

                        db.Database.ExecuteSqlCommand("INSERT INTO processodoc(ano,numero,coddoc,data) VALUES(@ano,@numero,@coddoc,@data)", Parametros);
                    } catch (Exception ex) {
                        return ex;
                    }
                }
                return null;
            }
        }

        public Exception ValidaProcesso(string sInput) {
            Exception AppEx;
            string sNumero = sInput.Trim();
            int Numero, Ano ,Dv ;
            string sDv , sNumTmp , AnoTmp ;
            if (sNumero.Length < 7) {
                AppEx = new Exception("Número de processo inválido!");
                return AppEx;
            } else {
                AnoTmp = (sNumero.Substring(sNumero.Length - 4));
                if (!GtiCore.IsNumeric(AnoTmp)) {
                    AppEx = new Exception("Número de processo inválido!");
                    return AppEx;
                } else {
                    Ano = Convert.ToInt32(AnoTmp);
                    if (Ano < 1900 || Ano > DateTime.Now.Year + 1) {
                        AppEx = new Exception("Número de processo inválido!");
                        return AppEx;
                    } else {
                        if (sNumero.Contains("-") && sNumero.Contains("/")) {
                            if (sNumero.IndexOf("-") > sNumero.IndexOf("/")) {
                                AppEx = new Exception("Número de processo inválido!");
                                return AppEx;
                            } else {
                                if (sNumero.IndexOf("/") - sNumero.IndexOf("-") > 2) {
                                    AppEx = new Exception("Número de processo inválido!");
                                    return AppEx;
                                }
                            }
                        }
                        if (sNumero.Contains("-")) {
                            sDv = sNumero.Substring(sNumero.IndexOf("-") + 1, 1);
                            sNumTmp = sNumero.Substring(0, sNumero.IndexOf("-"));
                        } else {
                            sDv = sNumero.Substring(sNumero.IndexOf("/") - 1, 1);
                            sNumTmp = sNumero.Substring(0, sNumero.IndexOf("/") - 1);
                        }
                        Numero = Convert.ToInt32(sNumTmp);
                        if (!GtiCore.IsNumeric(sDv)) {
                            AppEx = new Exception("Número de processo inválido!");
                            return AppEx;
                        } else {
                            Dv = Convert.ToInt32(sDv);
                            if (Dv != DvProcesso(Numero)) {
                                AppEx = new Exception("Número de processo inválido!");
                                return AppEx;
                            } else
                                if (!Existe_Processo(Ano, Numero)) {
                                AppEx = new Exception("Processo não cadastrado!");
                                return AppEx;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public bool Existe_Processo(int Ano, int Numero) {
            bool bValido = false;
            using (GTI_Context db = new GTI_Context(_connection)) {
                var existingReg = db.Processogti.Count(a => a.Ano == Ano && a.Numero == Numero);
                if (existingReg > 0)
                    bValido = true;
            }
            return bValido;
        }

        public int ExtractNumeroProcessoNoDV(string NumProc) {
            if (String.IsNullOrWhiteSpace(NumProc))
                return 0;
            else {
                if (NumProc.Contains("-"))
                    return Convert.ToInt32(NumProc.Substring(0, NumProc.IndexOf("-")));
                else
                    return Convert.ToInt32(NumProc.Substring(0, NumProc.IndexOf("/") - 1));
            }
        }

        public string Retorna_Processo_com_DV(string Numero_Processo_sem_DV) {
            string sAno;
            int nNumero, nDv;
            sAno = GtiCore.StringRight(Numero_Processo_sem_DV, 4);
            nNumero = ExtractNumeroProcessoNoDV(Numero_Processo_sem_DV);
            nDv = DvProcesso(nNumero);
            return nNumero.ToString() + "-" + nDv.ToString() + "/" + sAno;
        }

        public short ExtractAnoProcesso(string NumProc) {
            return Convert.ToInt16(GtiCore.StringRight(NumProc, 4));
        }

        public ProcessoStruct Dados_Processo(int nAno, int nNumero) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var reg = (from c in db.Processogti
                           join u in db.Usuario on c.Userid equals u.Id into uc from u in uc.DefaultIfEmpty()
                           where c.Ano == nAno && c.Numero == nNumero select new ProcessoStruct {
                               Ano = c.Ano, CodigoAssunto = c.Codassunto, AtendenteNome = u.Nomelogin, CentroCusto = (int)c.Centrocusto,
                               CodigoCidadao = (int)c.Codcidadao, Complemento = c.Complemento, DataArquivado = c.Dataarquiva, DataCancelado = c.Datacancel, DataEntrada = c.Dataentrada, DataReativacao = c.Datareativa,
                               DataSuspensao = c.Datasuspenso, Fisico = c.Fisico, Hora = c.Hora, Inscricao = (int)c.Insc, Interno = c.Interno, Numero = c.Numero, ObsArquiva = c.Obsa,
                               ObsCancela = c.Obsc, ObsReativa = c.Obsr, ObsSuspensao = c.Obss, Observacao = c.Observacao, Origem = c.Origem, TipoEnd = c.Tipoend, AtendenteId = (int)u.Id
                           }).FirstOrDefault();
                if (reg == null)
                    return null;
                ProcessoStruct row = new ProcessoStruct {
                    AtendenteNome = reg.AtendenteNome,
                    AtendenteId = reg.AtendenteId,
                    Dv = DvProcesso(nNumero)
                };
                row.SNumero = nNumero.ToString() + "-" + row.Dv.ToString() + "/" + nAno.ToString();
                row.Complemento = reg.Complemento;
                row.CodigoAssunto = Convert.ToInt32(reg.CodigoAssunto);
                row.Assunto = Retorna_Assunto(Convert.ToInt32(reg.CodigoAssunto));
                row.Observacao = reg.Observacao;
                row.Hora = reg.Hora == null ? "00:00" : reg.Hora.ToString();
                row.Inscricao = Convert.ToInt32(reg.Inscricao);
                row.DataEntrada = reg.DataEntrada;
                row.DataSuspensao = reg.DataSuspensao;
                row.DataReativacao = reg.DataReativacao;
                row.DataCancelado = reg.DataCancelado;
                row.DataArquivado = reg.DataArquivado;
                row.ListaAnexo = ListProcessoAnexo(nAno, nNumero);
                row.Anexo = ListProcessoAnexo(nAno, nNumero).Count().ToString() + " Anexo(s)";
                row.ListaAnexoLog = ListProcessoAnexoLog(nAno, nNumero);
                row.Interno = Convert.ToBoolean(reg.Interno);
                row.Fisico = Convert.ToBoolean(reg.Fisico);
                row.Origem = Convert.ToInt16(reg.Origem);
                if (!row.Interno) {
                    row.CentroCusto = 0;
                    row.CodigoCidadao = Convert.ToInt32(reg.CodigoCidadao);
                    if (row.CodigoCidadao > 0) {
                        row.NomeCidadao = _cidadaoRepository.Retorna_Cidadao((int)row.CodigoCidadao).Nomecidadao;
                    } else
                        row.NomeCidadao = "";
                } else {
                    row.CentroCusto = Convert.ToInt16(reg.CentroCusto);
                    row.CodigoCidadao = 0;
                    row.NomeCidadao = "";
                }
                row.ListaProcessoEndereco = ListProcessoEnd(nAno, nNumero);
                row.ObsArquiva = reg.ObsArquiva;
                row.ObsCancela = reg.ObsCancela;
                row.ObsReativa = reg.ObsReativa;
                row.ObsSuspensao = reg.ObsSuspensao;
                row.ListaProcessoDoc = ListProcessoDoc(nAno, nNumero);
                if (reg.TipoEnd == "1" || reg.TipoEnd == "2")
                    row.TipoEnd = reg.TipoEnd.ToString();
                else
                    row.TipoEnd = "R";
                return row;
            }
        }

        public List<AssuntoDocStruct> Lista_Assunto_Documento(short Assunto) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var Sql = (from a in db.Assuntodoc join d in db.Documento on a.Coddoc equals d.Codigo where a.Codassunto == Assunto
                           select new AssuntoDocStruct { Codigo = (short)a.Coddoc, Nome = d.Nome }).OrderBy(u => u.Nome);
                return Sql.ToList();
            }
        }

        public List<ProcessoDocStruct> ListProcessoDoc(int nAno, int nNumero) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var Sql = (from pd in db.Processodoc join d in db.Documento on pd.Coddoc equals d.Codigo where pd.Ano == nAno && pd.Numero == nNumero
                           select new ProcessoDocStruct { CodigoDocumento = pd.Coddoc, NomeDocumento = d.Nome, DataEntrega = pd.Data });
                return Sql.ToList();
            }
        }

        public List<ProcessoEndStruct> ListProcessoEnd(int nAno, int nNumero) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var Sql = (from pe in db.Processoend join l in db.Logradouro on pe.Codlogr equals l.Codlogradouro where pe.Ano == nAno && pe.Numprocesso == nNumero
                           select new ProcessoEndStruct { CodigoLogradouro = pe.Codlogr, NomeLogradouro = l.Endereco, Numero = pe.Numero });
                return Sql.ToList();
            }
        }

        public List<ProcessoAnexoStruct> ListProcessoAnexo(int nAno, int nNumero) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var Sql = (from a in db.Anexo join p in db.Processogti on new { p1 = a.Anoanexo, p2 = a.Numeroanexo } equals new { p1 = p.Ano, p2 = p.Numero }
                           join c in db.Cidadao on p.Codcidadao equals c.Codcidadao into pc from c in pc.DefaultIfEmpty()
                           join u in db.Centrocusto on p.Centrocusto equals u.Codigo into pcu from u in pcu.DefaultIfEmpty()
                           where a.Ano == nAno && a.Numero == nNumero
                           select new ProcessoAnexoStruct { AnoAnexo = a.Anoanexo, NumeroAnexo = a.Numeroanexo, Complemento = p.Complemento, Requerente = c.Nomecidadao, CentroCusto = u.Descricao });
                return Sql.ToList();
            }
        }

        public List<Anexo_logStruct> ListProcessoAnexoLog(int nAno, int nNumero) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var Sql = (from a in db.Anexo_log
                           join u in db.Usuario on a.Userid equals u.Id into ac from u in ac.DefaultIfEmpty()
                           where a.Ano == nAno && a.Numero == nNumero
                           select new Anexo_logStruct {
                               Ano = (short)nAno, Numero = (short)nNumero, Ano_anexo = a.Ano_anexo, Numero_anexo = a.Numero_anexo,
                               Data = a.Data, Sid = a.Sid, Userid = a.Userid, Removido = a.Removido, Ocorrencia = a.Removido ? "Removido" : "Anexado", UserName = u.Nomecompleto
                           });
                return Sql.ToList();
            }
        }

        public string Retorna_Assunto(int Codigo) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                string Sql = (from c in db.Assunto where c.Codigo == Codigo select c.Nome).FirstOrDefault();
                return Sql;
            }
        }

        public Exception Excluir_Anexo(Anexo reg, string usuario) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                try {
                    db.Database.ExecuteSqlCommand("DELETE FROM anexo WHERE ano=@ano AND numero=@numero AND anoanexo=@anoanexo AND numeroanexo=@numeroanexo",
                        new SqlParameter("@ano", reg.Ano), new SqlParameter("@numero", reg.Numero), new SqlParameter("@anoanexo", reg.Anoanexo), new SqlParameter("@numeroanexo", reg.Numeroanexo));
                } catch (Exception ex) {
                    return ex;
                }
                string sMsg = "O processo " + reg.Numeroanexo.ToString() + "-" + DvProcesso(reg.Numeroanexo) + "/" + reg.Anoanexo.ToString() + " foi desanexado por " + _sistemaRepository.Retorna_User_FullName(usuario) + ".";
                Anexo_log p = new Anexo_log {
                    Ano = reg.Ano,
                    Numero = reg.Numero,
                    Ano_anexo = reg.Anoanexo,
                    Numero_anexo = reg.Numeroanexo,
                    Data = DateTime.Now,
                    Removido = true
                };
                p.Userid = _sistemaRepository.Retorna_User_LoginId(usuario);
                db.Anexo_log.Add(p);
                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }

                return null;
            }
        }

        public Exception Incluir_Anexo(Anexo reg, string usuario) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                db.Anexo.Add(reg);
                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                string sMsg = "O processo " + reg.Numeroanexo.ToString() + "-" + DvProcesso(reg.Numeroanexo) + "/" + reg.Anoanexo.ToString() + " foi anexado por " + _sistemaRepository.Retorna_User_FullName(usuario) + ".";
                Anexo_log p = new Anexo_log {
                    Ano = reg.Ano,
                    Numero = reg.Numero,
                    Ano_anexo = reg.Anoanexo,
                    Numero_anexo = reg.Numeroanexo,
                    Data = DateTime.Now,
                    Removido = false
                };
                p.Userid = _sistemaRepository.Retorna_User_LoginId(usuario);
                db.Anexo_log.Add(p);
                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public ProcessoCidadaoStruct Processo_cidadao_old(int ano, int numero) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var reg = (from pc in db.ProcessoCidadao
                           join l in db.Logradouro on pc.Codlogradouro equals l.Codlogradouro into pcl from l in pcl.DefaultIfEmpty()
                           join c in db.Cidade on new { p1 = pc.Siglauf, p2 = pc.Codcidade } equals new { p1 = c.Siglauf, p2 = c.Codcidade, } into pcc from c in pcc.DefaultIfEmpty()
                           join b in db.Bairro on new { p1 = pc.Siglauf, p2 = pc.Codcidade, p3 = pc.Codbairro } equals new { p1 = b.Siglauf, p2 = b.Codcidade, p3 = b.Codbairro } into pcb from b in pcb.DefaultIfEmpty()
                           where pc.Anoproc == ano && pc.Numproc == numero
                           select new ProcessoCidadaoStruct {
                               Codigo = pc.Codcidadao, Nome = pc.Nomecidadao, Documento = pc.Doc, Logradouro_Codigo = pc.Codlogradouro, Logradouro_Nome = l.Endereco.ToString(),
                               Numero = pc.Numimovel, Complemento = pc.Complemento, Bairro_Codigo = pc.Codbairro, Cidade_Nome = c.Desccidade, Bairro_Nome = b.Descbairro.ToString(),
                               UF = pc.Siglauf, Cep = pc.Cep, RG = pc.Rg.ToString(), Orgao = pc.Orgao.ToString()
                           }).FirstOrDefault();
                return reg;
            }
        }

        public Exception Incluir_Historico_Processo(short Ano, int Numero, string Historico, string Usuario) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                int cntSeq = (from p in db.Processo_historico where p.Ano == Ano && p.Numero == Numero select p).Count();
                int maxSeq = 1;
                if (cntSeq > 0)
                    maxSeq = (from p in db.Processo_historico where p.Ano == Ano && p.Numero == Numero select p.Seq).Max() + 1;
                Processo_historico reg = new Processo_historico {
                    Ano = Ano,
                    Numero = Numero,
                    Seq = maxSeq,
                    Historico = Historico.Length > 5000 ? Historico.Substring(0, 5000) : Historico,
                    Datahora = DateTime.Now,
                    Usuario = Usuario
                };
                db.Processo_historico.Add(reg);
                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public Exception Cancelar_Processo(int Ano, int Numero, string Observacao) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                Processogti p = db.Processogti.First(i => i.Ano == Ano && i.Numero == Numero);
                p.Datacancel = DateTime.Now;
                p.Dataarquiva = null;
                p.Datareativa = null;
                p.Datasuspenso = null;
                p.Obsc = Observacao;
                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public Exception Reativar_Processo(int Ano, int Numero, string Observacao) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                Processogti p = db.Processogti.First(i => i.Ano == Ano && i.Numero == Numero);
                p.Datareativa = DateTime.Now;
                p.Dataarquiva = null;
                p.Datacancel = null;
                p.Datasuspenso = null;
                p.Obsr = Observacao;
                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public Exception Suspender_Processo(int Ano, int Numero, string Observacao) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                Processogti p = db.Processogti.First(i => i.Ano == Ano && i.Numero == Numero);
                p.Datareativa = null;
                p.Dataarquiva = null;
                p.Datacancel = null;
                p.Datasuspenso = DateTime.Now;
                p.Obss = Observacao;
                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public Exception Arquivar_Processo(int Ano, int Numero, string Observacao) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                Processogti p = db.Processogti.First(i => i.Ano == Ano && i.Numero == Numero);
                p.Datareativa = null;
                p.Dataarquiva = DateTime.Now;
                p.Datacancel = null;
                p.Datasuspenso = null;
                p.Obsa = Observacao;
                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public List<ProcessoStruct> Lista_Processos(ProcessoFilter Filter) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                db.Database.CommandTimeout = 180;
                var Sql = (from p in db.Processogti
                           join c in db.Cidadao on p.Codcidadao equals c.Codcidadao into cp from c in cp.DefaultIfEmpty()
                           join a in db.Assunto on p.Codassunto equals a.Codigo into ap from a in ap.DefaultIfEmpty()
                           join e in db.Processoend on new { P1 = p.Ano, P2 = p.Numero } equals new { P1 = e.Ano, P2 = e.Numprocesso } into ep from e in ep.DefaultIfEmpty()
                           join l in db.Logradouro on e.Codlogr equals l.Codlogradouro into le from l in le.DefaultIfEmpty()
                           join u in db.Centrocusto on p.Centrocusto equals u.Codigo into pu from u in pu.DefaultIfEmpty()
                           orderby p.Ano, p.Numero
                           select new ProcessoStruct {
                               Ano = p.Ano, Numero = p.Numero, NomeCidadao = c.Nomecidadao, Assunto = a.Nome, DataEntrada = p.Dataentrada, DataCancelado = p.Datacancel,
                               DataReativacao = p.Datareativa, DataArquivado = p.Dataarquiva, DataSuspensao = p.Datasuspenso, Interno = p.Interno, Fisico = p.Fisico, LogradouroNome = l.Endereco,
                               LogradouroNumero = e.Numero, Complemento = p.Complemento, CentroCustoNome = u.Descricao, Inscricao = p.Insc, CodigoCidadao = p.Codcidadao, CodigoAssunto = p.Codassunto,
                               CentroCusto = p.Centrocusto
                           });
                if (!string.IsNullOrWhiteSpace(Filter.SNumProcesso))
                    Sql = Sql.Where(c => c.Ano == Filter.Ano && c.Numero == Filter.Numero);
                if (Filter.AnoIni > 0)
                    Sql = Sql.Where(c => c.Ano >= Filter.AnoIni);
                if (Filter.AnoFim > 0)
                    Sql = Sql.Where(c => c.Ano <= Filter.AnoFim);
                if (Filter.Arquivado == false)
                    Sql = Sql.Where(c => c.DataArquivado == null);
                if (Filter.Arquivado == true)
                    Sql = Sql.Where(c => c.DataArquivado != null);
                if (Filter.Requerente != null && Filter.Requerente > 0)
                    Sql = Sql.Where(c => c.CodigoCidadao == Filter.Requerente);
                if (Filter.DataEntrada != null)
                    Sql = Sql.Where(c => c.DataEntrada == Filter.DataEntrada);
                if (Filter.Setor > 0)
                    Sql = Sql.Where(c => c.CentroCusto == Filter.Setor);
                if (Filter.AssuntoCodigo > 0)
                    Sql = Sql.Where(c => c.CodigoAssunto <= Filter.AssuntoCodigo);
                if (Filter.Complemento != "")
                    Sql = Sql.Where(c => c.Complemento.Contains(Filter.Complemento));
                if (Filter.Fisico != null)
                    Sql = Sql.Where(c => c.Fisico == Filter.Fisico);
                if (Filter.Interno != null)
                    Sql = Sql.Where(c => c.Interno == Filter.Interno);

                return Sql.ToList();
            }
        }


    }
}
