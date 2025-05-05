using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesEntrada;

public class TelaRequisicaoEntrada : TelaBase<RequisicaoEntrada>, ITelaCrud
{
    private IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada;
    private TelaMedicamento telaMedicamento;
    private TelaFuncionario telaFuncionario;
    private IRepositorioMedicamento repositorioMedicamento;
    private IRepositorioFuncionario repositorioFuncionario;

    public TelaRequisicaoEntrada(
        IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada,
        TelaMedicamento telaMedicamento,
        TelaFuncionario telaFuncionario,
        IRepositorioMedicamento repositorioMedicamento,
        IRepositorioFuncionario repositorioFuncionario)
        : base("Requisição de Entrada", repositorioRequisicaoEntrada)
    {
        this.repositorioRequisicaoEntrada = repositorioRequisicaoEntrada;
        this.telaMedicamento = telaMedicamento;
        this.telaFuncionario = telaFuncionario;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
    }

    public override RequisicaoEntrada ObterDados()
    {
        Console.Write("Digite a data da requisição (DD/MM/YYYY): ");
        DateTime.TryParse(Console.ReadLine(), out DateTime data);

        Console.WriteLine();
        Console.WriteLine("Selecione o medicamento:");
        Console.WriteLine();

        telaMedicamento.VisualizarRegistros(false);

        Console.WriteLine();
        Console.Write("Digite o ID do medicamento: ");
        int idMedicamento = Convert.ToInt32(Console.ReadLine());

        Medicamento medicamento = repositorioMedicamento.SelecionarRegistroPorId(idMedicamento);

        Console.WriteLine();
        Console.WriteLine("Selecione o funcionário responsável:");
        Console.WriteLine();

        telaFuncionario.VisualizarRegistros(false);

        Console.WriteLine();
        Console.Write("Digite o ID do funcionário: ");
        int idFuncionario = Convert.ToInt32(Console.ReadLine());

        Funcionario funcionario = repositorioFuncionario.SelecionarRegistroPorId(idFuncionario);

        Console.Write("Digite a quantidade a ser adicionada ao estoque: ");
        int.TryParse(Console.ReadLine(), out int quantidade);

        RequisicaoEntrada requisicao = new RequisicaoEntrada(data, medicamento, funcionario, quantidade);

        return requisicao;
    }
    
    public override void CadastrarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"Cadastrando {nomeEntidade}...");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine();

        RequisicaoEntrada novoRegistro = ObterDados();

        string erros = novoRegistro.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);

            CadastrarRegistro();

            return;
        }

        if (repositorioRequisicaoEntrada is RepositorioRequisicaoEntradaEmArquivo repositorioEmArquivo)
        {
            repositorioEmArquivo.RegistrarEntradaEAtualizarEstoque(novoRegistro);
        }
        else
        {
            repositorio.CadastrarRegistro(novoRegistro);
        }

        Notificador.ExibirMensagem("O registro foi concluído com sucesso!", ConsoleColor.Green);
    }

    protected override void ExibirCabecalhoTabela()
    {
        Console.WriteLine(
            "{0, -6} | {1, -15} | {2, -30} | {3, -20} | {4, -15}",
            "Id", "Data", "Medicamento", "Funcionário", "Quantidade"
        );
    }

    protected override void ExibirLinhaTabela(RequisicaoEntrada registro)
    {
        Console.WriteLine(
            "{0, -6} | {1, -15} | {2, -30} | {3, -20} | {4, -15}",
            registro.Id,
            registro.Data.ToShortDateString(),
            registro.Medicamento?.Nome ?? "N/A",
            registro.Funcionario?.Nome ?? "N/A",
            registro.Quantidade
        );
    }
} 