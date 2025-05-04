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

    public bool VerificacaoCNPJ(string CNPJ)
    {
        List<Fornecedor> fornecedores = ObterRegistros();
        bool cnpjExiste = false;

        foreach( Fornecedor item in fornecedores)
        {
            if (item.CNPJ == CNPJ)
                cnpjExiste = true;
        }
        return cnpjExiste;
    }
}