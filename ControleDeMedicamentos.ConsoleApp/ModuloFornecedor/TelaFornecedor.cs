using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;

public class TelaFornecedor : TelaBase<Fornecedor>, ITelaCrud
{
    public TelaFornecedor(IRepositorioFornecedor repositorio) : base("Fornecedor", repositorio)
    {
    }
    public override Fornecedor ObterDados()
    {
        Console.Write( "Digite o nome: " );
        string nome = Console.ReadLine()!;

        Console.Write("Digite o telefone: ");
        string telefone = Console.ReadLine()!;

        ContextoDados contexto = new ContextoDados();
        RepositorioFornecedorEmArquivo repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);

        Console.Write("Digite o CNPJ no formato: XX.XXX.XXX/XXXX-XX");
        string cnpj = Console.ReadLine()!;

        bool cnpjExiste = false;

        cnpjExiste = repositorioFornecedor.VerificacaoCNPJ(cnpj);

        if (!cnpjExiste)
        {
            Fornecedor fornecedor = new Fornecedor(nome, telefone, cnpj);
            return fornecedor;
        }

        else 
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Este CNPJ ja esta cadastrado no nosso sistema, Aperte ENTER para tentar novamente");
            Console.ReadLine();
            Console.ResetColor();
            ObterDados();
            return null;
        }
    }
    protected override void ExibirCabecalhoTabela()
    {
        Console.WriteLine("{0, -10} | {1, -30} | {2, -20} | {3, -20}", 
            "Id", "Nome", "Telefone", "CNPJ");
    }

    protected override void ExibirLinhaTabela(Fornecedor fornecedor)
    {
        Console.WriteLine("{0, -10} | {1, -30} | {2, -20} | {3, -20}", 
            fornecedor.Id, fornecedor.Nome, fornecedor.Telefone, fornecedor.CNPJ);
    }
}
