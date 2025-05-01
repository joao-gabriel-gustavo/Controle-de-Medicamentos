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

    public bool ExisteCartaoSusDuplicado(string cartaoSus, int excecaoId = 0)
    {
        List<Paciente> pacientes = ObterRegistros();

        foreach (Paciente p in pacientes)
        {
            // Ignora o próprio registro durante a edição
            if (p.Id == excecaoId)
                continue;
                
            if (p.CartaoSus == cartaoSus)
                return true;
        }

        return false;
    }
}
