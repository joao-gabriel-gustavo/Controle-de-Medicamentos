using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

public interface IRepositorioPaciente : IRepositorio<Paciente>
{
    bool ExisteCartaoSusDuplicado(string cartaoSus, int excecaoId = 0);
}
