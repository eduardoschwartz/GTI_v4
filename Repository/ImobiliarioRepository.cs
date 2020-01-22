using GTI_v4.Classes;
using GTI_v4.Interfaces;
using GTI_v4.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GTI_v4.Repository {
    public class ImobiliarioRepository:IImobiliarioRepository {
        IEnderecoRepository enderecoRepository = new EnderecoRepository(GtiCore.Connection_Name());
        ICidadaoRepository cidadaoRepository = new CidadaoRepository(GtiCore.Connection_Name());

        private readonly string _connection;

        public ImobiliarioRepository(string Connection) {
            _connection = Connection;
        }

        public bool Existe_Bairro_Localizacao(string UF, int Cidade, int Bairro) {
            bool bRet = false;
            using (GTI_Context db = new GTI_Context(_connection)) {
                var existingReg = db.Cadimob.Count(a => a.Li_uf == UF && a.Li_codcidade == Cidade && a.Li_codbairro == Bairro);
                if (existingReg != 0) {
                    bRet = true;
                }
            }
            return bRet;
        }

        public bool Existe_Bairro_Condominio(string UF, int Cidade, int Bairro) {
            bool bRet = false;
            using (GTI_Context db = new GTI_Context(_connection)) {
                var existingReg = db.Condominio.Count(a => a.Cd_uf == UF && a.Cd_codcidade == Cidade && a.Cd_codbairro == Bairro);
                if (existingReg != 0) {
                    bRet = true;
                }
            }
            return bRet;
        }

        public bool Existe_Bairro_Entrega(string UF, int Cidade, int Bairro) {
            bool bRet = false;
            using (GTI_Context db = new GTI_Context(_connection)) {
                var existingReg = db.EndEntrega.Count(a => a.Ee_uf == UF && a.Ee_cidade == Cidade && a.Ee_bairro == Bairro);
                if (existingReg != 0) {
                    bRet = true;
                }
            }
            return bRet;
        }

        public ImovelStruct Dados_Imovel(int nCodigo) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var reg = (from i in db.Cadimob
                           join c in db.Condominio on i.Codcondominio equals c.Cd_codigo into ic from c in ic.DefaultIfEmpty()
                           join b in db.Benfeitoria on i.Dt_codbenf equals b.Codbenfeitoria into ib from b in ib.DefaultIfEmpty()
                           join p in db.Pedologia on i.Dt_codpedol equals p.Codpedologia into ip from p in ip.DefaultIfEmpty()
                           join t in db.Topografia on i.Dt_codtopog equals t.Codtopografia into it from t in it.DefaultIfEmpty()
                           join s in db.Situacao on i.Dt_codsituacao equals s.Codsituacao into ist from s in ist.DefaultIfEmpty()
                           join cp in db.Categprop on i.Dt_codcategprop equals cp.Codcategprop into icp from cp in icp.DefaultIfEmpty()
                           join u in db.Usoterreno on i.Dt_codusoterreno equals u.Codusoterreno into iu from u in iu.DefaultIfEmpty()
                           where i.Codreduzido == nCodigo
                           select new ImovelStruct {
                               Codigo = i.Codreduzido, Distrito = i.Distrito, Setor = i.Setor, Quadra = i.Quadra, Lote = i.Lote, Seq = i.Seq,
                               Unidade = i.Unidade, SubUnidade = i.Subunidade, NomeCondominio = c.Cd_nomecond, Imunidade = i.Imune, TipoMat = i.Tipomat, NumMatricula = i.Nummat,
                               Numero = i.Li_num, Complemento = i.Li_compl, QuadraOriginal = i.Li_quadras, LoteOriginal = i.Li_lotes, ResideImovel = i.Resideimovel, Inativo = i.Inativo,
                               FracaoIdeal = i.Dt_fracaoideal, Area_Terreno = i.Dt_areaterreno, Benfeitoria = i.Dt_codbenf, Categoria = i.Dt_codcategprop, Pedologia = i.Dt_codpedol, Topografia = i.Dt_codtopog,
                               Uso_terreno = i.Dt_codusoterreno, Situacao = i.Dt_codsituacao, EE_TipoEndereco = i.Ee_tipoend, Benfeitoria_Nome = b.Descbenfeitoria, Pedologia_Nome = p.Descpedologia,
                               Topografia_Nome = t.Desctopografia, Situacao_Nome = s.Descsituacao, Categoria_Nome = cp.Desccategprop, Uso_terreno_Nome = u.Descusoterreno, CodigoCondominio = c.Cd_codigo
                           }).FirstOrDefault();

                ImovelStruct row = new ImovelStruct();
                if (reg == null)
                    return row;
                row.Codigo = nCodigo;
                row.Distrito = reg.Distrito;
                row.Setor = reg.Setor;
                row.Quadra = reg.Quadra;
                row.Lote = reg.Lote;
                row.Seq = reg.Seq;
                row.Unidade = reg.Unidade;
                row.SubUnidade = reg.SubUnidade;
                row.Inscricao = reg.Distrito.ToString() + "." + reg.Setor.ToString("00") + "." + reg.Quadra.ToString("0000") + "." + reg.Lote.ToString("00000") + "." + reg.Seq.ToString("00") + "." + reg.Unidade.ToString("00") + "." + reg.SubUnidade.ToString("000");
                row.CodigoCondominio = reg.CodigoCondominio == null ? 0 : reg.CodigoCondominio;
                row.NomeCondominio = reg.NomeCondominio ?? "";
                row.Imunidade = reg.Imunidade == null ? false : Convert.ToBoolean(reg.Imunidade);
                row.Cip = reg.Cip == null ? false : Convert.ToBoolean(reg.Cip);
                row.ResideImovel = reg.ResideImovel == null ? false : Convert.ToBoolean(reg.ResideImovel);
                row.Inativo = reg.Inativo == null ? false : Convert.ToBoolean(reg.Inativo);
                if (reg.TipoMat == null || reg.TipoMat == "M")
                    row.TipoMat = "M";
                else
                    row.TipoMat = "T";
                row.NumMatricula = reg.NumMatricula;
                row.QuadraOriginal = reg.QuadraOriginal == null ? "" : reg.QuadraOriginal.ToString();
                row.LoteOriginal = reg.LoteOriginal == null ? "" : reg.LoteOriginal.ToString();
                row.FracaoIdeal = reg.FracaoIdeal;
                row.Area_Terreno = reg.Area_Terreno;
                row.Benfeitoria = reg.Benfeitoria;
                row.Benfeitoria_Nome = reg.Benfeitoria_Nome;
                row.Categoria = reg.Categoria;
                row.Categoria_Nome = reg.Categoria_Nome;
                row.Pedologia = reg.Pedologia;
                row.Pedologia_Nome = reg.Pedologia_Nome;
                row.Situacao = reg.Situacao;
                row.Situacao_Nome = reg.Situacao_Nome;
                row.Topografia = reg.Topografia;
                row.Topografia_Nome = reg.Topografia_Nome;
                row.Uso_terreno = reg.Uso_terreno;
                row.Uso_terreno_Nome = reg.Uso_terreno_Nome;
                row.EE_TipoEndereco = reg.EE_TipoEndereco;

                EnderecoStruct regEnd = Dados_Endereco(nCodigo, GtiCore.TipoEndereco.Local);
                row.CodigoLogradouro = regEnd.CodLogradouro;
                row.NomeLogradouro = regEnd.Endereco;
                row.NomeLogradouroAbreviado = regEnd.Endereco_Abreviado;
                row.Numero = regEnd.Numero;
                row.Complemento = regEnd.Complemento;
                row.Cep = regEnd.Cep;
                row.CodigoBairro = regEnd.CodigoBairro;
                row.NomeBairro = regEnd.NomeBairro;

                return row;
            }
        }

        public decimal Soma_Area(int Codigo) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var sum = db.Areas.Where(x => x.Codreduzido == Codigo).Sum(x => x.Areaconstr);
                return Convert.ToDecimal(sum);
            }
        }

        public int Qtde_Imovel_Cidadao(int CodigoImovel) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                int Sql = (from v in db.Vwproprietarioduplicado join p in db.Proprietario on v.Codproprietario equals p.Codcidadao where p.Codreduzido == CodigoImovel && p.Tipoprop == "P" select v.Qtdeimovel).FirstOrDefault();
                return Sql;
            }
        }

        public List<ProprietarioStruct> Lista_Proprietario(int CodigoImovel, bool Principal = false) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var reg = (from p in db.Proprietario
                           join c in db.Cidadao on p.Codcidadao equals c.Codcidadao
                           where p.Codreduzido == CodigoImovel
                           select new { p.Codcidadao, c.Nomecidadao, p.Tipoprop, p.Principal, c.Cpf, c.Cnpj, c.Rg });

                if (Principal)
                    reg = reg.Where(u => u.Tipoprop == "P" && u.Principal == true);

                List<ProprietarioStruct> Lista = new List<ProprietarioStruct>();
                foreach (var query in reg) {
                    string sDoc;
                    if (!string.IsNullOrEmpty(query.Cpf) && query.Cpf.ToString().Length > 5)
                        sDoc = query.Cpf;
                    else {
                        if (!string.IsNullOrEmpty(query.Cnpj) && query.Cnpj.ToString().Length > 10)
                            sDoc = query.Cnpj;
                        else
                            sDoc = "";
                    }

                    ProprietarioStruct Linha = new ProprietarioStruct {
                        Codigo = query.Codcidadao,
                        Nome = query.Nomecidadao,
                        Tipo = query.Tipoprop,
                        Principal = Convert.ToBoolean(query.Principal),
                        CPF = sDoc,
                        RG = query.Rg
                    };
                    Lista.Add(Linha);
                }
                return Lista;
            }
        }

        public List<LogradouroStruct> Lista_Logradouro(string Filter = "") {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var reg = (from l in db.Logradouro
                           select new { l.Codlogradouro, l.Endereco });
                if (!String.IsNullOrEmpty(Filter))
                    reg = reg.Where(u => u.Endereco.Contains(Filter));

                List<LogradouroStruct> Lista = new List<LogradouroStruct>();
                foreach (var query in reg) {
                    LogradouroStruct Linha = new LogradouroStruct {
                        CodLogradouro = query.Codlogradouro,
                        Endereco = query.Endereco
                    };
                    Lista.Add(Linha);
                }
                return Lista;
            }
        }

        public List<FacequadraStruct> Lista_FaceQuadra(int distrito, int setor, int quadra, int face) {
            using (GTI_Context db = new GTI_Context(_connection)) {
                var reg = (from f in db.Facequadra
                           join l in db.Logradouro on f.Codlogr equals l.Codlogradouro into lf from l in lf.DefaultIfEmpty()
                           where f.Coddistrito == distrito && f.Codsetor == setor && f.Codquadra == quadra && f.Codface == face
                           select new FacequadraStruct { Agrupamento = f.Codagrupa, Logradouro_codigo = f.Codlogr, Logradouro_nome = l.Endereco });

                List<FacequadraStruct> Lista = new List<FacequadraStruct>();
                foreach (var query in reg) {
                    FacequadraStruct Linha = new FacequadraStruct {
                        Logradouro_codigo = query.Logradouro_codigo,
                        Logradouro_nome = query.Logradouro_nome,
                        Agrupamento = query.Agrupamento
                    };
                    Lista.Add(Linha);
                }
                return Lista;
            }
        }

        public bool Existe_Imovel(int nCodigo) {
            bool bRet = false;
            using (GTI_Context db = new GTI_Context(_connection)) {
                var existingReg = db.Cadimob.Count(a => a.Codreduzido == nCodigo);
                if (existingReg != 0) {
                    bRet = true;
                }
            }
            return bRet;
        }

        public int Existe_Imovel(int distrito, int setor, int quadra, int lote, int unidade, int subunidade) {
            int nRet = 0;
            using (GTI_Context db = new GTI_Context(_connection)) {
                nRet = (from a in db.Cadimob where a.Distrito == distrito && a.Setor == setor && a.Quadra == quadra && a.Lote == lote && a.Unidade == unidade && a.Subunidade == subunidade select a.Codreduzido).FirstOrDefault();
            }
            return nRet;
        }

        public int Retorna_Imovel_Inscricao(int distrito, int setor, int quadra, int lote, int face, int unidade, int subunidade) {
            int nRet = 0;
            using (GTI_Context db = new GTI_Context(_connection)) {
                nRet = (from a in db.Cadimob where a.Distrito == distrito && a.Setor == setor && a.Quadra == quadra && a.Lote == lote && a.Seq == face && a.Unidade == unidade && a.Subunidade == subunidade select a.Codreduzido).FirstOrDefault();
            }
            return nRet;
        }

        public EnderecoStruct Dados_Endereco(int Codigo, GtiCore.TipoEndereco Tipo) {
            EnderecoStruct regEnd = new EnderecoStruct();
            using (GTI_Context db = new GTI_Context(_connection)) {
                if (Tipo == GtiCore.TipoEndereco.Local) {
                    var reg = (from i in db.Cadimob
                               join b in db.Bairro on i.Li_codbairro equals b.Codbairro into ib from b in ib.DefaultIfEmpty()
                               join fq in db.Facequadra on new { p1 = i.Distrito, p2 = i.Setor, p3 = i.Quadra, p4 = i.Seq } equals new { p1 = fq.Coddistrito, p2 = fq.Codsetor, p3 = fq.Codquadra, p4 = fq.Codface } into ifq from fq in ifq.DefaultIfEmpty()
                               join l in db.Logradouro on fq.Codlogr equals l.Codlogradouro into lfq from l in lfq.DefaultIfEmpty()
                               where i.Codreduzido == Codigo && b.Siglauf == "SP" && b.Codcidade == 413
                               select new {
                                   i.Li_num, i.Li_codbairro, b.Descbairro, fq.Codlogr, l.Endereco, i.Li_compl, l.Endereco_resumido
                               }).FirstOrDefault();
                    if (reg == null)
                        return regEnd;
                    else {
                        regEnd.CodigoBairro = reg.Li_codbairro;
                        regEnd.NomeBairro = reg.Descbairro.ToString();
                        regEnd.CodigoCidade = 413;
                        regEnd.NomeCidade = "JABOTICABAL";
                        regEnd.UF = "SP";
                        regEnd.CodLogradouro = reg.Codlogr;
                        regEnd.Endereco = reg.Endereco ?? "";
                        regEnd.Endereco_Abreviado = reg.Endereco_resumido ?? "";
                        regEnd.Numero = reg.Li_num;
                        regEnd.Complemento = reg.Li_compl ?? "";
                        regEnd.CodigoBairro = reg.Li_codbairro;
                        regEnd.NomeBairro = reg.Descbairro;
                        regEnd.Cep = enderecoRepository.RetornaCep(Convert.ToInt32(reg.Codlogr), Convert.ToInt16(reg.Li_num)) == 0 ? "14870000" : enderecoRepository.RetornaCep(Convert.ToInt32(reg.Codlogr), Convert.ToInt16(reg.Li_num)).ToString("0000");
                    }
                } else if (Tipo == GtiCore.TipoEndereco.Entrega) {
                    var reg = (from ee in db.EndEntrega
                               join b in db.Bairro on new { p1 = ee.Ee_uf, p2 = ee.Ee_cidade, p3 = ee.Ee_bairro } equals new { p1 = b.Siglauf, p2 = (short?)b.Codcidade, p3 = (short?)b.Codbairro } into eeb from b in eeb.DefaultIfEmpty()
                               join c in db.Cidade on new { p1 = ee.Ee_uf, p2 = ee.Ee_cidade } equals new { p1 = c.Siglauf, p2 = (short?)c.Codcidade } into eec from c in eec.DefaultIfEmpty()
                               join l in db.Logradouro on ee.Ee_codlog equals l.Codlogradouro into lee from l in lee.DefaultIfEmpty()
                               where ee.Codreduzido == Codigo
                               select new {
                                   ee.Ee_numimovel, ee.Ee_bairro, b.Descbairro, c.Desccidade, ee.Ee_uf, ee.Ee_cidade, ee.Ee_codlog, ee.Ee_nomelog, l.Endereco, ee.Ee_complemento, l.Endereco_resumido, ee.Ee_cep
                               }).FirstOrDefault();
                    if (reg == null)
                        return regEnd;
                    else {
                        regEnd.CodigoBairro = reg.Ee_bairro;
                        regEnd.NomeBairro = reg.Descbairro == null ? "" : reg.Descbairro.ToString();
                        regEnd.CodigoCidade = reg.Ee_cidade == null ? 0 : reg.Ee_cidade;
                        regEnd.NomeCidade = reg.Descbairro == null ? "" : reg.Desccidade;
                        regEnd.UF = "SP";
                        regEnd.CodLogradouro = reg.Ee_codlog;
                        regEnd.Endereco = reg.Ee_nomelog ?? "";
                        regEnd.Endereco_Abreviado = reg.Endereco_resumido ?? "";
                        if (!String.IsNullOrEmpty(reg.Endereco))
                            regEnd.Endereco = reg.Endereco.ToString();
                        regEnd.Numero = reg.Ee_numimovel;
                        regEnd.Complemento = reg.Ee_complemento ?? "";
                        regEnd.CodigoBairro = reg.Ee_bairro;
                        regEnd.NomeBairro = reg.Descbairro;
                        if (regEnd.CodLogradouro == 0)
                            regEnd.Cep = GtiCore.RetornaNumero(reg.Ee_cep) == "" ? "00000000" : Convert.ToInt32(GtiCore.RetornaNumero(reg.Ee_cep)).ToString("00000000");
                        else
                            regEnd.Cep = enderecoRepository.RetornaCep(Convert.ToInt32(regEnd.CodLogradouro), Convert.ToInt16(reg.Ee_numimovel)) == 0 ? "00000000" : enderecoRepository.RetornaCep(Convert.ToInt32(regEnd.CodLogradouro), Convert.ToInt16(reg.Ee_numimovel)).ToString("0000");
                    }
                } else if (Tipo == GtiCore.TipoEndereco.Proprietario) {
                    List<ProprietarioStruct> _lista_proprietario = Lista_Proprietario(Codigo, true);
                    int _codigo_proprietario = _lista_proprietario[0].Codigo;
                    CidadaoStruct _cidadao = cidadaoRepository.LoadReg(_codigo_proprietario);
                    if (_cidadao.EtiquetaC == "S") {
                        regEnd.Endereco = _cidadao.EnderecoC;
                        regEnd.Endereco_Abreviado = _cidadao.EnderecoC;
                        regEnd.Numero = _cidadao.NumeroC;
                        regEnd.Complemento = _cidadao.ComplementoC;
                        regEnd.CodigoBairro = _cidadao.CodigoBairroC;
                        regEnd.NomeBairro = _cidadao.NomeBairroC;
                        regEnd.CodigoCidade = _cidadao.CodigoCidadeC;
                        regEnd.NomeCidade = _cidadao.NomeCidadeC;
                        regEnd.UF = _cidadao.UfC;
                        regEnd.Cep = _cidadao.CepC.ToString();
                    } else {
                        regEnd.Endereco = _cidadao.EnderecoR;
                        regEnd.Endereco_Abreviado = _cidadao.EnderecoR;
                        regEnd.Numero = _cidadao.NumeroR;
                        regEnd.Complemento = _cidadao.ComplementoR;
                        regEnd.CodigoBairro = _cidadao.CodigoBairroR;
                        regEnd.NomeBairro = _cidadao.NomeBairroR;
                        regEnd.CodigoCidade = _cidadao.CodigoCidadeR;
                        regEnd.NomeCidade = _cidadao.NomeCidadeR;
                        regEnd.UF = _cidadao.UfR;
                        regEnd.Cep = _cidadao.CepR.ToString();
                    }
                }
            }

            return regEnd;
        }



    }
}
