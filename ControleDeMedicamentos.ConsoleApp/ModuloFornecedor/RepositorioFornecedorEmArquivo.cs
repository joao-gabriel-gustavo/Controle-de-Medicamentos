using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;

public class RepositorioFornecedorEmArquivo : RepositorioBaseEmArquivo<Fornecedor>, IRepositorioFornecedor
{
    public RepositorioFornecedorEmArquivo(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<Fornecedor> ObterRegistros()
    {
        return contexto.Fornecedores;
    }
}