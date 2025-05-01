using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios
{
    public class RepositorioFuncionarioEmArquivo : RepositorioBaseEmArquivo<Funcionario>, IRepositorioFuncionario
    {
        public RepositorioFuncionarioEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }
        protected override List<Funcionario> ObterRegistros()
        {
                  return contexto.Funcionarios;
        }
    }
}
