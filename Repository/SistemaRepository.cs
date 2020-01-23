﻿using GTI_v4.Classes;
using GTI_v4.Interfaces;
using GTI_v4.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GTI_v4.Repository {
    public class SistemaRepository:ISistemaRepository {
        IImobiliarioRepository imobiliarioRepository = new ImobiliarioRepository(GtiCore.Connection_Name());
        IEnderecoRepository enderecoRepository = new EnderecoRepository(GtiCore.Connection_Name());
        IEmpresaRepository empresaRepository = new EmpresaRepository(GtiCore.Connection_Name());
        ICidadaoRepository cidadaoRepository = new CidadaoRepository(GtiCore.Connection_Name());

        public string Connection { get; }

        public SistemaRepository(string _connection) {
            Connection = _connection;
        }
        
        public string Retorna_User_Password(string login) {
            using (GTI_Context db = new GTI_Context(Connection)) {
                string Sql = (from u in db.Usuario where u.Nomelogin == login select u.Senha2).FirstOrDefault();
                return Sql;
            }
        }

        public string Retorna_User_FullName(string login) {
            using (GTI_Context db = new GTI_Context(Connection)) {
                string Sql = (from u in db.Usuario where u.Nomelogin == login select u.Nomecompleto).FirstOrDefault();
                return Sql;
            }
        }

        public int Retorna_User_LoginId(string login) {
            using (GTI_Context db = new GTI_Context(Connection)) {
                int Sql = (from u in db.Usuario where u.Nomelogin == login select (int)u.Id).FirstOrDefault();
                return Sql;
            }
        }

        public Exception Alterar_Usuario(Usuario reg) {
            using (GTI_Context db = new GTI_Context(Connection)) {
                Usuario b = db.Usuario.First(i => i.Id == reg.Id);
                b.Nomecompleto = reg.Nomecompleto;
                b.Nomelogin = reg.Nomelogin;
                b.Setor_atual = reg.Setor_atual;
                b.Ativo = reg.Ativo;
                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public Exception Alterar_Senha(Usuario reg) {
            using (GTI_Context db = new GTI_Context(Connection)) {
                string sLogin = reg.Nomelogin;
                Usuario b = db.Usuario.First(i => i.Nomelogin == sLogin);
                b.Senha2 = reg.Senha2;
                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public UsuarioStruct Retorna_Usuario(int Id) {
            using (GTI_Context db = new GTI_Context(Connection)) {
                var reg = (from t in db.Usuario
                           join cc in db.Centrocusto on t.Setor_atual equals cc.Codigo into tcc from cc in tcc.DefaultIfEmpty()
                           where t.Id == Id
                           orderby t.Nomelogin select new UsuarioStruct {
                               Nome_login = t.Nomelogin, Nome_completo = t.Nomecompleto, Ativo = t.Ativo,
                               Id = t.Id, Senha = t.Senha, Senha2 = t.Senha2, Setor_atual = t.Setor_atual, Nome_setor = cc.Descricao
                           }).FirstOrDefault();
                UsuarioStruct Sql = new UsuarioStruct {
                    Id = reg.Id,
                    Nome_completo = reg.Nome_completo,
                    Nome_login = reg.Nome_login,
                    Senha = reg.Senha,
                    Senha2 = reg.Senha2,
                    Setor_atual = reg.Setor_atual,
                    Nome_setor = reg.Nome_setor,
                    Ativo = reg.Ativo
                };
                return Sql;
            }
        }

        public int GetSizeofBinary() {
            using (GTI_Context db = new GTI_Context(Connection)) {
                int nSize = (from t in db.Security_Event orderby t.Id descending select t.Id).FirstOrDefault();
                return nSize;
            }
        }

        public string GetUserBinary(int id) {
            using (GTI_Context db = new GTI_Context(Connection)) {
                try {
                    string Sql = (from u in db.Usuario where u.Id == id select u.Userbinary).FirstOrDefault();
                    if (Sql == null) {
                        Sql = "0";
                        int nSize = GetSizeofBinary();
                        int nDif = nSize - Sql.Length;
                        string sZero = new string('0', nDif);
                        Sql += sZero;
                        return GtiCore.Encrypt(Sql);
                    }
                    return Sql;
                } catch (Exception ex) {
                    throw ex;
                }

            }
        }

        public List<UsuarioStruct> Lista_Usuarios() {
            using (GTI_Context db = new GTI_Context(Connection)) {
                var reg = (from t in db.Usuario
                           join cc in db.Centrocusto on t.Setor_atual equals cc.Codigo into tcc from cc in tcc.DefaultIfEmpty()
                           where t.Ativo == 1
                           orderby t.Nomecompleto select new { t.Nomelogin, t.Nomecompleto, t.Ativo, t.Id, t.Senha, t.Setor_atual, cc.Descricao }).ToList();
                List<UsuarioStruct> Lista = new List<UsuarioStruct>();
                foreach (var item in reg) {
                    UsuarioStruct Linha = new UsuarioStruct {
                        Nome_login = item.Nomelogin,
                        Nome_completo = item.Nomecompleto,
                        Ativo = item.Ativo,
                        Id = item.Id,
                        Senha = item.Senha,
                        Setor_atual = item.Setor_atual,
                        Nome_setor = item.Descricao
                    };
                    Lista.Add(Linha);
                }
                return Lista;
            }
        }

        public string Retorna_User_LoginName(int idUser) {
            using (GTI_Context db = new GTI_Context(Connection)) {
                string Sql = (from u in db.Usuario where u.Id == idUser select u.Nomelogin).FirstOrDefault();
                return Sql;
            }
        }

        public List<Security_event> Lista_Sec_Eventos() {
            using (GTI_Context db = new GTI_Context(Connection)) {
                var reg = (from t in db.Security_Event orderby t.Id select t).ToList();
                List<Security_event> Lista = new List<Security_event>();
                foreach (var item in reg) {
                    Security_event Linha = new Security_event { Id = item.Id, IdMaster = item.IdMaster, Descricao = item.Descricao };
                    Lista.Add(Linha);
                }
                return Lista;
            }
        }

        public Exception SaveUserBinary(Usuario reg) {
            using (GTI_Context db = new GTI_Context(Connection)) {
                int nId = (int)reg.Id;
                Usuario b = db.Usuario.First(i => i.Id == nId);
                b.Userbinary = reg.Userbinary;
                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    return ex;
                }
                return null;
            }
        }

        public Contribuinte_Header Contribuinte_Header(int _codigo, bool _principal = false) {
            string _nome = "", _inscricao = "", _endereco = "", _complemento = "", _bairro = "", _cidade = "", _uf = "", _cep = "", _quadra = "", _lote = "";
            string _endereco_entrega = "", _complemento_entrega = "", _bairro_entrega = "", _cidade_entrega = "", _uf_entrega = "", _cep_entrega = "";
            string _cpf_cnpj = "", _atividade = "", _rg = "", _endereco_abreviado = "", _endereco_entrega_abreviado = "";
            int _numero = 0, _numero_entrega = 0;
            GtiCore.TipoEndereco _tipo_end = GtiCore.TipoEndereco.Local;
            bool _ativo = false;
            GtiCore.TipoCadastro _tipo_cadastro;
            _tipo_cadastro = _codigo < 100000 ? GtiCore.TipoCadastro.Imovel : (_codigo >= 100000 && _codigo < 500000) ? GtiCore.TipoCadastro.Empresa : GtiCore.TipoCadastro.Cidadao;

            if (_tipo_cadastro == GtiCore.TipoCadastro.Imovel) {
                ImovelStruct _imovel = imobiliarioRepository.Dados_Imovel(_codigo);
                List<ProprietarioStruct> _proprietario = imobiliarioRepository.Lista_Proprietario(_codigo, _principal);
                _nome = _proprietario[0].Nome;
                _cpf_cnpj = _proprietario[0].CPF;
                _rg = _proprietario[0].RG;
                _inscricao = _imovel.Inscricao;
                _endereco = _imovel.NomeLogradouro;
                _endereco_abreviado = _imovel.NomeLogradouroAbreviado;
                _numero = (int)_imovel.Numero;
                _complemento = _imovel.Complemento;
                _bairro = _imovel.NomeBairro;
                _cidade = "JABOTICABAL";
                _uf = "SP";
                _ativo = (bool)_imovel.Inativo ? false : true;
                _lote = _imovel.LoteOriginal;
                _quadra = _imovel.QuadraOriginal;
                _cep = enderecoRepository.RetornaCep((int)_imovel.CodigoLogradouro, (short)_imovel.Numero).ToString();
                //Carrega Endereço de Entrega do imóvel
                _tipo_end = _imovel.EE_TipoEndereco == 0 ? GtiCore.TipoEndereco.Local : _imovel.EE_TipoEndereco == 1 ? GtiCore.TipoEndereco.Proprietario : GtiCore.TipoEndereco.Entrega;
                EnderecoStruct regEntrega = imobiliarioRepository.Dados_Endereco(_codigo, _tipo_end);
                if (regEntrega != null) {
                    _endereco_entrega = regEntrega.Endereco ?? "";
                    _endereco_entrega_abreviado = regEntrega.Endereco_Abreviado ?? "";
                    _numero_entrega = (int)regEntrega.Numero;
                    _complemento_entrega = regEntrega.Complemento ?? "";
                    _uf_entrega = regEntrega.UF.ToString();
                    _cidade_entrega = regEntrega.NomeCidade.ToString();
                    _bairro_entrega = regEntrega.NomeBairro ?? "";
                    _cep_entrega = regEntrega.Cep == null ? "00000-000" : Convert.ToInt32(regEntrega.Cep.ToString()).ToString("00000-000");
                }
            } else {
                if (_tipo_cadastro == GtiCore.TipoCadastro.Empresa) {
                    EmpresaStruct _empresa = empresaRepository.Retorna_Empresa(_codigo);
                    _nome = _empresa.Razao_social;
                    _inscricao = _codigo.ToString();
                    _rg = string.IsNullOrWhiteSpace(_empresa.Inscricao_estadual) ? _empresa.Rg : _empresa.Inscricao_estadual;
                    _cpf_cnpj = _empresa.Cpf_cnpj;
                    _endereco = _empresa.Endereco_nome;
                    _endereco_abreviado = _empresa.Endereco_nome_abreviado;
                    _numero = _empresa.Numero == null ? 0 : (int)_empresa.Numero;
                    _complemento = _empresa.Complemento;
                    _bairro = _empresa.Bairro_nome;
                    _cidade = _empresa.Cidade_nome;
                    _uf = _empresa.UF;
                    _cep = _empresa.Cep;
                    _ativo = _empresa.Data_Encerramento == null ? true : false;
                    _atividade = _empresa.Atividade_extenso;

                    //Carrega Endereço de Entrega da Empresa
                    Mobiliarioendentrega regEntrega = empresaRepository.Empresa_Endereco_entrega(_codigo);
                    if (regEntrega.Nomelogradouro == null) {
                        _endereco_entrega = _endereco;
                        _numero_entrega = _numero;
                        _complemento_entrega = _complemento;
                        _uf_entrega = _uf;
                        _cidade_entrega = _cidade;
                        _bairro_entrega = _bairro;
                        _cep_entrega = _cep;
                    } else {
                        _endereco_entrega = regEntrega.Nomelogradouro ?? "";
                        _numero_entrega = (int)regEntrega.Numimovel;
                        _complemento_entrega = regEntrega.Complemento ?? "";
                        _uf_entrega = regEntrega.Uf ?? "";
                        _cidade_entrega = regEntrega.Desccidade;
                        _bairro_entrega = regEntrega.Descbairro;
                        _cep_entrega = regEntrega.Cep == null ? "00000-000" : Convert.ToInt32(dalCore.RetornaNumero(regEntrega.Cep).ToString()).ToString("00000-000");
                    }
                } else {
                    CidadaoStruct _cidadao = cidadaoRepository.Dados_Cidadao(_codigo);
                    _nome = _cidadao.Nome;
                    _inscricao = _codigo.ToString();
                    _cpf_cnpj = _cidadao.Cpf;
                    _rg = _cidadao.Rg;
                    _ativo = true;
                    if (_cidadao.EtiquetaC == "S") {
                        if (_cidadao.CodigoCidadeC == 413) {
                            _endereco = _cidadao.EnderecoC.ToString();
                            if (_cidadao.NumeroC == null || _cidadao.NumeroC == 0 || _cidadao.CodigoLogradouroC == null || _cidadao.CodigoLogradouroC == 0)
                                _cep = "14870000";
                            else
                                _cep = enderecoRepository.RetornaCep((int)_cidadao.CodigoLogradouroC, Convert.ToInt16(_cidadao.NumeroC)).ToString("00000000");
                        } else {
                            _endereco = _cidadao.CodigoCidadeC.ToString();
                            _cep = _cidadao.CepC.ToString();
                        }
                        _numero = (int)_cidadao.NumeroC;
                        _complemento = _cidadao.ComplementoC;
                        _bairro = _cidadao.NomeBairroC;
                        _cidade = _cidadao.NomeCidadeC;
                        _uf = _cidadao.UfC;
                    } else {
                        if (_cidadao.CodigoCidadeR == 413) {
                            _endereco = _cidadao.EnderecoR ?? "";
                            if (_cidadao.NumeroR == null || _cidadao.NumeroR == 0 || _cidadao.CodigoLogradouroR == null || _cidadao.CodigoLogradouroR == 0)
                                _cep = "14870000";
                            else
                                _cep = enderecoRepository.RetornaCep((int)_cidadao.CodigoLogradouroR, Convert.ToInt16(_cidadao.NumeroR)).ToString("00000000");
                        } else {
                            _endereco = _cidadao.CodigoCidadeR.ToString();
                            _cep = _cidadao.CepR.ToString();
                        }
                        _numero = _cidadao.NumeroR == null ? 0 : (int)_cidadao.NumeroR;
                        _complemento = _cidadao.ComplementoR;
                        _bairro = _cidadao.NomeBairroR;
                        _cidade = _cidadao.NomeCidadeR;
                        _uf = _cidadao.UfR;
                    }
                    _endereco_abreviado = _endereco;
                    _endereco_entrega = _endereco;
                    _numero_entrega = _numero;
                    _complemento_entrega = _complemento;
                    _uf_entrega = _uf;
                    _cidade_entrega = _cidade;
                    _bairro_entrega = _bairro;
                    _cep_entrega = _cep;
                }
            }

            Contribuinte_Header reg = new Contribuinte_Header {
                Codigo = _codigo,
                Tipo = _tipo_cadastro,
                TipoEndereco = _tipo_end,
                Nome = _nome,
                Inscricao = _inscricao,
                Inscricao_Estadual = _inscricao,
                Cpf_cnpj = _cpf_cnpj,
                Endereco = _endereco,
                Endereco_abreviado = _endereco_abreviado,
                Endereco_entrega = _endereco_entrega,
                Endereco_entrega_abreviado = _endereco_entrega_abreviado,
                Numero = (short)_numero,
                Numero_entrega = (short)_numero_entrega,
                Complemento = _complemento,
                Complemento_entrega = _complemento_entrega,
                Nome_bairro = _bairro,
                Nome_bairro_entrega = _bairro_entrega,
                Nome_cidade = _cidade,
                Nome_cidade_entrega = _cidade_entrega,
                Nome_uf = _uf,
                Nome_uf_entrega = _uf_entrega,
                Cep = _cep,
                Cep_entrega = _cep_entrega,
                Ativo = _ativo,
                Lote_original = _lote,
                Quadra_original = _quadra,
                Atividade = _atividade
            };

            return reg;
        }


    }
}
