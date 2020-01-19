using System;
using System.Collections.Generic;
using GTI_v4.Interfaces;
using GTI_v4.Models;
using System.Linq;
using GTI_v4.Classes;

namespace GTI_v4.Repository {
    class CidadaoRepository:ICidadaoRepository {
        private readonly IEnderecoRepository _enderecoRepository = new EnderecoRepository(GtiCore.Connection_Name());
        private readonly string _connection;

        public CidadaoRepository(string Connection) {
            _connection = Connection;
        }

        public List<Profissao> Lista_Profissao() {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var Sql = (from p in db.Profissao orderby p.Nome select p);
                return Sql.ToList();
            }
        }

        public CidadaoStruct LoadReg(int nCodigo) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var reg = (from c in db.Cidadao
                           join l in db.Logradouro on c.Codlogradouro equals l.Codlogradouro into cl1 from l in cl1.DefaultIfEmpty()
                           join l2 in db.Logradouro on c.Codlogradouro2 equals l2.Codlogradouro into cl2 from l2 in cl2.DefaultIfEmpty()
                           join b in db.Bairro on new { p1 = c.Siglauf, p2 = c.Codcidade, p3 = c.Codbairro } equals new { p1 = b.Siglauf, p2 = (short?)b.Codcidade, p3 = (short?)b.Codbairro } into cb from b in cb.DefaultIfEmpty()
                           join b2 in db.Bairro on new { p1 = c.Siglauf2, p2 = c.Codcidade2, p3 = c.Codbairro2 } equals new { p1 = b2.Siglauf, p2 = (short?)b2.Codcidade, p3 = (short?)b2.Codbairro } into cb2 from b2 in cb2.DefaultIfEmpty()
                           join d in db.Cidade on new { p1 = c.Siglauf, p2 = c.Codcidade } equals new { p1 = d.Siglauf, p2 = (short?)d.Codcidade } into cd from d in cd.DefaultIfEmpty()
                           join d2 in db.Cidade on new { p1 = c.Siglauf2, p2 = c.Codcidade2 } equals new { p1 = d2.Siglauf, p2 = (short?)d2.Codcidade } into cd2 from d2 in cd2.DefaultIfEmpty()
                           join p in db.Pais on c.Codpais equals p.Id_pais into cp from p in cp.DefaultIfEmpty()
                           join p2 in db.Pais on c.Codpais equals p2.Id_pais into cp2 from p2 in cp2.DefaultIfEmpty()
                           where c.Codcidadao == nCodigo
                           select new {
                               c.Codcidadao, c.Nomecidadao, c.Cpf, c.Cnpj, c.Rg, c.Orgao, c.Codprofissao, c.Data_nascimento, c.Juridica,
                               c.Codlogradouro, c.Codlogradouro2, enderecoR = l.Endereco, enderecoC = l2.Endereco, c.Numimovel, c.Numimovel2, c.Complemento, c.Complemento2,
                               c.Etiqueta, c.Etiqueta2, c.Siglauf, c.Siglauf2, c.Codbairro, c.Codbairro2, c.Codcidade, c.Codcidade2, c.Cep, c.Cep2, c.Codpais, c.Codpais2, c.Telefone, c.Telefone2,
                               c.Email, c.Email2, c.Nomelogradouro, c.Nomelogradouro2, c.Profissao, nomebairroR = b.Descbairro, nomebairroC = b2.Descbairro, nomecidadeR = d.Desccidade,
                               nomecidadeC = d2.Desccidade, nomepaisR = p.Nome_pais, nomepaisC = p2.Nome_pais, c.Temfone, c.Temfone2, c.Whatsapp, c.Whatsapp2
                           }).FirstOrDefault();


                CidadaoStruct Linha = new CidadaoStruct {
                    Codigo = reg.Codcidadao,
                    Nome = reg.Nomecidadao
                };

                if (!string.IsNullOrEmpty(reg.Cpf) && reg.Cpf.ToString().Length > 10) {
                    Linha.Cpf = reg.Cpf;
                    Linha.Cnpj = "";
                    Linha.Tipodoc = 1;
                } else {
                    if (!string.IsNullOrEmpty(reg.Cnpj) && reg.Cnpj.ToString().Length > 10) {
                        Linha.Cpf = "";
                        Linha.Cnpj = reg.Cnpj;
                        Linha.Tipodoc = 2;
                    } else {
                        Linha.Cpf = "";
                        Linha.Cnpj = "";
                        Linha.Tipodoc = 0;
                    }
                }

