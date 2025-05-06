

using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios
{
    public class Funcionario : EntidadeBase<Funcionario>
    {
        public string Nome { get; set;}
        public string Telefone { get; set;}
        public string CPF { get; set;}
        public Funcionario(string nome, string telefone, string cpf)
        {
            Nome = nome;
            Telefone = telefone;
            CPF = cpf;
        }
        public override void AtualizarRegistro(Funcionario registroEditado)
        {
            Nome = registroEditado.Nome;    
            Telefone = registroEditado.Telefone;
            CPF = registroEditado.CPF;
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

            if (string.IsNullOrEmpty(CPF))
                erros += "O campo CPF é obrigatório.\n";

            if (CPF.Length < 11 || CPF.Length > 11)
                erros += "O campo CPF deve conter 11 digitos.\n";

            return erros;
        }
    }
}
