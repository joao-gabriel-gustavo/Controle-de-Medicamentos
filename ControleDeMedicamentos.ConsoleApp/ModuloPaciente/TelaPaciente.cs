using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

public class TelaPaciente : TelaBase<Paciente>, ITelaCrud
{
    private IRepositorioPaciente repositorioPaciente;

    public TelaPaciente(IRepositorioPaciente repositorio) : base("Paciente", repositorio)
    {
        this.repositorioPaciente = repositorio;
    }

    public override Paciente ObterDados()
    {
        Console.Write("Digite o nome do paciente: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o telefone do paciente: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o número do cartão do SUS do paciente (15 dígitos): ");
        string cartaoSus = Console.ReadLine() ?? string.Empty;

        Paciente paciente = new Paciente(nome, telefone, cartaoSus);

        return paciente;
    }

    public override void CadastrarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"Cadastrando {nomeEntidade}...");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine();

        Paciente novoRegistro = ObterDados();

        string erros = novoRegistro.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);

            CadastrarRegistro();

            return;
        }

        // Verificar cartão SUS duplicado
        if (repositorioPaciente.ExisteCartaoSusDuplicado(novoRegistro.CartaoSus))
        {
            Notificador.ExibirMensagem("Erro: Já existe um paciente cadastrado com este cartão do SUS.", ConsoleColor.Red);

            CadastrarRegistro();

            return;
        }

        repositorio.CadastrarRegistro(novoRegistro);

        Notificador.ExibirMensagem("O registro foi concluído com sucesso!", ConsoleColor.Green);
    }

    public override void EditarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Editando {nomeEntidade}...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarRegistros(false);

        Console.Write("Digite o ID do registro que deseja selecionar: ");
        int idRegistro = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine();

        Paciente registroEditado = ObterDados();

        string erros = registroEditado.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);

            EditarRegistro();

            return;
        }

        // Verificar cartão SUS duplicado, excluindo o próprio registro
        if (repositorioPaciente.ExisteCartaoSusDuplicado(registroEditado.CartaoSus, idRegistro))
        {
            Notificador.ExibirMensagem("Erro: Já existe outro paciente cadastrado com este cartão do SUS.", ConsoleColor.Red);

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

    protected override void ExibirCabecalhoTabela()
    {
        Console.WriteLine(
            "{0, -6} | {1, -20} | {2, -20} | {3, -30}",
            "Id", "Nome", "Telefone", "Cartão do SUS"
        );
    }

    protected override void ExibirLinhaTabela(Paciente registro)
    {
        Console.WriteLine(
            "{0, -6} | {1, -20} | {2, -20} | {3, -30}",
            registro.Id, registro.Nome, registro.Telefone, registro.CartaoSus
        );
    }
}
