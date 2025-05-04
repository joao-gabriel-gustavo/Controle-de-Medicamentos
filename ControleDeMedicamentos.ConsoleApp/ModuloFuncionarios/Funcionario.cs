

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

            if (string.IsNullOrEmpty(Telefone))
                erros += "O campo Telefone é obrigatório.\n";

            if (!Regex.IsMatch(Telefone, @"^\(?\d{2}\)?\s?(9\d{4}|\d{4})-?\d{4}$"))
                erros += "O campo 'Telefone' é deve seguir o padrão (DDD) 0000-0000 ou (DDD) 00000-0000.\n";

            if (string.IsNullOrEmpty(CPF))
                erros += "O campo CNPJ é obrigatório.\n";

            return erros;
        }
    }
}
