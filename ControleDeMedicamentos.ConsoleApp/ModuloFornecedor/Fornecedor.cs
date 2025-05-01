using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
public class Fornecedor : EntidadeBase<Fornecedor>
{
    public string Nome { get; set; }

    public string Telefone { get; set; }

    public string CNPJ { get; set; }

    public Fornecedor(string nome, string telefone, string cnpj)
    {
        Nome = nome;
        Telefone = telefone;
        CNPJ = cnpj;
    }

    public override void AtualizarRegistro(Fornecedor registroEditado)
    {
        Nome = registroEditado.Nome;    
        Telefone = registroEditado.Telefone;
        CNPJ = registroEditado.CNPJ;
    }

    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrEmpty(Nome))
            erros += "O campo Nome é obrigatório.\n";
        
        if (Nome.Length < 3 || Nome.Length > 30)
            erros += "O campo Nome deve ter ao menos 3 caracteres e nao pode passar de 30 caracteres.\n";

        if (string.IsNullOrEmpty(Telefone))
            erros += "O campo Telefone é obrigatório.\n";

        if (!Regex.IsMatch(Telefone, @"^\(?\d{2}\)?\s?(9\d{4}|\d{4})-?\d{4}$"))
                erros += "O campo 'Telefone' é deve seguir o padrão (DDD) 0000-0000 ou (DDD) 00000-0000.\n";

        if (string.IsNullOrEmpty(CNPJ))
            erros += "O campo CNPJ é obrigatório.\n";

        if (CNPJ.Length < 18 || CNPJ.Length > 18)
            erros += "O campo CNPJ deve seguir o formato: XX.XXX.XXX/XXX-XX"; 

        return erros.Trim();
    }
}
