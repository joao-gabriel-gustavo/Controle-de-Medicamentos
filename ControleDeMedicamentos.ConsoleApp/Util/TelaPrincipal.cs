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
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);
        IRepositorioMedicamento repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contexto);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contexto);
        IRepositorioPrescricaoMedica repositorioPrescricaoMedica = new RepositorioPrescricaoMedicaEmArquivo(contexto);
        IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada = new RepositorioRequisicaoEntradaEmArquivo(contexto, (RepositorioMedicamentoEmArquivo)repositorioMedicamento);
        IRepositorioRequisicaoSaida repositorioRequisicoesSaida = new RepositorioRequisicaoSaidaEmArquivo(contexto);
       
        telaPaciente = new TelaPaciente(repositorioPaciente);
        telaFornecedor = new TelaFornecedor(repositorioFornecedor, repositorioMedicamento);
        telaFuncionario = new TelaFuncionario(repositorioFuncionario);
        telaPrescricaoMedica = new TelaPrescricaoMedica(repositorioPrescricaoMedica, repositorioMedicamento);
       
        telaMedicamento = new TelaMedicamento(
            repositorioMedicamento, 
            repositorioFornecedor, 
            telaFornecedor,
            repositorioRequisicaoEntrada,
            repositorioRequisicoesSaida);
        
        telaRequisicaoEntrada = new TelaRequisicaoEntrada(
            repositorioRequisicaoEntrada,
            telaMedicamento,
            telaFuncionario,
            repositorioMedicamento,
            repositorioFuncionario);

        telaRequisicoesSaida = new TelaRequisicaoSaida(
            repositorioRequisicoesSaida, 
            telaPaciente, 
            telaMedicamento, 
            telaPrescricaoMedica, 
            (RepositorioMedicamentoEmArquivo)repositorioMedicamento, 
            (RepositorioPacienteEmArquivo)repositorioPaciente, 
            (RepositorioPrescricaoMedicaEmArquivo)repositorioPrescricaoMedica);
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