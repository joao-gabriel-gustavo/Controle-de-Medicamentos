using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;

public interface IRepositorioFornecedor : IRepositorio<Fornecedor>
{
    bool VerificacaoCNPJ(string cnpj);
}
