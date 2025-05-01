using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

public class Medicamento : EntidadeBase<Medicamento>
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int QuantidadeEmEstoque { get; set; }
    public Fornecedor Fornecedor { get; set; }

    public Medicamento() { }

    public Medicamento(string nome, string descricao, int quantidadeEmEstoque, Fornecedor fornecedor) : this()
    {
        Nome = nome;
        Descricao = descricao;
        QuantidadeEmEstoque = quantidadeEmEstoque;
        Fornecedor = fornecedor;
    }

    public override void AtualizarRegistro(Medicamento registroEditado)
    {
        Nome = registroEditado.Nome;
        Descricao = registroEditado.Descricao;
        QuantidadeEmEstoque = registroEditado.QuantidadeEmEstoque;
        Fornecedor = registroEditado.Fornecedor;
    }

    public override string Validar()
    {
        string erros = "";

        // Validação do nome (3-100 caracteres)
        if (string.IsNullOrWhiteSpace(Nome))
            erros += "O campo 'Nome' é obrigatório.\n";
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros += "O campo 'Nome' deve conter entre 3 e 100 caracteres.\n";

        // Validação da descrição (5-255 caracteres)
        if (string.IsNullOrWhiteSpace(Descricao))
            erros += "O campo 'Descrição' é obrigatório.\n";
        else if (Descricao.Length < 5 || Descricao.Length > 255)
            erros += "O campo 'Descrição' deve conter entre 5 e 255 caracteres.\n";

        // Validção do estoque maior que 0
        if (QuantidadeEmEstoque < 0)
            erros += "O campo 'Quantidade em Estoque' deve ser um número positivo.\n";

        // Validação do fornecedor
        if (Fornecedor == null)
            erros += "O campo 'Fornecedor' é obrigatório.\n";

        return erros;
    }

    public bool EstaEmFalta()
    {
        return QuantidadeEmEstoque < 20;
    }
} 