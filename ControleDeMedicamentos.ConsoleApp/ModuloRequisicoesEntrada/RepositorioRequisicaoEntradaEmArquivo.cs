using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesEntrada;

public class RepositorioRequisicaoEntradaEmArquivo : RepositorioBaseEmArquivo<RequisicaoEntrada>, IRepositorioRequisicaoEntrada
{
    private RepositorioMedicamentoEmArquivo repositorioMedicamento;

    public RepositorioRequisicaoEntradaEmArquivo(ContextoDados contexto, RepositorioMedicamentoEmArquivo repositorioMedicamento) : base(contexto)
    {
        this.repositorioMedicamento = repositorioMedicamento;
    }

    protected override List<RequisicaoEntrada> ObterRegistros()
    {
        return contexto.RequisicoesEntrada;
    }

    public void RegistrarEntradaEAtualizarEstoque(RequisicaoEntrada novoRegistro)
    {
        base.CadastrarRegistro(novoRegistro);

        novoRegistro.Medicamento.QuantidadeEmEstoque += novoRegistro.Quantidade;

        contexto.Salvar();
    }
} 