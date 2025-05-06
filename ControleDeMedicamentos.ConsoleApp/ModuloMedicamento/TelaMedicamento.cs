using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.Util;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesEntrada;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesSaida;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

public class TelaMedicamento : TelaBase<Medicamento>, ITelaCrud
{
    private IRepositorioMedicamento repositorioMedicamento;
    private IRepositorioFornecedor repositorioFornecedor;
    private TelaFornecedor telaFornecedor;
    private IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada;
    private IRepositorioRequisicaoSaida repositorioRequisicaoSaida;

    public TelaMedicamento(
        IRepositorioMedicamento repositorio, 
        IRepositorioFornecedor repositorioFornecedor, 
        TelaFornecedor telaFornecedor,
        IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada,
        IRepositorioRequisicaoSaida repositorioRequisicaoSaida) 
        : base("Medicamento", repositorio)
    {
        this.repositorioMedicamento = repositorio;
        this.repositorioFornecedor = repositorioFornecedor;
        this.telaFornecedor = telaFornecedor;
        this.repositorioRequisicaoEntrada = repositorioRequisicaoEntrada;
        this.repositorioRequisicaoSaida = repositorioRequisicaoSaida;
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

    public override void ExcluirRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Excluindo {nomeEntidade}...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarRegistros(false);

        Console.Write("Digite o ID do registro que deseja selecionar: ");
        int idRegistro = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine();

        // Verificar se o medicamento possui requisições vinculadas
        bool podeSerExcluido = true;
        
        // Verificar requisições de entrada
        List<RequisicaoEntrada> requisicoesEntrada = repositorioRequisicaoEntrada.SelecionarRegistros();
        foreach (RequisicaoEntrada requisicao in requisicoesEntrada)
        {
            if (requisicao.Medicamento != null && requisicao.Medicamento.Id == idRegistro)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Este medicamento possui requisições de entrada vinculadas e não pode ser excluído\nAperte ENTER para continuar");
                Console.ReadLine();
                Console.ResetColor();
                podeSerExcluido = false;
                break;
            }
        }

        // Verificar requisições de saída se ainda não encontrou incompatibilidade
        if (podeSerExcluido)
        {
            List<RequisicaoSaida> requisicoesSaida = repositorioRequisicaoSaida.SelecionarRegistros();
            foreach (RequisicaoSaida requisicao in requisicoesSaida)
            {
                if (requisicao.medicamento != null && requisicao.medicamento.Id == idRegistro)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Este medicamento possui requisições de saída vinculadas e não pode ser excluído\nAperte ENTER para continuar");
                    Console.ReadLine();
                    Console.ResetColor();
                    podeSerExcluido = false;
                    break;
                }
            }
        }

        bool conseguiuExcluir = false;

        if (podeSerExcluido)
        {
            conseguiuExcluir = repositorio.ExcluirRegistro(idRegistro);
        }

        if (!conseguiuExcluir)
        {
            Notificador.ExibirMensagem("Houve um erro durante a exclusão do registro...", ConsoleColor.Red);
            return;
        }

        Notificador.ExibirMensagem("O registro foi excluído com sucesso!", ConsoleColor.Green);
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