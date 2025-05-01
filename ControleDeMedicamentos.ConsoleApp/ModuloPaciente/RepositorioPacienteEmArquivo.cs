using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

public class RepositorioPacienteEmArquivo : RepositorioBaseEmArquivo<Paciente>, IRepositorioPaciente
{
    public RepositorioPacienteEmArquivo(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<Paciente> ObterRegistros()
    {
        return contexto.Pacientes;
    }
}
