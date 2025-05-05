using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios
{
    public class RepositorioFuncionarioEmArquivo : RepositorioBaseEmArquivo<Funcionario>, IRepositorioFuncionario
    {
        public RepositorioFuncionarioEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }



        public bool VerificarCPF(string cpf)
        {
            List<Funcionario> funcionarios = contexto.Funcionarios;
            bool cpfExiste = false;

            foreach (Funcionario item in funcionarios)
            {
                if (cpf == item.CPF)
                {
                    cpfExiste = true;
                }
            }
            return cpfExiste;
        }

        protected override List<Funcionario> ObterRegistros()
        {
                  return contexto.Funcionarios;
        }

 
    }
}
