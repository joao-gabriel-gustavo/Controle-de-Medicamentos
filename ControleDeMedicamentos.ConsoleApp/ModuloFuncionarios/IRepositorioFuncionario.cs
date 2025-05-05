using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;


namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios
{
    public interface IRepositorioFuncionario : IRepositorio<Funcionario>
    {
        public bool VerificarCPF(string cpf);
    }
}
