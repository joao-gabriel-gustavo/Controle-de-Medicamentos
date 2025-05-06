using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesSaida;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricoesMedicas;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesEntrada;

namespace ControleDeMedicamentos.ConsoleApp.Util;

public class TelaPrincipal
{
    public char opcaoPrincipal;

    private ContextoDados contexto;
    private TelaFornecedor telaFornecedor;
    private TelaPaciente telaPaciente;
    private TelaMedicamento telaMedicamento;
    private TelaFuncionario telaFuncionario;
    private TelaPrescricaoMedica telaPrescricaoMedica;
    private TelaRequisicaoEntrada telaRequisicaoEntrada;
    private TelaRequisicaoSaida telaRequisicoesSaida;

    public TelaPrincipal()
    {
        contexto = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
        telaFornecedor = new TelaFornecedor(repositorioFornecedor);

        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);
        telaPaciente = new TelaPaciente(repositorioPaciente);
        
        IRepositorioMedicamento repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contexto);
        telaMedicamento = new TelaMedicamento(repositorioMedicamento, repositorioFornecedor, telaFornecedor);

        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contexto);
        telaFuncionario = new TelaFuncionario(repositorioFuncionario);

        IRepositorioPrescricaoMedica repositorioPrescricaoMedica = new RepositorioPrescricaoMedicaEmArquivo(contexto);
        telaPrescricaoMedica = new TelaPrescricaoMedica(repositorioPrescricaoMedica);

        IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada = new RepositorioRequisicaoEntradaEmArquivo(contexto, (RepositorioMedicamentoEmArquivo)repositorioMedicamento);
        telaRequisicaoEntrada = new TelaRequisicaoEntrada(
            repositorioRequisicaoEntrada,
            telaMedicamento,
            telaFuncionario,
            repositorioMedicamento,
            repositorioFuncionario);

        IRepositorioRequisicaoSaida repositorioRequisicoesSaida = new RepositorioRequisicaoSaidaEmArquivo(contexto);
        telaRequisicoesSaida = new TelaRequisicaoSaida(repositorioRequisicoesSaida, telaPaciente, telaMedicamento, telaPrescricaoMedica, (RepositorioMedicamentoEmArquivo)repositorioMedicamento, (RepositorioPacienteEmArquivo)repositorioPaciente, (RepositorioPrescricaoMedicaEmArquivo)repositorioPrescricaoMedica);
    }

    public void ApresentarMenuPrincipal()
    {
        Console.Clear();

        Console.WriteLine("------------------------------------------");
        Console.WriteLine("|        Controle de Medicamentos        |");
        Console.WriteLine("------------------------------------------");

        Console.WriteLine();

        Console.WriteLine("1 - Cadastro de Fornecedores");
        Console.WriteLine("2 - Controle de Pacientes");
        Console.WriteLine("3 - Controle de Medicamentos");
        Console.WriteLine("4 - Controle de Funcionarios");
        Console.WriteLine("5 - Controle de Prescrições Médicas");
        Console.WriteLine("6 - Controle de Requisições de Entrada");
        Console.WriteLine("7 - Controle de Requisições de Saida");
        Console.WriteLine("S - Sair");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        opcaoPrincipal = Console.ReadLine()![0];
    }

    public ITelaCrud ObterTela()
    {
        if (opcaoPrincipal == '1')
            return telaFornecedor;

        if (opcaoPrincipal == '2')
            return telaPaciente;
            
        if (opcaoPrincipal == '3')
            return telaMedicamento;

        if (opcaoPrincipal == '4')
            return telaFuncionario;

        if (opcaoPrincipal == '5')
            return telaPrescricaoMedica;
            
        if (opcaoPrincipal == '6')
            return telaRequisicaoEntrada;

        if (opcaoPrincipal == '7')
            return telaRequisicoesSaida;

        else
            return null;
    }
}