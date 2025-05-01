using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

namespace ControleDeMedicamentos.ConsoleApp.Util;

public class TelaPrincipal
{
    private char opcaoPrincipal;

    private ContextoDados contexto;
    private TelaFornecedor telaFornecedor;
    private TelaPaciente telaPaciente;

    public TelaPrincipal()
    {
        contexto = new ContextoDados(true);

        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
        telaFornecedor = new TelaFornecedor(repositorioFornecedor);

        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);
        telaPaciente = new TelaPaciente(repositorioPaciente);
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
        Console.WriteLine("S - Sair");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        opcaoPrincipal = Console.ReadLine()[0];
    }

    public ITelaCrud ObterTela()
    {
        if (opcaoPrincipal == '1')
            return telaFornecedor;

        if (opcaoPrincipal == '2')
            return telaPaciente;

        return null;
    }
}