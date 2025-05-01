using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

public class Paciente : EntidadeBase<Paciente>
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string CartaoSus { get; set; }
    
    public Paciente() { }

    public Paciente(string nome, string telefone, string cartaoSus) : this()
    {
        Nome = nome;
        Telefone = telefone;
        CartaoSus = cartaoSus;
    }

    public override void AtualizarRegistro(Paciente registroEditado)
    {
        Nome = registroEditado.Nome;
        Telefone = registroEditado.Telefone;
        CartaoSus = registroEditado.CartaoSus;
    }

    public override string Validar()
    {
        string erros = "";

        // Validação do Nome (3-100 caracteres)
        if (string.IsNullOrWhiteSpace(Nome))
            erros += "O campo 'Nome' é obrigatório.\n";
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros += "O campo 'Nome' deve conter entre 3 e 100 caracteres.\n";

        // Validação do Telefone (formatos: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX)
        if (string.IsNullOrWhiteSpace(Telefone))
            erros += "O campo 'Telefone' é obrigatório.\n";
        else if (!Regex.IsMatch(Telefone, @"^\(\d{2}\)\s\d{4,5}-\d{4}$") && 
                !Regex.IsMatch(Telefone, @"^\(\d{2}\)\s\d{8,9}$") && 
                !Regex.IsMatch(Telefone, @"^\d{10,11}$"))
            erros += "O campo 'Telefone' deve seguir um dos formatos: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.\n";

        // Validação do Cartão do SUS (15 dígitos)
        if (string.IsNullOrWhiteSpace(CartaoSus))
            erros += "O campo 'Cartão do SUS' é obrigatório.\n";
        else if (!Regex.IsMatch(CartaoSus, @"^\d{15}$"))
            erros += "O campo 'Cartão do SUS' deve conter exatamente 15 dígitos numéricos.\n";

        return erros;
    }
}
