using GTI_v4.Models;
using System;
using System.Collections.Generic;

namespace GTI_v4.Interfaces {
    public interface IProtocoloRepository {
        Exception Alterar_Processo(Processogti reg);
        Exception Arquivar_Processo(int Ano, int Numero, string Observacao);
        Exception Cancelar_Processo(int Ano, int Numero, string Observacao);
        ProcessoStruct Dados_Processo(int nAno, int nNumero);
        int DvProcesso(int Numero);
        Exception Excluir_Anexo(Anexo reg, string usuario);
        bool Existe_Processo(int Ano, int Numero);
        short ExtractAnoProcesso(string NumProc);
        int ExtractNumeroProcessoNoDV(string NumProc);
        Exception Incluir_Anexo(Anexo reg, string usuario);
        Exception Incluir_Historico_Processo(short Ano, int Numero, string Historico, string Usuario);
        Exception Incluir_Processo(Processogti reg);
        Exception Incluir_Processo_Documento(List<Processodoc> Lista, int Ano, int Numero);
        Exception Incluir_Processo_Endereco(List<Processoend> Lista, int Ano, int Numero);
        List<ProcessoAnexoStruct> ListProcessoAnexo(int nAno, int nNumero);
        List<Anexo_logStruct> ListProcessoAnexoLog(int nAno, int nNumero);
        List<ProcessoDocStruct> ListProcessoDoc(int nAno, int nNumero);
        List<ProcessoEndStruct> ListProcessoEnd(int nAno, int nNumero);
        List<Assunto> Lista_Assunto(bool Somente_Ativo, bool Somente_Inativo, string Filter = "");
        List<AssuntoDocStruct> Lista_Assunto_Documento(short Assunto);
        List<Centrocusto> Lista_Local(bool Somente_Ativo, bool Local);
        List<ProcessoStruct> Lista_Processos(ProcessoFilter Filter);
        ProcessoCidadaoStruct Processo_cidadao_old(int ano, int numero);
        Exception Reativar_Processo(int Ano, int Numero, string Observacao);
        string Retorna_Assunto(int Codigo);
        int Retorna_Numero_Disponivel(int Ano);
        string Retorna_Processo_com_DV(string Numero_Processo_sem_DV);
        Exception Suspender_Processo(int Ano, int Numero, string Observacao);
        Exception ValidaProcesso(string sInput);
    }
}
