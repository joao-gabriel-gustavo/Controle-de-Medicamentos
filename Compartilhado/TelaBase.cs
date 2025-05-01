using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public abstract class TelaBase<T> where T : EntidadeBase<T>
{
    protected string nomeEntidade;
    protected IRepositorio<T> repositorio;

    protected TelaBase(string nomeEntidade, IRepositorio<T> repositorio)
    {
        this.nomeEntidade = nomeEntidade;
        this.repositorio = repositorio;
    }

    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine($"Controle de {nomeEntidade}s");
        Console.WriteLine("--------------------------------------------");
    }

    public virtual char ApresentarMenu()
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

    public virtual void CadastrarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"Cadastrando {nomeEntidade}...");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine();

        T novoRegistro = ObterDados();

        string erros = novoRegistro.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);

            CadastrarRegistro();

            return;
        }

        repositorio.CadastrarRegistro(novoRegistro);

        Notificador.ExibirMensagem("O registro foi concluído com sucesso!", ConsoleColor.Green);
    }

    public virtual void EditarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Editando {nomeEntidade}...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarRegistros(false);

        Console.Write("Digite o ID do registro que deseja selecionar: ");
        int idRegistro = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine();

        T registroEditado = ObterDados();

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

    public virtual void ExcluirRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Excluindo {nomeEntidade}...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarRegistros(false);

        Console.Write("Digite o ID do registro que deseja selecionar: ");
        int idRegistro = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine();

        bool conseguiuExcluir = repositorio.ExcluirRegistro(idRegistro);

        if (!conseguiuExcluir)
        {
            Notificador.ExibirMensagem("Houve um erro durante a exclusão do registro...", ConsoleColor.Red);

            return;
        }

        Notificador.ExibirMensagem("O registro foi excluído com sucesso!", ConsoleColor.Green);
    }

    public void VisualizarRegistros(bool exibirTitulo)
    {
        if (exibirTitulo)
            ExibirCabecalho();

        Console.WriteLine($"Visualizando {nomeEntidade}s...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        ExibirCabecalhoTabela();

        List<T> registros = repositorio.SelecionarRegistros();

        foreach (T registro in registros)
            ExibirLinhaTabela(registro);

        Console.WriteLine();

        Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
    }

    public abstract T ObterDados();

    protected abstract void ExibirCabecalhoTabela();

    protected abstract void ExibirLinhaTabela(T registro);
}