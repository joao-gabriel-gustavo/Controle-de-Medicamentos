using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

public class TelaMedicamento : TelaBase<Medicamento>, ITelaCrud
{
    private IRepositorioMedicamento repositorioMedicamento;
    private IRepositorioFornecedor repositorioFornecedor;
    private TelaFornecedor telaFornecedor;

    public TelaMedicamento(IRepositorioMedicamento repositorio, IRepositorioFornecedor repositorioFornecedor, TelaFornecedor telaFornecedor) 
        : base("Medicamento", repositorio)
    {
        this.repositorioMedicamento = repositorio;
        this.repositorioFornecedor = repositorioFornecedor;
        this.telaFornecedor = telaFornecedor;
    }

    public override Medicamento ObterDados()
    {
        Console.Write("Digite o nome do medicamento: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite a descrição do medicamento: ");
        string descricao = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite a quantidade em estoque: ");
        int.TryParse(Console.ReadLine(), out int quantidadeEmEstoque);

        Console.WriteLine();
        Console.WriteLine("Selecione o fornecedor:");
        Console.WriteLine();

        telaFornecedor.VisualizarRegistros(false);

        Console.WriteLine();
        Console.Write("Digite o ID do fornecedor: ");
        int idFornecedor = Convert.ToInt32(Console.ReadLine());

        Fornecedor fornecedor = repositorioFornecedor.SelecionarRegistroPorId(idFornecedor);

        Medicamento medicamento = new Medicamento(nome, descricao, quantidadeEmEstoque, fornecedor);

        return medicamento;
    }

    public override char ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"1 - Cadastrar {nomeEntidade}");
        Console.WriteLine($"2 - Editar {nomeEntidade}");
        Console.WriteLine($"3 - Excluir {nomeEntidade}");
        Console.WriteLine($"4 - Visualizar {nomeEntidade}s");

        Console.WriteLine("S - Voltar");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        char operacaoEscolhida = Convert.ToChar(Console.ReadLine()!);

        return operacaoEscolhida;
    }

    protected override void ExibirCabecalhoTabela()
    {
        Console.WriteLine(
            "{0, -6} | {1, -30} | {2, -30} | {3, -20} | {4, -20}",
            "Id", "Nome", "Descrição", "Quantidade", "Fornecedor"
        );
    }

    protected override void ExibirLinhaTabela(Medicamento registro)
    {
        string quantidade = registro.QuantidadeEmEstoque.ToString();
        
        if (registro.EstaEmFalta())
            quantidade += " (EM FALTA)";

        Console.WriteLine(
            "{0, -6} | {1, -30} | {2, -30} | {3, -20} | {4, -20}",
            registro.Id, 
            registro.Nome, 
            registro.Descricao.Length > 27 ? registro.Descricao.Substring(0, 27) + "..." : registro.Descricao, 
            quantidade,
            registro.Fornecedor?.Nome ?? "N/A"
        );
    }
} 