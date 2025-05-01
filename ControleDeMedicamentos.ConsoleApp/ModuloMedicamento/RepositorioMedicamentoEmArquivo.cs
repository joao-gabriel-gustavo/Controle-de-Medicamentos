using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

public class RepositorioMedicamentoEmArquivo : RepositorioBaseEmArquivo<Medicamento>, IRepositorioMedicamento
{
    public RepositorioMedicamentoEmArquivo(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<Medicamento> ObterRegistros()
    {
        return contexto.Medicamentos;
    }
} 