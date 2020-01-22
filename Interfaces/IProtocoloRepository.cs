﻿using GTI_v4.Models;
using System;
using System.Collections.Generic;

namespace GTI_v4.Interfaces {
    public interface IProtocoloRepository {
        Exception Alterar_Despacho(int Ano, int Numero, int Seq, short CodigoDespacho);
        Exception Alterar_Observacao_Tramite(int Ano, int Numero, int Seq, string Observacao);
        Exception Alterar_Processo(Processogti reg);
        Exception Alterar_Tramite(Tramitacao Reg);
        Exception Arquivar_Processo(int Ano, int Numero, string Observacao);
        Exception Cancelar_Processo(int Ano, int Numero, string Observacao);
        ProcessoStruct Dados_Processo(int nAno, int nNumero);
        List<TramiteStruct> Dados_Tramite(short Ano, int Numero, int CodAssunto);
        int DvProcesso(int Numero);
        Exception Excluir_Anexo(Anexo reg, string usuario);
        Exception Excluir_Tramite(int Ano, int Numero, int Seq);
        bool Existe_Processo(int Ano, int Numero);
        short Extract_Ano_Processo(string NumProc);
        int Extract_Numero_ProcessoNoDV(string NumProc);
        Exception Incluir_Anexo(Anexo reg, string usuario);
        Exception Incluir_Historico_Processo(short Ano, int Numero, string Historico, string Usuario);
        Exception Incluir_MovimentoCC(short Ano, int Numero, List<TramiteStruct> Lista);
        Exception Incluir_Processo(Processogti reg);
        Exception Incluir_Processo_Documento(List<Processodoc> Lista, int Ano, int Numero);
        Exception Incluir_Processo_Endereco(List<Processoend> Lista, int Ano, int Numero);
        Exception Incluir_Tramite(Tramitacao Reg);
        List<Despacho> Lista_Despacho();
        List<ProcessoAnexoStruct> Lista_Processo_Anexo(int nAno, int nNumero);
        List<Anexo_logStruct> Lista_Processo_Anexo_Log(int nAno, int nNumero);
        List<ProcessoDocStruct> Lista_Processo_Doc(int nAno, int nNumero);
        List<ProcessoEndStruct> Lista_Processo_End(int nAno, int nNumero);
        List<Assunto> Lista_Assunto(bool Somente_Ativo, bool Somente_Inativo, string Filter = "");
        List<AssuntoDocStruct> Lista_Assunto_Documento(short Assunto);
        List<UsuariocentroCusto> Lista_CentroCusto_Usuario(int idLogin);
        List<UsuarioFuncStruct> Lista_Funcionario(int LoginId);
        List<Centrocusto> Lista_Local(bool Somente_Ativo, bool Local);
        List<ProcessoStruct> Lista_Processos(ProcessoFilter Filter);
        ProcessoCidadaoStruct Processo_cidadao_old(int ano, int numero);
        Exception Reativar_Processo(int Ano, int Numero, string Observacao);
        string Retorna_Assunto(int Codigo);
        int Retorna_Numero_Disponivel(int Ano);
        string Retorna_Processo_com_DV(string Numero_Processo_sem_DV);
        Exception Suspender_Processo(int Ano, int Numero, string Observacao);
        Exception Valida_Processo(string sInput);
    }
}
