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

        if (string.IsNullOrWhiteSpace(Telefone))
            erros += "O campo 'Telefone' é obrigatório.\n";

        else if (!Regex.IsMatch(Telefone, @"^\(\d{2}\)\s\d{4,5}-\d{4}$") &&
                !Regex.IsMatch(Telefone, @"^\(\d{2}\)\s\d{8,9}$") &&
                !Regex.IsMatch(Telefone, @"^\d{10,11}$"))
            erros += "O campo 'Telefone' deve seguir um dos formatos: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX ou apenas os numeros com o DD de dois digitos sem formatação.\n";

        if (string.IsNullOrEmpty(CNPJ))
            erros += "O campo CNPJ é obrigatório.\n";

        if (CNPJ.Length < 18 || CNPJ.Length > 18)
            erros += "O campo CNPJ deve seguir o formato: XX.XXX.XXX/XXXX-XX"; 

        return erros.Trim();
    }
}
