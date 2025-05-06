using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
public class TelaFuncionario : TelaBase<Funcionario>, ITelaCrud
{
    private IRepositorioFuncionario repositorio;
    public TelaFuncionario(IRepositorioFuncionario repositorio) : base("Funcionario", repositorio)
    {
        this.repositorio = repositorio;
    }

    public override void EditarRegistro()
    {
        
        ExibirCabecalho();

        Console.WriteLine($"Editando Funcionario...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarRegistros(false);

        Console.Write("Digite o ID do registro que deseja selecionar: ");
        int idRegistro = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("--------------------------------------------------------------------------");
        Console.WriteLine("Coloque o mesmo CPF que foi usado anteriormente");
        Console.WriteLine("--------------------------------------------------------------------------");

        Funcionario registroEditado = ObterDados();

        List<Funcionario> funcionarios = repositorio.SelecionarRegistros();

        bool cpfAlterado = true;
        foreach (Funcionario item in funcionarios)
        {
            if(registroEditado.CPF == item.CPF)
            {
                cpfAlterado = false;
            }
        }

        if(cpfAlterado)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Nao é possivel alterar o CPF aperte enter para tentar novamente");
            Console.ReadLine();
            Console.ResetColor();
            registroEditado = null;
            EditarRegistro();
        }
        string erros = registroEditado.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);

            EditarRegistro();

            return;
        }

        bool conseguiuEditar = repositorio.EditarRegistro(idRegistro, registroEditado);

        if (!conseguiuEditar)
        {
            Notificador.ExibirMensagem("Houve um erro durante a edição do registro...", ConsoleColor.Red);

            return;
        }

        Notificador.ExibirMensagem("O registro foi editado com sucesso!", ConsoleColor.Green);
    
    }
    public override Funcionario ObterDados()
    {
        Console.Write("Digite o nome: ");
        string nome = Console.ReadLine()!;

        Console.Write("Digite o telefone: ");
        string telefone = Console.ReadLine()!;

        Console.Write("Digite o CPF:");
        string cpf = Console.ReadLine()!;

        bool cpfExiste = repositorio.VerificarCPF(cpf);

        if(cpfExiste)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Este CPF ja esta cadastrado em nosso sistema\nAperte ENTER para tentar novamente");
            Console.ReadLine();
            Console.ResetColor();
            ObterDados();
        }

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