                Linha.Rg = reg.Rg;
                Linha.Orgao = reg.Orgao;
                Linha.Profissao = reg.Profissao;
                Linha.DataNascto = reg.Data_nascimento;
                Linha.Juridica = Convert.ToBoolean(reg.Juridica);
                Linha.CodigoLogradouroR = reg.Codlogradouro;

                Linha.CodigoLogradouroC = reg.Codlogradouro2;

                if (reg.Codcidade == 413) {
                    Linha.EnderecoR = reg.enderecoR;
                    Linha.CepR = Convert.ToInt32(_enderecoRepository.RetornaCep(Convert.ToInt32(reg.Codlogradouro), Convert.ToInt16(reg.Numimovel)));
                } else {
                    Linha.EnderecoR = reg.Nomelogradouro;
                    Linha.CepR = reg.Cep;
                }
                if (reg.Codcidade2 == 413) {
                    Linha.EnderecoC = reg.enderecoC;
                    Linha.CepC = Convert.ToInt32(_enderecoRepository.RetornaCep(Convert.ToInt32(reg.Codlogradouro2), Convert.ToInt16(reg.Numimovel2)));
                } else {
                    Linha.EnderecoC = reg.Nomelogradouro2;
                    Linha.CepC = reg.Cep2;
                }
                Linha.NumeroR = reg.Numimovel;
                Linha.NumeroC = reg.Numimovel2;
                Linha.ComplementoR = reg.Complemento;
                Linha.ComplementoC = reg.Complemento2;
                Linha.EtiquetaR = reg.Etiqueta;
                Linha.EtiquetaC = reg.Etiqueta2;
                Linha.UfR = reg.Siglauf;
                Linha.UfC = reg.Siglauf2;
                Linha.CodigoBairroR = reg.Codbairro;
                Linha.CodigoBairroC = reg.Codbairro2;
                Linha.NomeBairroR = reg.nomebairroR;
                Linha.NomeBairroC = reg.nomebairroC;
                Linha.CodigoCidadeR = reg.Codcidade;
                Linha.CodigoCidadeC = reg.Codcidade2;
                Linha.NomeCidadeR = reg.nomecidadeR;
                Linha.NomeCidadeC = reg.nomecidadeC;
                Linha.PaisR = reg.nomepaisR;
                Linha.PaisC = reg.nomepaisC;
                Linha.TelefoneR = reg.Telefone;
                Linha.TelefoneC = reg.Telefone2;
                Linha.EmailR = reg.Email;
                Linha.EmailC = reg.Email2;
                Linha.CodigoPaisR = reg.Codpais;
                Linha.CodigoPaisC = reg.Codpais2;
                Linha.Temfone = reg.Temfone;
                Linha.Temfone2 = reg.Temfone2;
                Linha.Whatsapp = reg.Whatsapp;
                Linha.Whatsapp2 = reg.Whatsapp2;
                return Linha;
            }
        }

        public bool ExisteCidadao(int nCodigo) {
            bool bRet = false;
            using (GTI_Context db = new GTI_Context(_connection)) {
                var existingReg = db.Cidadao.Count(a => a.Codcidadao == nCodigo);
                if (existingReg != 0) {
                    bRet = true;
                }
            }
            return bRet;
        }

        public Exception Incluir_cidadao(Cidadao reg) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var cntCod = (from c in db.Cidadao select c).Count();
                int nMax;
                if (cntCod > 0) {
                    var maxCod = (from c in db.Cidadao select c.Codcidadao).Max();
                    nMax = Convert.ToInt32(maxCod + 1);
                } else
                    nMax = 1;
                reg.Codcidadao = nMax;
                db.Cidadao.Add(reg);
                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public int Retorna_Ultimo_Codigo_Cidadao() {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var Sql = (from c in db.Cidadao orderby c.Codcidadao descending select c.Codcidadao).FirstOrDefault();
                return Sql;
            }
        }

        public Exception Alterar_cidadao(Cidadao reg) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                Cidadao b = db.Cidadao.First(i => i.Codcidadao == reg.Codcidadao);
                b.Nomecidadao = reg.Nomecidadao;
                b.Rg = reg.Rg;
                b.Juridica = reg.Juridica;
                b.Cnpj = reg.Cnpj;
                b.Cpf = reg.Cpf;
                b.Data_nascimento = reg.Data_nascimento;
                b.Codprofissao = reg.Codprofissao;

                b.Etiqueta = reg.Etiqueta;
                b.Codlogradouro = reg.Codlogradouro;
                b.Numimovel = reg.Numimovel;
                b.Complemento = reg.Complemento;
                b.Codbairro = reg.Codbairro;
                b.Codcidade = reg.Codcidade;
                b.Siglauf = reg.Siglauf;
                b.Cep = reg.Cep;
                b.Pais = reg.Pais;
                b.Telefone = reg.Telefone;
                b.Email = reg.Email;
                b.Temfone = reg.Temfone;
                b.Whatsapp = reg.Whatsapp;

                b.Etiqueta2 = reg.Etiqueta2;
                b.Codlogradouro2 = reg.Codlogradouro2;
                b.Numimovel2 = reg.Numimovel2;
                b.Complemento2 = reg.Complemento2;
                b.Codbairro2 = reg.Codbairro2;
                b.Codcidade2 = reg.Codcidade2;
                b.Siglauf2 = reg.Siglauf2;
                b.Cep2 = reg.Cep2;
                b.Pais2 = reg.Pais2;
                b.Telefone2 = reg.Telefone2;
                b.Email2 = reg.Email2;
                b.Temfone2 = reg.Temfone2;
                b.Whatsapp2 = reg.Whatsapp2;

                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public Exception Excluir_cidadao(int Codigo) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                try {
                    Cidadao b = db.Cidadao.First(i => i.Codcidadao == Codigo);
                    db.Cidadao.Remove(b);
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public Exception Incluir_observacao_cidadao(ObsCidadao reg) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                db.ObsCidadao.Add(reg);
                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public List<Historico_CidadaoStruct> Lista_Historico(int CodigoCidadao) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var Sql = (from h in db.HistoricoCidadao join u in db.Usuario on h.Userid equals u.Id where h.Codigo == CodigoCidadao orderby h.Data
                           select new Historico_CidadaoStruct { Codigo = CodigoCidadao, Data = h.Data, Id = h.Id, Id_Usuario = h.Userid, Nome_Usuario = u.Nomecompleto, Obs = h.Obs });
                return Sql.ToList();
            }
        }

        public List<Observacao_CidadaoStruct> Lista_Observacao(int CodigoCidadao) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var Sql = (from h in db.ObsCidadao join u in db.Usuario on h.Userid equals u.Id where h.Codigo == CodigoCidadao orderby h.timestamp
                           select new Observacao_CidadaoStruct { Codigo = CodigoCidadao, Data_Hora = h.timestamp, Id = h.Id, Id_Usuario = h.Userid, Nome_Usuario = u.Nomecompleto, Obs = h.Obs });
                return Sql.ToList();
            }
        }

        public Exception Incluir_profissao(Profissao reg) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var maxCod = (from p in db.Profissao select p.Codigo).Max();
                int nMax = Convert.ToInt32(maxCod + 1);
                reg.Codigo = nMax;
                db.Profissao.Add(reg);
                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public Exception Alterar_Profissao(Profissao reg) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                try {
                    int _id_profissao = reg.Codigo;
                    Profissao b = db.Profissao.First(i => i.Codigo == _id_profissao);
                    b.Nome = reg.Nome;
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public Exception Excluir_Profissao(Profissao reg) {
            using (GTI_Context db = new GTI_Context(_connection)) {

                int _id_profissao = reg.Codigo;
                Profissao b = db.Profissao.First(i => i.Codigo == _id_profissao);
                try {
                    db.Profissao.Remove(b);
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public bool Profissao_cidadao(int id_profissao) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var cntCod1 = (from c in db.Cidadao where c.Codprofissao == id_profissao select c.Codcidadao).Count();
                return cntCod1 > 0 ? true : false;
            }
        }

        public bool Existe_Bairro(string UF, int Cidade, int Bairro) {
            bool bRet = false;
            using (GTI_Context db = new GTI_Context(_connection)) {
                var existingReg = db.Cidadao.Count(a => a.Siglauf == UF && a.Codcidade == Cidade && a.Codbairro == Bairro);
                var existingReg2 = db.Cidadao.Count(a => a.Siglauf2 == UF && a.Codcidade2 == Cidade && a.Codbairro2 == Bairro);
                if (existingReg != 0 || existingReg2!=0) {
                    bRet = true;
                }
            }
            return bRet;
        }

    }
}
