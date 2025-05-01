using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
public class TelaFuncionario : TelaBase<Funcionario>, ITelaCrud
{
    public TelaFuncionario(IRepositorioFuncionario repositorio) : base("Funcionario", repositorio)
    {
    }
    public override Funcionario ObterDados()
    {
        Console.Write("Digite o nome: ");
        string nome = Console.ReadLine()!;

        Console.Write("Digite o telefone: ");
        string telefone = Console.ReadLine()!;

        Console.Write("Digite o CPF:");
        string cpf = Console.ReadLine()!;

        Funcionario funcionario = new Funcionario(nome, telefone, cpf);
        return funcionario;
    }
    protected override void ExibirCabecalhoTabela()
    {
        Console.WriteLine("{0, -10} | {1, -30} | {2, -20} | {3, -20}",
            "Id", "Nome", "Telefone", "CPF");
    }

    protected override void ExibirLinhaTabela(Funcionario funcionario)
    {
        Console.WriteLine("{0, -10} | {1, -30} | {2, -20} | {3, -20}",
            funcionario.Id, funcionario.Nome, funcionario.Telefone, funcionario.CPF);
    }
}
