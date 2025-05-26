using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;

public class TelaFornecedor : TelaBase<Fornecedor>, ITelaCrud
{
    private IRepositorioFornecedor repositorioFornecedor;
    public IRepositorioMedicamento repositorioMedicamento;
    public TelaFornecedor(IRepositorioFornecedor repositorioFornecedor, IRepositorioMedicamento repositorioMedicamento) : base("Fornecedor", repositorioFornecedor)
    {
        this.repositorioFornecedor = repositorioFornecedor;
        this.repositorioMedicamento = repositorioMedicamento;
    }

    public override void EditarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Editando Fornecedor");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarRegistros(false);

        Console.Write("Digite o ID do registro que deseja selecionar: ");
        int idRegistro = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("--------------------------------------------------------------------------");
        Console.WriteLine("Coloque o mesmo CNPJ que foi usado anteriormente");
        Console.WriteLine("--------------------------------------------------------------------------");

        Fornecedor registroEditado = ObterDados();

        List<Fornecedor> fornecedoress = repositorio.SelecionarRegistros();

        bool cnpjAlterado = true;
        foreach (Fornecedor item in fornecedoress)
        {
            if (registroEditado.CNPJ == item.CNPJ)
            {
                cnpjAlterado = false;
            }
        }

        if (cnpjAlterado)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Nao é possivel alterar o CNPJ aperte enter para tentar novamente");
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
    public override void ExcluirRegistro()
    {
        
        ExibirCabecalho();

        Console.WriteLine($"Excluindo fornecedor...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarRegistros(false);

        Console.Write("Digite o ID do registro que deseja selecionar: ");
        int idRegistro = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine();
        
        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarRegistros();
        bool podeSerExcluido = true;
        foreach(Medicamento  item in medicamentos)
        {
            if (item.Fornecedor.Id == idRegistro)
            {
                if(item.QuantidadeEmEstoque > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Este fornecedor possui medicamentos cadastrados e nao pode ser excluido\nAperte ENTER para continuar");
                    Console.ReadLine();
                    Console.ResetColor();
                    podeSerExcluido = false;
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
    
    public override Fornecedor ObterDados()
    {
        Console.Write( "Digite o nome: " );
        string nome = Console.ReadLine()!;

        Console.Write("Digite o telefone: ");
        string telefone = Console.ReadLine()!;

        ContextoDados contexto = new ContextoDados();

        Console.Write("Digite o CNPJ no formato: XX.XXX.XXX/XXXX-XX");
        string cnpj = Console.ReadLine()!;

        bool cnpjExiste = false;

        cnpjExiste = repositorioFornecedor.VerificacaoCNPJ(cnpj);


        if(cnpjExiste)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Este CNPJ ja esta cadastrado no nosso sistema, Aperte ENTER para tentar novamente");
            Console.ReadLine();
            Console.ResetColor();
            ObterDados();
        }

            Fornecedor fornecedor = new Fornecedor(nome, telefone, cnpj);
            return fornecedor;
    }
    
    protected override void ExibirCabecalhoTabela()
    {
        Console.WriteLine("{0, -10} | {1, -30} | {2, -20} | {3, -20}", 
            "Id", "Nome", "Telefone", "CNPJ");
    }
    protected override void ExibirLinhaTabela(Fornecedor fornecedor)
    {
        Console.WriteLine("{0, -10} | {1, -30} | {2, -20} | {3, -20}", 
            fornecedor.Id, fornecedor.Nome, fornecedor.Telefone, fornecedor.CNPJ);
    }
}
